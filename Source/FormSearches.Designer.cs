namespace LogViewer
{
    partial class FormSearches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearches));
            this.listSearchTerms = new BrightIdeasSoftware.ObjectListView();
            this.olvcPattern = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listSearchTerms)).BeginInit();
            this.SuspendLayout();
            // 
            // listSearchTerms
            // 
            this.listSearchTerms.AllColumns.Add(this.olvcPattern);
            this.listSearchTerms.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listSearchTerms.CellEditUseWholeCell = false;
            this.listSearchTerms.CheckBoxes = true;
            this.listSearchTerms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcPattern});
            this.listSearchTerms.ContextMenuStrip = this.contextMenu;
            this.listSearchTerms.Cursor = System.Windows.Forms.Cursors.Default;
            this.listSearchTerms.FullRowSelect = true;
            this.listSearchTerms.HasCollapsibleGroups = false;
            this.listSearchTerms.HeaderUsesThemes = true;
            this.listSearchTerms.HideSelection = false;
            this.listSearchTerms.Location = new System.Drawing.Point(12, 12);
            this.listSearchTerms.MultiSelect = false;
            this.listSearchTerms.Name = "listSearchTerms";
            this.listSearchTerms.OwnerDraw = false;
            this.listSearchTerms.ShowGroups = false;
            this.listSearchTerms.Size = new System.Drawing.Size(529, 322);
            this.listSearchTerms.TabIndex = 0;
            this.listSearchTerms.UseCompatibleStateImageBehavior = false;
            this.listSearchTerms.View = System.Windows.Forms.View.Details;
            // 
            // olvcPattern
            // 
            this.olvcPattern.AspectName = "Pattern";
            this.olvcPattern.Text = "Pattern";
            this.olvcPattern.Width = 134;
            // 
            // contextMenu
            // 
            this.contextMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenu.Size = new System.Drawing.Size(74, 4);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(299, 340);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(120, 40);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(425, 340);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(120, 40);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormSearches
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(557, 392);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listSearchTerms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSearches";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Terms";
            ((System.ComponentModel.ISupportInitialize)(this.listSearchTerms)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView listSearchTerms;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private BrightIdeasSoftware.OLVColumn olvcPattern;
    }
}