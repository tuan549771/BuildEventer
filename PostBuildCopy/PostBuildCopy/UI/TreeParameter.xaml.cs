using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Parameter.xaml
    /// </summary>
    public partial class TreeParameter : UserControl
    {
        private string strRoot = "Parameters";
        private string suggestion = "Right Click to add parameters\nand you may drag them\ninto Explorer";
        private static PathTreeNodeData root;
        public TreeParameter()
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
            UCParameter.SetData(root);
        }

        public void SetDataRoot(PathTreeNodeData iRoot)
        {
            root = iRoot;
            root.IsExpanded = true;
            UCParameter.SetData(root);
        }

        public static PathTreeNodeData GetData()
        {
            return root;
        }

        private void InitializeEvent()
        {
            UCParameter.OnPathCreate += UCParameter_OnPathCreate;
            UCParameter.OnPathDelete += UCParameter_OnPathDelete;
            UCParameter.OnSetAllowNodeDrop+=UCParameter_OnSetAllowNodeDrop;
        }

        private PathTreeNodeData UCParameter_OnSetAllowNodeDrop(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = false;
            return iNode;
        }

        private void UCParameter_OnPathCreate(PathTreeNodeData iNodeParent, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Parameters
            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            root.AddChild(node);
            if (true == root.ContainsChildPath(suggestion))
                UCParameter_OnPathDelete(root.Children[0]);
        }

        private void UCParameter_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = parent = iNode.Parent;
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


