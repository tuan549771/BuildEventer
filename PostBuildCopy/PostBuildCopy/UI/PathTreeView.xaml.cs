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
        #region Constructor

        public PathTreeView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Methods

        // Set data for treeview via data input iNodeRoot
        public void SetData(PathTreeNodeData iNodeRoot)
        {
            m_Root.Add(iNodeRoot);
            if (1 < m_Root.Count)
                m_Root.RemoveAt(0);
            treeView.ItemsSource = m_Root;
        }

        // Get the PathTreeNodeData object from node that expanded
        // With each child, call delegate GetChildren
        private void TreeExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            PathTreeNodeData data = (PathTreeNodeData)item.Header;
            if (null != GetChildren && null != data)
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
                Point currentPosition = e.GetPosition(treeView);
                if ((10.0 < Math.Abs(currentPosition.X - m_FirstMouseDown.X)) || (10.0 < Math.Abs(currentPosition.Y - m_FirstMouseDown.Y)))
                {
                    PathTreeNodeData data = (PathTreeNodeData)treeView.SelectedItem;
                    if (null != OnSetAllowNodeDrop)
                        data = OnSetAllowNodeDrop(data);
                    if ((null != data) && (null != data.Parent))
                    {
                        DragDrop.DoDragDrop(treeView, data, DragDropEffects.Copy);
                    }
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
                if (null != OnSetPropertyForSourceNode)
                    sourceNode = OnSetPropertyForSourceNode(sourceNode);
                PathTreeNodeData targetNode = (PathTreeNodeData)container.Header;
                if (null != OnNodeDrop)
                    OnNodeDrop(sourceNode, targetNode);
            }
        }

        // Used to selete a node via the right mouse  
        private void TreeMouseRight(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);
            m_NodeSeleted = (PathTreeNodeData)item.Header;
            if (null != item)
                item.IsSelected = true;
        }

        // Show a message box to get a new path
        // Get PathTreeNodeData that selected in tree
        // Raise the OnPathCreate(PathTreeNodeData, a new path)
        private void TreeNewItem(object sender, RoutedEventArgs e)
        {
            string path = string.Empty;
            InputPathDialog inputDialog = new InputPathDialog();
            if ((true == inputDialog.ShowDialog()) && (null != OnPathCreate))
            {
                path = inputDialog.Path;
                OnPathCreate(m_NodeSeleted, path);
            }
        }

        private void TreeDeleteItem(object sender, RoutedEventArgs e)
        {
            InputPathDialog inputDialog = new InputPathDialog();
            if ((null != OnPathDelete) && (null != m_NodeSeleted))
            {
                OnPathDelete(m_NodeSeleted);
            }
        }

        private void TreeRefreshItem(object sender, RoutedEventArgs e)
        {
            if (null != OnPathRefresh)
                OnPathRefresh();
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

        private void TreeMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                m_FirstMouseDown = e.GetPosition(treeView);
        }

        #endregion

        #region Private members

        private PathTreeNodeData m_NodeSeleted;
        private Point m_FirstMouseDown;
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();

        #endregion

        #region Events

        // On delegate get child nodes of a node
        public delegate void GetChildrenDelegate(PathTreeNodeData iNode);
        public GetChildrenDelegate GetChildren;

        // On allow drop a node
        public delegate PathTreeNodeData HandleOnSetAllowNodeDrop(PathTreeNodeData iNode);
        public event HandleOnSetAllowNodeDrop OnSetAllowNodeDrop;

        // On property drop a node
        public delegate PathTreeNodeData HandleOnSetPropertyNodeDrop(PathTreeNodeData iNode);
        public event HandleOnSetPropertyNodeDrop OnSetPropertyForSourceNode;

        // On drop a node
        public delegate void HandleOnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget);
        public event HandleOnNodeDrop OnNodeDrop;

        // On create a path node
        public delegate void HandleOnPathCreate(PathTreeNodeData iNode, string iPath);
        public event HandleOnPathCreate OnPathCreate;

        // On delete a path node
        public delegate void HandleOnPathDelete(PathTreeNodeData iNode);
        public event HandleOnPathDelete OnPathDelete;

        // On refresh tree
        public delegate void HandleOnPathRefresh();
        public event HandleOnPathRefresh OnPathRefresh;

        #endregion

        #region Dependency property for PathTreeView

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

        public static readonly DependencyProperty AllowRefreshProperty =
            DependencyProperty.RegisterAttached("AllowRefresh", typeof(Boolean), typeof(PathTreeView),
            new FrameworkPropertyMetadata(false));

        public Boolean AllowRefresh
        {
            get { return (Boolean)GetValue(AllowRefreshProperty); }
            set { SetValue(AllowRefreshProperty, value); }
        }

        #endregion
    }
}
