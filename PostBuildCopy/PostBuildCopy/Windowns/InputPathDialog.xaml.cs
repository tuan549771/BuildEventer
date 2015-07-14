using System;
using System.Windows;

namespace PostBuildCopy.Widowns
{
    public partial class InputPathDialog : Window
    {
        #region Constructor

        public InputPathDialog()
        {
            InitializeComponent();
        }

        public InputPathDialog(string strTextBox)
        {
            txtPathName.Text = strTextBox;
        }

        #endregion

        #region Methods

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtPathName.SelectAll();
            txtPathName.Focus();
        }

        #endregion

        #region Property

        public string Path
        {
            get { return txtPathName.Text; }
        }

        #endregion
    }
}
