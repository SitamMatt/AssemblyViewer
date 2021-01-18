using Model.VisitorPattern;
using System.Reflection;

namespace Model.Data
{
    public class PropertyInfo : MemberInfo
    {
        public PropertyAttributes Attributes { get; set; }

        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }

        //public MethodInfo SetMethod { get; set; }
        //public MethodInfo GetMethod { get; set; }
        public TypeInfo Type { get; set; }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}