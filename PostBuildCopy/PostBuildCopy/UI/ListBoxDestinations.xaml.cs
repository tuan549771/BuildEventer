/*
<License>
Copyright 2015 Virtium Technology
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http ://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
</License>
*/

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
        private enum eObj { TreeExplorer = 1 };

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
            lbDestination.OnAllowDrop +=lbDestination_OnAllowDrop;
        }

        private bool lbDestination_OnAllowDrop(int id)
        {
            if ((int)eObj.TreeExplorer == id)
                return true;
            return false;
        }

        private void ListBoxDestination_OnPathDrop(string iStrPath)
        {
            if (null != iStrPath)
            {
                PathDataModel PathData = new PathDataModel(iStrPath);
                if (true == IsFile(iStrPath))
                    return;
                ActionManager.Actions.Add(new CopySourcesToDestination(PathData));
                lbDestination.ItemSource = ActionManager.GetListDestinationOfActionManager();
            }
        }

        private void ListBoxDestination_OnDeletePath()
        {
            if (-1 != s_IndexDestination)
            {
                ActionManager.Actions.RemoveAt(s_IndexDestination);
                lbDestination.ItemSource = ActionManager.GetListDestinationOfActionManager();
                lbDestination.SelectedIndex = lbDestination.ItemsCount - 1;
            }
        }

        public bool IsFile(string iRelativePath)
        {
            if (File.Exists(iRelativePath))
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
