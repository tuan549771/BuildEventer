using Microsoft.Win32;
using PostBuildCopy.Classes;
using PostBuildCopy.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.Widowns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Register();
        }

        private void Register()
        {
            listBoxDestinations.LoadItemListBox(listBoxSources.SelectedIndex, listBoxSources.LoadListBoxItemSources1);
        }

        private void btnLoadXml_Click(object sender, RoutedEventArgs e)
        {
            string pathToXmlFile = GetFileDialog();
            if (null == pathToXmlFile)
                return;
            try
            {
                // loading data parameters treeview
                PathTreeNodeData parameterNode = new PathTreeNodeData();
                parameterNode = xmlLoader.LoadListPathFromXmlToNodeTree(pathToXmlFile, "Parameters", "Parameter");
                UIParameter.SetDataRoot(parameterNode);

                // loading data filters treeview
                PathTreeNodeData filterNode = new PathTreeNodeData();
                filterNode = xmlLoader.LoadListPathFromXmlToNodeTree(pathToXmlFile, "Filters", "Filter");
                UIFilter.SetDataRoot(filterNode);

                // loading data the branchs of explorer
                BranchsExplorer.SetBranchsExplorers(xmlLoader.LoadExplorer(pathToXmlFile));

                // loading data listBoxs
                ActionManager.actions = xmlLoader.LoadActionsManager(pathToXmlFile);
                listBoxDestinations.ItemSource = ActionManager.GetListDestinationOfActionManager();
                listBoxDestinations.SelectedIndex = 0;
                Explorer.Initialize();
            }
            catch (Exception ex) { MessageBox.Show("Configuration xml:" + ex.Message, "Information"); }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PathTreeNodeData parameterNode = Parameter.GetData();
            PathTreeNodeData filterNode = Filter.GetData();
            List<CouplePath> couplePaths = BranchsExplorer.GetBranchsExplorers();
            xmlGenerator.GenerateXml(ActionManager.actions, parameterNode, filterNode, couplePaths);
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
        private XmlLoader xmlLoader = new XmlLoader();
    }
}
