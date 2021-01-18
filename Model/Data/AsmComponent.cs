using System;
using System.Collections.Generic;
using Model.VisitorPattern;

namespace Model.Data
{
    public abstract class AsmComponent : IVisitable
    {
        public virtual Guid Guid { get; set; }
        public string Name { get; set; }
        public List<AttributeInfo> CustomAttributes { get; set; }
        public abstract object Accept(IVisitor visitor);
    }
}