using Model.VisitorPattern;

namespace Model.Data
{
    public class ConstructorInfo : MethodBaseInfo
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }
    }
}