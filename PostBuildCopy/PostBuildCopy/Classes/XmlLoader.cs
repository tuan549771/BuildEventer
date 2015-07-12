using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace PostBuildCopy.Classes
{
    class XmlLoader
    {
        private XmlDocument xmlDocument = new XmlDocument();

        //Using load parameters and loaf filters
        public PathTreeNodeData LoadListPathFromXmlToNodeTree(string iPathToXmlFile, string iArgumentXml, string iArgumentNameXml)
        {
            xmlDocument.Load(iPathToXmlFile);
            PathTreeNodeData root = new PathTreeNodeData(iArgumentXml);
            try
            {
                XmlElement objects = xmlDocument.DocumentElement;
                XmlNode arguments = objects.SelectSingleNode(iArgumentXml);
                XmlNodeList argumentNames = arguments.SelectNodes(iArgumentNameXml);
                foreach (XmlNode argv in argumentNames)
                {
                    if (argv.Name == iArgumentNameXml)
                        root.AddChild(new PathTreeNodeData(argv.InnerText));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return root;
        }


        public List<CouplePath> LoadExplorer(string iPathToXml)
        {
            xmlDocument.Load(iPathToXml);
            List<CouplePath> couplePaths = new List<CouplePath>();
            try
            {
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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return couplePaths;
        }

        // Loading ActionsManager
        public List<Action> LoadActionsManager(string savefile_xml)
        {
            xmlDocument.Load(savefile_xml);
            List<Action> actions = new List<Action>();
            try
            {
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
                    index++;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return actions;
        }
    }
}
