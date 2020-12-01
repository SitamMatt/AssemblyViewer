using System;
using System.Collections.Generic;
using Model.VisitorPattern;

namespace Model.Data
{
    public class AssemblyInfo : AsmComponent
    {
        public List<ModuleInfo> Modules { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Handle(this);
        }
    }
}