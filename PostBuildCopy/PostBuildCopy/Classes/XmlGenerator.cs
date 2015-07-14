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

        private XElement SetPathNodeToXmlFile(string iParent, string iSub, PathTreeNodeData iRootNode)
        {
            XElement parents = new XElement(iParent);
            foreach (PathTreeNodeData sub in iRootNode.Children)
                parents.Add(new XElement(iSub, sub.Path));
            return parents;
        }

        private XElement SetBranchExplorerToXmlFile(List<CouplePath> iCouplePaths)
        {
            XElement branchExplorer = new XElement("BranchExplorers");
            foreach (CouplePath couplePath in iCouplePaths)
                branchExplorer.Add(new XElement("BranchExplorer", new XElement("Parent", couplePath.ParentPath), new XElement("Sub", couplePath.SubPath)));
            return branchExplorer;
        }

        private bool InformationWarning(List<Action> iListAction)
        {
            foreach (object itemAction in iListAction)
            {
                if (0 == (itemAction as CopySourcesToDestination).Sources.Count)
                {
                    string destfree = (itemAction as CopySourcesToDestination).Destination.PathModel;
                    MessageBox.Show("You have not selected Sources to " + destfree, "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            return true;
        }

        private string SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files (*.xml)|*.xml";
            sfd.FileName = "generate";
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
