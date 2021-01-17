using Model.VisitorPattern;

namespace Model.Data
{
    public class ConstructorInfo : MethodBaseInfo
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}