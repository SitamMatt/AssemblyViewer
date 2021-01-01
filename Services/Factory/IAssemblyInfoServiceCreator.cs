using Model.Data;
using Services.Interfaces;

namespace Services.Factory
{
    public interface IAssemblyInfoServiceCreator
    {
        IAssemblyInfoService Create(AssemblyInfo assemblyInfo);
    }
}