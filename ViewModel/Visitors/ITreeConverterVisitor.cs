using Model.VisitorPattern;
using ViewModel.Data;

namespace ViewModel.Visitors
{
    public interface ITreeConverterVisitor : IVisitor
    {
        TreeNode Result { get; }
    }
}