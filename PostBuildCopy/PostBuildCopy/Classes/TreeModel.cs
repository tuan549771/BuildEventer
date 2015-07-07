using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class TreeModel
    {
        public static PathTreeNodeData GetTreeNodeData()
        {
            PathTreeNodeData root = new PathTreeNodeData() { path = "A", parent = null };

            PathTreeNodeData child1 = new PathTreeNodeData() { path = "-A1", parent = root };

            child1.childrens.Add(new PathTreeNodeData() { path = "--A1.1", parent = child1 });
            child1.childrens.Add(new PathTreeNodeData() { path = "--A1.2", parent = child1 });

            root.childrens.Add(child1);
            root.childrens.Add(new PathTreeNodeData() { path = "-A2", parent = root });

            return root;
        }
    }
}
