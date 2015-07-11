using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Parameter.xaml
    /// </summary>
    public partial class Parameter : UserControl
    {
        private PathTreeNodeData root = new PathTreeNodeData();
        public string suggest = "Right Click to add parameters\nand you may drag them\ninto Explorer";
        public Parameter()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            root = ParameterModel.GetTreeNodeData();
            UCParameter.SetData(root);
            UCParameter.OnPathCreate += UCParameter_OnPathCreate;
            UCParameter.OnPathDelete += UCParameter_OnPathDelete;
        }

        private void UCParameter_OnPathCreate(PathTreeNodeData iNodeParent, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Parameters
            PathTreeNodeData node = new PathTreeNodeData() { Path = iPathChildNode };
            root.AddChild(node);
            if (true == root.PathChildNodeExist(suggest))
                UCParameter_OnPathDelete(root.Children[0]);
        }

        private void UCParameter_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            if (null != iNode.Parent)
                parent.Children.Remove(iNode);
            // Add suggest if no have
            //if (0 == parent.Children.Count)
                //UCParameter_OnPathCreate(m_Root[0], ParameterModel.suggest);
        }
    }
}


