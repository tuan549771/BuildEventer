using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class Subfunction
    {
        public static void CreatePath(PathTreeNodeData iNode, PathTreeNodeData iRootNode, string iPath, string iSuggest)
        {
            iNode = iRootNode;
            PathTreeNodeData node = new PathTreeNodeData() { Path = iPath, Parent = iRootNode };
            if (false == iRootNode.Children.Contains(node))
                iNode.Children.Add(node);
            // Clear suggest if have
            if (iSuggest == iRootNode.Children[0].Path)
                DeletePath(iRootNode.Children[0], iSuggest);
        }


        public static void DeletePath(PathTreeNodeData iNode, string iSuggest)
        {
            PathTreeNodeData parent = iNode.Parent;
            if (null != iNode.Parent)
                parent.Children.Remove(iNode);
            // Add suggest if no have
            if (0 == parent.Children.Count)
                parent.Children.Add(new PathTreeNodeData() { Path = iSuggest, Parent = parent });
        }


    }
}
