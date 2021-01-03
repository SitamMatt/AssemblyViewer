using Model.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Attribute = System.Reflection.FieldAttributes;

namespace Services.Tests
{
    internal class XmlAssemblyExporterTests
    {
        private AssemblyInfo expectedAsm;
        private ModuleInfo moduleInfo;
        private TypeInfo typeA;
        private TypeInfo typeB;
        private FieldInfo fieldA;
        private FieldInfo fieldB;
        private readonly string expectedXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<AssemblyInfo xmlns:sys=\"https://extendedxmlserializer.github.io/system\" xmlns:exs=\"https://extendedxmlserializer.github.io/v2\" exs:identity=\"6\" Guid=\"4f71ea04-18a1-4d28-bfb8-0c7bf77b989e\" Name=\"TestAssembly\" xmlns=\"clr-namespace:Model.Data;assembly=Model\">\r\n  <Lookup>\r\n    <sys:Item Key=\"4f71ea04-18a1-4d28-bfb8-0c7bf77b989e\">\r\n      <Value exs:type=\"AssemblyInfo\" exs:reference=\"6\" />\r\n    </sys:Item>\r\n    <sys:Item Key=\"30c2863e-5063-4066-9a16-6a0170259aaf\">\r\n      <Value exs:type=\"ModuleInfo\" exs:identity=\"5\" Guid=\"30c2863e-5063-4066-9a16-6a0170259aaf\" Name=\"TestModule\">\r\n        <Types Capacity=\"4\">\r\n          <TypeInfo exs:identity=\"1\" Guid=\"b39b4846-1e1e-407f-880a-6ef2c7c08ec3\" Name=\"Tester.A\" Attributes=\"NotPublic\">\r\n            <Fields Capacity=\"4\">\r\n              <FieldInfo exs:identity=\"4\" Guid=\"f954e8e0-6ea2-4527-a7c1-a47310d3cd27\" Name=\"b\" Attributes=\"Public\">\r\n                <Type exs:identity=\"2\" Guid=\"62740ab3-d166-4a79-8ed7-d25d90cc0531\" Name=\"Tester.B\" Attributes=\"NotPublic\">\r\n                  <Fields Capacity=\"4\">\r\n                    <FieldInfo exs:identity=\"3\" Guid=\"80a0afe8-74dd-4c37-be95-5047b9d4c396\" Name=\"a\" Attributes=\"Public\">\r\n                      <Type exs:reference=\"1\" />\r\n                      <DeclaringType exs:reference=\"2\" />\r\n                    </FieldInfo>\r\n                  </Fields>\r\n                </Type>\r\n                <DeclaringType exs:reference=\"1\" />\r\n              </FieldInfo>\r\n            </Fields>\r\n          </TypeInfo>\r\n          <TypeInfo exs:reference=\"2\" />\r\n        </Types>\r\n      </Value>\r\n    </sys:Item>\r\n    <sys:Item Key=\"b39b4846-1e1e-407f-880a-6ef2c7c08ec3\">\r\n      <Value exs:type=\"TypeInfo\" exs:reference=\"1\" />\r\n    </sys:Item>\r\n    <sys:Item Key=\"62740ab3-d166-4a79-8ed7-d25d90cc0531\">\r\n      <Value exs:type=\"TypeInfo\" exs:reference=\"2\" />\r\n    </sys:Item>\r\n    <sys:Item Key=\"f954e8e0-6ea2-4527-a7c1-a47310d3cd27\">\r\n      <Value exs:type=\"FieldInfo\" exs:reference=\"4\" />\r\n    </sys:Item>\r\n    <sys:Item Key=\"80a0afe8-74dd-4c37-be95-5047b9d4c396\">\r\n      <Value exs:type=\"FieldInfo\" exs:reference=\"3\" />\r\n    </sys:Item>\r\n  </Lookup>\r\n  <Modules Capacity=\"4\">\r\n    <ModuleInfo exs:reference=\"5\" />\r\n  </Modules>\r\n</AssemblyInfo>";

        [SetUp]
        public void SetUp()
        {
            expectedAsm = new AssemblyInfo
            {
                Guid = Guid.Parse("4f71ea04-18a1-4d28-bfb8-0c7bf77b989e"),
                Name = "TestAssembly"
            };
            expectedAsm.Modules = new List<ModuleInfo>();
            expectedAsm.Lookup = new Dictionary<Guid, AsmComponent>();
            expectedAsm.Lookup[expectedAsm.Guid] = expectedAsm;
            moduleInfo = new ModuleInfo
            {
                Guid = Guid.Parse("30c2863e-5063-4066-9a16-6a0170259aaf"),
                Name = "TestModule"
            };
            moduleInfo.Types = new List<TypeInfo>();
            expectedAsm.Modules.Add(moduleInfo);
            expectedAsm.Lookup[moduleInfo.Guid] = moduleInfo;
            typeA = new TypeInfo
            {
                Guid = Guid.Parse("b39b4846-1e1e-407f-880a-6ef2c7c08ec3"),
                Name = "Tester.A",
            };
            moduleInfo.Types.Add(typeA);
            expectedAsm.Lookup[typeA.Guid] = typeA;
            typeB = new TypeInfo
            {
                Guid = Guid.Parse("62740ab3-d166-4a79-8ed7-d25d90cc0531"),
                Name = "Tester.B"
            };
            moduleInfo.Types.Add(typeB);
            expectedAsm.Lookup[typeB.Guid] = typeB;
            fieldA = new FieldInfo
            {
                Guid = Guid.Parse("f954e8e0-6ea2-4527-a7c1-a47310d3cd27"),
                Name = "b",
                Type = typeB,
                DeclaringType = typeA,
                Attributes = Attribute.Public
            };
            typeA.Fields = new List<FieldInfo> { fieldA };
            expectedAsm.Lookup[fieldA.Guid] = fieldA;
            fieldB = new FieldInfo
            {
                Guid = Guid.Parse("80a0afe8-74dd-4c37-be95-5047b9d4c396"),
                Name = "a",
                Type = typeA,
                DeclaringType = typeB,
                Attributes = Attribute.Public
            };
            typeB.Fields = new List<FieldInfo> { fieldB };
            expectedAsm.Lookup[fieldB.Guid] = fieldB;
        }

        [Test]
        public void ExportTest()
        {
            var stream = new MemoryStream();
            var exporter = new XmlAssemblyExporter(stream);
            exporter.Export(expectedAsm);
            stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            Assert.AreEqual(expectedXml, text);
        }
    }
}