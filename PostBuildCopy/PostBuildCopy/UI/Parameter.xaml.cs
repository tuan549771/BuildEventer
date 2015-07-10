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
        private PathTreeNodeData Root = new PathTreeNodeData();
        public Parameter()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            Root = ParameterModel.GetTreeNodeData();
            UCParameter.SetData(Root);
            UCParameter.OnPathCreate += UCParameter_OnPathCreate;
            UCParameter.OnPathDelete += UCParameter_OnPathDelete;
        }

        private void UCParameter_OnPathCreate(PathTreeNodeData iNodeParent, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node that path name is Parameters
            PathTreeNodeData node = new PathTreeNodeData() { Path = iPathChildNode };
            Root.AddChild(node);
            if (true == Root.HasPathChild(ParameterModel.suggest))
                UCParameter_OnPathDelete(Root.Children[0]);
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


