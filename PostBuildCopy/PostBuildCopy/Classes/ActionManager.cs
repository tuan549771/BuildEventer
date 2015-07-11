using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class ActionManager
    {
        public static List<Action> actions
        {
            get { return m_Actions; }
            set { m_Actions = value; }
        }

        private static List<Action> m_Actions = new List<Action>();

        public static ObservableCollection<PathDataModel> GetListDestinationOfActionManager()
        {
            ObservableCollection<PathDataModel> destinationTemps = new ObservableCollection<PathDataModel>();
            foreach(CopySourcesToDestination copy in actions)
                destinationTemps.Add(copy.Destination);
            return destinationTemps;
        }
    }
}
