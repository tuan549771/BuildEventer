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

        public bool HasChildren(PathTreeNodeData iNode)
        {
            if (0 < iNode.Children.Count)
                return true;
            return false;
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
    }
}
