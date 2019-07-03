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
        private readonly SynchronizationContext synchronizationContext;
        private CancellationTokenSource cancellationTokenSource;        
        private HourGlass hourGlass;
        private bool processing;
        private Color highlightColour = Color.Lime;
        private Color contextColour = Color.LightGray;
        private Configuration config;        
        private Dictionary<string, LogFile> logs;
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
            logs = new Dictionary<string, LogFile>();
            
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
            this.contextColour = config.GetContextColour();

            menuFileOpen.Enabled = false;
            menuFileClose.Enabled = false;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_DragDrop(object sender, DragEventArgs e)
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

            if (logs.Count == 0)
            {
                LoadFile(files[0], true);
            }
            else
            {
                LoadFile(files[0], false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (processing == true)
            {
                return;
            }

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        #endregion

        #region Log File Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private void LoadFile(string filePath, bool newTab)
        {
            this.processing = true;
            this.hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            this.cancellationTokenSource = new CancellationTokenSource();
            menuToolsMultiStringSearch.Enabled = true;

            if (newTab == true)
            {
                LogFile lf = new LogFile();
                logs.Add(lf.Guid, lf);

                tabControl.TabPages.Add(lf.Initialise(filePath));
                lf.SetContextMenu(contextMenu);
                lf.ViewMode = Global.ViewMode.Standard;
                lf.ProgressUpdate += LogFile_LoadProgress;
                lf.LoadComplete += LogFile_LoadComplete;
                lf.SearchComplete += LogFile_SearchComplete;
                lf.ExportComplete += LogFile_ExportComplete;
                lf.LoadError += LogFile_LoadError;
                lf.List.ItemActivate += new EventHandler(this.listLines_ItemActivate);
                lf.List.DragDrop += new DragEventHandler(this.listLines_DragDrop);
                lf.List.DragEnter += new DragEventHandler(this.listLines_DragEnter);
                lf.Load(filePath, synchronizationContext, cancellationTokenSource.Token);
            }
            else
            {
                if (tabControl.SelectedTab == null)
                {
                    UserInterface.DisplayMessageBox(this, "Cannot identify current tab", MessageBoxIcon.Exclamation);
                    return;
                }

                if (!logs.ContainsKey(tabControl.SelectedTab.Tag.ToString()))
                {
                    UserInterface.DisplayMessageBox(this, "Cannot identify current tab", MessageBoxIcon.Exclamation);
                    return;
                }

                // Get the current selected log file and open the file using that object
                LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];
                tabControl.SelectedTab.ToolTipText = filePath;
                lf.Dispose();
                lf.Load(filePath, synchronizationContext, cancellationTokenSource.Token);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void SearchFile()
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            SearchCriteria sc = new SearchCriteria();
            sc.Type = (Global.SearchType)dropdownSearchType.SelectedIndex;
            sc.Pattern = textSearch.Text;
            sc.Id = lf.Searches.Add(sc, toolButtonCumulative.Checked);

            if (sc.Id == 0)
            {
                UserInterface.DisplayMessageBox(this, "The search pattern already exists", MessageBoxIcon.Exclamation);
                return;
            }           

            // Add the ID so that any matches show up straight away
            lf.FilterIds.Add(sc.Id);

            this.processing = true;
            this.hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            this.cancellationTokenSource = new CancellationTokenSource();
            lf.Search(sc, toolButtonCumulative.Checked, cancellationTokenSource.Token, config.NumContextLines);
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

            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            if (lf.List.ModelFilter == null)
            {
                lf.Export(filePath, cancellationTokenSource.Token);
            }
            else
            {
                lf.Export(lf.List.FilteredObjects, filePath, cancellationTokenSource.Token);
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

            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];
            lf.Export(lf.List.SelectedObjects, filePath, cancellationTokenSource.Token);
        }
        #endregion

        #region Log File Object Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        private void LogFile_LoadError(string fileName, string message)
        {
            UserInterface.DisplayErrorMessageBox(this, message + " (" + fileName + ")");

            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                this.processing = false;

                // Lets clear the LogFile state and set the UI correctly
                menuFileClose_Click(this, null);

            }), null);
        }

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
        private void LogFile_SearchComplete(LogFile lf, string fileName, TimeSpan duration, long matches, int numTerms, bool cancelled)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                lf.List.Refresh();
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                UpdateStatusLabel("Matched " + matches + " lines (Search Terms: " + numTerms + ") # Duration: " + duration + " (" + fileName + ")", statusLabelMain);

                this.processing = false;

            }), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void LogFile_ExportComplete(LogFile lf, string fileName, TimeSpan duration, bool val)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                UpdateStatusLabel("Export complete # Duration: " + duration + " (" + fileName + ")", statusLabelMain);
                this.processing = false;
            }), null);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogFile_LoadComplete(LogFile lf, string fileName, TimeSpan duration, bool cancelled)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                lf.List.SetObjects(lf.Lines);

                // Try and measure the length of the longest line in pixels
                // This is rough, and tends to be too short, but cannot find
                // another method to make column wide enough :-)
                using (var image = new Bitmap(1, 1))
                {
                    using (var g = Graphics.FromImage(image))
                    {
                        string temp = lf.GetLine(lf.LongestLine.LineNumber);
                        var result = g.MeasureString(temp, new Font("Consolas", 9, FontStyle.Regular, GraphicsUnit.Pixel));
                        lf.List.Columns[1].Width = Convert.ToInt32(result.Width + 200);
                    }
                }

                lf.List.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                statusProgress.Visible = false;
               
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();
                UpdateStatusLabel(lf.Lines.Count + " Lines # Duration: " + duration + " (" + fileName + ")", statusLabelMain);             
                menuFileClose.Enabled = true;
                menuFileOpen.Enabled = true; // Enable the standard file open, since we can now open in an existing tab, since at least one tab exists
                int index = tabControl.TabPages.IndexOfKey("tabPage" + lf.Guid);
                tabControl.TabPages[index].Text = lf.FileName;
                this.hourGlass.Dispose();
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
            //if (this.viewMode != Global.ViewMode.FilterHide)
            //{
            if ((LogLine)e.Model == null)
            {
                return;
            }

            LogFile lf = logs[e.ListView.Tag.ToString()];

            if (((LogLine)e.Model).SearchMatches.Intersect(lf.FilterIds).Any() == true)
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
            else
            {
                e.Effect = DragDropEffects.None;
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

            LoadFile(files[0], false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listLines_ItemActivate(object sender, EventArgs e)
        {
            var lv = (FastObjectListView)sender;
            if (lv.SelectedObjects.Count != 1)
            {
                return;
            }

            LogFile lf = logs[lv.Tag.ToString()];
            LogLine ll = (LogLine)lv.SelectedObjects[0];
            using (FormLine f = new FormLine(lf.GetLine(ll.LineNumber)))
            {
                f.ShowDialog(this);
            }
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
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];
            // Get the currently selected row
            var ll = (LogLine)lf.List.SelectedObject;

            lf.List.ModelFilter = null;
            lf.ViewMode = Global.ViewMode.Standard;

            if (ll != null)
            {
                lf.List.EnsureVisible(ll.LineNumber - 1);
                lf.List.SelectedIndex = ll.LineNumber - 1;
                if (lf.List.SelectedItem != null)
                {
                    lf.List.FocusedItem = lf.List.SelectedItem;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuFilterShowMatched_Click(object sender, EventArgs e)
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];
            lf.ViewMode = Global.ViewMode.FilterShow;

            lf.List.ModelFilter = new ModelFilter(delegate (object x)
            {
                return x != null && (((LogLine)x).SearchMatches.Intersect(lf.FilterIds).Any() == true || (((LogLine)x).IsContextLine == true));
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuFilterHideMatched_Click(object sender, EventArgs e)
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];
            lf.ViewMode = Global.ViewMode.FilterShow;
            lf.List.ModelFilter = new ModelFilter(delegate (object x)
            {
                return x != null && (((LogLine)x).SearchMatches.Intersect(lf.FilterIds).Any() == false);
            });
        }

        /// <summary>
        /// Show the Searches window to allow the user to enable/disable search terms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuSearchViewTerms_Click(object sender, EventArgs e)
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            using (FormSearchTerms f = new FormSearchTerms(lf.Searches))
            {
                DialogResult dr = f.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                lf.Searches = f.Searches;
                lf.FilterIds.Clear();
                foreach (SearchCriteria sc in lf.Searches.Items)
                {
                    if (sc.Enabled == false)
                    {
                        continue;
                    }

                    lf.FilterIds.Add(sc.Id);
                }

                lf.List.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuSearchColourMatch_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult dr = cd.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            this.highlightColour = cd.Color;

            logs[tabControl.SelectedTab.Tag.ToString()].List.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuSearchColourContext_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult dr = cd.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            this.contextColour = cd.Color;

            logs[tabControl.SelectedTab.Tag.ToString()].List.Refresh();
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
            sfd.Title = "Select export file";

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
            sfd.Title = "Select export file";

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
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            StringBuilder sb = new StringBuilder();
            foreach (LogLine ll in lf.List.SelectedObjects)
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
        private void contextLinesGoToLine_Click(object sender, EventArgs e)
        {
            using (FormGoToLine f = new FormGoToLine())
            {
                DialogResult dr = f.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

                lf.List.EnsureVisible(f.LineNumber - 1);
                var ll = lf.Lines.SingleOrDefault(x => x.LineNumber == f.LineNumber);
                if (ll != null)
                {
                    lf.List.SelectedIndex = ll.LineNumber - 1;
                    if (lf.List.SelectedItem != null)
                    {
                        lf.List.FocusedItem = lf.List.SelectedItem;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextLinesGoToFirstLine_Click(object sender, EventArgs e)
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            lf.List.EnsureVisible(0);
            lf.List.SelectedIndex = 0;
            if (lf.List.SelectedItem != null)
            {
                lf.List.FocusedItem = lf.List.SelectedItem;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextLinesGoToLastLine_Click(object sender, EventArgs e)
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            lf.List.EnsureVisible(lf.LineCount - 1);
            lf.List.SelectedIndex = lf.LineCount - 1;
            if (lf.List.SelectedItem != null)
            {
                lf.List.FocusedItem = lf.List.SelectedItem;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool enableLineOps = true;

            LogFile lf = null;
            if (tabControl.SelectedTab != null)
            {
                lf = logs[tabControl.SelectedTab.Tag.ToString()];
            }
           
            if (lf == null)
            {
                enableLineOps = false;
            }
            else
            {
                if (lf.LineCount == 0)
                {
                    enableLineOps = false;
                }
            }

            contextLinesGoToFirstLine.Enabled = enableLineOps;
            contextLinesGoToLastLine.Enabled = enableLineOps;
            contextLinesGoToLine.Enabled = enableLineOps;

            if (lf != null)
            {
                if (lf.List.SelectedObjects.Count > this.config.MultiSelectLimit)
                {
                    contextMenuCopy.Enabled = false;
                    contextMenuExportSelected.Enabled = false;
                    return;
                }
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
            openFileDialog.Title = "Select log file";

            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            LoadFile(openFileDialog.FileName, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileOpenNewTab_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";
            openFileDialog.FileName = "*.*";
            openFileDialog.Title = "Select log file";

            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            LoadFile(openFileDialog.FileName, true);
        }

        /// <summary>
        /// Close the resources used for opening and processing the log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileClose_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == null || tabControl.SelectedIndex == -1)
            {
                return;
            }

            var tag = tabControl.SelectedTab.Tag.ToString();

            // Get rid of the event handlers to prevent a memory leak
            logs[tag].ProgressUpdate -= LogFile_LoadProgress;
            logs[tag].LoadComplete -= LogFile_LoadComplete;
            logs[tag].SearchComplete -= LogFile_SearchComplete;
            logs[tag].ExportComplete -= LogFile_ExportComplete;
            logs[tag].LoadError -= LogFile_LoadError;
            // Clear the rest
            logs[tag].List.ClearObjects();
            logs[tag].Dispose();
            logs.Remove(tag);

            tabControl.TabPages.Remove(tabControl.SelectedTab);

            if (logs.Count == 0)
            {
                menuFileOpen.Enabled = false;
                menuFileClose.Enabled = false;
            }

            UpdateStatusLabel("", statusLabelMain);
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
            Misc.ShellExecuteFile(System.IO.Path.Combine(Misc.GetApplicationDirectory(), "help.pdf"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuHelpAbout_Click(object sender, EventArgs e)
        {
            using (FormAbout f = new FormAbout())
            {
                f.ShowDialog(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsMultiStringSearch_Click(object sender, EventArgs e)
        {
            LogFile lf = logs[tabControl.SelectedTab.Tag.ToString()];

            using (FormSearch f = new FormSearch(lf.Searches))
            {
                DialogResult dr = f.ShowDialog(this);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                // Clear any existing filter ID's as we will only show the multi-string search
                lf.FilterIds.Clear();
                lf.Searches.Reset();
                foreach (SearchCriteria sc in f.NewSearches)
                {
                    // Add the ID so that any matches show up straight away
                    lf.FilterIds.Add(sc.Id);
                    lf.Searches.Add(sc);
                }

                this.processing = true;
                this.hourGlass = new HourGlass(this);
                SetProcessingState(false);
                statusProgress.Visible = true;
                this.cancellationTokenSource = new CancellationTokenSource();
                lf.SearchMulti(f.NewSearches, cancellationTokenSource.Token, config.NumContextLines);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuToolsConfiguration_Click(object sender, EventArgs e)
        {
            using (FormConfiguration f = new FormConfiguration(this.config))
            {
                f.ShowDialog(this);
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
                menuFileOpenNewTab.Enabled = enabled;
                menuFileClose.Enabled = enabled;
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
