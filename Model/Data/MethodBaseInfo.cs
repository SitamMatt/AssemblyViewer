using System.Collections.Generic;
using System.Reflection;

namespace Model.Data
{
    public abstract class MethodBaseInfo : MemberInfo
    {
        public MethodAttributes Attributes { get; set; }
        public List<ParameterInfo> Parameters { get; set; }
    }
}