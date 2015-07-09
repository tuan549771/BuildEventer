using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace PostBuildCopy.Widowns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnLoadXml_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Implementing...");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Implementing...");
        }

       

        
    }
}
