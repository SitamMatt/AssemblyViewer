using System;
using System.Collections.ObjectModel;

namespace ViewModel.Data
{
    public class TreeNode
    {
        public string Name { get; set; }
        public ObservableCollection<TreeNode> Children { get; set; }
        public Guid Guid { get; set; }
    }
}