using System;
using System.Linq;
using System.Windows.Forms;

namespace LogViewer
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSearchTerms : Form
    {
        #region Member Variables/Properties
        public Searches Searches { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FormSearchTerms(Searches searches)
        {
            InitializeComponent();

            this.listSearchTerms.BooleanCheckStateGetter = delegate (Object rowObject) {
                return ((SearchCriteria)rowObject).Enabled;
            };

            this.listSearchTerms.BooleanCheckStatePutter = delegate (Object rowObject, bool newValue) {
                ((SearchCriteria)rowObject).Enabled = newValue;
                return newValue; // return the value that you want the control to use
            };

            listSearchTerms.SetObjects(searches.Items);
            this.Searches = searches;
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
            foreach (SearchCriteria sc in listSearchTerms.Objects)
            {
                SearchCriteria temp = this.Searches.Items.Single(i => i.Id == sc.Id);
                temp.Enabled = sc.Enabled;
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
