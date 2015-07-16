using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace PostBuildCopy.Classes
{
    class XmlManager
    {
        // Loading all argumentNames of a argument
        // Into a child nodes of root node
        // And return 
        public PathTreeNodeData LoadListPathFromXmlToNodeTree(string iPathToXmlFile, string iArgumentXml, string iArgumentNameXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(iPathToXmlFile);
            PathTreeNodeData root = new PathTreeNodeData(iArgumentXml);
            XmlElement objects = xmlDocument.DocumentElement;
            XmlNode arguments = objects.SelectSingleNode(iArgumentXml);
            XmlNodeList argumentNames = arguments.SelectNodes(iArgumentNameXml);
            foreach (XmlNode argv in argumentNames)
            {
                if (argv.Name == iArgumentNameXml)
                    root.AddChild(new PathTreeNodeData(argv.InnerText), false);
            }
            xmlDocument = null;
            return root;
        }

        // Loading branchs of explorer
        public List<CouplePath> LoadExplorer(string iPathToXml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(iPathToXml);
            List<CouplePath> couplePaths = new List<CouplePath>();

            XmlElement objects = xmlDocument.DocumentElement;
            XmlNode branchExplorers = objects.SelectSingleNode("BranchExplorers");
            XmlNodeList strPaths = branchExplorers.SelectNodes("BranchExplorer");
            foreach (XmlNode strPath in strPaths)
            {
                CouplePath couplePath = new CouplePath();
                foreach (XmlNode item in strPath.ChildNodes)
                {
                    if (item.Name == "Parent")
                        couplePath.ParentPath = item.InnerText;
                    if (item.Name == "Sub")
                        couplePath.SubPath = item.InnerText;
                }
                couplePaths.Add(couplePath);
            }
            xmlDocument = null;
            return couplePaths;
        }

        // Loading actions
        public List<Action> LoadActionsManager(string savefile_xml)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(savefile_xml);
            List<Action> actions = new List<Action>();
            XmlElement objects = xmlDocument.DocumentElement;
            XmlNode action = objects.SelectSingleNode("Actions");
            XmlNodeList copies = action.SelectNodes("Copy");
            int index = 0;
            foreach (XmlNode itemcopy in copies)
            {
                foreach (XmlNode itempara in itemcopy.ChildNodes)
                {
                    if (itempara.Name == "Destination")
                        actions.Add(new CopySourcesToDestination(new PathDataModel(itempara.InnerText)));
                    if (itempara.Name == "Sources")
                    {
                        XmlNodeList Sources = itempara.SelectNodes("Source");
                        foreach (XmlNode itemsource in Sources)
                            (actions[index] as CopySourcesToDestination).Sources.Add(new PathDataModel(itemsource.InnerText));
                    }
                }
                ++index;
            }
            xmlDocument = null;
            return actions;
        }

        /// <summary>
        /// //// Save Xml
        /// </summary>

        #region Public functions

        public void GenerateXml(string iPathToXmlFile, List<Action> iListAction, PathTreeNodeData iRootParameterNode,
                                PathTreeNodeData iRootFilterNode, List<CouplePath> iCouplePaths)
        {
            XDocument xmlFile = new XDocument(new XElement("Objects"));
            xmlFile.Element("Objects").Add(AddContentOfPathNodesToXElement("Parameters", "Parameter", iRootParameterNode));
            xmlFile.Element("Objects").Add(AddContentOfActionsToXElement(iListAction));
            xmlFile.Element("Objects").Add(AddContentOfPathNodesToXElement("Filters", "Filter", iRootFilterNode));
            xmlFile.Element("Objects").Add(AddBranchExplorersToXElement(iCouplePaths));
            xmlFile.Save(iPathToXmlFile);
        }

        #endregion

        #region Private functions

        // Add detination paths and source paths of actions into xml file. 
        private XElement AddContentOfActionsToXElement(List<Action> iListAction)
        {
            XElement actions = new XElement("Actions");
            foreach (object itemAction in iListAction)
            {
                XElement Sou = new XElement("Sources");
                foreach (PathDataModel itemSource in (itemAction as CopySourcesToDestination).Sources)
                    Sou.Add(new XElement("Source", itemSource.PathModel));
                string pathDest = (itemAction as CopySourcesToDestination).Destination.PathModel;
                actions.Add(new XElement("Copy", new XElement("Destination", pathDest), new XElement(Sou)));
            }
            return actions;
        }

        // This function used to
        // Add argumentNames of a argurment into xml file 
        private XElement AddContentOfPathNodesToXElement(string iParent, string iSub, PathTreeNodeData iRootNode)
        {
            XElement parents = new XElement(iParent);
            foreach (PathTreeNodeData sub in iRootNode.Children)
                parents.Add(new XElement(iSub, sub.Path));
            return parents;
        }


        // This function used to add branchs of explorer 
        // A branch include a couple paths: subpath and parentpath
        // Into xml file
        private XElement AddBranchExplorersToXElement(List<CouplePath> iCouplePaths)
        {
            XElement branchExplorer = new XElement("BranchExplorers");
            foreach (CouplePath couplePath in iCouplePaths)
                branchExplorer.Add(new XElement("BranchExplorer", new XElement("Parent", couplePath.ParentPath), new XElement("Sub", couplePath.SubPath)));
            return branchExplorer;
        }

        #endregion

    }
}
