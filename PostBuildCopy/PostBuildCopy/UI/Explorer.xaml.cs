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
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for Explorer.xaml
    /// </summary>
    /// 
    public partial class Explorer : UserControl
    {
        #region On delegate get root node

        public delegate void SetRootTreeDelegate(PathTreeNodeData iRootNode);
        public SetRootTreeDelegate SetRootTree;

        #endregion

        #region Private member

        private List<PathTreeNodeData> m_Root = new List<PathTreeNodeData>();

        #endregion

        #region Constructor and Methods

        public Explorer()
        {
            InitializeComponent();
            InitializeData();
            InitializeEvent();
        }

        public void InitializeData()
        {
            treeViewExplorer.SetData(ExplorerModel.GetTreeNodeData());
        }

        private void InitializeEvent()
        {
            treeViewExplorer.GetChildren = GetTreeNodeChildren;
            treeViewExplorer.OnNodeDrop += treeView_OnNodeDrop;
            treeViewExplorer.OnPathRefresh += treeViewExplorer_OnPathRefresh;
            treeViewExplorer.OnSetPropertyForSourceNode += treeViewExplorer_OnSetPropertyForSourceNode;
        }

        private PathTreeNodeData treeViewExplorer_OnSetPropertyForSourceNode(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = true;
            string strNameNode = string.Copy(iNode.Path);
            PathTreeNodeData node = new PathTreeNodeData(strNameNode);
            node.ForegroundBinding = Brushes.Magenta;
            return node;
        }

        private void GetTreeNodeChildren(PathTreeNodeData iNode)
        {
            string fullPath = iNode.GetFullPath(iNode);
            ExplorerModel.GetChildNodes(iNode);
        }

        private void treeView_OnNodeDrop(PathTreeNodeData iNodeSource, PathTreeNodeData iNodeTarget)
        {
            if (true == iNodeSource.AllowDropNode)
            {
                iNodeTarget.AddChildHasMessageExist(iNodeSource);
                BranchsExplorer.SetOneBranchsExplorer(new CouplePath(iNodeTarget.GetFullPath(iNodeTarget), iNodeSource.Path));
            }
        }

        private void treeViewExplorer_OnPathRefresh()
        {
            InitializeData();
        }

        #endregion
    }
}
