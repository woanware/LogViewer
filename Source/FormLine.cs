using System;
using System.Windows.Forms;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormLine : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormLine()
        {
            InitializeComponent();
        }
        #endregion

        #region Button Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
