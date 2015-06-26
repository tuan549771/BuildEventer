//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!

using Microsoft.Win32;
using PostBuildCopy.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy
{
    public partial class MainWindow : Window
    {
        #region Constructors
        
        public MainWindow()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Initialize

        private void Initialize()
        {
            VariablesManager.currentDirectory = "D:\\";//VariablesManager.currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directoryRoot = new DirectoryInfo(VariablesManager.currentDirectory);
            VariablesManager.treeItemRoot = subFunctions.CreateTreeItem(directoryRoot);
            userControlExplorer.tvExplorer.Items.Add(VariablesManager.treeItemRoot);
            Register();
        }

        private void Register()
        {
            userControlDestinationsListBox.LoadItemListBox(userControlSourcesListBox.lbSources.SelectedIndex,
                                                        userControlSourcesListBox.LoadListBoxItemSources1);
        }

        #endregion

        #region Event handlers
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Classes.Action> listAction = new ObservableCollection<Classes.Action>();
            listAction = VariablesManager.actionManagers.GetAction();
            xmlCode.GenerateXml(listAction, VariablesManager.parameters, VariablesManager.filters, VariablesManager.strBranchsExplorer);
        }

        private void btnLoadXml_Click(object sender, RoutedEventArgs e)
        {
            string pathToXmlFile = OpenDialog();
            if (null == pathToXmlFile)
                return;
            VariablesManager.strBranchsExplorer = xmlCode.LoadExplorer(pathToXmlFile);
            VariablesManager.parameters = xmlCode.LoadString(pathToXmlFile, "Parameters", "Parameter");
            VariablesManager.filters = xmlCode.LoadString(pathToXmlFile, "Filters", "Filter");
            VariablesManager.actionManagers = xmlCode.LoadActionsManager(pathToXmlFile);

            userControlExplorer.tvExplorer.Items.Clear();
            parseObject(ref userControlParameterTreeView.treeNodeParameter, VariablesManager.parameters);
            parseObject(ref userControlFilterTreeView.TreeNodeFilter, VariablesManager.filters);
            userControlDestinationsListBox.lbDest.ItemsSource = subFunctions.GetDestFromActionManager(VariablesManager.actionManagers);
            userControlDestinationsListBox.lbDest.SelectedIndex = 0;
            DirectoryInfo directoryRoot = new DirectoryInfo(VariablesManager.currentDirectory);
            userControlExplorer.tvExplorer.Items.Add(subFunctions.CreateTreeItem(directoryRoot));
        }

        #endregion

        #region Private functions

        private string OpenDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml files (*.xml)|*.xml";
            ofd.Title = "Load xml file";
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
            if (true == ofd.ShowDialog())
                return ofd.FileName;
            return null;
        }

        private void parseObject(ref TreeViewItem iTreeItem, List<string> iParams)
        {
            iTreeItem.Items.Clear();
            foreach (string argv in iParams)
            {
                TreeViewItem item_para = new TreeViewItem();
                item_para.Header = argv;
                iTreeItem.Items.Add(item_para);
            }
        }

        #endregion

        #region Members
        
        private XmlCode xmlCode = new XmlCode();
        private SubFunctionsManager subFunctions = new SubFunctionsManager();

        #endregion
    }
}
