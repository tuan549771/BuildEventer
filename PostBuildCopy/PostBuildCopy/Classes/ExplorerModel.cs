using System.IO;

namespace PostBuildCopy.Classes
{
    public class ExplorerModel
    {
        public static PathTreeNodeData GetTreeNodeData()
        {
            string currentDirectory = @"C:\Users\Dell\Desktop\New folder";
            PathTreeNodeData root = new PathTreeNodeData() { Path = currentDirectory, Parent = null };
            GetTreeNode(currentDirectory, root);
            return root;
        }

        public static void GetTreeNode(string iPath, PathTreeNodeData iNode)
        {
            if (Directory.Exists(iPath))
            {
                string[] dirs = Directory.GetDirectories(iPath);
                string[] files = Directory.GetFiles(iPath);
                foreach (string dir in dirs)
                {
                    string pathName = Path.GetFileName(dir);
                    PathTreeNodeData node = new PathTreeNodeData() { Path = pathName };
                    iNode.Children.Add(node);
                    node.Parent = iNode;
                }
                foreach (string file in files)
                {
                    string pathName = Path.GetFileName(file);
                    PathTreeNodeData node = new PathTreeNodeData() { Path = pathName };
                    iNode.Children.Add(node);
                    node.Parent = iNode;
                }
            }
        }

    }
}
