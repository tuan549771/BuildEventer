using System.ComponentModel;
using System.Windows.Media;

namespace PostBuildCopy.DataModel
{
    /// <summary>
    /// TreeViewItemBase hold property
    /// </summary>
    public class TreeViewItemBase : INotifyPropertyChanged
    {
        #region Private members

        private bool isExpanded;
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
            set
            {
                if (value != this.allowDropNode)
                {
                    this.allowDropNode = value;
                    NotifyPropertyChanged("AllowDropNode");
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
