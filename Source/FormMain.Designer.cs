namespace LogViewer
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusSep1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelSearch = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolLabelSearch = new System.Windows.Forms.ToolStripLabel();
            this.textSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolLabelType = new System.Windows.Forms.ToolStripLabel();
            this.dropdownSearchType = new System.Windows.Forms.ToolStripComboBox();
            this.toolButtonCumulative = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolButtonSearch = new System.Windows.Forms.ToolStripButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.listLines = new BrightIdeasSoftware.FastObjectListView();
            this.olvcLineNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFilterShowMatched = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFilterHideMatched = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuFilterClear = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSearchViewTerms = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSearchColour = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuCopyLine = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listLines)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(1346, 33);
            this.menuStrip.TabIndex = 1;
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileOpen,
            this.menuFileSep1,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(50, 29);
            this.menuFile.Text = "&File";
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.Size = new System.Drawing.Size(141, 30);
            this.menuFileOpen.Text = "&Open";
            this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // menuFileSep1
            // 
            this.menuFileSep1.Name = "menuFileSep1";
            this.menuFileSep1.Size = new System.Drawing.Size(138, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(141, 30);
            this.menuFileExit.Text = "&Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpHelp,
            this.menuHelpSep1,
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(61, 29);
            this.menuHelp.Text = "&Help";
            // 
            // menuHelpHelp
            // 
            this.menuHelpHelp.Name = "menuHelpHelp";
            this.menuHelpHelp.Size = new System.Drawing.Size(147, 30);
            this.menuHelpHelp.Text = "&Help";
            this.menuHelpHelp.Click += new System.EventHandler(this.menuHelpHelp_Click);
            // 
            // menuHelpSep1
            // 
            this.menuHelpSep1.Name = "menuHelpSep1";
            this.menuHelpSep1.Size = new System.Drawing.Size(144, 6);
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(147, 30);
            this.menuHelpAbout.Text = "&About";
            this.menuHelpAbout.Click += new System.EventHandler(this.menuHelpAbout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProgress,
            this.statusLabelMain,
            this.statusSep1,
            this.statusLabelSearch});
            this.statusStrip.Location = new System.Drawing.Point(0, 502);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1346, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusProgress
            // 
            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(100, 22);
            this.statusProgress.Visible = false;
            this.statusProgress.Click += new System.EventHandler(this.statusProgress_Click);
            // 
            // statusLabelMain
            // 
            this.statusLabelMain.Name = "statusLabelMain";
            this.statusLabelMain.Size = new System.Drawing.Size(0, 17);
            // 
            // statusSep1
            // 
            this.statusSep1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusSep1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.statusSep1.Name = "statusSep1";
            this.statusSep1.Size = new System.Drawing.Size(4, 17);
            // 
            // statusLabelSearch
            // 
            this.statusLabelSearch.Name = "statusLabelSearch";
            this.statusLabelSearch.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolLabelSearch,
            this.textSearch,
            this.toolLabelType,
            this.dropdownSearchType,
            this.toolButtonCumulative,
            this.toolStripSeparator1,
            this.toolButtonSearch});
            this.toolStrip.Location = new System.Drawing.Point(0, 33);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(1346, 33);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolLabelSearch
            // 
            this.toolLabelSearch.Name = "toolLabelSearch";
            this.toolLabelSearch.Size = new System.Drawing.Size(64, 30);
            this.toolLabelSearch.Text = "Search";
            // 
            // textSearch
            // 
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(500, 33);
            // 
            // toolLabelType
            // 
            this.toolLabelType.Name = "toolLabelType";
            this.toolLabelType.Size = new System.Drawing.Size(49, 30);
            this.toolLabelType.Text = "Type";
            // 
            // dropdownSearchType
            // 
            this.dropdownSearchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdownSearchType.Items.AddRange(new object[] {
            "Sub String Case Insensitive",
            "Sub String Case Sensitive",
            "Regex Case Insensitive",
            "Regex Case Sensitive"});
            this.dropdownSearchType.Name = "dropdownSearchType";
            this.dropdownSearchType.Size = new System.Drawing.Size(250, 33);
            // 
            // toolButtonCumulative
            // 
            this.toolButtonCumulative.Checked = true;
            this.toolButtonCumulative.CheckOnClick = true;
            this.toolButtonCumulative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolButtonCumulative.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolButtonCumulative.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonCumulative.Image")));
            this.toolButtonCumulative.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonCumulative.Name = "toolButtonCumulative";
            this.toolButtonCumulative.Size = new System.Drawing.Size(104, 30);
            this.toolButtonCumulative.Text = "Cumulative";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // toolButtonSearch
            // 
            this.toolButtonSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolButtonSearch.Image = ((System.Drawing.Image)(resources.GetObject("toolButtonSearch.Image")));
            this.toolButtonSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolButtonSearch.Name = "toolButtonSearch";
            this.toolButtonSearch.Size = new System.Drawing.Size(28, 30);
            this.toolButtonSearch.ToolTipText = "Search";
            this.toolButtonSearch.Click += new System.EventHandler(this.toolButtonSearch_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.listLines);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 66);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1346, 436);
            this.panelMain.TabIndex = 5;
            // 
            // listLines
            // 
            this.listLines.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.listLines.AllColumns.Add(this.olvcLineNumber);
            this.listLines.AllColumns.Add(this.olvcText);
            this.listLines.AllowDrop = true;
            this.listLines.CellEditUseWholeCell = false;
            this.listLines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcLineNumber,
            this.olvcText});
            this.listLines.ContextMenuStrip = this.contextMenu;
            this.listLines.Cursor = System.Windows.Forms.Cursors.Default;
            this.listLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLines.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listLines.FullRowSelect = true;
            this.listLines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listLines.HideSelection = false;
            this.listLines.Location = new System.Drawing.Point(0, 0);
            this.listLines.Name = "listLines";
            this.listLines.OwnerDraw = false;
            this.listLines.ShowGroups = false;
            this.listLines.Size = new System.Drawing.Size(1346, 436);
            this.listLines.TabIndex = 0;
            this.listLines.UseCompatibleStateImageBehavior = false;
            this.listLines.UseFiltering = true;
            this.listLines.UseHotControls = false;
            this.listLines.View = System.Windows.Forms.View.Details;
            this.listLines.VirtualMode = true;
            this.listLines.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.listLines_FormatRow);
            this.listLines.DragDrop += new System.Windows.Forms.DragEventHandler(this.listLines_DragDrop);
            this.listLines.DragEnter += new System.Windows.Forms.DragEventHandler(this.listLines_DragEnter);
            // 
            // olvcLineNumber
            // 
            this.olvcLineNumber.Text = "Line";
            // 
            // olvcText
            // 
            this.olvcText.Hideable = false;
            this.olvcText.ShowTextInHeader = false;
            this.olvcText.Text = "Data";
            this.olvcText.Width = 439;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuFilter,
            this.contextMenuSep1,
            this.contextMenuSearch,
            this.contextMenuSep2,
            this.contextMenuExport,
            this.contextMenuSep3,
            this.contextMenuCopyLine});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenu.Size = new System.Drawing.Size(176, 142);
            // 
            // contextMenuFilter
            // 
            this.contextMenuFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuFilterShowMatched,
            this.contextMenuFilterHideMatched,
            this.toolStripMenuItem1,
            this.contextMenuFilterClear});
            this.contextMenuFilter.Name = "contextMenuFilter";
            this.contextMenuFilter.Size = new System.Drawing.Size(175, 30);
            this.contextMenuFilter.Text = "Filtering";
            // 
            // contextMenuFilterShowMatched
            // 
            this.contextMenuFilterShowMatched.Name = "contextMenuFilterShowMatched";
            this.contextMenuFilterShowMatched.Size = new System.Drawing.Size(215, 30);
            this.contextMenuFilterShowMatched.Text = "Show matched";
            this.contextMenuFilterShowMatched.Click += new System.EventHandler(this.contextMenuFilterShowMatched_Click);
            // 
            // contextMenuFilterHideMatched
            // 
            this.contextMenuFilterHideMatched.Name = "contextMenuFilterHideMatched";
            this.contextMenuFilterHideMatched.Size = new System.Drawing.Size(215, 30);
            this.contextMenuFilterHideMatched.Text = "Hide matched";
            this.contextMenuFilterHideMatched.Click += new System.EventHandler(this.contextMenuFilterHideMatched_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(212, 6);
            // 
            // contextMenuFilterClear
            // 
            this.contextMenuFilterClear.Name = "contextMenuFilterClear";
            this.contextMenuFilterClear.Size = new System.Drawing.Size(215, 30);
            this.contextMenuFilterClear.Text = "Clear";
            this.contextMenuFilterClear.Click += new System.EventHandler(this.contextMenuFilterClear_Click);
            // 
            // contextMenuSep1
            // 
            this.contextMenuSep1.Name = "contextMenuSep1";
            this.contextMenuSep1.Size = new System.Drawing.Size(172, 6);
            // 
            // contextMenuSearch
            // 
            this.contextMenuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuSearchViewTerms,
            this.contextMenuSearchColour});
            this.contextMenuSearch.Name = "contextMenuSearch";
            this.contextMenuSearch.Size = new System.Drawing.Size(175, 30);
            this.contextMenuSearch.Text = "Search";
            // 
            // contextMenuSearchViewTerms
            // 
            this.contextMenuSearchViewTerms.Name = "contextMenuSearchViewTerms";
            this.contextMenuSearchViewTerms.Size = new System.Drawing.Size(185, 30);
            this.contextMenuSearchViewTerms.Text = "View Terms";
            this.contextMenuSearchViewTerms.Click += new System.EventHandler(this.contextMenuSearchViewTerms_Click);
            // 
            // contextMenuSearchColour
            // 
            this.contextMenuSearchColour.Name = "contextMenuSearchColour";
            this.contextMenuSearchColour.Size = new System.Drawing.Size(185, 30);
            this.contextMenuSearchColour.Text = "Colour";
            this.contextMenuSearchColour.Click += new System.EventHandler(this.contextMenuSearchColour_Click);
            // 
            // contextMenuSep2
            // 
            this.contextMenuSep2.Name = "contextMenuSep2";
            this.contextMenuSep2.Size = new System.Drawing.Size(172, 6);
            // 
            // contextMenuExport
            // 
            this.contextMenuExport.Name = "contextMenuExport";
            this.contextMenuExport.Size = new System.Drawing.Size(175, 30);
            this.contextMenuExport.Text = "Export";
            this.contextMenuExport.Click += new System.EventHandler(this.contextMenuExport_Click);
            // 
            // contextMenuSep3
            // 
            this.contextMenuSep3.Name = "contextMenuSep3";
            this.contextMenuSep3.Size = new System.Drawing.Size(172, 6);
            // 
            // contextMenuCopyLine
            // 
            this.contextMenuCopyLine.Name = "contextMenuCopyLine";
            this.contextMenuCopyLine.Size = new System.Drawing.Size(175, 30);
            this.contextMenuCopyLine.Text = "Copy Line";
            this.contextMenuCopyLine.Click += new System.EventHandler(this.contextMenuCopyLine_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 524);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogViewer";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listLines)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMain;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripLabel toolLabelSearch;
        private System.Windows.Forms.ToolStripTextBox textSearch;
        private System.Windows.Forms.ToolStripButton toolButtonSearch;
        private System.Windows.Forms.Panel panelMain;
        private BrightIdeasSoftware.FastObjectListView listLines;
        private BrightIdeasSoftware.OLVColumn olvcLineNumber;
        private BrightIdeasSoftware.OLVColumn olvcText;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
        private System.Windows.Forms.ToolStripSeparator menuFileSep1;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.ToolStripStatusLabel statusSep1;
        private System.Windows.Forms.ToolStripSeparator menuHelpSep1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripComboBox dropdownSearchType;
        private System.Windows.Forms.ToolStripLabel toolLabelType;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFilter;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFilterClear;
        private System.Windows.Forms.ToolStripButton toolButtonCumulative;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFilterShowMatched;
        private System.Windows.Forms.ToolStripMenuItem contextMenuFilterHideMatched;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator contextMenuSep1;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearch;
        private System.Windows.Forms.ToolStripSeparator contextMenuSep2;
        private System.Windows.Forms.ToolStripMenuItem contextMenuExport;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelSearch;
        private System.Windows.Forms.ToolStripSeparator contextMenuSep3;
        private System.Windows.Forms.ToolStripMenuItem contextMenuCopyLine;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearchViewTerms;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearchColour;
    }
}

