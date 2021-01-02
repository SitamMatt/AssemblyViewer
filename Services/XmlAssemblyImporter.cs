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
        private readonly Stream stream;

        public XmlAssemblyImporter(Stream stream)
        {
            this.stream = stream;
        }

        public AssemblyInfo Import()
        {
            IExtendedXmlSerializer serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableReferences()
                .Create();
            return serializer.Deserialize<AssemblyInfo>(stream);
        }
    }
}
