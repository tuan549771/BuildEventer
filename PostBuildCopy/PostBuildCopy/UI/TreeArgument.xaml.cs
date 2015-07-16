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
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for TreeArgument.xaml
    /// </summary>
    public partial class TreeArgument : UserControl
    {
        #region Private members

        private string strRoot = string.Empty;
        private string suggestion = string.Empty;
        private PathTreeNodeData root = new PathTreeNodeData();
        
        #endregion
        
        #region Constructor
        
        public TreeArgument()
        {
            InitializeComponent();
            InitializeEvent();
        }

        #endregion

        #region Methods

        public void SetDataInput(string iStrRoot, string iSuggestion, PathTreeNodeData iRoot)
        {
            strRoot = iStrRoot;
            suggestion = iSuggestion;
            root = iRoot;
            PathTreeNodeData suggestionNode = new PathTreeNodeData(suggestion);
            root.AddChildNoMessageExist(suggestionNode);
            root.IsExpanded = true;
            treeArgument.SetData(root);
        }

        // Initialize events
        private void InitializeEvent()
        {
            treeArgument.OnPathCreate += TreeArgument_OnPathCreate;
            treeArgument.OnPathDelete += TreeArgument_OnPathDelete;
            treeArgument.OnSetAllowNodeDrop += TreeArgument_OnSetAllowNodeDrop;
        }

        // Reset data when load xml 
        public void SetDataRoot(PathTreeNodeData iRoot)
        {
            root = iRoot;
            root.IsExpanded = true;
            treeArgument.SetData(root);
        }

        // Get data will return a root noot
        public PathTreeNodeData GetData()
        {
            return root;
        }

        // When drag a node in treeview
        // We set property allow a node dropped or no
        private PathTreeNodeData TreeArgument_OnSetAllowNodeDrop(PathTreeNodeData iNode)
        {
            iNode.AllowDropNode = false;
            return iNode;
        }

        // Processing create a node
        private void TreeArgument_OnPathCreate(PathTreeNodeData iNodeParent, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node
            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            root.AddChildHasMessageExist(node);
            if (true == root.ContainsChildPath(suggestion))
                TreeArgument_OnPathDelete(root.Children[0]);
        }

        // Processing delete a node
        private void TreeArgument_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            parent.DeleteChild(iNode);
            // Add suggestion if no have suggestion node
            if (false == parent.HasChildren())
            {
                PathTreeNodeData node = new PathTreeNodeData(suggestion);
                root.AddChildHasMessageExist(node);
            }
        }

        #endregion
    }
}
