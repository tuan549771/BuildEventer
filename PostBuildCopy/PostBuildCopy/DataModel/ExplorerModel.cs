using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PostBuildCopy.Classes
{
    public class ExplorerModel
    {
        // Get a root node data that added child node
        public static PathTreeNodeData GetTreeNodeData()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            PathTreeNodeData rootNode = new PathTreeNodeData(currentDirectory);
            GetChildNodes(rootNode);
            return rootNode;
        }

        // Get files system
        // Process assigning files system into the child nodes of the parent node
        // Loading from BranchsExplorer into a root node
        public static void GetChildNodes(PathTreeNodeData iNodePath)
        {
            string fullpath = iNodePath.GetFullPath(iNodePath);
            if (true == Directory.Exists(fullpath))
            {
                try
                {
                    string[] dirs = Directory.GetDirectories(fullpath);
                    string[] files = Directory.GetFiles(fullpath);
                    GetFileSystemIntoTreeNode(dirs, iNodePath);
                    GetFileSystemIntoTreeNode(files, iNodePath);
                }
                catch (Exception) { } //the reason try catch here is have some file denied access in some computer
            }
            GetBranchsExplorersIntoTreeNode(iNodePath);
        }

        // Assigning files system into the child nodes of the parent node
        // If a path is file set node foreground (colour).
        public static void GetFileSystemIntoTreeNode(string[] strPaths, PathTreeNodeData iNode)
        {
            foreach (string strPath in strPaths)
            {
                string pathName = Path.GetFileName(strPath);
                PathTreeNodeData node = new PathTreeNodeData(pathName);
                if (true == File.Exists(strPath))
                    node.ForegroundBinding = Brushes.DarkSlateGray;
                iNode.AddChildNoMessageExist(node);
            }
        }

        // Branchs of explorer is that we drop into tree explorer
        // Include the parent path and the sub path
        // Set properties for node
        // Loading from BranchsExplorers variable into a node
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

