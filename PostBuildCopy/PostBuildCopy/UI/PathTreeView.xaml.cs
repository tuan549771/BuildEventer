using PostBuildCopy.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for PathTreeView.xaml
    /// </summary>
    public partial class PathTreeView : UserControl
    {
        ObservableCollection<PathTreeNodeData> root = new ObservableCollection<PathTreeNodeData>();

        //// On Node Drag
        //public delegate string HandleOnNodeDrag(PathTreeNodeData iNode);
        //public event HandleOnNodeDrag OnNodeDrag;

        // On Node Drop
        public delegate void HandleOnNodeDrop(PathTreeNodeData nodeSource, PathTreeNodeData nodeTarget);
        public event HandleOnNodeDrop OnNodeDrop;

        //// On Path Create
        //public delegate void HandleOnPathCreate(PathTreeNodeData iNode, string iPath);
        //public event HandleOnPathCreate OnPathCreate;

        private void TreeExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            PathTreeNodeData pathNode = (PathTreeNodeData)item.Tag;
            if (0 < item.Items.Count && item.Items[0] is string)
            {
                item.Items.RemoveAt(0);
                ObservableCollection<PathTreeNodeData> children = pathNode.Children;
                foreach (PathTreeNodeData child in children)
                {
                    item.Items.Add(CreateTreeItem(child));
                }
            }
        }

        private void TreeMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            TreeViewItem item = e.Source as TreeViewItem;
            if (null == item)
                return;

            if ((null != item) && (e.LeftButton == MouseButtonState.Pressed))
            {
                DragDrop.DoDragDrop(treeView, item, DragDropEffects.Copy);
            }
        }

        private void TreeDrop(object sender, DragEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.Data.GetData(typeof(TreeViewItem));
            TreeViewItem itemTarget = GetNearestContainer(e.OriginalSource as UIElement);
            PathTreeNodeData sourceDrap = (PathTreeNodeData)item.Tag;
            PathTreeNodeData tagetDrop = (PathTreeNodeData)itemTarget.Tag;
            // chung ta co the add item drop tai day va khong can raise event nhu thang expand
            // 
            //if (null != OnNodeDrop)
            //    OnNodeDrop(sourceDrap, tagetDrop);//
        }

        public TreeViewItem GetNearestContainer(UIElement element)
        {
            TreeViewItem container = element as TreeViewItem;
            while ((null == container) && (null != element))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }


        private void NewItem(object sender, RoutedEventArgs e)
        {
            //if (null != OnPathCreate)
            //    OnPathCreate();
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }

        // constructor
        public PathTreeView()
        {
            InitializeComponent();
            root.Add(TreeModel.GetTreeNodeData());
            SetData(root);
        }

        private void SetData(ObservableCollection<PathTreeNodeData> root)
        {
            //treeView.ItemsSource = root;
            //TreeViewItem item = new TreeViewItem();

            //item.Items.Add(root[0].Path);
            //item.Items.Add("Loading...");
            treeView.Items.Add(CreateTreeItem(root[0]));
        }

        public TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            item.Tag = o;
            item.Header = (o as PathTreeNodeData).Path;
            item.Items.Add("Loading...");
            return item;
        }
    }
}
