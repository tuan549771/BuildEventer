using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class PathTreeNodeData
    {
        public string Path { get; set; }
        public ObservableCollection<PathTreeNodeData> Children { get; set; }
        public PathTreeNodeData Parent { get; set; }

        public PathTreeNodeData()
        {
            this.Children = new ObservableCollection<PathTreeNodeData>();
            this.Parent = null;
        }

        // Constructor has 2 parameter Path and parent node
        public PathTreeNodeData(string iPath, PathTreeNodeData iParent)
        {
            this.Path = iPath;
            this.Parent = iParent;
            this.Children = null;
        }

        public void AddChild(PathTreeNodeData iChild)
        {
            iChild.Parent = this;
            this.Children.Add(iChild);
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

        public bool HasChildren(PathTreeNodeData iNode)
        {
            if (0 < iNode.Children.Count)
                return true;
            return false;
        }

        public bool HasPathChild(string iPathChild)
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
