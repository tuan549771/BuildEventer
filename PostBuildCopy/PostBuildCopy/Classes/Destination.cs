
namespace PostBuildCopy.Classes
{
    class Destination : ViewModelBase
    {
        public string destination
        {
            set
            {
                m_Destination = value;
                NotifyPropertyChanged("Destination");
            }
            get { return m_Destination; }
        }
        private string m_Destination;
    }
}
