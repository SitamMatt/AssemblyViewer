using Model.Data;
using Model.Services.Interfaces;

namespace Model.Services.Data
{
    public interface IAssemblyInfoServiceCreator
    {
        IAssemblyInfoService Create(AssemblyInfo assemblyInfo);
    }
}