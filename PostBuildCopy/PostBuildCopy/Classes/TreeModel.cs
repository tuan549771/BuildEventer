using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostBuildCopy.Classes
{
    public class TreeModel
    {
        public static PathTreeNodeData GetTreeNodeData()
        {

            PathTreeNodeData root = new PathTreeNodeData() { Path = "A", Parent = null };

            PathTreeNodeData child1 = new PathTreeNodeData() { Path = "-A1", Parent = root };

            child1.Children.Add(new PathTreeNodeData() { Path = "--A1.1", Parent = child1 });
            child1.Children.Add(new PathTreeNodeData() { Path = "--A1.2", Parent = child1 });

            root.Children.Add(child1);
            root.Children.Add(new PathTreeNodeData() { Path = "-A2", Parent = root });

            return root;
        }
        //public static ObservableCollection<PathTreeNodeData> GetTreeNodeData()
        //{
        //    ObservableCollection<PathTreeNodeData> roots = new ObservableCollection<PathTreeNodeData>();

        //    PathTreeNodeData root = new PathTreeNodeData() { Path = "A", Parent = null };

        //    PathTreeNodeData child1 = new PathTreeNodeData() { Path = "-A1", Parent = root };

        //    child1.Children.Add(new PathTreeNodeData() { Path = "--A1.1", Parent = child1 });
        //    child1.Children.Add(new PathTreeNodeData() { Path = "--A1.2", Parent = child1 });

        //    root.Children.Add(child1);
        //    root.Children.Add(new PathTreeNodeData() { Path = "-A2", Parent = root });

        //    roots.Add(root);

        //    return roots;
        //}
    }
}
