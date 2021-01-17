namespace Model.VisitorPattern
{
    public interface IVisitable
    {
        object Accept(IVisitor visitor);
    }
}