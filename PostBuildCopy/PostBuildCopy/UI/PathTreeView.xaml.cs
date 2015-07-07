using PostBuildCopy.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for PathTreeView.xaml
    /// </summary>
    public partial class PathTreeView : UserControl
    {
        ObservableCollection<PathTreeNodeData> root = new ObservableCollection<PathTreeNodeData>();
        public PathTreeView()
        {
            InitializeComponent();
            root.Add(TreeModel.GetTreeNodeData());
            SetData(root);
        }

        private void SetData(ObservableCollection<PathTreeNodeData> root)
        {
            treeView.ItemsSource = root;
        }

        private void TreeMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void TreeDrop(object sender, DragEventArgs e)
        {

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {

        }

        private void NewItem(object sender, RoutedEventArgs e)
        {

        }

        private void TreeExpanded(object sender, RoutedEventArgs e)
        {
            //if (false != root.HasChildren(root))
            //    return;
            //Create item
        }
    }
}
