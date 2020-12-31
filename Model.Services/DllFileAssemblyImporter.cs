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

        public DllFileAssemblyImporter(string path)
        {
            this.path = path;
        }

        public AssemblyInfo Import()
        {
            var assembly = Assembly.LoadFile(path);
            var converter = new Converter();
            var asmInfo = converter.Convert(assembly);
            return asmInfo;
        }
    }
}
