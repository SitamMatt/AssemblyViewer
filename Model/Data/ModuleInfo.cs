using System.Collections.Generic;
using Model.Visitors;

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