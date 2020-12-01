using System.Reflection;
using Model.Visitors;

namespace Model.Data
{
    public class PropertyInfo : MemberInfo
    {
        public PropertyAttributes Attributes { get; set; }
        public MethodInfo SetMethod { get; set; }
        public MethodInfo GetMethod { get; set; }
        public TypeInfo Type { get; set; }
        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }
    }
}