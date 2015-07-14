using System.Collections.ObjectModel;

namespace PostBuildCopy.Classes
{
    /// <summary>
    /// 
    /// A CopySourcesToDestination hold
    /// A destination and many sources 
    /// 
    /// </summary>
    public class CopySourcesToDestination : Action
    {
        // private members
        private PathDataModel m_Destination;
        private ObservableCollection<PathDataModel> m_Sources = new ObservableCollection<PathDataModel>();

        // Property 
        public PathDataModel Destination
        {
            get { return m_Destination; }
            set { m_Destination = value; }
        }

        // Property 
        public ObservableCollection<PathDataModel> Sources
        {
            get { return m_Sources; }
            set { m_Sources = value; }
        }

        // Constructor
        public CopySourcesToDestination(PathDataModel iDestination)
        {
            Destination = iDestination;
            Sources = new ObservableCollection<PathDataModel>();
        }
    }
}
