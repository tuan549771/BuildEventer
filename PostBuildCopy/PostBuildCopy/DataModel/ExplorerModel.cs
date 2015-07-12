using System.Collections.Generic;
using System.IO;

namespace PostBuildCopy.Classes
{
    public class ExplorerModel
    {
        public static PathTreeNodeData GetTreeNodeData()
        {
            string currentDirectory = @"C:\Users\Dell\Desktop\New folder";//Directory.GetCurrentDirectory();
            PathTreeNodeData root = new PathTreeNodeData(currentDirectory);
            GetTreeNode(currentDirectory, root);
            return root;
        }

        public static void GetTreeNode(string iPath, PathTreeNodeData iNode)
        {
            if (Directory.Exists(iPath))
            {
                string[] dirs = Directory.GetDirectories(iPath);
                string[] files = Directory.GetFiles(iPath);
                GetFileSystemIntoTreeNode(dirs, iNode);
                GetFileSystemIntoTreeNode(files, iNode);

            }
            GetBranchsExplorersIntoTreeNode(iNode);
        }

        // Get files system and assign filename into node path
        public static void GetFileSystemIntoTreeNode(string[] strPaths, PathTreeNodeData iNode)
        {
            foreach (string strPath in strPaths)
            {
                string pathName = Path.GetFileName(strPath);
                PathTreeNodeData node = new PathTreeNodeData(pathName);
                iNode.AddChildNoMessageExsit(node);
            }
        }

        // Loading from BranchsExplorer
        public static void GetBranchsExplorersIntoTreeNode(PathTreeNodeData iNode)
        {
            List<string> subPaths = new List<string>();
            subPaths = BranchsExplorer.GetSubPathExplorer(iNode.GetFullPath(iNode));
            foreach (string subPath in subPaths)
            {
                PathTreeNodeData node = new PathTreeNodeData(subPath);
                iNode.AddChildNoMessageExsit(node);
            }
        }
    }
}

