
using PostBuildCopy.Classes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

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

        private List<PathTreeNodeData> m_Root = new List<PathTreeNodeData>();
        public Explorer()
        {
            InitializeComponent();
            Initialize();
            treeViewExplorer.GetChildren = GetTreeNodeChildren;
            treeViewExplorer.OnNodeDrop += treeView_OnNodeDrop;
            treeViewExplorer.OnPathRefresh +=treeViewExplorer_OnPathRefresh;
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

        private void treeViewExplorer_OnPathRefresh()
        {
            Initialize();
        }

    }
}
