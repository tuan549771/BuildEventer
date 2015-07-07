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
            if (null != iNodeDropped)
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
