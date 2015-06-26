//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostBuildCopy.Classes
{

    class SubFunctionsManager
    {
        // ===================
        // Sub Functions
        // ===================

        #region Public functions

        public ObservableCollection<Destination> GetDestFromActionManager(ActionManager iActions)
        {
            ObservableCollection<Destination> destinationTemps = new ObservableCollection<Destination>();
            foreach (CopySourcesToDestination copy in iActions.listAction)
            {
                destinationTemps.Add(copy.destination);
            }
            return destinationTemps;
        }

        public TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            item.Tag = o;
            item.Header = o.ToString();
            item.FontSize = 14;
            if (o is DirectoryInfo)
            {
                item.Foreground = Brushes.Black;
                item.Items.Add("Loading...");
            }
            if (o is FileInfo)
                item.Foreground = Brushes.Gray;
            return item;
        }

        public string GetPathRoot(string iPath)
        {
            string pathRoot = VariablesManager.treeItemRoot.Header.ToString();
            if (pathRoot == iPath)
                return null;
            if ("\\" == pathRoot[VariablesManager.currentDirectory.Length - 1].ToString())
                pathRoot = pathRoot.Substring(0, pathRoot.Length - 1);
            iPath = iPath.Substring(pathRoot.Length + 1);
            return iPath;
        }

        public TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            TreeViewItem container = element as TreeViewItem;
            while ((null == container) && (null != element))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }

        public string GetPathFromExplorer(TreeViewItem item)
        {
            if (null != item)
            {
                string pathDrag = item.Header.ToString();
                while (null != item)
                {
                    ItemsControl parent = ItemsControl.ItemsControlFromItemContainer(item);
                    if (null != parent)
                    {
                        item = parent as TreeViewItem;
                        if (null != item)
                        {
                            string temp = item.Header.ToString() + "\\";
                            pathDrag = String.Concat(temp, pathDrag);
                        }
                    }
                }
                pathDrag = pathDrag.Replace("\\\\", "\\");
                return pathDrag;
            }
            return null;
        }

        public void CopyItem(TreeViewItem iSourceItem, TreeViewItem iTargetItem)
        {
            TreeViewItem item1 = new TreeViewItem();
            item1.Header = iSourceItem.Header;
            item1.Foreground = Brushes.Magenta;
            item1.Items.Add("Loading...");
            iTargetItem.Items.Add(item1);
        }

        #endregion

    }
}
