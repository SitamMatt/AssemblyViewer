using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Tester
{
    [Serializable]
    public class AttributedClass
    {
        [XmlElement(ElementName = "TaxRate")]
        public int MyProperty { get; set; }
    }
}
