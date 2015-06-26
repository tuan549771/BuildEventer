
namespace PostBuildCopy.Classes
{
    class Source : ViewModelBase
    {
        public string source
        {
            set
            {
                m_Sources = value;
                NotifyPropertyChanged("Sources");
            }
            get { return m_Sources; }
        }
        private string m_Sources;
    }
}
