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

using PostBuildCopy.DataModel;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PostBuildCopy.Classes
{
    public class PathTreeNodeData : PathTreeNodeBase
    {
        #region Private members

        public string Path { get; set; }
        public PathTreeNodeData Parent { get; set; }
        public ObservableCollection<PathTreeNodeData> Children { get; set; }

        #endregion

        #region Constructor

        public PathTreeNodeData()
        {
            this.Path = string.Empty;
            this.Children = new ObservableCollection<PathTreeNodeData>();
            this.Parent = null;
        }

        public PathTreeNodeData(string iPath)
        {
            Path = iPath;
            Children = new ObservableCollection<PathTreeNodeData>();
            Parent = null;
        }

        public PathTreeNodeData(string iPath, PathTreeNodeData iParent)
        {
            Path = iPath;
            Parent = iParent;
            Children = new ObservableCollection<PathTreeNodeData>();
        }

        #endregion

        #region Methods

        public bool AddChild(PathTreeNodeData iChild, bool iHasMessageExisted)
        {
            if (false == this.ContainsChildPath(iChild.Path, iHasMessageExisted))
            {
                string path = String.Copy(iChild.Path);
                PathTreeNodeData child = new PathTreeNodeData(path, this);
                child.ForegroundBinding = iChild.ForegroundBinding;
                child.ImagePath = iChild.ImagePath;
                child.ID = iChild.ID;
                this.Children.Add(child);
                return true;
            }
            return false;
        }

        public void DeleteChild(PathTreeNodeData iChild)
        {
            this.Children.Remove(iChild);
        }

        public string GetFullPath(PathTreeNodeData iNode)
        {
            string fullPath = iNode.Path;
            while (null != iNode.Parent)
            {
                iNode = iNode.Parent;
                fullPath = iNode.Path + "\\" + fullPath;
            }
            return fullPath;
        }

        public bool HasChildren()
        {
            if (0 < this.Children.Count)
                return true;
            return false;
        }

        public bool ContainsChildPath(string iPathChild, bool iHasMessageResult)
        {
            foreach (PathTreeNodeData child in this.Children)
            {
                if (child.Path == iPathChild)
                {
                    if (true == iHasMessageResult)
                        MessageBox.Show("\"" + iPathChild + "\"" + " has existed in " + "\"" + this.Path + "\"", "Information");
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
