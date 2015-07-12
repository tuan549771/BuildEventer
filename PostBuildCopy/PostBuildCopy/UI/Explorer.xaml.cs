
using PostBuildCopy.Classes;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Explorer.xaml
    /// </summary>
    public partial class Explorer : UserControl
    {
        // On delegate Get Node Children
        public delegate void SetRootTreeDelegate(PathTreeNodeData iRootNode);
        public SetRootTreeDelegate SetRootTree;

        public Explorer()
        {
            InitializeComponent();
            Initialize();
            treeViewExplorer.GetChildren = GetTreeNodeChildren;
            treeViewExplorer.OnNodeDrop += treeView_OnNodeDrop;
            
        }

        public void Initialize()
        {
            treeViewExplorer.SetData(ExplorerModel.GetTreeNodeData());
        }


        private void GetTreeNodeChildren(PathTreeNodeData iNode)
        {
            string cd = iNode.GetFullPath(iNode);
            ExplorerModel.GetTreeNode(cd, iNode);
        }

        private void treeView_OnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget)
        {
            iNodeTarget.AddChild(iNodeSource);
            BranchsExplorer.SetOneBranchsExplorer(new CouplePath(iNodeTarget.GetFullPath(iNodeTarget), iNodeSource.Path));
        }

    }
}
