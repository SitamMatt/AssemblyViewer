using Model.Data;

namespace Model.Services.Data
{
    public class AssemblyInfoServiceCreator : IAssemblyInfoServiceCreator
    {
        public IAssemblyInfoService Create(AssemblyInfo assemblyInfo)
        {
            return new AssemblyInfoService(assemblyInfo);
        }
    }
}