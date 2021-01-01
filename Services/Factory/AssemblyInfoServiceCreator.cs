using Model.Data;
using Services.Interfaces;

namespace Services.Factory
{
    public class AssemblyInfoServiceCreator : IAssemblyInfoServiceCreator
    {
        public IAssemblyInfoService Create(AssemblyInfo assemblyInfo)
        {
            return new AssemblyInfoService(assemblyInfo);
        }
    }
}