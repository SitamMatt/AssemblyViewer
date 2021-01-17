using System.Collections.Generic;
using Model.VisitorPattern;

namespace Model.Data
{
    public class ModuleInfo : AsmComponent
    {
        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }

        public List<TypeInfo> Types { get; set; }
    }
}