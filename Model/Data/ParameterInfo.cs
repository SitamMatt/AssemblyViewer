using Model.VisitorPattern;
using System.Reflection;

namespace Model.Data
{
    public class ParameterInfo : AsmComponent
    {
        public TypeInfo Type { get; set; }
        public ParameterAttributes Attributes { get; set; }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}