using System.Reflection;
using Model.VisitorPattern;

namespace Model.Data
{
    public class FieldInfo : MemberInfo
    {
        public TypeInfo Type { get; set; }
        public FieldAttributes Attributes { get; set; }
        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }
    }
}