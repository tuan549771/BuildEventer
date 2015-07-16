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
    /// Interaction logic for Parameter.xaml
    /// </summary>
    public partial class TreeParameter : UserControl
    {
        #region Private members

        private string m_StrRoot = "Parameters";
        private string m_Suggestion = "Right Click to add parameters\nand you may drag them\ninto Explorer";
        private PathTreeNodeData m_Root; 

        #endregion

        #region Constructor

        public TreeParameter()
        {
            InitializeComponent();
            InitializeData();
            InitializeEvent();
        }

        #endregion

        #region Method

        private void InitializeData()
        {
            m_Root = new PathTreeNodeData(m_StrRoot);
            treeParameter.SetDataInput(m_StrRoot, m_Suggestion, m_Root);
        }

        private void InitializeEvent()
        {
            treeParameter.OnGetArgumentSuggestion +=treeParameter_OnGetArgumentSuggestion;
        }

        private string treeParameter_OnGetArgumentSuggestion()
        {
            string argumentName = "$(ProjectName)";
            return argumentName;
        }

        public void SetDataFromXmlData(PathTreeNodeData iRoot)
        {
            treeParameter.SetDataRoot(iRoot);
        }

        // Get data will return a root noot
        public PathTreeNodeData GetData()
        {
            return treeParameter.GetData();
        } 

        #endregion
    }
}


