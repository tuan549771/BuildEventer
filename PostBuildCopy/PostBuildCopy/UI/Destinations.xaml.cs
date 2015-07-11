using PostBuildCopy.Classes;
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
            indexDest = UCDestination.SelectedIndexLB;
            LoadItemListBox(indexDest, itemTemp);
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
                //UCDestination.ItemSourceLB = ActionManager.GetListDestinationOfActionManager();
                UCDestination.SetData(ActionManager.GetListDestinationOfActionManager());
            }
        }

        private void UCDestination_OnDeletePath()
        {
            if (indexDest != -1)
            {
                ActionManager.actions.RemoveAt(indexDest);
                UCDestination.SetData(ActionManager.GetListDestinationOfActionManager());
                
                // I am trying to implement dependency SelectedIndex here
                UCDestination.lbPath.SelectedIndex = UCDestination.lbPath.Items.Count - 1;
                //UCDestination.SelectedIndexLB = UCDestination.lbPath.Items.Count - 1;
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
