using Model.Visitors;

namespace Model.Data
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}