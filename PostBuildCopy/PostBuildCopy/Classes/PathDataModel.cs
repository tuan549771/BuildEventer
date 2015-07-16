/*
<License>
Copyright 2015 Virtium Technology
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http ://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
</License>
*/

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
