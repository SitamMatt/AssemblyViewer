using System.Collections.ObjectModel;

namespace ViewModel
{
    public class TreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<TreeNode> Children { get; set; }
        public int Hash { get; set; }
    }
}