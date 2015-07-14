using PostBuildCopy.Classes;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Destinations.xaml
    /// </summary>
    public partial class Destinations : UserControl
    {
        public Destinations()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            UCDestination.OnPathDrop += UCDestination_OnPathDrop;
            UCDestination.OnDeletePath += UCDestination_OnDeletePath;
            UCDestination.OnSelectedPath += UCDestination_OnSelectedPath;
        }

        private void UCDestination_OnSelectedPath()
        {
            indexDest = UCDestination.SelectedIndex;
            LoadItemListBox(indexDest, itemTemp);
        }

        public int SelectedIndex
        {
            get { return UCDestination.SelectedIndex; }
            set { UCDestination.SelectedIndex = value; }
        }

        public IEnumerable ItemSource
        {
            set { UCDestination.ItemSource = value; }
        }

        // loading listbox using delegate
        public void LoadItemListBox(int index, delegateLoadItemListBox item)
        {
            if (null != item)
            {
                item(index);
                itemTemp = item;
            }
        }

        private void UCDestination_OnPathDrop(PathTreeNodeData iNodeDropped)
        {
            if (iNodeDropped != null)
            {
                string absolutePath = iNodeDropped.GetFullPath(iNodeDropped);
                string relativePath = iNodeDropped.GetRelativePath(iNodeDropped);
                PathDataModel PathData = new PathDataModel(relativePath);
                if (true == CheckFileExist(absolutePath))
                    return;
                ActionManager.actions.Add(new CopySourcesToDestination(PathData));
                UCDestination.ItemSource = ActionManager.GetListDestinationOfActionManager();
            }
        }

        private void UCDestination_OnDeletePath()
        {
            if (indexDest != -1)
            {
                ActionManager.actions.RemoveAt(indexDest);
                UCDestination.ItemSource = ActionManager.GetListDestinationOfActionManager();
                UCDestination.SelectedIndex = UCDestination.ItemsCount - 1;
            }
        }

        public bool CheckFileExist(string iAbsolutePath)
        {
            if (File.Exists(iAbsolutePath))
            {
                MessageBox.Show("You should be choice a folder for destination.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            return false;
        }

        public static int indexDest = -1;
        public delegateLoadItemListBox itemTemp;
    }
}
