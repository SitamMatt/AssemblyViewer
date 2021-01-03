using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using Model.Data;
using Services.Interfaces;
using System.IO;
using System.Xml;

namespace Services
{
    public class XmlAssemblyExporter : IAssemblyExporter
    {
        private readonly Stream stream;

        public XmlAssemblyExporter(Stream stream)
        {
            this.stream = stream;
        }
        public void Export(AssemblyInfo assemblyInfo)
        {
            IExtendedXmlSerializer serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableReferences()
                .Create();
            serializer.Serialize(
                new XmlWriterSettings { Indent = true },
                stream,
                assemblyInfo);
        }
    }
}
