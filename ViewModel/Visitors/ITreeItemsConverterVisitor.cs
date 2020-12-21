using System.Collections.ObjectModel;
using Model.VisitorPattern;
using ViewModel.Data;

namespace ViewModel.Visitors
{
    public interface ITreeItemsConverterVisitor : IVisitor
    {
        ObservableCollection<TreeNode> Result { get; }
    }
}