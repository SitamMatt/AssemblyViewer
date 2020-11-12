using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data
{
    public record TypeInfo(string Name)
    {
        public List<TypeInfo> SubTypes { get; set; } = new List<TypeInfo>();
    }
}
