using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    internal class LogFile 
    {
        #region Delegates
        public delegate void BoolEvent(bool val);
        public delegate void DefaultEvent();
        public delegate void ProgressUpdateEvent(int percent);
        #endregion

        #region Events
        public event BoolEvent SearchComplete;
        public event BoolEvent LoadComplete;
        public event BoolEvent ExportComplete;
        public event ProgressUpdateEvent ProgressUpdate;
        #endregion

        #region Member Variables
        public List<LogLine> Lines { get; private set; } = new List<LogLine>();
        public LogLine LongestLine { get; private set; } = new LogLine();
        public int LineCount { get; private set; } = 0;
        private FileStream fileStream;
        private Mutex readMutex = new Mutex();
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public LogFile()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ct"></param>
        public void Load(string filePath, CancellationToken ct)
        {
            Task.Run(() => {

                bool cancelled = false;
                try
                {
                    byte[] tempBuffer = new byte[1024 * 1024];

                    this.fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    FileInfo fileInfo = new FileInfo(filePath);

                    long offset = 0;
                    bool lastSection = false;
                    int counter = 0;
                    long position = 0;
                    int startIndex = 0;
                    int bufferRemainder = 0;
                    int numBytesRead = 0;
                    string tempStr = "";
                    int charCount;
                    long lineEnd;
                    int indexOf;
                    
                    while (position < this.fileStream.Length)
                    {
                        numBytesRead = this.fileStream.Read(tempBuffer, 0, 1024 * 1024);
                        if (numBytesRead < 1048576)
                        {
                            lastSection = true;
                        }

                        tempStr = Encoding.ASCII.GetString(tempBuffer).Substring(0, numBytesRead);
                        startIndex = 0;

                        // Does the buffer contain at least one "\n", so now enumerate all instances of "\n"
                        if (tempStr.IndexOf('\n') != -1)
                        {
                            while ((indexOf = tempStr.IndexOf('\n', startIndex)) != -1 && startIndex < numBytesRead)
                            {
                                if (indexOf != -1)
                                {
                                    charCount = 0;
                                    lineEnd = 0;

                                    // Check if the line contains a CR as well
                                    if (indexOf != 0 && (int)tempBuffer[Math.Max(0, indexOf - 1)] == 13)
                                    {
                                        charCount = bufferRemainder + (indexOf - startIndex - 1);
                                        lineEnd = position + 2L;
                                    }
                                    else
                                    {
                                        charCount = bufferRemainder + (indexOf - startIndex);
                                        lineEnd = position + 1L;
                                    }


                                    AddLine(offset, charCount);

                                    position = lineEnd + (long)charCount;
                                    bufferRemainder = 0;
                                    offset = position;
                                    startIndex = indexOf + 1;
                                }
                            }

                            if (lastSection == true)
                            {
                                AddLine(offset, bufferRemainder + (numBytesRead - startIndex));
                            }
                            
                            bufferRemainder += numBytesRead - startIndex;
                        }
                        else
                        {
                            if (lastSection == true)
                            {
                                AddLine(offset, bufferRemainder + (numBytesRead - startIndex));
                            }
     
                            bufferRemainder += numBytesRead;
                        }

                        if (counter++ % 50 == 0)
                        {
                            OnProgressUpdate((int)((double)position / (double)fileInfo.Length * 100));

                            if (ct.IsCancellationRequested)
                            {
                                cancelled = true;
                                return;
                            }
                        }                       
                    }
                }
                finally
                {
                    OnProgressUpdate(100);
                    OnLoadComplete(cancelled);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Lines.Clear();
            this.fileStream.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchType"></param>
        public void Search(SearchCriteria sc, bool cumulative, CancellationToken ct)
        {
            Task.Run(() => {

                long counter = 0;
                string line = "";
                bool located = false;

                foreach (LogLine ll in this.Lines)
                {
                    if (cumulative == false)
                    {
                        // Reset the match flag
                        ll.SearchMatches.Clear();
                    }

                    line = this.GetLine(ll.LineNumber);

                    located = false;
                    switch (sc.Type)
                    {
                        case Global.SearchType.SubStringCaseInsensitive:
                            if (line.IndexOf(sc.Pattern, 0, StringComparison.OrdinalIgnoreCase) > -1)
                            {
                                located = true;
                            }
                            break;

                        case Global.SearchType.SubStringCaseSensitive:
                            if (line.IndexOf(sc.Pattern, 0, StringComparison.Ordinal) > -1)
                            {
                                located = true;
                            }
                            break;

                        case Global.SearchType.RegexCaseInsensitive:   
                            if (Regex.Match(line, sc.Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled) != Match.Empty)
                            {
                                located = true;
                            }                            
                            break;

                        case Global.SearchType.RegexCaseSensitive:
                            if (Regex.Match(line, sc.Pattern, RegexOptions.Compiled) != Match.Empty)
                            {
                                located = true;
                            }
                            break;

                        default:
                            break;
                    }

                    if (located == false)
                    {
                        ll.SearchMatches.Remove(sc.Id);
                    }
                    else
                    {
                        ll.SearchMatches.Add(sc.Id);
                    }
                
                    if (counter++ % 50 == 0)
                    {
                        OnProgressUpdate((int)((double)counter / (double)this.Lines.Count * 100));

                        if (ct.IsCancellationRequested)
                        {
                            OnProgressUpdate(100);
                            OnSearchComplete(true);
                            return;
                        }
                    }
                }

                OnProgressUpdate(100);
                OnSearchComplete(false);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ct"></param>
        public void Export(string filePath, CancellationToken ct)
        {
            this.ExportToFile(this.Lines, filePath, ct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ct"></param>
        public void Export(IEnumerable lines, string filePath, CancellationToken ct)
        {
            this.ExportToFile(lines, filePath, ct);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ct"></param>
        private void ExportToFile(IEnumerable lines, string filePath, CancellationToken ct)
        {
            Task.Run(() =>
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    string line2 = string.Empty;
                    byte[] line;
                    byte[] endLine = new byte[2];
                    endLine[0] = 13;
                    endLine[1] = 10;

                    long counter = 0;
                    foreach (LogLine ll in lines)
                    {
                        line2 = this.GetLine(ll.LineNumber);
                        //line = this.GetLineBytes(ll.LineNumber);
                        line = Encoding.ASCII.GetBytes(line2);
                        fs.Write(line, 0, line.Length);
                        fs.Write(endLine, 0, 2);

                        if (counter++ % 50 == 0)
                        {
                            OnProgressUpdate((int)((double)counter / (double)Lines.Count * 100));

                            if (ct.IsCancellationRequested)
                            {
                                OnProgressUpdate(100);
                                OnSearchComplete(true);
                                return;
                            }
                        }
                    }

                    OnProgressUpdate(100);
                    OnSearchComplete(false);
                }                    
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="charCount"></param>
        private void AddLine(long offset, int charCount)
        {           
            LogLine ll = new LogLine();
            ll.Offset = offset;
            ll.CharCount = charCount;
            //  ll.FirstNonWhiteSpace = this.firstNonWhitespace(stringBuilder.ToString());
            //  ll.LastNonWhiteSpace = this.lastNonWhitespace(stringBuilder.ToString());
            ll.LineNumber = this.LineCount;
            this.Lines.Add(ll);
            if (charCount > this.LongestLine.CharCount)
            {
                this.LongestLine.CharCount = charCount;
                this.LongestLine.LineNumber = ll.LineNumber;
            }

            this.LineCount++;
        }

        private int firstNonWhitespace(string s)
        {
            if (s.Length == 0)
                return 0;
            int index = 0;
            while (index < s.Length && ((int)s[index] == 32 || (int)s[index] == 9))
                ++index;
            if (index == s.Length)
                return 0;
            return index;
        }

        private int lastNonWhitespace(string s)
        {
            if (s.Length == 0)
                return 0;
            int index = s.Length - 1;
            while (((int)s[index] == 32 || (int)s[index] == 9 || ((int)s[index] == 13 || (int)s[index] == 10)) && index > 0)
                --index;
            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public string GetLine(int lineNumber)
        {
            if (lineNumber >= this.Lines.Count)
            {
                return "";
            }

            byte[] buffer = new byte[this.Lines[lineNumber].CharCount + 1];
            try
            {
                this.readMutex.WaitOne();
                this.fileStream.Seek(this.Lines[lineNumber].Offset, SeekOrigin.Begin);
                this.fileStream.Read(buffer, 0, this.Lines[lineNumber].CharCount);
                this.readMutex.ReleaseMutex();
            }
            catch (Exception){}

            return Regex.Replace(Encoding.ASCII.GetString(buffer), "[\0-\b\n\v\f\x000E-\x001F\x007F-ÿ]", "", RegexOptions.Compiled);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <returns></returns>
        public byte[] GetLineBytes(int lineNumber)
        {
            if (lineNumber >= this.Lines.Count)
            {
                return new byte[]{};
            }

            byte[] buffer = new byte[this.Lines[lineNumber].CharCount + 1];
            try
            {
                this.readMutex.WaitOne();
                this.fileStream.Seek(this.Lines[lineNumber].Offset, SeekOrigin.Begin);
                this.fileStream.Read(buffer, 0, this.Lines[lineNumber].CharCount);
                this.readMutex.ReleaseMutex();
            }
            catch (Exception) { }

            return buffer;
        }

        #region Event Methods
        /// <summary>
        /// 
        /// </summary>
        private void OnProgressUpdate(int progress)
        {
            var handler = ProgressUpdate;
            if (handler != null)
            {
                handler(progress);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnLoadComplete(bool cancelled)
        {
            var handler = LoadComplete;
            if (handler != null)
            {
                handler(cancelled);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnSearchComplete(bool cancelled)
        {
            var handler = SearchComplete;
            if (handler != null)
            {
                handler(cancelled);
            }
        }
        #endregion
    }
}
