using Model.VisitorPattern;

namespace Model.Data
{
    public class MethodInfo : MethodBaseInfo
    {
        public TypeInfo ReturnType { get; set; }
        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }
    }
}