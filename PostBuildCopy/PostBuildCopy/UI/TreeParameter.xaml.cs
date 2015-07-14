using PostBuildCopy.Classes;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Parameter.xaml
    /// </summary>
    public partial class TreeParameter : UserControl
    {
        #region Private members

        private string strRoot = "Parameters";
        private string suggestion = "Right Click to add parameters\nand you may drag them\ninto Explorer";
        private static PathTreeNodeData root;

        #endregion

        #region Methods and Constructor

        // Initialize events
        private void InitializeEvent()
        {
            treeParameter.OnPathCreate += TreeParameter_OnPathCreate;
            treeParameter.OnPathDelete += TreeParameter_OnPathDelete;
            treeParameter.OnSetAllowNodeDrop += TreeParameter_OnSetAllowNodeDrop;
        }

        // Constructor
        public TreeParameter()
        {
            InitializeComponent();
            InitializeData();
            InitializeEvent();
        }

        // Initialize data for tree parameter
        private void InitializeData()
        {
            root = new PathTreeNodeData(strRoot);
            PathTreeNodeData suggestionNode = new PathTreeNodeData(suggestion);
            root.AddChildHasMessageExist(suggestionNode);
            root.IsExpanded = true;
            treeParameter.SetData(root);
        }

        // Reset data when load xml 
        public void SetDataRoot(PathTreeNodeData iRoot)
        {
            root = iRoot;
            root.IsExpanded = true;
            treeParameter.SetData(root);
        }

        // Get data will return a root noot
        public static PathTreeNodeData GetData()
        {
            return root;
        }

        // When drag a node in treeview
        // We set property allow a node dropped or no
        private PathTreeNodeData TreeParameter_OnSetAllowNodeDrop(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = false;
            return iNode;
        }

        // Processing create a node
        private void TreeParameter_OnPathCreate(PathTreeNodeData iNodeParent, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Parameters
            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            root.AddChildHasMessageExist(node);
            if (true == root.ContainsChildPath(suggestion))
                TreeParameter_OnPathDelete(root.Children[0]);
        }

        // Processing delete a node
        private void TreeParameter_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            parent.DeleteChild(iNode);
            // Add suggestion if no have suggestion node
            if (false == parent.HasChildren())
            {
                PathTreeNodeData node = new PathTreeNodeData(suggestion);
                root.AddChildHasMessageExist(node);
            }
        }

        #endregion
    }
}


