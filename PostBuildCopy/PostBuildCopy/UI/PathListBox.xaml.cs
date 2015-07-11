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

        public static  DependencyProperty AllowDeletePathProperty =
            DependencyProperty.RegisterAttached("AllowDeletePathListBox", typeof(Boolean), typeof(PathTreeView),
            new FrameworkPropertyMetadata(false));

        public Boolean AllowDeletePathListBox
        {
            get { return (Boolean)GetValue(AllowDeletePathProperty); }
            set { SetValue(AllowDeletePathProperty, value); }
        }


        public static DependencyProperty SelectedIndexProperty =
            DependencyProperty.RegisterAttached("SelectedIndexLB", typeof(int), typeof(PathTreeView),
            new FrameworkPropertyMetadata(-1, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int SelectedIndexLB
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static DependencyProperty ItemSourceLBProperty =
            DependencyProperty.RegisterAttached("ItemSourceLB", typeof(IEnumerable), typeof(PathTreeView));

        public IEnumerable ItemSourceLB
        {
            get { return (IEnumerable)GetValue(ItemSourceLBProperty); }
            set { SetValue(ItemSourceLBProperty, value); }
        }
        
    }
}
