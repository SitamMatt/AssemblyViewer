using Model.VisitorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class AttributeInfo : AsmComponent
    {
        public TypeInfo Type { get; set; }
        public ConstructorInfo ConstructorInfo { get; set; }
        public override object Accept(IVisitor visitor)
        {
            return visitor.Handle(this);
        }
    }
}
