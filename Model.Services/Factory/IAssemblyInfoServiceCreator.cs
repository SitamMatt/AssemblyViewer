using Model.Data;

namespace Model.Services.Data
{
    public interface IAssemblyInfoServiceCreator
    {
        IAssemblyInfoService Create(AssemblyInfo assemblyInfo);
    }
}