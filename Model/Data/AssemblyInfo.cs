using System;
using System.Collections.Generic;
using Model.VisitorPattern;

namespace Model.Data
{
    //todo disallow lookup modification from outside ?
    public class AssemblyInfo : AsmComponent
    {
        public virtual Dictionary<Guid, AsmComponent> Lookup { get; set; }
        public List<ModuleInfo> Modules { get; set; }

        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}