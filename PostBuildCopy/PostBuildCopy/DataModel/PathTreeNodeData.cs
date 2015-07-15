﻿using PostBuildCopy.DataModel;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;

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
                child.FontWeightBinding = iChild.FontWeightBinding;
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
