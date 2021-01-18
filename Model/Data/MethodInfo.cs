using Model.VisitorPattern;

namespace Model.Data
{
    public class MethodInfo : MethodBaseInfo
    {
        public TypeInfo ReturnType { get; set; }
        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}