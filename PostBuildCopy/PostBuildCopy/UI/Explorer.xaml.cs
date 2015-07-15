
using PostBuildCopy.Classes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Explorer.xaml
    /// </summary>
    /// 
    public partial class Explorer : UserControl
    {
        #region On delegate get root node

        public delegate void SetRootTreeDelegate(PathTreeNodeData iRootNode);
        public SetRootTreeDelegate SetRootTree;

        #endregion

        #region Private member

        private List<PathTreeNodeData> m_Root = new List<PathTreeNodeData>();
        #endregion

        #region Constructor and Methods

        public Explorer()
        {
            InitializeComponent();
            InitializeData();
            InitializeEvent();
        }

        public void InitializeData()
        {
            treeViewExplorer.SetData(ExplorerModel.GetTreeNodeData());
        }

        private void InitializeEvent()
        {
            treeViewExplorer.GetChildren = GetTreeNodeChildren;
            treeViewExplorer.OnNodeDrop += treeView_OnNodeDrop;
            treeViewExplorer.OnPathRefresh += treeViewExplorer_OnPathRefresh;
            treeViewExplorer.OnSetPropertyNodeDrop += treeViewExplorer_OnSetPropertyNodeDrop;
        }

        private PathTreeNodeData treeViewExplorer_OnSetPropertyNodeDrop(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = true;
            string strNameNode = string.Copy(iNode.Path);
            PathTreeNodeData node = new PathTreeNodeData(strNameNode);
            node.FontWeightBinding = FontWeights.Bold;
            return node;
        }

        private void GetTreeNodeChildren(PathTreeNodeData iNode)
        {
            string fullPath = iNode.GetFullPath(iNode);
            ExplorerModel.GetTreeNode(fullPath, iNode);
        }

        private void treeView_OnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget)
        {
            iNodeTarget.AddChildHasMessageExist(iNodeSource);
            BranchsExplorer.SetOneBranchsExplorer(new CouplePath(iNodeTarget.GetFullPath(iNodeTarget), iNodeSource.Path));
        }

        private void treeViewExplorer_OnPathRefresh()
        {
            InitializeData();
        }

        #endregion
    }
}
