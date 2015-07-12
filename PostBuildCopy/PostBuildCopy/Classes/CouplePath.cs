

namespace PostBuildCopy.Classes
{
    public class CouplePath
    {
        private string parentPath;
        private string subPath;

        public string ParentPath { get { return parentPath; } set { parentPath = value; } }
        public string SubPath { get { return subPath; } set { subPath = value; } }

        public CouplePath(string iParentPath, string iSubPath)
        {
            parentPath = iParentPath;
            subPath = iSubPath;
        }

        public CouplePath()
        {

        }
    }
}
