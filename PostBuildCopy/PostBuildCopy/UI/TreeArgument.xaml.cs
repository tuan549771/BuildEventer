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
using System.Windows.Controls;
using System.Windows.Media;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for TreeArgument.xaml
    /// </summary>
    public partial class TreeArgument : UserControl
    {
        #region Private members and Event

        private string m_StrRoot = string.Empty;
        private string m_Suggestion = string.Empty;
        private PathTreeNodeData m_Root = new PathTreeNodeData();

        public delegate string HandleGetArgurmentNameSuggestion();
        public event HandleGetArgurmentNameSuggestion OnGetArgumentSuggestion;

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
            m_StrRoot = iStrRoot;
            m_Suggestion = iSuggestion;
            m_Root = iRoot;
            PathTreeNodeData suggestionNode = new PathTreeNodeData(m_Suggestion);
            m_Root.AddChild(suggestionNode, false);
            m_Root.IsExpanded = true;
            treeArgument.SetData(m_Root);
        }

        // Initialize events
        private void InitializeEvent()
        {
            treeArgument.OnPathCreate += TreeArgument_OnPathCreate;
            treeArgument.OnPathDelete += TreeArgument_OnPathDelete;
            treeArgument.OnPropertyNodeDrag += TreeArgument_OnPropertyNodeDrag;
            treeArgument.OnGetArgumentNameDelegate +=treeArgument_OnGetSuggestionNewItemDelegate;
        }

        private string treeArgument_OnGetSuggestionNewItemDelegate()
        {
            if (null != OnGetArgumentSuggestion)
                return OnGetArgumentSuggestion();
            return string.Empty;
        }

        // Reset data when load xml 
        public void SetDataRoot(PathTreeNodeData iRoot)
        {
            m_Root = iRoot;
            m_Root.IsExpanded = true;
            treeArgument.SetData(m_Root);
        }

        // Get data will return a root noot
        public PathTreeNodeData GetData()
        {
            return m_Root;
        }

        // When drag a node in treeview
        // We set property for a node dropped
        private PathTreeNodeData TreeArgument_OnPropertyNodeDrag(PathTreeNodeData iNode)
        {
            PathTreeNodeData node = new PathTreeNodeData();
            node.Path = string.Copy(iNode.Path);
            //node.AllowDropNode = false;
            node.ForegroundBinding = Brushes.Magenta;
            return node;
        }

        // Processing create a node
        private void TreeArgument_OnPathCreate(PathTreeNodeData iNodeParent, string iPathChildNode)
        {
            // We will add node into the root node
            // Thus, iNodeParent no use here
            // We will use the root node
            PathTreeNodeData node = new PathTreeNodeData(iPathChildNode);
            m_Root.AddChild(node, true);
            if (true == m_Root.ContainsChildPath(m_Suggestion, false))
                TreeArgument_OnPathDelete(m_Root.Children[0]);
        }

        // Processing delete a node
        private void TreeArgument_OnPathDelete(PathTreeNodeData iNode)
        {
            PathTreeNodeData parent = iNode.Parent;
            parent.DeleteChild(iNode);
            // Add suggestion if no have suggestion node
            if (false == parent.HasChildren())
            {
                PathTreeNodeData node = new PathTreeNodeData(m_Suggestion);
                m_Root.AddChild(node, true);
            }
        }

        #endregion
    }
}
