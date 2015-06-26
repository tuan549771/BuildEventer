//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace PostBuildCopy.Classes
{
    class XmlCode
    {
        #region Public functions

        // This function generate a xml file that include parameters, filters, actions
        public void GenerateXml(ObservableCollection<Action> iListAction, List<string> iListParameter,
                                List<string> iListFilter, List<StringPath> iStringPaths)
        {
            if (0 == iListAction.Count)
            {
                MessageBox.Show("There are no have any action","My App", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (false == InformationWarning(iListAction))
                return;
            string pathXml = SaveFile();
            if (null == pathXml)
                return;

            XDocument xmlFile = new XDocument(new XElement("Objects"));
            xmlFile.Element("Objects").Add(SaveString("Parameters", "Parameter", iListParameter));
            xmlFile.Element("Objects").Add(SaveString("Filters", "Filter", iListFilter));

            XElement Actions = new XElement("Actions");
            foreach (object itemAction in iListAction)
            {
                XElement Sou = new XElement("Sources");
                foreach (Source itemSource in (itemAction as CopySourcesToDestination).sources)
                    Sou.Add(new XElement("Source", itemSource.source));
                string pathDest = (itemAction as CopySourcesToDestination).destination.destination;
                Actions.Add(new XElement("Copy", new XElement("Destination", pathDest), new XElement(Sou)));
            }
            xmlFile.Element("Objects").Add(Actions);

            XElement branchExplorer = new XElement("BranchExplorers");
            foreach (StringPath stringPath in iStringPaths)
                branchExplorer.Add(new XElement("BranchExplorer", new XElement("Parent", stringPath.parentPath), new XElement("Sub", stringPath.subPath)));
            xmlFile.Element("Objects").Add(branchExplorer);

            xmlFile.Save(pathXml);
        }

        #endregion

        #region Private functions
        
        // This function check any copy action have both destination and sources
        private bool InformationWarning(ObservableCollection<Action> iListAction)
        {
            foreach (object itemAction in iListAction)
            {
                if (0 == (itemAction as CopySourcesToDestination).sources.Count)
                {
                    string destfree = (itemAction as CopySourcesToDestination).destination.destination;
                    MessageBox.Show("You have not selected Sources to " + destfree, "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            return true;
        }

        // This function save a list string with name list and it's element to xml 
        private XElement SaveString(string iParent, string iSub, List<string> iListSub)
        {
            XElement parents = new XElement(iParent);
            foreach (string sub in iListSub)
                parents.Add(new XElement(iSub, sub));
            return parents;
        }

        private string SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "xml files (*.xml)|*.xml";
            sfd.FileName = "BuildEventer";
            sfd.Title = "Save xml file";
            sfd.InitialDirectory = Directory.GetCurrentDirectory();
            if (true == sfd.ShowDialog())
            {
                return sfd.FileName;
            }
            return null;
        }

        #endregion

        #region Public functions

        // This function load parameter or filter from xml to Gui
        public List<string> LoadString(string iPathToXml, string iParent, string iSub)
        {
            xmlDocument.Load(iPathToXml);
            List<string> loadString = new List<string>();
            try
            {
                XmlElement objects = xmlDocument.DocumentElement;
                XmlNode parameters = objects.SelectSingleNode(iParent);
                XmlNodeList parameterNames = parameters.SelectNodes(iSub);
                foreach (XmlNode argv in parameterNames)
                {
                    if (argv.Name == iSub)
                        loadString.Add(argv.InnerText);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return loadString;
        }

        // This function load branchs of explorer from xml to Gui
        public List<StringPath> LoadExplorer(string iPathToXml)
        {
            xmlDocument.Load(iPathToXml);
            List<StringPath> loadStringPaths = new List<StringPath>();
            try
            {
                XmlElement objects = xmlDocument.DocumentElement;
                XmlNode branchExplorers = objects.SelectSingleNode("BranchExplorers");
                XmlNodeList strPaths = branchExplorers.SelectNodes("BranchExplorer");
                foreach (XmlNode strPath in strPaths)
                {
                    StringPath strPathTemp = new StringPath();
                    foreach (XmlNode item in strPath.ChildNodes)
                    {
                        if (item.Name == "Parent")
                            strPathTemp.parentPath = item.InnerText;
                        if (item.Name == "Sub")
                            strPathTemp.subPath = item.InnerText;
                    }
                    loadStringPaths.Add(strPathTemp);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return loadStringPaths;
        }

        // This function load actions from xml to Gui
        public ActionManager LoadActionsManager(string savefile_xml)
        {
            xmlDocument.Load(savefile_xml);
            ActionManager actionManagerLoad = new ActionManager();
            try
            {
                XmlElement objects = xmlDocument.DocumentElement;
                XmlNode actions = objects.SelectSingleNode("Actions");
                XmlNodeList copies = actions.SelectNodes("Copy");
                foreach (XmlNode itemcopy in copies)
                {
                    Destination destTemp = new Destination();
                    ObservableCollection<Source> sourcesTemp = new ObservableCollection<Source>();
                    CopySourcesToDestination copyMutipleSourcesToOneDestTemp = new CopySourcesToDestination();
                    foreach (XmlNode itempara in itemcopy.ChildNodes)
                    {
                        if (itempara.Name == "Destination")
                            destTemp.destination = itempara.InnerText;
                        if (itempara.Name == "Sources")
                        {
                            XmlNodeList Sources = itempara.SelectNodes("Source");
                            foreach (XmlNode itemsource in Sources)
                                sourcesTemp.Add(new Source() { source = itemsource.InnerText });
                        }
                    }
                    copyMutipleSourcesToOneDestTemp.destination = destTemp;
                    copyMutipleSourcesToOneDestTemp.sources = sourcesTemp;
                    actionManagerLoad.listAction.Add(copyMutipleSourcesToOneDestTemp);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return actionManagerLoad;
        }

        #endregion

        #region Members

        private XmlDocument xmlDocument = new XmlDocument();

        #endregion
    }
}
