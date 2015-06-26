using System.Collections.ObjectModel;

namespace PostBuildCopy.Classes
{
    public class ActionManager
    {
        public ObservableCollection<Action> GetAction()
        {
            return listAction;
        }

        public ObservableCollection<Action> listAction = new ObservableCollection<Action>();
    }
}
