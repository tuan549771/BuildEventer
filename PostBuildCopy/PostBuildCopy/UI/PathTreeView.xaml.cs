﻿using PostBuildCopy.Classes;
using PostBuildCopy.Widowns;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    public partial class PathTreeView : UserControl
    {
        // Member private
        private ObservableCollection<PathTreeNodeData> m_root = new ObservableCollection<PathTreeNodeData>();
        private PathTreeNodeData m_newNodeTarget;

        // On delegate Get Node Children
        public delegate void GetChildrenDelegate(PathTreeNodeData iNode);
        public GetChildrenDelegate GetChildren;

        // On Node Drop
        public delegate void HandleOnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget);
        public event HandleOnNodeDrop OnNodeDrop;

        // On Path Create
        public delegate void HandleOnPathCreate(PathTreeNodeData iNode, string iPath);
        public event HandleOnPathCreate OnPathCreate;

        // Constructor
        public PathTreeView()
        {
            InitializeComponent();
            m_root.Add(TreeModel.GetTreeNodeData());
            treeView.ItemsSource = m_root;
        }

        // Get the PathTreeNodeData object from node that expanded
        // With each child, call delegate GetChildren
        private void TreeExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            PathTreeNodeData data = (PathTreeNodeData)item.Header;
            if (null != GetChildren)
            {
                for (int i = 0; i < data.Children.Count; ++i)
                {
                    GetChildren(data.Children[i]);
                }
            }
        }

        // Get the PathTreeNodeData object from node that dragged
        // Raise the OnNodeDrag event with the PathTreeNodeData parameter
        private void TreeMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                PathTreeNodeData data = (PathTreeNodeData)treeView.SelectedItem;
                if (null != data)
                {
                    DragDrop.DoDragDrop(treeView, data, DragDropEffects.Copy);
                }
            }
        }

        // Raise the OnNodeDrag event with the PathTreeNodeData parameter that dragged
        private void TreeDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
            TreeViewItem container = GetNearestContainer(e.OriginalSource as UIElement);
            if (container != null)
            {
                PathTreeNodeData sourceDrap = (PathTreeNodeData)e.Data.GetData(typeof(PathTreeNodeData));
                PathTreeNodeData tagetDrop = (PathTreeNodeData)container.Header;
                if (null != OnNodeDrop)
                    OnNodeDrop(sourceDrap, tagetDrop);
            }
        }

        // Used to selete a node via the right mouse  
        private void MouseRight(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);
            m_newNodeTarget = (PathTreeNodeData)item.Header;
            if (null != item)
                item.IsSelected = true;
        }

        // Show a message box to get a new path
        // Get PathTreeNodeData that selected in tree
        // Raise the OnPathCreate(PathTreeNodeData, a new path)
        private void NewItem(object sender, RoutedEventArgs e)
        {
            string path = "#";
            InputPath inputDialog = new InputPath();
            if (null != inputDialog.ShowDialog())
            {
                path = inputDialog.Path;
            }
            if (null != OnPathCreate)
            {
                OnPathCreate(m_newNodeTarget, path);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }

        // Walk up the element tree to the nearest tree view item.
        private TreeViewItem GetNearestContainer(UIElement element)
        {
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }
    }
}
