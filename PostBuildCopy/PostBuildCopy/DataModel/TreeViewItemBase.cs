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
    public class TreeViewItemBase : INotifyPropertyChanged
    {
        #region Private members

        private bool isExpanded = false;
        private bool allowDropNode = true;
        private Brush foregroundBinding = Brushes.Black;

        #endregion

        #region Property

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

        public bool AllowDropNode
        {
            get { return this.allowDropNode; }
            set { this.allowDropNode = value;}
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
