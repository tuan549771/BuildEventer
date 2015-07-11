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
        public static PathTreeNodeData GetTreeNodeData(string iSuggest)
        {
            string strRoot = "Parameters";

            PathTreeNodeData root = new PathTreeNodeData(strRoot,  null );
            root.Children.Add(new PathTreeNodeData(iSuggest, root ));
            return root;
        }
    }
}
