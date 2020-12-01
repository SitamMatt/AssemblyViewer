using System.Collections.Generic;
using Model.Converters;
using Model.Data;
using Model.VisitorPattern;
using Tester;

namespace Model.Services
{
    public class AssemblyInfoService : IAssemblyInfoService
    {
        private readonly AssemblyInfo _assemblyInfo;
        private readonly Dictionary<int, IVisitable> _lookup;

        public AssemblyInfoService()
        {
            var c = new Converter();
            _assemblyInfo = c.Convert(typeof(Program).Assembly);
            _lookup = c.NodesLookup;
        }

        public void AcceptRoot(IVisitor visitor)
        {
            _assemblyInfo.Accept(visitor);
        }

        public void Accept(int key, IVisitor visitor)
        {
            _lookup[key].Accept(visitor);
        }
    }
}