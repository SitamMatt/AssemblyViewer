using Model.Data;
using Services.Interfaces;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Services
{
    public class DllAssemblyImporter : IAssemblyImporter
    {
        private readonly IAssemblyConverter converter;
        private readonly Assembly assembly;

        public DllAssemblyImporter(Assembly assembly, IAssemblyConverter converter)
        {
            this.assembly = assembly;
            this.converter = converter;
        }

        public AssemblyInfo Import()
        {
            var asmInfo = converter.Convert(assembly);
            return asmInfo;
        }
    }
}
