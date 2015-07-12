using PostBuildCopy.Classes;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Sources.xaml
    /// </summary>
    /// 
    public delegate void delegateLoadItemListBox(int indexItem);
    public partial class Sources : UserControl
    {
        public Sources()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            UCSources.OnPathDrop += UCSources_OnPathDrop;
            UCSources.OnDeletePath += UCSources_OnDeletePath;
        }

        public void LoadListBoxItem(int index, delegateLoadItemListBox item)
        {
            item(index);
        }

        public void LoadListBoxItemSources1(int index)
        {
            if (-1 != index)
                UCSources.SetData((ActionManager.actions[Destinations.indexDest] as CopySourcesToDestination).Sources);
            else
                UCSources.SetData(null);
        }

        private void UCSources_OnPathDrop(PathTreeNodeData iNodeDropped)
        {
            if (iNodeDropped != null)
            {
                if (false == CheckDropSource(Destinations.indexDest))
                    return;
                string absolutePath = iNodeDropped.GetFullPath(iNodeDropped);
                string relativePath = iNodeDropped.GetRelativePath(iNodeDropped);
                PathDataModel PathData = new PathDataModel(relativePath);
                (ActionManager.actions[Destinations.indexDest] as CopySourcesToDestination).Sources.Add(PathData);
                UCSources.SetData((ActionManager.actions[Destinations.indexDest] as CopySourcesToDestination).Sources);
            }
        }

        private void UCSources_OnDeletePath()
        {
            int indexSource = UCSources.lbPath.SelectedIndex;
            if (Destinations.indexDest != -1 && indexSource != -1)// 0 <= (indexDest * indexSource)
                (ActionManager.actions[Destinations.indexDest] as CopySourcesToDestination).Sources.RemoveAt(indexSource);
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
    }
}
