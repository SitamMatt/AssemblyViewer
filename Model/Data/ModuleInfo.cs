using System.Collections.Generic;
using Model.VisitorPattern;

namespace Model.Data
{
    public class ModuleInfo : AsmComponent
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }

        public List<TypeInfo> Types { get; set; }
    }
}