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
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace PostBuildCopy.Classes
{
    class XmlGenerator
    {
        #region Public functions

        public void GenerateXml(List<Action> iListAction, PathTreeNodeData iRootParameterNode,
                                PathTreeNodeData iRootFilterNode, List<CouplePath> iCouplePaths)
        {
            if (0 == iListAction.Count)
            {
                MessageBox.Show("There are no have any action", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (false == InformationWarning(iListAction))
                return;
            string pathXml = SaveFile();
            if (null == pathXml)
                return;

            XDocument xmlFile = new XDocument(new XElement("Objects"));

            xmlFile.Element("Objects").Add(SetPathNodeToXmlFile("Parameters", "Parameter", iRootParameterNode));
            xmlFile.Element("Objects").Add(SetActionsToXmlFile(iListAction));
            xmlFile.Element("Objects").Add(SetPathNodeToXmlFile("Filters", "Filter", iRootFilterNode));
            xmlFile.Element("Objects").Add(SetBranchExplorerToXmlFile(iCouplePaths));
            xmlFile.Save(pathXml);
        }

        #endregion

        #region Private functions

        // Set detinations and sources of actions into xml file. 
        private XElement SetActionsToXmlFile(List<Action> iListAction)
        {
            XElement Actions = new XElement("Actions");
            foreach (object itemAction in iListAction)
            {
                XElement Sou = new XElement("Sources");
                foreach (PathDataModel itemSource in (itemAction as CopySourcesToDestination).Sources)
                    Sou.Add(new XElement("Source", itemSource.PathModel));
                string pathDest = (itemAction as CopySourcesToDestination).Destination.PathModel;
                Actions.Add(new XElement("Copy", new XElement("Destination", pathDest), new XElement(Sou)));
            }
            return Actions;
        }

        // This function used to
        // Set argumentName of a argurment into xml file 
        private XElement SetPathNodeToXmlFile(string iParent, string iSub, PathTreeNodeData iRootNode)
        {
            XElement parents = new XElement(iParent);
            foreach (PathTreeNodeData sub in iRootNode.Children)
                parents.Add(new XElement(iSub, sub.Path));
            return parents;
        }


        // This function used to set branchs of explorer 
        // A branch include a couple paths: subpath and parentpath
        // Into xml file
        private XElement SetBranchExplorerToXmlFile(List<CouplePath> iCouplePaths)
        {
            XElement branchExplorer = new XElement("BranchExplorers");
            foreach (CouplePath couplePath in iCouplePaths)
                branchExplorer.Add(new XElement("BranchExplorer", new XElement("Parent", couplePath.ParentPath), new XElement("Sub", couplePath.SubPath)));
            return branchExplorer;
        }

        // Check each action include sources in destination
        // If not output a message to inform
        private bool InformationWarning(List<Action> iListAction)
        {
            foreach (object itemAction in iListAction)
            {
                if (0 == (itemAction as CopySourcesToDestination).Sources.Count)
                {
                    string destfree = (itemAction as CopySourcesToDestination).Destination.PathModel;
                    MessageBox.Show("You have not selected Sources to " + destfree, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            return true;
        }

        // Save file Dialog
        private string SaveFile()
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
    }
}
