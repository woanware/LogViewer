using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using woanware;
using System.Threading;
using System.Text;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormMain : Form
    {
        #region Member Variables
        private LogFile lf;
        private Searches searches;
        private HourGlass hourGlass;
        private readonly SynchronizationContext synchronizationContext;
        private CancellationTokenSource cancellationTokenSource;
        private List<ushort> filterIds;
        private bool processing;
        private Color highlightColour = Color.Lime;
        private Configuration config;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            synchronizationContext = SynchronizationContext.Current;
            dropdownSearchType.SelectedIndex = 0;
            searches = new Searches();
            filterIds = new List<ushort>();
        }
        #endregion

        #region Form Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.config = new Configuration();
            string ret = this.config.Load();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, ret);
            }

            this.highlightColour = config.GetHighlightColour();

            this.olvcLineNumber.AspectGetter = delegate (object x)
            {
                return (((LogLine)x).LineNumber + 1);
            };

            this.olvcText.AspectGetter = delegate (object x)
            {
                return (lf.GetLine(((LogLine)x).LineNumber));
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            config.HighlightColour = this.highlightColour.ToKnownColor().ToString();
            string ret = config.Save();
            if (ret.Length > 0)
            {
                UserInterface.DisplayErrorMessageBox(this, ret);
            }
        }
        #endregion

        #region Log File Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private void LoadFile(string filePath)
        {
            this.processing = true;
            this.hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            this.cancellationTokenSource = new CancellationTokenSource();

            if (lf != null)
            {
                listLines.ClearObjects();
                lf.ProgressUpdate -= LogFile_LoadProgress;
                lf.LoadComplete -= LogFile_LoadComplete;
                lf.SearchComplete -= LogFile_SearchComplete;
                lf.ExportComplete -= LogFile_ExportComplete;
                lf.Dispose();
            }

            lf = new LogFile();
            lf.ProgressUpdate += LogFile_LoadProgress;
            lf.LoadComplete += LogFile_LoadComplete;
            lf.SearchComplete += LogFile_SearchComplete;
            lf.ExportComplete += LogFile_ExportComplete;
            lf.Load(filePath, cancellationTokenSource.Token);        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void SearchFile()
        {
            SearchCriteria sc = new SearchCriteria();
            sc.Type = (Global.SearchType)dropdownSearchType.SelectedIndex;
            sc.Pattern = textSearch.Text;
            sc.Id = searches.Add(sc, toolButtonCumulative.Checked);

            // Add the ID so that any matches show up straight away
            filterIds.Add(sc.Id);

            this.processing = true;
            this.hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            this.cancellationTokenSource = new CancellationTokenSource();
            lf.Search(sc, toolButtonCumulative.Checked, cancellationTokenSource.Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        private void Export(string filePath)
        {
            this.processing = true;
            this.hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            this.cancellationTokenSource = new CancellationTokenSource();

            if (listLines.ModelFilter == null)
            {
                lf.Export(filePath, cancellationTokenSource.Token);
            }
            else
            {
                lf.Export(listLines.FilteredObjects, filePath, cancellationTokenSource.Token);
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        private void ExportSelected(string filePath)
        {
            this.processing = true;
            this.hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            this.cancellationTokenSource = new CancellationTokenSource();

            lf.Export(listLines.SelectedObjects, filePath, cancellationTokenSource.Token);
        }
        #endregion

        #region Log File Object Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="percent"></param>
        private void LogFile_LoadProgress(int percent)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Value = (int)o;
            }), percent);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogFile_SearchComplete(TimeSpan duration, long matches, bool cancelled)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                listLines.Refresh();
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                UpdateStatusLabel("Matched " + matches + " lines (Search Terms: " + this.searches.Count + ") # Duration: " + duration, statusLabelSearch);
                this.processing = false;

            }), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void LogFile_ExportComplete(TimeSpan duration, bool val)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                UpdateStatusLabel("Export complete # Duration: " + duration, statusLabelSearch);
                this.processing = false;

            }), null);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogFile_LoadComplete(TimeSpan duration, bool cancelled)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                listLines.SetObjects(lf.Lines);

                // Try and measure the length of the longest line in pixels
                // This is rough, and tends to be too short, but cannot find
                // another method to make column wide enough :-)
                using (var image = new Bitmap(1, 1))
                {
                    using (var g = Graphics.FromImage(image))
                    {
                        string temp = lf.GetLine(lf.LongestLine.LineNumber);
                        var result = g.MeasureString(temp, new Font("Consolas", 9, FontStyle.Regular, GraphicsUnit.Pixel));
                        olvcText.Width = Convert.ToInt32(result.Width);
                    }
                }

                statusProgress.Visible = false;
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                UpdateStatusLabel(lf.Lines.Count + " Lines # Duration: " + duration, statusLabelMain);
                this.processing = false;

            }), null);           
        }
        #endregion

        #region List Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listLines_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            if (((LogLine)e.Model).SearchMatches.Intersect(filterIds).Any() == true)
            {
                e.Item.BackColor = highlightColour;
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listLines_DragEnter(object sender, DragEventArgs e)
        {
            if (processing == true)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listLines_DragDrop(object sender, DragEventArgs e)
        {
            if (processing == true)
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                return;
            }

            if (files.Length > 1)
            {
                UserInterface.DisplayMessageBox(this, "Only one file can be processed at one time", MessageBoxIcon.Exclamation);
                return;
            }

            LoadFile(files[0]);
        }
        #endregion

        #region Context Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuFilterClear_Click(object sender, EventArgs e)
        {
            this.listLines.ModelFilter = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuFilterShowMatched_Click(object sender, EventArgs e)
        {
            this.listLines.ModelFilter = new ModelFilter(delegate (object x) {
                return x != null && (((LogLine)x).SearchMatches.Intersect(filterIds).Any() == true);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuFilterHideMatched_Click(object sender, EventArgs e)
        {           
            this.listLines.ModelFilter = new ModelFilter(delegate (object x) {
                return x != null && (((LogLine)x).SearchMatches.Intersect(filterIds).Any() == false);
            });
        }

        /// <summary>
        /// Show the Searches window to allow the user to enable/disable search terms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuSearchViewTerms_Click(object sender, EventArgs e)
        {
            using (FormSearches f = new FormSearches(this.searches))
            {
                DialogResult dr = f.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                this.searches = f.Searches;

                filterIds.Clear();
                foreach (SearchCriteria sc in searches.Items)
                {
                    if (sc.Enabled == false)
                    {
                        continue;
                    }

                    filterIds.Add(sc.Id);
                }

                listLines.Refresh();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuSearchColour_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult dr = cd.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            this.highlightColour = cd.Color;
            listLines.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuExportAll_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*";
            sfd.FileName = "*.*";
            sfd.Title = "Select the export file";

            if (sfd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            Export(sfd.FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuExportSelected_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*";
            sfd.FileName = "*.*";
            sfd.Title = "Select the export file";

            if (sfd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            ExportSelected(sfd.FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuCopy_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (LogLine ll in listLines.SelectedObjects)
            {
                sb.AppendLine(lf.GetLine(ll.LineNumber));
            }

            Clipboard.SetText(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listLines.SelectedObjects.Count > this.config.MultiSelectLimit)
            {
                contextMenuCopy.Enabled = false;
                contextMenuExportSelected.Enabled = false;
                return;
            }

            contextMenuCopy.Enabled = true;
            contextMenuExportSelected.Enabled = true;
        }
        #endregion

        #region Toolbar Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolButtonSearch_Click(object sender, EventArgs e)
        {
            if (dropdownSearchType.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The search type is not selected", MessageBoxIcon.Exclamation);
                dropdownSearchType.Select();
                return;
            }

            SearchCriteria sc = this.searches.Items.SingleOrDefault(x => x.Pattern == textSearch.Text);
            if (sc != null)
            {
                UserInterface.DisplayMessageBox(this, "The search pattern already exists", MessageBoxIcon.Exclamation);
                return;
            }

            SearchFile();
        }
        #endregion

        #region Menu Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";
            openFileDialog.FileName = "*.*";
            openFileDialog.Title = "Select the log file";

            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            LoadFile(openFileDialog.FileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpHelp_Click(object sender, EventArgs e)
        {
           // Misc.ShellExecuteFile(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "Help.pdf"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            using (FormAbout formAbout = new FormAbout())
            {
                formAbout.ShowDialog(this);
            }
        }
        #endregion

        #region UI Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void SetProcessingState(bool enabled)
        {
            MethodInvoker methodInvoker = delegate
            {
                menuFileOpen.Enabled = enabled;
                menuFileExit.Enabled = enabled;
                toolButtonCumulative.Enabled = enabled;
                toolButtonSearch.Enabled = enabled;
            };

            if (this.InvokeRequired == true)
            {
                this.BeginInvoke(methodInvoker);
            }
            else
            {
                methodInvoker.Invoke();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void UpdateStatusLabel(string text, ToolStripStatusLabel control)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                control.Text = (string)o;
            }), text);
        }
        #endregion

        #region Other Control Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statusProgress_Click(object sender, EventArgs e)
        {
            this.cancellationTokenSource.Cancel();          
        }
        #endregion
    }
}
