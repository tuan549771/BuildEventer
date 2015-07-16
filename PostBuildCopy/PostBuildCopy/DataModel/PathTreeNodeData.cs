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
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PostBuildCopy.Classes
{
    public class PathTreeNodeData : TreeViewItemBase
    {
        #region Private members

        public string Path { get; set; }
        public PathTreeNodeData Parent { get; set; }
        public ObservableCollection<PathTreeNodeData> Children { get; set; }

        #endregion

        #region Constructor

        public PathTreeNodeData()
        {
            Children = new ObservableCollection<PathTreeNodeData>();
            Parent = null;
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

        public void AddChildHasMessageExist(PathTreeNodeData iChild)
        {
            if (false == AddChild(iChild))
                MessageBox.Show("\"" + iChild.Path + "\"" + " has existed in " + "\"" + this.Path + "\"", "Information");
        }

        public void AddChildNoMessageExist(PathTreeNodeData iChild)
        {
            if (true == AddChild(iChild))
                return;
        }

        public bool AddChild(PathTreeNodeData iChild)
        {
            if (false == this.ContainsChildPath(iChild.Path))
            {
                string path = String.Copy(iChild.Path);
                PathTreeNodeData child = new PathTreeNodeData(path, this);
                child.ForegroundBinding = iChild.ForegroundBinding;
                this.Children.Add(child);
                return true;
            }
            return false;
        }

        public void DeleteChild(PathTreeNodeData iChild)
        {
            if (null != iChild.Parent)
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

        public string GetRelativePath(PathTreeNodeData iNode)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if ("\\" == currentDirectory[currentDirectory.Length - 1].ToString())
                currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 1);
            string fullPath = GetFullPath(iNode);
            fullPath = fullPath.Replace("\\\\", "\\");
            string relativePath = fullPath.Substring(currentDirectory.Length + 1);
            return relativePath;
        }

        public bool HasChildren()
        {
            if (0 < this.Children.Count)
                return true;
            return false;
        }

        public bool ContainsChildPath(string iPathChild)
        {
            foreach (PathTreeNodeData child in this.Children)
            {
                if (child.Path == iPathChild)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
