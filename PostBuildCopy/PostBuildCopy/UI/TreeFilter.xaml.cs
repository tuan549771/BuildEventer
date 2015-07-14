using PostBuildCopy.Classes;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    public partial class Filter : UserControl
    {
        #region Private members

        private string strRoot = "Filters";
        private string suggestion = "Right Click to add Filters\nand you may drag them\ninto Explorer";
        private static PathTreeNodeData root;

        #endregion

        #region Methods and Constructor

        // Constructor Filter
        public Filter()
        {
            InitializeComponent();
            InitializeData();
            InitializeEvent();
        }

        // Initialize events
        private void InitializeEvent()
        {
            treeFilter.OnPathCreate += UCFilter_OnPathCreate;
            treeFilter.OnPathDelete += UCFilter_OnPathDelete;
            treeFilter.OnSetAllowNodeDrop += UCFilter_OnSetAllowNodeDrop;
        }

        // Initialize data for tree filter
        private void InitializeData()
        {
            root = new PathTreeNodeData(strRoot);
            PathTreeNodeData suggestionNode = new PathTreeNodeData(suggestion);
            root.AddChildHasMessageExist(suggestionNode);
            root.IsExpanded = true;
            treeFilter.SetData(root);
        }

        // Reset data when load xml
        public void SetDataRoot(PathTreeNodeData iRoot)
        {
            root = iRoot;
            root.IsExpanded = true;
            treeFilter.SetData(root);
        }

        // Get data will return a root noot
        public static PathTreeNodeData GetData()
        {
            return root;
        }

        // When drag a node in treeview
        // We set property allow a node dropped or no
        private PathTreeNodeData UCFilter_OnSetAllowNodeDrop(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = false;
            return iNode;
        }

        // Processing create a node
        private void UCFilter_OnPathCreate(PathTreeNodeData iNode, string iPathChildNode)
        {
            // We will add node into the root node
            // and not add any node others
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Filters

            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            root.AddChildHasMessageExist(node);
            if (true == root.ContainsChildPath(suggestion))
                UCFilter_OnPathDelete(root.Children[0]);
        }

        // Processing delete a node
        private void UCFilter_OnPathDelete(PathTreeNodeData iNode)
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
