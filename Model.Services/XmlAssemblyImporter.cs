using System;
using System.Collections.Generic;
using System.Text;
using Model.Data;
using Model.Services.Interfaces;
using System.IO;
using System.Xml;
using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;

namespace Model.Services
{
    public class XmlAssemblyImporter: IAssemblyImporter
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
                return serializer.Deserialize<AssemblyInfo>(_path);
            }
        }
    }
}
