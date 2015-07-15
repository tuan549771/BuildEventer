using PostBuildCopy.Classes;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Parameter.xaml
    /// </summary>
    public partial class TreeParameter : UserControl
    {
        #region Private members

        private string m_StrRoot = "Parameters";
        private string m_Suggestion = "Right Click to add parameters\nand you may drag them\ninto Explorer";
        private PathTreeNodeData m_Root; 

        #endregion

        #region Constructor

        public TreeParameter()
        {
            InitializeComponent();
            InitializeData();
        }

        #endregion

        #region Method

        private void InitializeData()
        {
            m_Root = new PathTreeNodeData(m_StrRoot);
            treeParameter.SetDataInput(m_StrRoot, m_Suggestion, m_Root);
        }

        public void SetDataFromXmlData(PathTreeNodeData iRoot)
        {
            treeParameter.SetDataRoot(iRoot);
        }

        #endregion
    }
}


