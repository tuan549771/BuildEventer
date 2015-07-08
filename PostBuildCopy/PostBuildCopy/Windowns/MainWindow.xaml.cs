using PostBuildCopy.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PostBuildCopy.Widowns
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            treeView.newItem.IsEnabled = true;
            treeView.GetChildren = GetTreeNodeChildren;
            treeView.OnPathCreate += treeView_OnPathCreate;
            treeView.OnNodeDrop += treeView_OnNodeDrop;
        }

        private void treeView_OnPathCreate(PathTreeNodeData nodeTarget, string iPath)
        {
            PathTreeNodeData node = new PathTreeNodeData() { Path = iPath };
            nodeTarget.AddChild(node);
        }

        void treeView_OnNodeDrop(PathTreeNodeData nodeSource, PathTreeNodeData nodeTarget)
        {
            PathTreeNodeData node = new PathTreeNodeData() { Path = nodeSource.Path };
            nodeTarget.AddChild(node);
        }

        private void GetTreeNodeChildren(PathTreeNodeData node)
        {
            node.Children.Clear();
            node.Children.Add(new PathTreeNodeData() { Path = "Child 1", Parent = node });
            node.Children.Add(new PathTreeNodeData() { Path = "Child 2", Parent = node });
        }
    }
}
