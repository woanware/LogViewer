using System;
using System.Windows.Forms;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormConfiguration : Form
    {
        #region Member Variables/Properties
        public Configuration Config { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormConfiguration(Configuration config)
        {
            InitializeComponent();
            this.Config = config;
            if (this.Config.NumContextLines > 0)
            {
                checkShowContextLines.Checked = true;
                comboNumLines.SelectedIndex = this.Config.NumContextLines - 1;
            }
            else
            {
                checkShowContextLines.Checked = false;
                comboNumLines.SelectedIndex = 0;
            }

            checkShowContextLines_CheckedChanged(this, null);
        }
        #endregion

        #region Buttton Event Handlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkShowContextLines.Checked == true)
            {
                Config.NumContextLines = comboNumLines.SelectedIndex + 1;
            }
            else
            {
                Config.NumContextLines = 0;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkShowContextLines_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowContextLines.Checked == true)
            {
                comboNumLines.Enabled = true;
            }
            else
            {
                comboNumLines.Enabled = false;
            }
        }
    }
}
