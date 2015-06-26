using System.Collections.ObjectModel;

namespace PostBuildCopy.Classes
{
    class CopySourcesToDestination : Action
    {
        public Destination destination { get; set; }
        public ObservableCollection<Source> sources { get; set; }
    }
}
