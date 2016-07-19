using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using woanware;

namespace LogViewer
{
    /// <summary>
    /// Window to allow user to configure a multi string search
    /// </summary>
    public partial class FormSearch : Form
    {
        #region Member Variables
        public List<Pattern> Patterns { get; private set;  } = new List<Pattern>();
        private Searches existingSearches;
        public List<SearchCriteria> NewSearches { get; private set; } = new List<SearchCriteria>();
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormSearch(Searches searches)
        {
            InitializeComponent();
            comboType.SelectedIndex = 0;
            this.existingSearches = searches;
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files|*.*";
            openFileDialog.FileName = "*.*";
            openFileDialog.Title = "Select file";

            if (openFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            string line = string.Empty;
            Pattern p = null;           
            using (StreamReader sr = new System.IO.StreamReader(openFileDialog.FileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    p = new Pattern(line);
                    this.Patterns.Add(p);
                }
            }

            listTerms.SetObjects(this.Patterns);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboType.SelectedIndex == -1)
            {
                UserInterface.DisplayMessageBox(this, "The type is not selected", MessageBoxIcon.Exclamation);
                comboType.Select();
                return;
            }

            if (this.Patterns.Count == 0)
            {
                UserInterface.DisplayMessageBox(this, "No search patterns loaded", MessageBoxIcon.Exclamation);
                listTerms.Select();
                return;
            }

            // Disable all existing SearchCriteria
            foreach (SearchCriteria sc in this.existingSearches.Items)
            {
                sc.Enabled = false;
            }

            bool exists = false;
            foreach (Pattern p in this.Patterns)
            {
                SearchCriteria sc = new SearchCriteria();
                sc.Type = (Global.SearchType)comboType.SelectedIndex;
                sc.Pattern = p.Data;
                sc.Enabled = true;
                sc.Id = this.existingSearches.Add(sc);
                //if (sc.Id == 0)
                //{
                //    sc.Enabled = true;
                //    exists = true;
                //    continue;
                //}

                NewSearches.Add(sc);
            }

            if (exists == true)
            {
                UserInterface.DisplayMessageBox(this, "At least one pattern already exists and has not been added", MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion
    }
}
