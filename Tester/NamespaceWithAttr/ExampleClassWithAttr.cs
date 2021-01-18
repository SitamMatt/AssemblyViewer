using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester.NamespaceWithAttr
{
    [Example("Hello", AttrProperty = 5.5)]
    public class ExampleClassWithAttr
    {
        [Example("It's field")]
        public int fieldWithAttribute;
    }
}
