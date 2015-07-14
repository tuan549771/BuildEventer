using Microsoft.Win32;
using PostBuildCopy.Classes;
using PostBuildCopy.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace PostBuildCopy.Widowns
{
    public partial class MainWindow : Window
    {
        #region Private members

        private XmlGenerator xmlGenerator = new XmlGenerator();
        private XmlLoader xmlLoader = new XmlLoader();

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            Register();
        }

        #endregion

        #region Methods

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
                ActionManager.Actions = xmlLoader.LoadActionsManager(pathToXmlFile);
                listBoxDestinations.ItemSource = ActionManager.GetListDestinationOfActionManager();
                listBoxDestinations.SelectedIndex = 0;
                Explorer.InitializeData();
            }
            catch (Exception ex) { MessageBox.Show("Load configuration xml error: " + ex.Message, "Information"); }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PathTreeNodeData parameterNode = TreeParameter.GetData();
            PathTreeNodeData filterNode = Filter.GetData();
            List<CouplePath> couplePaths = BranchsExplorer.GetBranchsExplorers();
            xmlGenerator.GenerateXml(ActionManager.Actions, parameterNode, filterNode, couplePaths);
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

        #endregion

    }
}
