using Model.Converters;
using Model.Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Services
{
    public class DllFileAssemblyImporter : IAssemblyImporter
    {
        private readonly Stream stream;
        private readonly IAssemblyConverter converter;

        public DllFileAssemblyImporter(Stream path, IAssemblyConverter converter)
        {
            this.stream = path;
            this.converter = converter;
        }

        public AssemblyInfo Import()
        {
            var assembly = AssemblyLoadContext.Default.LoadFromStream(stream);
            var asmInfo = converter.Convert(assembly);
            return asmInfo;
        }
    }
}
