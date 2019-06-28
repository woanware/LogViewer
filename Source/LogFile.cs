using BrightIdeasSoftware;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    internal class LogFile 
    {
        #region Delegates
        public delegate void SearchCompleteEvent(TimeSpan duration, long matches, int numSearchTerms, bool cancelled);
        public delegate void CompleteEvent(LogFile lf, TimeSpan duration, bool cancelled);
        public delegate void BoolEvent(bool val);
        public delegate void DefaultEvent();
        public delegate void MessageEvent(string message);
        public delegate void ProgressUpdateEvent(int percent);
        #endregion

        #region Events
        public event SearchCompleteEvent SearchComplete;
        public event CompleteEvent LoadComplete;
        public event CompleteEvent ExportComplete;
        public event ProgressUpdateEvent ProgressUpdate;
        public event MessageEvent LoadError;
        #endregion

        private Color highlightColour = Color.Lime;
        private Color contextColour = Color.LightGray;

        #region Member Variables
       
        public List<LogLine> Lines { get; private set; } = new List<LogLine>();
        public LogLine LongestLine { get; private set; } = new LogLine();
        public int LineCount { get; private set; } = 0;
        private FileStream fileStream;
        private Mutex readMutex = new Mutex();
        private string filePath;
        public List<ushort> FilterIds { get; private set; }  = new List<ushort>();
        public FastObjectListView List { get; set; }
        public string Guid { get; private set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public LogFile()
        {
           
            this.Guid = System.Guid.NewGuid().ToString();
        }

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ct"></param>
        public void Load(string filePath, SynchronizationContext st, CancellationToken ct)
        {
            this.filePath = filePath;

            Task.Run(() => {

                DateTime start = DateTime.Now;
                bool cancelled = false;
                bool error = false;
                try
                {                    
                    byte[] tempBuffer = new byte[1024 * 1024];

                    this.fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    FileInfo fileInfo = new FileInfo(filePath);

                    // Calcs and finally point the position to the end of the line
                    long position = 0;
                    // Holds the offset to the start of the next line
                    long lineStartOffset = 0; 
                    // Checks if we have read less than requested e.g. buffer not filled/end of file
                    bool lastSection = false;
                    // Counter for process reporting
                    int counter = 0; 
                    // Holds a counter to start checking for the next indexOf('\r')
                    int startIndex = 0;
                    // Once all of the \r (lines) have been emnumerated, there might still be data left in the
                    // buffer, so this holds the number of bytes that need to be added onto the next line
                    int bufferRemainder = 0; 
                    // Holds how many bytes were read from the last file stream read
                    int numBytesRead = 0;
                    // Holds the temporary string generated from the file stream buffer
                    string tempStr = string.Empty;
                    // Length of the current line
                    int charCount;
                    // Return value from IndexOf function
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

                                    // Check if the line contains a CR as well, if it does then we remove the last char as the char count
                                    if (indexOf != 0 && (int)tempBuffer[Math.Max(0, indexOf - 1)] == 13)
                                    {
                                        charCount = bufferRemainder + (indexOf - startIndex - 1);                           
                                        position += (long)charCount + 2L; 
                                    }
                                    else
                                    {
                                        charCount = bufferRemainder + (indexOf - startIndex);
                                        position += (long)charCount + 1L;
                                    }

                                    AddLine(lineStartOffset, charCount);

                                    // The remaining number in the buffer gets set to 0 e.g. after 
                                    //the first iteration as it would add onto the first line
                                    bufferRemainder = 0;

                                    // Set the offset to the end of the last line that has just been added
                                    lineStartOffset = position;
                                    startIndex = indexOf + 1;
                                }
                            }

                            // We had some '\r' in the last buffer read, now they are processing, so just add the rest as the last line
                            if (lastSection == true)
                            {
                                AddLine(lineStartOffset, bufferRemainder + (numBytesRead - startIndex));
                                return;
                            }
                            
                            bufferRemainder += numBytesRead - startIndex;
                        }
                        else
                        {
                            // The entire content of the buffer doesn't contain \r so just add the rest of content as the last line
                            if (lastSection == true)
                            {
                                AddLine(lineStartOffset, bufferRemainder + (numBytesRead - startIndex));
                                return;
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
                    } // WHILE
                }
                catch (IOException ex)
                {
                    OnLoadError(ex.Message);
                    error = true;
                }
                finally
                {
                    if (error == false)
                    {
                        DateTime end = DateTime.Now;

                        OnProgressUpdate(100);

                        st.Post(new SendOrPostCallback(o =>
                        {
                            this.List.SetObjects(this.Lines);

                            // Try and measure the length of the longest line in pixels
                            // This is rough, and tends to be too short, but cannot find
                            // another method to make column wide enough :-)
                            using (var image = new Bitmap(1, 1))
                            {
                                using (var g = Graphics.FromImage(image))
                                {
                                    string temp = this.GetLine(this.LongestLine.LineNumber);
                                    var result = g.MeasureString(temp, new Font("Consolas", 9, FontStyle.Regular, GraphicsUnit.Pixel));
                                    this.List.Columns[1].Width = Convert.ToInt32(result.Width + 100);
                                }
                            }

                            this.List.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);                         

                        }), null);

                        OnLoadComplete(end - start, cancelled);
                    }                   
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Lines.Clear();
            this.LongestLine = new LogLine();
            this.LineCount = 0;   
            this.filePath = string.Empty;
            this.FilterIds = new List<ushort>();
            this.List.ModelFilter = null;
            this.FilterIds.Clear();
            this.List.ClearObjects();

            if (this.fileStream != null)
            {
                this.fileStream.Dispose();
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchType"></param>
        public void SearchMulti(List<SearchCriteria> scs, CancellationToken ct, int numContextLines)
        {
            Task.Run(() => {

                DateTime start = DateTime.Now;
                bool cancelled = false;
                long matches = 0;
                try
                {
                    long counter = 0;
                    string line = string.Empty;
                    bool located = false;

                    foreach (LogLine ll in this.Lines)
                    {
                        // Reset the match flag
                        ll.SearchMatches.Clear();
                        ClearContextLine(ll.LineNumber, numContextLines);

                        foreach (SearchCriteria sc in scs)
                        {
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

                            if (located == true)
                            {
                                matches++;
                                ll.SearchMatches.Add(sc.Id);

                                if (numContextLines > 0)
                                {
                                    this.SetContextLines(ll.LineNumber, numContextLines);
                                }
                            }                              
                        }

                        if (counter++ % 50 == 0)
                        {
                            OnProgressUpdate((int)((double)counter / (double)this.Lines.Count * 100));

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
                    DateTime end = DateTime.Now;

                    OnProgressUpdate(100);
                    OnSearchComplete(end - start, matches, scs.Count, cancelled);
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="searchType"></param>
        public void Search(SearchCriteria sc, bool cumulative, CancellationToken ct, int numContextLines)
        {
            Task.Run(() => {

                DateTime start = DateTime.Now;
                bool cancelled = false;
                long matches = 0;
                try
                {
                    long counter = 0;
                    string line = string.Empty;
                    bool located = false;

                    foreach (LogLine ll in this.Lines)
                    {
                        if (cumulative == false)
                        {
                            // Reset the match flag
                            ll.SearchMatches.Clear();
                            //ll.IsContextLine = false;

                            ClearContextLine(ll.LineNumber, numContextLines);
                        }
                        else
                        {
                            if (ll.SearchMatches.Count > 0) {
                                continue;
                            }
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
                            matches++;
                            ll.SearchMatches.Add(sc.Id);

                            if (numContextLines > 0)
                            {
                                this.SetContextLines(ll.LineNumber, numContextLines);
                            }
                        }

                        if (counter++ % 50 == 0)
                        {
                            OnProgressUpdate((int)((double)counter / (double)this.Lines.Count * 100));

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
                    DateTime end = DateTime.Now;

                    OnProgressUpdate(100);
                    OnSearchComplete(end - start, matches, 1, cancelled);
                }
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
        /// <param name="lines"></param>
        /// <param name="filePath"></param>
        /// <param name="ct"></param>
        public void Export(IEnumerable lines, string filePath, CancellationToken ct)
        {
            this.ExportToFile(lines, filePath, ct);
        }
        #endregion

        public TabPage Initialise()
        {
            OLVColumn colLineNumber = ((OLVColumn)(new OLVColumn()));
            OLVColumn colText = ((OLVColumn)(new OLVColumn()));

            colLineNumber.Text = "Line No.";
            colLineNumber.Width = 95;
            colText.Text = "Data";

            colLineNumber.AspectGetter = delegate (object x)
            {
                if (((LogLine)x) == null)
                {
                    return "";
                }

                return (((LogLine)x).LineNumber + 1);
            };

            colText.AspectGetter = delegate (object x)
            {
                if (((LogLine)x) == null)
                {
                    return "";
                }

                return (this.GetLine(((LogLine)x).LineNumber));
            };

            FastObjectListView lv = new FastObjectListView();

            lv.AllColumns.Add(colLineNumber);
            lv.AllColumns.Add(colText);
            lv.AllowDrop = true;
            lv.AutoArrange = false;
            lv.CausesValidation = false;
            lv.CellEditUseWholeCell = false;
            lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            colLineNumber,
            colText});
            //lv.ContextMenuStrip = this.contextMenu;
            lv.Dock = System.Windows.Forms.DockStyle.Fill;
            lv.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lv.FullRowSelect = true;
            lv.GridLines = true;
            lv.HasCollapsibleGroups = false;
            lv.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            lv.HideSelection = false;
            lv.IsSearchOnSortColumn = false;
            lv.Location = new System.Drawing.Point(3, 3);
            lv.Margin = new System.Windows.Forms.Padding(4);
            lv.Name = "listLines0";
            lv.SelectColumnsMenuStaysOpen = false;
            lv.SelectColumnsOnRightClick = false;
            lv.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.None;
            lv.ShowFilterMenuOnRightClick = false;
            lv.ShowGroups = false;
            lv.ShowSortIndicators = false;
            lv.Size = new System.Drawing.Size(1679, 940);
            lv.TabIndex = 1;
            lv.TriggerCellOverEventsWhenOverHeader = false;
            lv.UseCompatibleStateImageBehavior = false;
            lv.UseFiltering = true;
            lv.View = System.Windows.Forms.View.Details;
            lv.VirtualMode = true;
            lv.Tag = this.Guid;
            lv.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.FormatRow);

            this.List = lv;

            TabPage tp = new TabPage();
            tp.Controls.Add(lv);
            tp.Location = new System.Drawing.Point(4, 33);
            tp.Name = "tabPage1";
            tp.Padding = new System.Windows.Forms.Padding(3);
            tp.Size = new System.Drawing.Size(1685, 946);
            tp.TabIndex = 0;
            tp.Text = "tabPage1";
            tp.UseVisualStyleBackColor = true;
            tp.Tag = this.Guid;

            return tp;
        }


        public void FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            //if (this.viewMode != Global.ViewMode.FilterHide)
            //{
            if ((LogLine)e.Model == null)
            {
                return;
            }

            if (((LogLine)e.Model).SearchMatches.Intersect(this.FilterIds).Any() == true)
            {
                e.Item.BackColor = highlightColour;
            }
            else if (((LogLine)e.Model).IsContextLine == true)
            {
                e.Item.BackColor = contextColour;
            }
            //}            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="numLines"></param>
        private void SetContextLines(long lineNumber, int numLines)
        {
            long temp = numLines;
            if (lineNumber < this.Lines.Count)
            {
                if (numLines + lineNumber > this.Lines.Count - 1)
                {
                    temp = this.Lines.Count - lineNumber - 1;
                }
                for (int index = 1; index <= temp; index++)
                {
                    this.Lines[(int)lineNumber + index].IsContextLine = true;
                }
            }           

            if (lineNumber > 0)
            {
                if (lineNumber - numLines < 0)
                {
                    temp = lineNumber;
                }
                for (int index = 1; index <= temp; index++)
                {
                    this.Lines[(int)lineNumber - index].IsContextLine = true;
                }
            }            
        }

        /// <summary>
        /// Clear the line that is the next after the farthest context
        /// line, so the flag is reset and we won't overwrite
        /// </summary>
        /// <param name="lineNumber"></param>
        /// <param name="numLines"></param>
        private void ClearContextLine(long lineNumber, int numLines)
        {
            long temp = numLines;
            if ((int)lineNumber + numLines + 1 < this.Lines.Count - 1)
            {
                this.Lines[(int)lineNumber + numLines + 1].IsContextLine = false;
            }
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
                DateTime start = DateTime.Now;
                bool cancelled = false;
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        string lineStr = string.Empty;
                        byte[] lineBytes;
                        byte[] endLine = new byte[2] { 13, 10 };

                        long counter = 0;
                        foreach (LogLine ll in lines)
                        {
                            lineStr = this.GetLine(ll.LineNumber);
                            lineBytes = Encoding.ASCII.GetBytes(lineStr);
                            fs.Write(lineBytes, 0, lineBytes.Length);
                            // Add \r\n
                            fs.Write(endLine, 0, 2);

                            if (counter++ % 50 == 0)
                            {
                                OnProgressUpdate((int)((double)counter / (double)Lines.Count * 100));

                                if (ct.IsCancellationRequested)
                                {
                                    cancelled = true;
                                    return;
                                }
                            }
                        }

                    }
                }
                finally
                {
                    DateTime end = DateTime.Now;

                    OnProgressUpdate(100);
                    OnExportComplete(end - start, cancelled);
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
            ll.LineNumber = this.LineCount;
            this.Lines.Add(ll);
            if (charCount > this.LongestLine.CharCount)
            {
                this.LongestLine.CharCount = charCount;
                this.LongestLine.LineNumber = ll.LineNumber;
            }

            this.LineCount++;
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
                return string.Empty;
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
        //public byte[] GetLineBytes(int lineNumber)
        //{
        //    if (lineNumber >= this.Lines.Count)
        //    {
        //        return new byte[]{};
        //    }

        //    byte[] buffer = new byte[this.Lines[lineNumber].CharCount + 1];
        //    try
        //    {
        //        this.readMutex.WaitOne();
        //        this.fileStream.Seek(this.Lines[lineNumber].Offset, SeekOrigin.Begin);
        //        this.fileStream.Read(buffer, 0, this.Lines[lineNumber].CharCount);
        //        this.readMutex.ReleaseMutex();
        //    }
        //    catch (Exception) { }

        //    return buffer;
        //}

        #region Event Methods
        /// <summary>
        /// 
        /// </summary>
        private void OnLoadError(string message)
        {
            var handler = LoadError;
            if (handler != null)
            {
                handler(message);
            }
        }

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
        private void OnLoadComplete(TimeSpan duration, bool cancelled)
        {
            var handler = LoadComplete;
            if (handler != null)
            {
                handler(this, duration, cancelled);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnExportComplete(TimeSpan duration, bool cancelled)
        {
            var handler = ExportComplete;
            if (handler != null)
            {
                handler(this, duration, cancelled);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnSearchComplete(TimeSpan duration, long matches, int numTerms, bool cancelled)
        {
            var handler = SearchComplete;
            if (handler != null)
            {
                handler(duration, matches, numTerms, cancelled);
            }
        }
        #endregion
    }
}
