﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PostBuildCopy.Widowns
{
    /// <summary>
    /// Interaction logic for InputPath.xaml
    /// </summary>
    public partial class InputPath : Window
    {
        public InputPath()
        {
            InitializeComponent();
        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtPathName.SelectAll();
            txtPathName.Focus();
        }

        public string Path
        {
            get { return txtPathName.Text; }
        }
    }
}
