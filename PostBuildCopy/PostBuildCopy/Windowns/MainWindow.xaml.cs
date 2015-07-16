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

        private XmlManager m_XmlManager = new XmlManager();

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
                UIParameter.SetDataFromXmlData(m_XmlManager.LoadListPathFromXmlToNodeTree(pathToXmlFile, "Parameters", "Parameter"));

                // Loading data into filters treeview
                UIFilter.SetDataFromXmlData(m_XmlManager.LoadListPathFromXmlToNodeTree(pathToXmlFile, "Filters", "Filter"));

                // Loading data into the branchs of explorer
                BranchsExplorer.SetBranchsExplorers(m_XmlManager.LoadExplorer(pathToXmlFile));

                // Loading data into listBoxs
                ActionManager.Actions = m_XmlManager.LoadActionsManager(pathToXmlFile);
                listBoxDestinations.ItemSource = ActionManager.GetListDestinationOfActionManager();
                listBoxDestinations.SelectedIndex = 0;

                // Initialize Explorer
                Explorer.InitializeData();
            }
            catch (Exception ex) { MessageBox.Show("Load configuration xml error: " + ex.Message, "Information"); }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (0 < ActionManager.Actions.Count)
            {
                if (true == ActionManager.HasContentInActions(ActionManager.Actions))
                {
                    string pathXml = GetPathToSaveFile();
                    if (null != pathXml)
                    {
                        PathTreeNodeData parameterNode = UIParameter.GetData();
                        PathTreeNodeData filterNode = UIFilter.GetData();
                        List<CouplePath> couplePaths = BranchsExplorer.GetBranchsExplorers();
                        m_XmlManager.GenerateXml(pathXml, ActionManager.Actions, parameterNode, filterNode, couplePaths);
                    }
                }
            }
            else
            {
                MessageBox.Show("There are no both sources and destinations in Gui script", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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

        private string GetPathToSaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files (*.xml)|*.xml";
            sfd.FileName = "BuildEventer";
            sfd.Title = "Save XML File";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            if (true == sfd.ShowDialog())
            {
                return sfd.FileName;
            }
            return null;
        }

        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
