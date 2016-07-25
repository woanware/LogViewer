using System;
using System.Windows.Forms;
using woanware;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormGoToLine : Form
    {
        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormGoToLine()
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
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textLineNum.Text.Trim().Length == 0)
            {
                UserInterface.DisplayMessageBox(this, "The line number must be entered", MessageBoxIcon.Exclamation);
                textLineNum.Select();
                return;
            }

            if (Misc.IsNumber(textLineNum.Text) == false)
            {
                UserInterface.DisplayMessageBox(this, "The line number value is invalid", MessageBoxIcon.Exclamation);
                textLineNum.Select();
                return;
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

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public int LineNumber
        {
            get
            {
                return Convert.ToInt32(textLineNum.Text);
            }
        }
        #endregion
    }
}
