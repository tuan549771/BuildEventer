
using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.IO;
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
            treeViewExplorer.SetData(ExplorerModel.GetTreeNodeData());
            treeViewExplorer.GetChildren = GetTreeNodeChildren;
            treeViewExplorer.OnNodeDrop += treeView_OnNodeDrop;
        }

        private void GetTreeNodeChildren(PathTreeNodeData iNode)
        {
            string cd = iNode.GetFullPath(iNode);
            ExplorerModel.GetTreeNode(cd, iNode);
        }

        private void treeView_OnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget)
        {
            if (false == iNodeTarget.HasPathChild(iNodeSource.Path))
                iNodeTarget.AddChild(iNodeSource);
        }

    }
}
