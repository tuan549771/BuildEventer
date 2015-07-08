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
            PathTreeNodeData root = new PathTreeNodeData() { Path = "Item", Parent = null };
            PathTreeNodeData child1 = new PathTreeNodeData() { Path = "Item1", Parent = root };
            PathTreeNodeData child2 = new PathTreeNodeData() { Path = "Item2", Parent = root };
            root.Children.Add(child1);
            root.Children.Add(child2);
            return root;
        }
    }
}
