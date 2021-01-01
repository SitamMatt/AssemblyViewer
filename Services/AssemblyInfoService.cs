using System;
using Model.Data;
using Model.VisitorPattern;
using Services.Interfaces;

namespace Services
{
    public class AssemblyInfoService : IAssemblyInfoService
    {
        private readonly AssemblyInfo _assemblyInfo;

        public AssemblyInfoService(AssemblyInfo info)
        {
            _assemblyInfo = info;
        }

        public void AcceptRoot(IVisitor visitor)
        {
            _assemblyInfo.Accept(visitor);
        }

        public void Accept(Guid key, IVisitor visitor)
        {
            _assemblyInfo.Lookup[key].Accept(visitor);
        }
    }
}