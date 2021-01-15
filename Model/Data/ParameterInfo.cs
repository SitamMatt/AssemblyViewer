using Model.VisitorPattern;

namespace Model.Data
{
    public class ParameterInfo : AsmComponent
    {
        public TypeInfo Type { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }
    }
}