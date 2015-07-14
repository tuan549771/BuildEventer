using PostBuildCopy.Classes;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    public delegate void delegateLoadItemListBox(int indexItem);

    public partial class Sources : UserControl
    {
        # region Constructor

        public Sources()
        {
            InitializeComponent();
            InitializeEvent();
        }

        #endregion

        #region Property

        public int SelectedIndex
        {
            get { return lbSources.SelectedIndex; }
            set { lbSources.SelectedIndex = value; }
        }

        #endregion

        #region Methods

        public void InitializeEvent()
        {
            lbSources.OnPathDrop += ListBoxSource_OnPathDrop;
            lbSources.OnDeletePath += ListBoxSource_OnDeletePath;
        }

        private void ListBoxSource_OnPathDrop(PathTreeNodeData iNodeDropped)
        {
            if (iNodeDropped != null)
            {
                if (false == CheckDropSource(Destinations.s_IndexDestination))
                    return;
                string absolutePath = iNodeDropped.GetFullPath(iNodeDropped);
                string relativePath = iNodeDropped.GetRelativePath(iNodeDropped);
                PathDataModel PathData = new PathDataModel(relativePath);
                (ActionManager.Actions[Destinations.s_IndexDestination] as CopySourcesToDestination).Sources.Add(PathData);
                lbSources.ItemSource = (ActionManager.Actions[Destinations.s_IndexDestination] as CopySourcesToDestination).Sources;
            }
        }

        private void ListBoxSource_OnDeletePath()
        {
            int indexSource = lbSources.SelectedIndex;
            if (Destinations.s_IndexDestination != -1 && indexSource != -1)
                (ActionManager.Actions[Destinations.s_IndexDestination] as CopySourcesToDestination).Sources.RemoveAt(indexSource);
        }

        public bool CheckDropSource(int indexDest)
        {
            if (-1 == indexDest)
            {
                MessageBox.Show("First of all, You must choice a destination.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        public void LoadListBoxItem(int index, delegateLoadItemListBox item)
        {
            item(index);
        }

        public void LoadListBoxItemSources1(int index)
        {
            if (-1 != index)
                lbSources.ItemSource = (ActionManager.Actions[Destinations.s_IndexDestination] as CopySourcesToDestination).Sources;
            else
                lbSources.ItemSource = null;
        }

        #endregion
    }
}
