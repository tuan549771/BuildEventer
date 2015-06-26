using System;
using System.Windows;

namespace PostBuildCopy.Windowns
{
    /// <summary>
    /// Interaction logic for WindowNewParameter.xaml
    /// </summary>
    public partial class WindowNewParameter : Window
    {
        public WindowNewParameter()
        {
            InitializeComponent();
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }
    }
}
