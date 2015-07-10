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
        private PathTreeNodeData Root = new PathTreeNodeData();

        public Filter()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Root = FilterModel.GetTreeNodeData();
            treeViewFilter.SetData(Root);
            treeViewFilter.OnPathCreate += UCFilter_OnPathCreate;
            treeViewFilter.OnPathDelete += UCFilter_OnPathDelete;
        }

        private void UCFilter_OnPathCreate(PathTreeNodeData iNode, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Parameters
            PathTreeNodeData node = new PathTreeNodeData() { Path = iPathChildNode };
            Root.AddChild(node);
            if (true == Root.HasPathChild(FilterModel.suggest))
                UCFilter_OnPathDelete(Root.Children[0]);
        }

        private void UCFilter_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            if (null != iNode.Parent)
                parent.Children.Remove(iNode);
        }
    }
}
