using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class CopySourcesToDestination : Action
    {
        // ==================
        // private members
        // ==================
        private PathDataModel m_Destination;
        private ObservableCollection<PathDataModel> m_Sources = new ObservableCollection<PathDataModel>();

        public PathDataModel Destination
        {
            get { return m_Destination; }
            set { m_Destination = value; }
        }

        public ObservableCollection<PathDataModel> Sources
        {
            get { return m_Sources; }
            set { m_Sources = value; }
        }

        public CopySourcesToDestination(PathDataModel iDestination)
        {
            Destination = iDestination;
            Sources = new ObservableCollection<PathDataModel>();
        }
    }
}
