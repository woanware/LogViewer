namespace LogViewer
{
    partial class FormSearchTerms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchTerms));
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
            this.listSearchTerms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listSearchTerms.FullRowSelect = true;
            this.listSearchTerms.HasCollapsibleGroups = false;
            this.listSearchTerms.HeaderUsesThemes = true;
            this.listSearchTerms.HideSelection = false;
            this.listSearchTerms.Location = new System.Drawing.Point(15, 14);
            this.listSearchTerms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listSearchTerms.MultiSelect = false;
            this.listSearchTerms.Name = "listSearchTerms";
            this.listSearchTerms.OwnerDraw = false;
            this.listSearchTerms.ShowGroups = false;
            this.listSearchTerms.Size = new System.Drawing.Size(673, 440);
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
            this.contextMenu.Size = new System.Drawing.Size(61, 4);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(384, 470);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(147, 48);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(539, 470);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(147, 48);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormSearchTerms
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(706, 533);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listSearchTerms);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(728, 587);
            this.Name = "FormSearchTerms";
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