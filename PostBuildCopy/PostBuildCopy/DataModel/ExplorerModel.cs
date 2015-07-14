using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace PostBuildCopy.Classes
{
    public class ExplorerModel
    {
        public static PathTreeNodeData GetTreeNodeData()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            //string currentDirectory = @"C:\Users\Dell\Desktop\New folder";
            PathTreeNodeData root = new PathTreeNodeData(currentDirectory);
            GetTreeNode(currentDirectory, root);
            root.IsExpanded = false;
            return root;
        }

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

        // Get files system and assign filename into node path
        public static void GetFileSystemIntoTreeNode(string[] strPaths, PathTreeNodeData iNode)
        {
            foreach (string strPath in strPaths)
            {
                string pathName = Path.GetFileName(strPath);
                PathTreeNodeData node = new PathTreeNodeData(pathName);
                iNode.AddChildNoMessageExist(node);
            }
        }

        // Loading from BranchsExplorer into node
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

