using Model.VisitorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public class ForeignTypeInfo : AsmComponent
    {
        public override object Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
