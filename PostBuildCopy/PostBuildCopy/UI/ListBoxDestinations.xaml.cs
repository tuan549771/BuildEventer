using PostBuildCopy.Classes;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    public partial class Destinations : UserControl
    {

        public static int s_IndexDestination = -1;
        public delegateLoadItemListBox delegateLoadItem;

        #region Constructor

        public Destinations()
        {
            InitializeComponent();
            InitializeEvent();
        }

        #endregion

        #region Property

        public int SelectedIndex
        {
            get { return lbDestination.SelectedIndex; }
            set { lbDestination.SelectedIndex = value; }
        }

        public IEnumerable ItemSource
        {
            set { lbDestination.ItemSource = value; }
        }

        #endregion

        #region Methods

        public void InitializeEvent()
        {
            lbDestination.OnPathDrop += ListBoxDestination_OnPathDrop;
            lbDestination.OnDeletePath += ListBoxDestination_OnDeletePath;
            lbDestination.OnSelectedPath += ListBoxDestination_OnSelectedPath;
        }

        private void ListBoxDestination_OnPathDrop(PathTreeNodeData iNodeDropped)
        {
            if (iNodeDropped != null)
            {
                string absolutePath = iNodeDropped.GetFullPath(iNodeDropped);
                string relativePath = iNodeDropped.GetRelativePath(iNodeDropped);
                PathDataModel PathData = new PathDataModel(relativePath);
                if (true == CheckFileExist(absolutePath))
                    return;
                ActionManager.Actions.Add(new CopySourcesToDestination(PathData));
                lbDestination.ItemSource = ActionManager.GetListDestinationOfActionManager();
            }
        }

        private void ListBoxDestination_OnDeletePath()
        {
            if (s_IndexDestination != -1)
            {
                ActionManager.Actions.RemoveAt(s_IndexDestination);
                lbDestination.ItemSource = ActionManager.GetListDestinationOfActionManager();
                lbDestination.SelectedIndex = lbDestination.ItemsCount - 1;
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

        private void ListBoxDestination_OnSelectedPath()
        {
            s_IndexDestination = lbDestination.SelectedIndex;
            LoadItemListBox(s_IndexDestination, delegateLoadItem);
        }

        public void LoadItemListBox(int index, delegateLoadItemListBox item)
        {
            if (null != item)
            {
                item(index);
                delegateLoadItem = item;
            }
        }

        #endregion
    }
}
