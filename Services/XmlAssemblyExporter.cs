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
        private readonly string _path;

        public XmlAssemblyExporter(string path)
        {
            _path = path;
        }
        public void Export(AssemblyInfo assemblyInfo)
        {
            //TODO: this should be DI'ed
            IExtendedXmlSerializer serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableReferences()
                .Create();
            //TODO: exceptions handling
            using (var fs = File.Create(_path))
            {
                serializer.Serialize(
                    new XmlWriterSettings { Indent = true },
                    fs,
                    assemblyInfo);
            }
        }
    }
}
