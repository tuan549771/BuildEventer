
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
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();
        public Explorer()
        {
            InitializeComponent();
            m_Root.Add(ExplorerModel.GetTreeNodeData());
            UCExplorer.treeView.ItemsSource = m_Root;
            UCExplorer.GetChildren = GetTreeNodeChildren;
            UCExplorer.OnNodeDrop += treeView_OnNodeDrop;
        }

        void treeView_OnNodeDrop(PathTreeNodeData nodeSource, PathTreeNodeData nodeTarget)
        {
            PathTreeNodeData node = new PathTreeNodeData() { Path = nodeSource.Path, Parent = nodeTarget };
            nodeTarget.Children.Add(node);
        }


        private void GetTreeNodeChildren(PathTreeNodeData iNode)
        {
            iNode.Children.Clear();
            string cd = iNode.GetFullPath(iNode);
            ExplorerModel.GetTreeNode(cd, iNode);
        }
    }
}
