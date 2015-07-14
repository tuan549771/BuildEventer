using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PostBuildCopy.Classes
{
    public class ActionManager
    {
        // Private member
        private static List<Action> m_s_Actions = new List<Action>();
        
        // Property 
        public static List<Action> Actions
        {
            get { return m_s_Actions; }
            set { m_s_Actions = value; }
        }

        // Actions variable hold all actions
        // This function used to get all destinations in the actions 
        public static ObservableCollection<PathDataModel> GetListDestinationOfActionManager()
        {
            ObservableCollection<PathDataModel> destinationTemps = new ObservableCollection<PathDataModel>();
            foreach(CopySourcesToDestination copy in Actions)
                destinationTemps.Add(copy.Destination);
            return destinationTemps;
        }
    }
}
