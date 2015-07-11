using Microsoft.Win32;
using PostBuildCopy.Classes;
using System.Collections.Generic;
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
            Register();
        }

        private void Register()
        {
            listBoxDestinations.LoadItemListBox(listBoxSources.UCSources.SelectedIndexLB, listBoxSources.LoadListBoxItemSources1);
        }

        private void btnLoadXml_Click(object sender, RoutedEventArgs e)
        {
            string pathToXmlFile = GetFileDialog();
            if (null == pathToXmlFile)
                return;
            MessageBox.Show("Implementing...");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<string> iListParameter = new List<string>();
            List<string> iListFilter = new List<string>();
            List<StringPath> iStringPaths = new List<StringPath>();
            xmlGenerator.GenerateXml(ActionManager.actions, iListParameter, iListFilter, iStringPaths);
            MessageBox.Show("Implementing...");
        }

        private string GetFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml files (*.xml)|*.xml";
            ofd.Title = "Load XML File";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (true == ofd.ShowDialog())
                return ofd.FileName;
            return null;
        }

        private XmlGenerator xmlGenerator = new XmlGenerator();
    }
}
