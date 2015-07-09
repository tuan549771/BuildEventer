using PostBuildCopy.Classes;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : UserControl
    {
        private ObservableCollection<PathTreeNodeData> m_Root = new ObservableCollection<PathTreeNodeData>();
        public Filter()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            m_Root.Add(FilterModel.GetTreeNodeData());
            UCFilter.treeView.ItemsSource = m_Root;
            UCFilter.OnPathCreate += UCFilter_OnPathCreate;
            UCFilter.OnPathDelete += UCFilter_OnPathDelete;
        }

        private void UCFilter_OnPathCreate(PathTreeNodeData iNode, string iPath)
        {
            Subfunction.CreatePath(iNode, m_Root[0], iPath, FilterModel.suggest);
        }

        private void UCFilter_OnPathDelete(PathTreeNodeData iNode)
        {
            Subfunction.DeletePath(iNode, FilterModel.suggest);
        }
    }
}
