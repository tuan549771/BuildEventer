using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class ParameterModel
    {
        public static string suggest = "Right Click to add parameters\nand you may drag them\ninto Explorer";
        public static PathTreeNodeData GetTreeNodeData()
        {
            string strRoot = "Parameters";

            PathTreeNodeData root = new PathTreeNodeData() { Path = strRoot, Parent = null };
            root.Children.Add(new PathTreeNodeData() { Path = suggest, Parent = root });
            return root;
        }
    }
}
