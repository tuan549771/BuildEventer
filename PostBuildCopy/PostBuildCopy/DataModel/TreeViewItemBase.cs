using System.ComponentModel;
using System.Windows.Media;

namespace PostBuildCopy.DataModel
{
    public class TreeViewItemBase : INotifyPropertyChanged
    {
        private bool isExpanded;
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

        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        private bool allowDropNode = true;
        public bool AllowDropNode
        {
            get { return this.allowDropNode; }
            set
            {
                if (value != this.allowDropNode)
                {
                    this.allowDropNode = value;
                    NotifyPropertyChanged("AllowDropNode");
                }
            }
        }

        private Brush foregroundBinding = Brushes.Black;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
