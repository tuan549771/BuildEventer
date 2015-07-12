using PostBuildCopy.Classes;
using PostBuildCopy.Widowns;
using System;
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
        private PathTreeNodeData m_NodeSeleted;
        public ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();

        // On delegate Get Node Children
        public delegate void GetChildrenDelegate(PathTreeNodeData iNode);
        public GetChildrenDelegate GetChildren;

        // On Node Drop
        public delegate void HandleOnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget);
        public event HandleOnNodeDrop OnNodeDrop;

        // On Path Create
        public delegate void HandleOnPathCreate(PathTreeNodeData iNode, string iPath);
        public event HandleOnPathCreate OnPathCreate;

        // On Path Delete
        public delegate void HandleOnPathDelete(PathTreeNodeData iNode);
        public event HandleOnPathDelete OnPathDelete;

        // Constructor
        public PathTreeView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        // Set Data
        public void SetData(PathTreeNodeData iNodeRoot)
        {
            if (0 == m_Root.Count)
            {
                m_Root.Add(iNodeRoot);
            }
            else
            {
                m_Root[0] = iNodeRoot;
            }
            treeView.ItemsSource = m_Root;
            
            //foreach (var item in treeView.Items)
            //{
            //    DependencyObject dObject = treeView.ItemContainerGenerator.ContainerFromItem(item);
            //    CollapseTreeviewItems(((TreeViewItem)dObject));
            //}
        }

        //private void CollapseTreeviewItems(TreeViewItem Item)
        //{
        //    Item.IsExpanded = false;

        //    foreach (var item in Item.Items)
        //    {
        //        DependencyObject dObject = treeView.ItemContainerGenerator.ContainerFromItem(item);

        //        if (dObject != null)
        //        {
        //            ((TreeViewItem)dObject).IsExpanded = false;

        //            if (((TreeViewItem)dObject).HasItems)
        //            {
        //                CollapseTreeviewItems(((TreeViewItem)dObject));
        //            }
        //        }
        //    }
        //}


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
                PathTreeNodeData sourceNode = (PathTreeNodeData)e.Data.GetData(typeof(PathTreeNodeData));
                PathTreeNodeData targetNode = (PathTreeNodeData)container.Header;
                if (null != OnNodeDrop)
                    OnNodeDrop(sourceNode, targetNode);
            }
        }

        // Used to selete a node via the right mouse  
        private void MouseRight(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);
            m_NodeSeleted = (PathTreeNodeData)item.Header;
            if (null != item)
                item.IsSelected = true;
        }

        // Show a message box to get a new path
        // Get PathTreeNodeData that selected in tree
        // Raise the OnPathCreate(PathTreeNodeData, a new path)
        private void NewItem(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            InputPathDialog inputDialog = new InputPathDialog();
            if ((true == inputDialog.ShowDialog()) && (null != OnPathCreate))
            {
                path = inputDialog.Path;
                OnPathCreate(m_NodeSeleted, path);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            InputPathDialog inputDialog = new InputPathDialog();
            if ((null != OnPathDelete) && (null != m_NodeSeleted))
            {
                OnPathDelete(m_NodeSeleted);
            }
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


        #region Dependency property

        public static readonly DependencyProperty AllowCreateNewPathProperty =
            DependencyProperty.RegisterAttached("AllowCreateNewPath", typeof(Boolean), typeof(PathTreeView),
            new FrameworkPropertyMetadata(false));

        public Boolean AllowCreateNewPath
        {
            get { return (Boolean)GetValue(AllowCreateNewPathProperty); }
            set { SetValue(AllowCreateNewPathProperty, value); }
        }


        public static readonly DependencyProperty AllowDeletePathProperty =
            DependencyProperty.RegisterAttached("AllowDeletePath", typeof(Boolean), typeof(PathTreeView),
            new FrameworkPropertyMetadata(false));

        public Boolean AllowDeletePath
        {
            get { return (Boolean)GetValue(AllowDeletePathProperty); }
            set { SetValue(AllowDeletePathProperty, value); }
        }

        public static readonly DependencyProperty AllowDropTreeProperty =
             DependencyProperty.RegisterAttached("AllowDropTree", typeof(Boolean), typeof(PathTreeView),
             new FrameworkPropertyMetadata(false));

        public Boolean AllowDropTree
        {
            get { return (Boolean)GetValue(AllowDropTreeProperty); }
            set { SetValue(AllowDropTreeProperty, value); }
        }


        //public static readonly DependencyProperty AllowExpandedProperty =
        //     DependencyProperty.RegisterAttached("AllowExpanded", typeof(Boolean), typeof(PathTreeView),
        //     new FrameworkPropertyMetadata(false));

        //public Boolean AllowExpanded
        //{
        //    get { return (Boolean)GetValue(AllowExpandedProperty); }
        //    set { SetValue(AllowExpandedProperty, value); }
        //}
        #endregion
    }
}
