using Model.Converters;
using Model.Data;
using Model.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Model.Services
{
    public class DllFileAssemblyImporter : IAssemblyImporter
    {
        private readonly string path;
        private readonly IAssemblyConverter converter;

        public DllFileAssemblyImporter(string path, IAssemblyConverter converter)
        {
            this.path = path;
            this.converter = converter;
        }

        public AssemblyInfo Import()
        {
            var assembly = Assembly.LoadFile(path);
            var asmInfo = converter.Convert(assembly);
            return asmInfo;
        }
    }
}
