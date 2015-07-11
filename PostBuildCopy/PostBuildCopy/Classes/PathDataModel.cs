using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class PathDataModel: INotifyPropertyChanged
    {
        // ==================
        // private members
        // ==================
        private string m_Path;
        public event PropertyChangedEventHandler PropertyChanged;

        // ==================
        // property
        // ==================
        public string pathModel
        {
            get { return m_Path; }
            set { m_Path = value; NotifyPropertyChanged("pathModel"); }
        }

        // ==================
        // constructor
        // ==================
        public PathDataModel(string str)
        {
            pathModel = str;
        }

        // ==================
        // method
        // ==================
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
