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
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    public partial class PathListBox : UserControl
    {
        #region Constructor

        public PathListBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        #endregion

        #region Methods

        public void SetData(ObservableCollection<PathDataModel> iPaths)
        {
            lbPath.ItemsSource = iPaths;
        }

        private void DropPath(object sender, DragEventArgs e)
        {
            PathTreeNodeData objDrop = (PathTreeNodeData)e.Data.GetData(typeof(PathTreeNodeData));
            bool allowDrop = false;
            if (null != OnAllowDrop)
                allowDrop = OnAllowDrop(objDrop.ID);
            if (true == allowDrop)
            {
                string pathDrop = ExplorerModel.GetRelativePath(objDrop);
                if (null != OnPathDrop)
                    OnPathDrop(pathDrop);
                lbPath.SelectedIndex = lbPath.Items.Count - 1;
            }
        }

        private void SelectedPath(object sender, SelectionChangedEventArgs e)
        {
            if (null != OnSelectedPath)
                OnSelectedPath();
        }

        private void DeletePath(object sender, RoutedEventArgs e)
        {
            if (null != OnDeletePath)
                OnDeletePath();
        }

        #endregion

        #region Property

        public int ItemsCount
        {
            get { return lbPath.Items.Count; }
        }

        public int SelectedIndex
        {
            get { return lbPath.SelectedIndex; }
            set { lbPath.SelectedIndex = value; }
        }

        public IEnumerable ItemSource
        {
            set { lbPath.ItemsSource = value; }
        }

        #endregion

        #region Events

        public delegate void HandelPathDropDelegate(string iDroppedObject);
        public event HandelPathDropDelegate OnPathDrop;

        public delegate void HandelSelectedPathDelegate();
        public event HandelSelectedPathDelegate OnSelectedPath;

        public delegate void HandleDeletePathDelegate();
        public event HandleDeletePathDelegate OnDeletePath;

        public delegate bool HandelAllowDrop(int id);
        public event HandelAllowDrop OnAllowDrop;

        #endregion

        #region DependencyProperty

        public static DependencyProperty AllowDeletePathProperty =
            DependencyProperty.RegisterAttached("AllowDeletePathListBox", typeof(Boolean), typeof(PathTreeView),
            new FrameworkPropertyMetadata(false));

        public Boolean AllowDeletePathListBox
        {
            get { return (Boolean)GetValue(AllowDeletePathProperty); }
            set { SetValue(AllowDeletePathProperty, value); }
        }

        #endregion
    }
}
