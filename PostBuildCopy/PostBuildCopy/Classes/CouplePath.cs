
namespace PostBuildCopy.Classes
{
    public class CouplePath
    {
        // Private member
        // A CouplePath include both sub path and parent path
        private string parentPath;
        private string subPath;

        // Property
        public string ParentPath { get { return parentPath; } set { parentPath = value; } }
        public string SubPath { get { return subPath; } set { subPath = value; } }

        // Constructor
        public CouplePath(string iParentPath, string iSubPath)
        {
            parentPath = iParentPath;
            subPath = iSubPath;
        }

        // Constructor
        public CouplePath(){
        }
    }
}
