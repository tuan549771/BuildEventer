using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PostBuildCopy.Classes
{
    public class PathDataModel : INotifyPropertyChanged
    {
        // Private member
        private string m_Path;

        // Property
        public string PathModel
        {
            get { return m_Path; }
            set { m_Path = value; NotifyPropertyChanged("PathModel"); }
        }

        // Constructor
        public PathDataModel(string iStrPath)
        {
            PathModel = iStrPath;
        }

        // Method and event property changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
