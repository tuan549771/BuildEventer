using PostBuildCopy.Classes;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for PathListBox.xaml
    /// </summary>
    public partial class PathListBox : UserControl
    {
        // On path Drop
        public delegate void OnPathDropDelegate(PathTreeNodeData iDroppedObject);
        public event OnPathDropDelegate OnPathDrop;

        // On path item Selected
        public delegate void OnSelectedPathDelegate();
        public event OnSelectedPathDelegate OnSelectedPath;

        // On path item Delete
        public delegate void OnDeletePathDelegate();
        public event OnDeletePathDelegate OnDeletePath;

        public PathListBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public void SetData(ObservableCollection<PathDataModel> iPaths)
        {
            lbPath.ItemsSource = iPaths;
        }

        private void DropPath(object sender, DragEventArgs e)
        {
            PathTreeNodeData objDrop = (PathTreeNodeData)e.Data.GetData(typeof(PathTreeNodeData));
            if (null != OnPathDrop)
                OnPathDrop(objDrop);
            lbPath.SelectedIndex = lbPath.Items.Count - 1;
        }

        private void SelectedPath(object sender, SelectionChangedEventArgs e)
        {
            if (null != OnSelectedPath)
                OnSelectedPath();
        }

        private void DeletePath(object sender, RoutedEventArgs e)
        {
            if (null != OnDeletePath)
                OnDeletePath();
        }

        public int ItemsCount
        {
            get { return lbPath.Items.Count; }
        }

        public int SelectedIndex
        {
            get { return lbPath.SelectedIndex; }
            set { lbPath.SelectedIndex = value; }
        }

        public IEnumerable ItemSource
        {
            set { lbPath.ItemsSource = value; }
        }

        public static  DependencyProperty AllowDeletePathProperty =
            DependencyProperty.RegisterAttached("AllowDeletePathListBox", typeof(Boolean), typeof(PathTreeView),
            new FrameworkPropertyMetadata(false));

        public Boolean AllowDeletePathListBox
        {
            get { return (Boolean)GetValue(AllowDeletePathProperty); }
            set { SetValue(AllowDeletePathProperty, value); }
        }
    }
}
