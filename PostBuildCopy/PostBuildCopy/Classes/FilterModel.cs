using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class FilterModel
    {
        public static string suggest = "Right Click to add Filters\nand you may drag them into Explorer";
        public static PathTreeNodeData GetTreeNodeData()
        {
            string strRoot = "Filters";
            
            PathTreeNodeData root = new PathTreeNodeData() { Path = strRoot, Parent = null };
            root.Children.Add(new PathTreeNodeData() { Path = suggest, Parent = root });
            return root;
        }
    }
}

