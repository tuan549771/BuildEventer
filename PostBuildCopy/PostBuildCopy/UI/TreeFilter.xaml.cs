using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : UserControl
    {
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();
        private string strRoot = "Filters";
        private string suggestion = "Right Click to add Filters\nand you may drag them\ninto Explorer";
        private static PathTreeNodeData root;
        public Filter()
        {
            InitializeComponent();
            InitializeData();
            InitializeEvent();
        }

        private void InitializeData()
        {
            root = new PathTreeNodeData(strRoot);
            PathTreeNodeData suggestionNode = new PathTreeNodeData(suggestion);
            root.AddChild(suggestionNode);
            root.IsExpanded = true;
            UCFilter.SetData(root);
        }

        public void SetDataRoot(PathTreeNodeData iRoot)
        {
            root = iRoot;
            root.IsExpanded = true;
            UCFilter.SetData(root);
        }

        public static PathTreeNodeData GetData()
        {
            return root;
        }

        private void InitializeEvent()
        {
            UCFilter.OnPathCreate += UCFilter_OnPathCreate;
            UCFilter.OnPathDelete += UCFilter_OnPathDelete;
            UCFilter.OnSetAllowNodeDrop += UCFilter_OnSetAllowNodeDrop;
        }

        private PathTreeNodeData UCFilter_OnSetAllowNodeDrop(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = false;
            return iNode;
        }


        private void UCFilter_OnPathCreate(PathTreeNodeData iNode, string iPathChildNode)
        {
            // We will add node into the root node
            // and not add any node others
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Filters

            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            root.AddChild(node);

            if (true == root.ContainsChildPath(suggestion))
                UCFilter_OnPathDelete(root.Children[0]);
        }

        private void UCFilter_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            parent.DeleteChild(iNode);
            // Add suggestion if no have suggestion node
            if (false == parent.HasChildren())
            {
                PathTreeNodeData node = new PathTreeNodeData(suggestion);
                root.AddChild(node);
            }
        }
    }
}
