//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!

using PostBuildCopy.Classes;
using PostBuildCopy.Windowns;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for TreeFilter.xaml
    /// </summary>
    public partial class TreeFilter : UserControl
    {
        SubFunctionsManager subFunctions = new SubFunctionsManager();
        public TreeFilter()
        {
            InitializeComponent();
            TreeNodeFilter.Items.Add("Right click to add filters ");
        }

        // ===============
        // Filter Treeview
        // ===============
        private void tvFilter_MouseRight(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if (null != item)
                item.IsSelected = true;
        }

        private void tvFilter_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {

                    VariablesManager.draggedItem = (TreeViewItem)tvFilter.SelectedItem;
                    if (null != VariablesManager.draggedItem)
                    {
                        DragDropEffects finalDropEffect = DragDrop.DoDragDrop(tvFilter, tvFilter.SelectedValue, DragDropEffects.Move);

                        if ((finalDropEffect == DragDropEffects.Move) && (null != VariablesManager.targetItem))
                        {
                            StringPath stringPath = new StringPath();
                            stringPath.parentPath = subFunctions.GetPathFromExplorer(VariablesManager.targetItem);
                            stringPath.subPath = VariablesManager.draggedItem.Header.ToString();
                            foreach (StringPath str in VariablesManager.strBranchsExplorer)
                                if ((stringPath.parentPath == str.parentPath) && (stringPath.subPath == str.subPath))
                                {
                                    MessageBox.Show(str.subPath + " exists","My App", MessageBoxButton.OK, MessageBoxImage.Information);
                                    return;
                                }
                            VariablesManager.strBranchsExplorer.Add(stringPath);
                            VariablesManager.oldStringPaths.Add(stringPath);
                            subFunctions.CopyItem(VariablesManager.draggedItem, VariablesManager.targetItem);
                            VariablesManager.targetItem = null;
                            VariablesManager.draggedItem = null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewFilterMenu_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem itemFilter = new TreeViewItem();
            WindowNewFilter inputDialog = new WindowNewFilter();
            if (inputDialog.ShowDialog() == true)
            {
                if (TreeNodeFilter.Items[0] is string)
                    TreeNodeFilter.Items.RemoveAt(0);
                itemFilter.Header = inputDialog.Answer;
                VariablesManager.filters.Add(itemFilter.Header.ToString());
                TreeNodeFilter.Items.Add(itemFilter);
            }
        }

        private void FilterDeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item1 = (TreeViewItem)tvFilter.SelectedItem;
            if (null == item1 || TreeNodeFilter.Items[0] is string)
                return;
            foreach (TreeViewItem filter in TreeNodeFilter.Items)
                if (filter.Header == item1.Header)
                    VariablesManager.filters.Remove(item1.Header.ToString());
            TreeNodeFilter.Items.Remove(item1);
            if (1 > TreeNodeFilter.Items.Count)
                TreeNodeFilter.Items.Add("Right click to add filters ");
        }
    }
}
