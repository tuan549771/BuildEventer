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

        private bool isExpanded;
        private bool allowDropNode = true;
        private FontWeight fontWeightBinding = FontWeights.Normal;

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

        public FontWeight FontWeightBinding
        {
            get { return this.fontWeightBinding; }
            set
            {
                if (value != this.fontWeightBinding)
                {
                    this.fontWeightBinding = value;
                    NotifyPropertyChanged("FontWeightBinding");
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
