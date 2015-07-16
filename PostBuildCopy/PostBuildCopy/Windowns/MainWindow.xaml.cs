/*
<License>
Copyright 2015 Virtium Technology
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http ://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
</License>
*/

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

        private XmlGenerator m_XmlGenerator = new XmlGenerator();
        private XmlLoader m_XmlLoader = new XmlLoader();

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
                // Loading data into parameters treeview
                UIParameter.SetDataFromXmlData(m_XmlLoader.LoadListPathFromXmlToNodeTree(pathToXmlFile, "Parameters", "Parameter"));

                // Loading data into filters treeview
                UIFilter.SetDataFromXmlData(m_XmlLoader.LoadListPathFromXmlToNodeTree(pathToXmlFile, "Filters", "Filter"));

                // Loading data into the branchs of explorer
                BranchsExplorer.SetBranchsExplorers(m_XmlLoader.LoadExplorer(pathToXmlFile));

                // Loading data into listBoxs
                ActionManager.Actions = m_XmlLoader.LoadActionsManager(pathToXmlFile);
                listBoxDestinations.ItemSource = ActionManager.GetListDestinationOfActionManager();
                listBoxDestinations.SelectedIndex = 0;

                // Initialize Explorer
                Explorer.InitializeData();
            }
            catch (Exception ex) { MessageBox.Show("Load configuration xml error: " + ex.Message, "Information"); }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            PathTreeNodeData parameterNode = UIParameter.GetData();
            PathTreeNodeData filterNode = UIFilter.GetData();
            List<CouplePath> couplePaths = BranchsExplorer.GetBranchsExplorers();
            m_XmlGenerator.GenerateXml(ActionManager.Actions, parameterNode, filterNode, couplePaths);
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
