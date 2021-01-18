using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class BoolHelper
    {
        public static bool AnyOf(params IEnumerable<object>[] collections)
        {
            foreach (var collection in collections)
            {
                if (collection != null && collection.Any()) return true; 
            }
            return false;
        }
    }
}
