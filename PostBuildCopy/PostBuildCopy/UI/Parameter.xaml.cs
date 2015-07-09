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
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();
        public Parameter()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            m_Root.Add(ParameterModel.GetTreeNodeData());
            UCParameter.treeView.ItemsSource = m_Root;
            UCParameter.OnPathCreate += UCParameter_OnPathCreate;
            UCParameter.OnPathDelete += UCParameter_OnPathDelete;
        }

        private void UCParameter_OnPathCreate(PathTreeNodeData iNode, string iPath)
        {
            Subfunction.CreatePath(iNode, m_Root[0], iPath, ParameterModel.suggest);
        }

        private void UCParameter_OnPathDelete(PathTreeNodeData iNode)
        {
            Subfunction.DeletePath(iNode, ParameterModel.suggest);
        }
    }
}
