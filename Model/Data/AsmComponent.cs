using Model.Visitors;

namespace Model.Data
{
    public abstract class AsmComponent : IVisitable
    {
        public string Name { get; set; }
        public abstract void Accept(IVisitor visitor);
    }
}