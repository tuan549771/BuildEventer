using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class PathTreeNodeData
    {
        public string Path { get; set; }
        public PathTreeNodeData Parent { get; set; }
        public ObservableCollection<PathTreeNodeData> Children { get; set; }

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

        public void AddChild(PathTreeNodeData iChild)
        {
            if (false == this.ContainsChild(iChild.Path))
            {
                string path = String.Copy(iChild.Path);
                PathTreeNodeData child = new PathTreeNodeData(path, this);
                this.Children.Add(child);
            }
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
            string currentDirectory = @"C:\Users\Dell\Desktop\New folder\";//Directory.GetCurrentDirectory();
            if ("\\" == currentDirectory[currentDirectory.Length - 1].ToString())
                currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 1);
            string fullPath = GetFullPath(iNode);
            string relativePath = fullPath.Substring(currentDirectory.Length + 1);
            return relativePath;
        }

        public bool HasChildren(PathTreeNodeData iNode)
        {
            if (0 < iNode.Children.Count)
                return true;
            return false;
        }

        public bool ContainsChild(string iPathChild)
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
    }
}
