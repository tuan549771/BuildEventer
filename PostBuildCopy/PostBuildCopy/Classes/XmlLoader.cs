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

using System.Collections.Generic;
using System.Xml;

namespace PostBuildCopy.Classes
{
    class XmlLoader
    {
        // Private member
        private XmlDocument xmlDocument = new XmlDocument();

        // Loading all argumentNames of a argument
        // Into a child nodes of root node
        // And return 
        public PathTreeNodeData LoadListPathFromXmlToNodeTree(string iPathToXmlFile, string iArgumentXml, string iArgumentNameXml)
        {
            xmlDocument.Load(iPathToXmlFile);
            PathTreeNodeData root = new PathTreeNodeData(iArgumentXml);
            XmlElement objects = xmlDocument.DocumentElement;
            XmlNode arguments = objects.SelectSingleNode(iArgumentXml);
            XmlNodeList argumentNames = arguments.SelectNodes(iArgumentNameXml);
            foreach (XmlNode argv in argumentNames)
            {
                if (argv.Name == iArgumentNameXml)
                    root.AddChildNoMessageExist(new PathTreeNodeData(argv.InnerText));
            }
            return root;
        }

        // Loading branchs of explorer
        public List<CouplePath> LoadExplorer(string iPathToXml)
        {
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
            return couplePaths;
        }

        // Loading actions
        public List<Action> LoadActionsManager(string savefile_xml)
        {
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
                index++;
            }
            return actions;
        }
    }
}
