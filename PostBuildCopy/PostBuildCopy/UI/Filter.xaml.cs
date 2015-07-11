using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : UserControl
    {
        private PathTreeNodeData root = new PathTreeNodeData();
        public string suggestion = "Right Click to add Filters\nand you may drag them\ninto Explorer";

        public Filter()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            root = FilterModel.GetTreeNodeData(suggestion);
            treeViewFilter.SetData(root);
            treeViewFilter.OnPathCreate += UCFilter_OnPathCreate;
            treeViewFilter.OnPathDelete += UCFilter_OnPathDelete;
        }

        private void UCFilter_OnPathCreate(PathTreeNodeData iNode, string iPathChildNode)
        {
            // We will add node into the root node
            // and not add any node others
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Filters

            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            root.AddChild(node);
            if (true == root.ContainsChild(suggestion))
                UCFilter_OnPathDelete(root.Children[0]);
        }

        private void UCFilter_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            if (null != iNode.Parent)
                parent.Children.Remove(iNode);
            // Add suggestion if no have
            if (0 == parent.Children.Count)
            {
                PathTreeNodeData node = new PathTreeNodeData(suggestion);
                root.AddChild(node);
            }
        }
    }
}
