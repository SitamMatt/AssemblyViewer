using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;
using System.IO;
using System.Xml;
using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using Services.Interfaces;

namespace Services
{
    public class XmlAssemblyImporter : IAssemblyImporter
    {
        private readonly string _path;

        public XmlAssemblyImporter(string path)
        {
            _path = path;
        }

        public AssemblyInfo Import()
        {
            IExtendedXmlSerializer serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableReferences()
                .Create();
            using (var fs = File.Open(_path, FileMode.Open))
            {
                return serializer.Deserialize<AssemblyInfo>(fs);
            }
        }
    }
}
