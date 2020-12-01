namespace Model.VisitorPattern
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}