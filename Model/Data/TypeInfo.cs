using System.Collections.Generic;
using System.Reflection;
using Model.VisitorPattern;

namespace Model.Data
{
    public class TypeInfo : AsmComponent
    {
        public TypeAttributes Attributes { get; set; }
        public List<FieldInfo> Fields { get; set; }
        public List<ConstructorInfo> Constructors { get; set; }
        public List<MethodInfo> Methods { get; set; }
        public List<PropertyInfo> Properties { get; set; }
        public List<TypeInfo> NestedTypes { get; set; }
        public string Namespace { get; set; }
        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}