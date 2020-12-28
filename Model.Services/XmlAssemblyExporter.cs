using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using Model.Data;
using Model.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Model.Services
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
            using(var fs = File.Create(_path))
            {
                serializer.Serialize(
                    new XmlWriterSettings { Indent = true },
                    fs,
                    assemblyInfo);
            }
        }
    }
}
