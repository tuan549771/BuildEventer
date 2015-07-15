
namespace PostBuildCopy.Classes
{
    public class CouplePath
    {
        // Private member
        // A CouplePath include both sub path and parent path
        private string m_ParentPath;
        private string m_SubPath;

        // Property
        public string ParentPath { get { return m_ParentPath; } set { m_ParentPath = value; } }
        public string SubPath { get { return m_SubPath; } set { m_SubPath = value; } }

        // Constructor
        public CouplePath(string iParentPath, string iSubPath)
        {
            m_ParentPath = iParentPath;
            m_SubPath = iSubPath;
        }

        // Constructor
        public CouplePath(){
        }
    }
}
