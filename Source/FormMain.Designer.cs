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
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolsMultiStringSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToolsConfiguration = new System.Windows.Forms.ToolStripMenuItem();
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
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFilterShowMatched = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuFilterHideMatched = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuFilterClear = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSearchViewTerms = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuSearchColour = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSearchMatch = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSearchColourContext = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuExportAll = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuExportSelected = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.contextLines = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLinesGoToLine = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLinesGoToFirstLine = new System.Windows.Forms.ToolStripMenuItem();
            this.contextLinesGoToLastLine = new System.Windows.Forms.ToolStripMenuItem();
            this.listLines = new BrightIdeasSoftware.FastObjectListView();
            this.olvcLineNumber = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcText = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.listLines)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
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
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsMultiStringSearch,
            this.toolStripMenuItem2,
            this.menuToolsConfiguration});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(65, 29);
            this.menuTools.Text = "Tools";
            // 
            // menuToolsMultiStringSearch
            // 
            this.menuToolsMultiStringSearch.Enabled = false;
            this.menuToolsMultiStringSearch.Name = "menuToolsMultiStringSearch";
            this.menuToolsMultiStringSearch.Size = new System.Drawing.Size(247, 30);
            this.menuToolsMultiStringSearch.Text = "Multi-String Search";
            this.menuToolsMultiStringSearch.Click += new System.EventHandler(this.menuToolsMultiStringSearch_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(244, 6);
            // 
            // menuToolsConfiguration
            // 
            this.menuToolsConfiguration.Name = "menuToolsConfiguration";
            this.menuToolsConfiguration.Size = new System.Drawing.Size(247, 30);
            this.menuToolsConfiguration.Text = "Configuration";
            this.menuToolsConfiguration.Click += new System.EventHandler(this.menuToolsConfiguration_Click);
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
            this.panelMain.AllowDrop = true;
            this.panelMain.Controls.Add(this.listLines);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 66);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1346, 436);
            this.panelMain.TabIndex = 5;
            this.panelMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.panelMain_DragDrop);
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
            this.contextMenuCopy,
            this.toolStripMenuItem5,
            this.contextLines});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenu.Size = new System.Drawing.Size(161, 178);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // contextMenuFilter
            // 
            this.contextMenuFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuFilterShowMatched,
            this.contextMenuFilterHideMatched,
            this.toolStripMenuItem1,
            this.contextMenuFilterClear});
            this.contextMenuFilter.Name = "contextMenuFilter";
            this.contextMenuFilter.Size = new System.Drawing.Size(160, 30);
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
            this.contextMenuSep1.Size = new System.Drawing.Size(157, 6);
            // 
            // contextMenuSearch
            // 
            this.contextMenuSearch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuSearchViewTerms,
            this.toolStripMenuItem4,
            this.contextMenuSearchColour});
            this.contextMenuSearch.Name = "contextMenuSearch";
            this.contextMenuSearch.Size = new System.Drawing.Size(160, 30);
            this.contextMenuSearch.Text = "Search";
            // 
            // contextMenuSearchViewTerms
            // 
            this.contextMenuSearchViewTerms.Name = "contextMenuSearchViewTerms";
            this.contextMenuSearchViewTerms.Size = new System.Drawing.Size(185, 30);
            this.contextMenuSearchViewTerms.Text = "View Terms";
            this.contextMenuSearchViewTerms.Click += new System.EventHandler(this.contextMenuSearchViewTerms_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(182, 6);
            // 
            // contextMenuSearchColour
            // 
            this.contextMenuSearchColour.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuSearchMatch,
            this.contextMenuSearchColourContext});
            this.contextMenuSearchColour.Name = "contextMenuSearchColour";
            this.contextMenuSearchColour.Size = new System.Drawing.Size(185, 30);
            this.contextMenuSearchColour.Text = "Colour";
            // 
            // contextMenuSearchMatch
            // 
            this.contextMenuSearchMatch.Name = "contextMenuSearchMatch";
            this.contextMenuSearchMatch.Size = new System.Drawing.Size(158, 30);
            this.contextMenuSearchMatch.Text = "Match";
            this.contextMenuSearchMatch.Click += new System.EventHandler(this.contextMenuSearchColourMatch_Click);
            // 
            // contextMenuSearchColourContext
            // 
            this.contextMenuSearchColourContext.Name = "contextMenuSearchColourContext";
            this.contextMenuSearchColourContext.Size = new System.Drawing.Size(158, 30);
            this.contextMenuSearchColourContext.Text = "Context";
            this.contextMenuSearchColourContext.Click += new System.EventHandler(this.contextMenuSearchColourContext_Click);
            // 
            // contextMenuSep2
            // 
            this.contextMenuSep2.Name = "contextMenuSep2";
            this.contextMenuSep2.Size = new System.Drawing.Size(157, 6);
            // 
            // contextMenuExport
            // 
            this.contextMenuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuExportAll,
            this.contextMenuExportSelected});
            this.contextMenuExport.Name = "contextMenuExport";
            this.contextMenuExport.Size = new System.Drawing.Size(160, 30);
            this.contextMenuExport.Text = "Export";
            // 
            // contextMenuExportAll
            // 
            this.contextMenuExportAll.Name = "contextMenuExportAll";
            this.contextMenuExportAll.Size = new System.Drawing.Size(163, 30);
            this.contextMenuExportAll.Text = "All";
            this.contextMenuExportAll.Click += new System.EventHandler(this.contextMenuExportAll_Click);
            // 
            // contextMenuExportSelected
            // 
            this.contextMenuExportSelected.Name = "contextMenuExportSelected";
            this.contextMenuExportSelected.Size = new System.Drawing.Size(163, 30);
            this.contextMenuExportSelected.Text = "Selected";
            this.contextMenuExportSelected.Click += new System.EventHandler(this.contextMenuExportSelected_Click);
            // 
            // contextMenuSep3
            // 
            this.contextMenuSep3.Name = "contextMenuSep3";
            this.contextMenuSep3.Size = new System.Drawing.Size(157, 6);
            // 
            // contextMenuCopy
            // 
            this.contextMenuCopy.Name = "contextMenuCopy";
            this.contextMenuCopy.Size = new System.Drawing.Size(160, 30);
            this.contextMenuCopy.Text = "Copy";
            this.contextMenuCopy.Click += new System.EventHandler(this.contextMenuCopy_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(157, 6);
            // 
            // contextLines
            // 
            this.contextLines.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextLinesGoToLine,
            this.contextLinesGoToFirstLine,
            this.contextLinesGoToLastLine});
            this.contextLines.Name = "contextLines";
            this.contextLines.Size = new System.Drawing.Size(160, 30);
            this.contextLines.Text = "Lines";
            // 
            // contextLinesGoToLine
            // 
            this.contextLinesGoToLine.Name = "contextLinesGoToLine";
            this.contextLinesGoToLine.Size = new System.Drawing.Size(217, 30);
            this.contextLinesGoToLine.Text = "Go To Line";
            this.contextLinesGoToLine.Click += new System.EventHandler(this.contextLinesGoToLine_Click);
            // 
            // contextLinesGoToFirstLine
            // 
            this.contextLinesGoToFirstLine.Name = "contextLinesGoToFirstLine";
            this.contextLinesGoToFirstLine.Size = new System.Drawing.Size(217, 30);
            this.contextLinesGoToFirstLine.Text = "Go To First Line";
            this.contextLinesGoToFirstLine.Click += new System.EventHandler(this.contextLinesGoToFirstLine_Click);
            // 
            // contextLinesGoToLastLine
            // 
            this.contextLinesGoToLastLine.Name = "contextLinesGoToLastLine";
            this.contextLinesGoToLastLine.Size = new System.Drawing.Size(217, 30);
            this.contextLinesGoToLastLine.Text = "Go To Last Line";
            this.contextLinesGoToLastLine.Click += new System.EventHandler(this.contextLinesGoToLastLine_Click);
            // 
            // listLines
            // 
            this.listLines.AllColumns.Add(this.olvcLineNumber);
            this.listLines.AllColumns.Add(this.olvcText);
            this.listLines.AllowDrop = true;
            this.listLines.CellEditUseWholeCell = false;
            this.listLines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcLineNumber,
            this.olvcText});
            this.listLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listLines.FullRowSelect = true;
            this.listLines.HideSelection = false;
            this.listLines.Location = new System.Drawing.Point(0, 0);
            this.listLines.Name = "listLines";
            this.listLines.OwnerDraw = false;
            this.listLines.ShowFilterMenuOnRightClick = false;
            this.listLines.ShowGroups = false;
            this.listLines.ShowSortIndicators = false;
            this.listLines.Size = new System.Drawing.Size(1346, 436);
            this.listLines.TabIndex = 0;
            this.listLines.UseCompatibleStateImageBehavior = false;
            this.listLines.View = System.Windows.Forms.View.Details;
            this.listLines.VirtualMode = true;
            this.listLines.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.listLines_FormatRow);
            this.listLines.ItemActivate += new System.EventHandler(this.listLines_ItemActivate);
            this.listLines.DragDrop += new System.Windows.Forms.DragEventHandler(this.listLines_DragDrop);
            this.listLines.DragEnter += new System.Windows.Forms.DragEventHandler(this.listLines_DragEnter);
            // 
            // olvcLineNumber
            // 
            this.olvcLineNumber.Text = "Line No.";
            // 
            // olvcText
            // 
            this.olvcText.FillsFreeSpace = true;
            this.olvcText.Text = "Data";
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.contextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.listLines)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem contextMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearchViewTerms;
        private System.Windows.Forms.ToolStripMenuItem contextMenuExportAll;
        private System.Windows.Forms.ToolStripMenuItem contextMenuExportSelected;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuToolsMultiStringSearch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuToolsConfiguration;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearchColour;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearchMatch;
        private System.Windows.Forms.ToolStripMenuItem contextMenuSearchColourContext;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem contextLines;
        private System.Windows.Forms.ToolStripMenuItem contextLinesGoToLine;
        private System.Windows.Forms.ToolStripMenuItem contextLinesGoToFirstLine;
        private System.Windows.Forms.ToolStripMenuItem contextLinesGoToLastLine;
        private BrightIdeasSoftware.FastObjectListView listLines;
        private BrightIdeasSoftware.OLVColumn olvcLineNumber;
        private BrightIdeasSoftware.OLVColumn olvcText;
    }
}

