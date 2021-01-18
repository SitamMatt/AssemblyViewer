using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.NamespaceWithAttr
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class ExampleAttribute : Attribute
    {
        public double AttrProperty { get; set; }
        private string field;

        public ExampleAttribute(string param)
        {
            this.field = param;
        }
    }
}
