using PostBuildCopy.Classes;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    public partial class TreeFilter : UserControl
    {
        #region Private members

        private string m_StrRoot = "Filters";
        private string m_Suggestion = "Right Click to add filters\nand you may drag them\ninto Explorer";
        private PathTreeNodeData m_Root;

        #endregion

        #region Constructor

        public TreeFilter()
        {
            InitializeComponent();
            InitializeData();
        }

        #endregion

        #region Method

        private void InitializeData()
        {
            m_Root = new PathTreeNodeData(m_StrRoot);
            treeFilter.SetDataInput(m_StrRoot, m_Suggestion, m_Root);
        }

        public void SetDataFromXmlData(PathTreeNodeData iRoot)
        {
            treeFilter.SetDataRoot(iRoot);
        }

        // Get data will return a root noot
        public PathTreeNodeData GetData()
        {
            return treeFilter.GetData();
        } 
        #endregion
    }
}
