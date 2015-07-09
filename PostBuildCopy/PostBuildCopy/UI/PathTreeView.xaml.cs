using PostBuildCopy.Classes;
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
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();
        private PathTreeNodeData m_NewNodeTarget;

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
            m_Root.Add(TreeModel.GetTreeNodeData());
            treeView.ItemsSource = m_Root;
            this.DataContext = this;
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
            m_NewNodeTarget = (PathTreeNodeData)item.Header;
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
                OnPathCreate(m_NewNodeTarget, path);
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


        #region Dependency property

        public static readonly DependencyProperty
           IsEnabledProperty = DependencyProperty.Register(
               "IsEnabledNewPath", typeof(object), typeof(PathTreeView),
               new UIPropertyMetadata("False"));

        public object IsEnabledNewPath
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty
           AllowDropProperty = DependencyProperty.Register(
               "AllowDrop", typeof(object), typeof(PathTreeView),
               new UIPropertyMetadata("ABC"));

        public object AllowDrop
        {
            get { return (bool)GetValue(AllowDropProperty); }
            set { SetValue(AllowDropProperty, value); }
        }
        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            // do something when property changes
        }

        #endregion
    }
}
