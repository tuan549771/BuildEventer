using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace PostBuildCopy.Classes
{
    public class ExplorerModel
    {
        // Get a root node data that added child node
        public static PathTreeNodeData GetTreeNodeData()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            PathTreeNodeData root = new PathTreeNodeData(currentDirectory);
            GetTreeNode(currentDirectory, root);
            root.IsExpanded = false;
            return root;
        }

        // Get files system
        // Process assigning files system into the child nodes of the parent node
        // Loading from BranchsExplorer into a root node
        public static void GetTreeNode(string iPath, PathTreeNodeData iNode)
        {
            if (Directory.Exists(iPath))
            {
                try
                {
                    string[] dirs = Directory.GetDirectories(iPath);
                    string[] files = Directory.GetFiles(iPath);
                    GetFileSystemIntoTreeNode(dirs, iNode);
                    GetFileSystemIntoTreeNode(files, iNode);
                }
                catch (Exception) { } //the reason try catch here is have some file denied access in some computer
            }
            GetBranchsExplorersIntoTreeNode(iNode);
        }

        // Assigning files system into the child nodes of the parent node
        public static void GetFileSystemIntoTreeNode(string[] strPaths, PathTreeNodeData iNode)
        {
            foreach (string strPath in strPaths)
            {
                string pathName = Path.GetFileName(strPath);
                PathTreeNodeData node = new PathTreeNodeData(pathName);
                iNode.AddChildNoMessageExist(node);
            }
        }

        // Branchs of explorer is that we drop into tree explorer
        // Include the parent path and the sub path
        // Loading from BranchsExplorers variable into a root node
        public static void GetBranchsExplorersIntoTreeNode(PathTreeNodeData iNode)
        {
            List<string> subPaths = new List<string>();
            subPaths = BranchsExplorer.GetSubPathExplorer(iNode.GetFullPath(iNode));
            foreach (string subPath in subPaths)
            {
                PathTreeNodeData node = new PathTreeNodeData(subPath);
                node.ForegroundBinding = Brushes.Magenta;
                iNode.AddChildNoMessageExist(node);
            }
        }
    }
}

