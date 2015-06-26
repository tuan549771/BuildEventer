//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!

using PostBuildCopy.Classes;
using PostBuildCopy.Windowns;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PostBuildCopy.UI
{
    public partial class TreeParameter : UserControl
    {
        private SubFunctionsManager subFunctions = new SubFunctionsManager();
        public TreeParameter()
        {
            InitializeComponent();
            treeNodeParameter.Items.Add("Right click to add parameters ");
        }

        private void tvParameter_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    VariablesManager.draggedItem = (TreeViewItem)tvParameters.SelectedItem;
                    if (null != VariablesManager.draggedItem)
                    {
                        DragDropEffects finalDropEffect = DragDrop.DoDragDrop(tvParameters, tvParameters.SelectedValue, DragDropEffects.Move);

                        if ((finalDropEffect == DragDropEffects.Move) && (null != VariablesManager.targetItem))
                        {
                            StringPath stringPath = new StringPath();
                            stringPath.parentPath = subFunctions.GetPathFromExplorer(VariablesManager.targetItem);
                            stringPath.subPath = VariablesManager.draggedItem.Header.ToString();
                            foreach (StringPath str in VariablesManager.strBranchsExplorer)
                                if ((stringPath.parentPath == str.parentPath) && (stringPath.subPath == str.subPath))
                                {
                                    MessageBox.Show(str.subPath + " exists", "My App", MessageBoxButton.OK, MessageBoxImage.Information);
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
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void tvParameter_MouseRight(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if (null != item)
                item.IsSelected = true;
        }


        private void NewParameterMenu_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item_parameter = new TreeViewItem();
            WindowNewParameter inputDialog = new WindowNewParameter();
            if (true == inputDialog.ShowDialog())
            {
                item_parameter.Header = inputDialog.Answer;
                VariablesManager.parameters.Add(item_parameter.Header.ToString());
                treeNodeParameter.Items.Add(item_parameter);
                if (treeNodeParameter.Items[0] is string)
                    treeNodeParameter.Items.RemoveAt(0);
            }
        }

        private void DeleteParameterMenu_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem item1 = (TreeViewItem)tvParameters.SelectedItem;
            if ((null == item1) || (treeNodeParameter.Items[0] is string))
                return;
            foreach (TreeViewItem argv in treeNodeParameter.Items)
            {
                if (argv.Header == item1.Header)
                    VariablesManager.parameters.Remove(item1.Header.ToString());
            }
            treeNodeParameter.Items.Remove(item1);
            if (1 > treeNodeParameter.Items.Count)
                treeNodeParameter.Items.Add("Right click to add parameters ");
        }
    }
}
