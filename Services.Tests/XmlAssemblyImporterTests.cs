using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Model.Data;
using Attribute = System.Reflection.FieldAttributes;

namespace Services.Tests
{
    class XmlAssemblyImporterTests
    {
        private readonly string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><AssemblyInfo xmlns:sys=\"https://extendedxmlserializer.github.io/system\" xmlns:exs=\"https://extendedxmlserializer.github.io/v2\" exs:identity=\"3\" Guid=\"4f71ea04-18a1-4d28-bfb8-0c7bf77b989e\" Name=\"TestAssembly\" xmlns=\"clr-namespace:Model.Data;assembly=Model\"><Lookup><sys:Item Key=\"b39b4846-1e1e-407f-880a-6ef2c7c08ec3\"><Value exs:type=\"TypeInfo\" exs:identity=\"1\" Guid=\"b39b4846-1e1e-407f-880a-6ef2c7c08ec3\" Name=\"Tester.A\" Attributes=\"NotPublic\"><Fields Capacity=\"1\"><FieldInfo exs:identity=\"5\" Guid=\"f954e8e0-6ea2-4527-a7c1-a47310d3cd27\" Name=\"b\" Attributes=\"Public\"><Type exs:identity=\"2\" Guid=\"62740ab3-d166-4a79-8ed7-d25d90cc0531\" Name=\"Tester.B\" Attributes=\"NotPublic\"><Fields Capacity=\"1\"><FieldInfo exs:identity=\"6\" Guid=\"80a0afe8-74dd-4c37-be95-5047b9d4c396\" Name=\"a\" Attributes=\"Public\"><Type exs:reference=\"1\" /><DeclaringType exs:reference=\"2\" /></FieldInfo></Fields></Type><DeclaringType exs:reference=\"1\" /></FieldInfo></Fields></Value></sys:Item><sys:Item Key=\"62740ab3-d166-4a79-8ed7-d25d90cc0531\"><Value exs:type=\"TypeInfo\" exs:reference=\"2\" /></sys:Item><sys:Item Key=\"80a0afe8-74dd-4c37-be95-5047b9d4c396\"><Value exs:type=\"FieldInfo\" exs:reference=\"6\" /></sys:Item><sys:Item Key=\"f954e8e0-6ea2-4527-a7c1-a47310d3cd27\"><Value exs:type=\"FieldInfo\" exs:reference=\"5\" /></sys:Item><sys:Item Key=\"30c2863e-5063-4066-9a16-6a0170259aaf\"><Value exs:type=\"ModuleInfo\" exs:identity=\"4\" Guid=\"30c2863e-5063-4066-9a16-6a0170259aaf\" Name=\"TestModule\"><Types Capacity=\"2\"><TypeInfo exs:reference=\"1\" /><TypeInfo exs:reference=\"2\" /></Types></Value></sys:Item><sys:Item Key=\"4f71ea04-18a1-4d28-bfb8-0c7bf77b989e\"><Value exs:type=\"AssemblyInfo\" exs:reference=\"3\" /></sys:Item></Lookup><Modules Capacity=\"1\"><ModuleInfo exs:reference=\"4\" /></Modules></AssemblyInfo>";

        private AssemblyInfo expectedAsm;
        private ModuleInfo moduleInfo;
        private TypeInfo typeA;
        private TypeInfo typeB;
        private FieldInfo fieldA;
        private FieldInfo fieldB;

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

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [Test]
        public void ImportTest()
        {
            var stream = GenerateStreamFromString(xml);
            var importer = new XmlAssemblyImporter(stream);
            var assemblyInfo = importer.Import();
            assemblyInfo
                .Should()
                .BeEquivalentTo(expectedAsm, opt => opt.IgnoringCyclicReferences().Excluding(x => x.Lookup));
            assemblyInfo.Lookup
                .Should()
                .BeEquivalentTo(expectedAsm.Lookup);
            assemblyInfo.Lookup[Guid.Parse("62740ab3-d166-4a79-8ed7-d25d90cc0531")]
                .Should()
                .BeEquivalentTo(typeB, opt => opt.ExcludingNestedObjects().IgnoringCyclicReferences());
            assemblyInfo.Lookup[Guid.Parse("b39b4846-1e1e-407f-880a-6ef2c7c08ec3")]
                .Should()
                .BeEquivalentTo(typeA, opt => opt.ExcludingNestedObjects().IgnoringCyclicReferences());
        }
    }
}
