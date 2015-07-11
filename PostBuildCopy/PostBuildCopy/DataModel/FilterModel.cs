using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class FilterModel
    {
        public static PathTreeNodeData GetTreeNodeData(string iSuggest)
        {
            string strRoot = "Filters";

            PathTreeNodeData root = new PathTreeNodeData(strRoot, null);
            root.Children.Add(new PathTreeNodeData(iSuggest, root));
            return root;
        }
    }
}

