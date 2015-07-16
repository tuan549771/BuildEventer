//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!
using PostBuildCopy.Classes;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    public partial class TreeExplorer : UserControl
    {
        #region Constructors

        public TreeExplorer()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handlers

        private void TreeExplorer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                m_lastMouseDown = e.GetPosition(tvExplorer);
            }
        }

        private void TreeExplorer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(tvExplorer);
                if ((20.0 < Math.Abs(currentPosition.X - m_lastMouseDown.X)) ||
                            (20.0 < Math.Abs(currentPosition.Y - m_lastMouseDown.Y)))
                {
                    TreeViewItem item = e.Source as TreeViewItem;
                    TreeViewItem item1 = item;
                    string pathExplorer = m_SubFunctions.GetPathFromExplorer(item);
                    if (null != item)
                        DragDrop.DoDragDrop(item1, pathExplorer, DragDropEffects.Copy);
                }
            }
        }

        private void tvExplorer_MouseRight(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if (null != item)
                item.IsSelected = true;
        }


        private void TreeExplorer_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if (0 < item.Items.Count && item.Items[0] is string)
            {
                item.Items.RemoveAt(0);
                DirectoryInfo expandedDir = null;
                FileInfo[] expandedFile = null;
                try
                {
                    if (item.Tag is DriveInfo)
                        expandedDir = (item.Tag as DriveInfo).RootDirectory;
                    if (item.Tag is DirectoryInfo)
                    {
                        expandedDir = (item.Tag as DirectoryInfo);
                        expandedFile = (item.Tag as DirectoryInfo).GetFiles();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                try
                {
                    if (null != expandedDir)
                        foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
                            item.Items.Add(m_SubFunctions.CreateTreeItem(subDir));
                    if (null != expandedFile)
                        foreach (FileInfo subFile in expandedFile)
                            item.Items.Add(m_SubFunctions.CreateTreeItem(subFile));
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            ShowParametersExplorer(ref item);
        }

        private void menuRefresh_Click(object sender, RoutedEventArgs e)
        {
            VariablesManager.currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo current_working_directory = new DirectoryInfo(VariablesManager.currentDirectory);
            tvExplorer.Items.Clear();
            tvExplorer.Items.Add(m_SubFunctions.CreateTreeItem(current_working_directory));
            VariablesManager.oldStringPaths.Clear();
        }


        private void menuDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TreeExplorer_Drop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                // Verify that this is a valid drop and then store the drop target
                TreeViewItem TargetItem = m_SubFunctions.GetNearestContainer(e.OriginalSource as UIElement);
                if ((null != TargetItem) && (null != VariablesManager.draggedItem))
                {
                    VariablesManager.targetItem = TargetItem;
                    e.Effects = DragDropEffects.Move;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region Private functions
        
        private void ShowParametersExplorer(ref TreeViewItem i_item)
        {
            string path = m_SubFunctions.GetPathFromExplorer(i_item);
            foreach (StringPath strPath in VariablesManager.strBranchsExplorer)
            {
                if (path == strPath.parentPath && false == (VariablesManager.oldStringPaths.Contains(strPath)))
                {
                    TreeViewItem itemTemp = new TreeViewItem();
                    itemTemp.Header = strPath.subPath;
                    itemTemp.Foreground = Brushes.Magenta;
                    itemTemp.Items.Add("Loading...");
                    i_item.Items.Add(itemTemp);
                    VariablesManager.oldStringPaths.Add(strPath);
                }
            }
        }

        #endregion

        #region Members

        private SubFunctionsManager m_SubFunctions = new SubFunctionsManager();
        private Point m_lastMouseDown;

        #endregion
    }
}
