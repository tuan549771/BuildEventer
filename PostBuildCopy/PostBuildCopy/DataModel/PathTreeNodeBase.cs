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

using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace PostBuildCopy.DataModel
{
    /// <summary>
    /// TreeViewItemBase hold property
    /// </summary>
    public class PathTreeNodeBase : INotifyPropertyChanged
    {
        #region Private members
        private bool isExpanded = false;
        private bool isEnabled = true;
        private string imagePath = "../Images/adding.png";
        private int id = 0;
        private Brush foregroundBinding = Brushes.Black;

        #endregion

        #region Property

        // ID = 1 represent explorer tree
        // ID = 2 and 3 ... represent another object

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }

        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                if (value != this.isEnabled)
                {
                    this.isEnabled = value;
                    NotifyPropertyChanged("IsEnabled");
                }
            }
        }

        public Brush ForegroundBinding
        {
            get { return this.foregroundBinding; }
            set
            {
                if (value != this.foregroundBinding)
                {
                    this.foregroundBinding = value;
                    NotifyPropertyChanged("ForegroundBinding");
                }
            }
        }

        #endregion

        # region Event and Method NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion
    }
}
