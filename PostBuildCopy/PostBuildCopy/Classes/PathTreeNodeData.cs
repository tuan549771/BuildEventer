using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class PathTreeNodeData
    {
        public bool HasChildren(PathTreeNodeData iNode)
        {
            if (0 < iNode.childrens.Count)
                return true;
            return false;
        }

        public string GetFullPath(PathTreeNodeData iNode)
        {
            string fullPath = iNode.path;
            while (null != iNode.parent)
            {
                iNode = iNode.parent;
                fullPath = iNode.path + "\\" + fullPath;
            }
            return fullPath;
        }

        public PathTreeNodeData()
        {
            this.childrens = new ObservableCollection<PathTreeNodeData>();
        }

        public string path { get; set; }
        public ObservableCollection<PathTreeNodeData> childrens { get; set; }
        public PathTreeNodeData parent { get; set; }

    }
}
