/*
<License>
Copyright 2015 Virtium Technology
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http ://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
</License>
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace PostBuildCopy.Classes
{
    public class ExplorerModel
    {
        private enum eObj { TreeExplorer = 1 };

        #region Methods

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
                catch (Exception)
                {
                    return;
                } //the reason try catch here is have some file denied access in some computer
            }
            GetBranchsExplorersIntoTreeNode(iNodePath);
        }

        // Assigning files system into the child nodes of the parent node
        public static void GetFileSystemIntoTreeNode(string[] strPaths, PathTreeNodeData iNode)
        {
            foreach (string strPath in strPaths)
            {
                string pathName = Path.GetFileName(strPath);
                PathTreeNodeData node = new PathTreeNodeData(pathName);
                node.ID = (int)eObj.TreeExplorer;
                iNode.AddChild(node, false);
            }
        }

        // Branchs of explorer is that we drop into tree explorer
        // Include the parent path and the sub path
        // Loading from BranchsExplorers variable into a node
        public static void GetBranchsExplorersIntoTreeNode(PathTreeNodeData iNode)
        {
            List<string> subPaths = new List<string>();
            subPaths = BranchsExplorer.GetSubPathExplorer(iNode.GetFullPath(iNode));
            foreach (string subPath in subPaths)
            {
                PathTreeNodeData node = new PathTreeNodeData(subPath);
                node.ID = (int)eObj.TreeExplorer;
                iNode.AddChild(node, false);
            }
        }

        // Get relative path
        public static string GetRelativePath(PathTreeNodeData iNode)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            if ("\\" == currentDirectory[currentDirectory.Length - 1].ToString())
                currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - 1);
            string fullPath = iNode.GetFullPath(iNode);
            fullPath = fullPath.Replace("\\\\", "\\");
            string relativePath = fullPath.Substring(currentDirectory.Length + 1);
            return relativePath;
        }

        #endregion
    }
}

