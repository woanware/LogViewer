using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using woanware;
using System.Threading;

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
            this.olvcLineNumber.AspectGetter = delegate (object x)
            {
                return (((LogLine)x).LineNumber + 1);
            };

            this.olvcText.AspectGetter = delegate (object x)
            {
                return (lf.GetLine(((LogLine)x).LineNumber));
            };
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
            if (lf != null)
            {
                listviewLines.ClearObjects();
                lf.ProgressUpdate -= LogFile_LoadProgress;
                lf.LoadComplete -= LogFile_LoadComplete;
                lf.SearchComplete -= LogFile_SearchComplete;
                lf.ExportComplete -= LogFile_ExportComplete;
                lf.Dispose();
            }

            cancellationTokenSource = new CancellationTokenSource();

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

            cancellationTokenSource = new CancellationTokenSource();

            lf.Search(sc, toolButtonCumulative.Checked, cancellationTokenSource.Token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        private void Export(string filePath)
        {
            cancellationTokenSource = new CancellationTokenSource();

            if (listviewLines.ModelFilter == null)
            {
                lf.Export(filePath, cancellationTokenSource.Token);
            }
            else
            {
                lf.Export(listviewLines.FilteredObjects, filePath, cancellationTokenSource.Token);
            }            
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
        private void LogFile_SearchComplete(bool cancelled)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                listviewLines.Refresh();
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();

            }), null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        private void LogFile_ExportComplete(bool val)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                statusProgress.Visible = false;
                this.hourGlass.Dispose();
                SetProcessingState(true);
                this.cancellationTokenSource.Dispose();

            }), null);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogFile_LoadComplete(bool cancelled)
        {
            synchronizationContext.Post(new SendOrPostCallback(o =>
            {
                listviewLines.SetObjects(lf.Lines);

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

            }), null);           
        }
        #endregion

        #region List Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listviewLines_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            //if (((LogLine)e.Model).Match == true)
            if (((LogLine)e.Model).SearchMatches.Intersect(filterIds).Any() == true)
            {
                e.Item.BackColor = Color.Red;
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
            this.listviewLines.ModelFilter = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuFilterShowMatched_Click(object sender, EventArgs e)
        {
            this.listviewLines.ModelFilter = new ModelFilter(delegate (object x) {
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
            this.listviewLines.ModelFilter = new ModelFilter(delegate (object x) {
                return x != null && (((LogLine)x).SearchMatches.Intersect(filterIds).Any() == false);
            });
        }

        /// <summary>
        /// Show the Searches window to allow the user to enable/disable search terms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuSearches_Click(object sender, EventArgs e)
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

                listviewLines.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "All Files|*.*";
            sfd.FileName = "*.*";
            sfd.Title = "Select the export file";

            if (sfd.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
            Export(sfd.FileName);
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

            hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
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

            hourGlass = new HourGlass(this);
            SetProcessingState(false);
            statusProgress.Visible = true;
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
