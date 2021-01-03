using ExtendedXmlSerializer;
using ExtendedXmlSerializer.Configuration;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Services.Tests
{
    public class AssemblyConverterTests
    {
        private Assembly assembly;

        [SetUp]
        public void Setup()
        {
            var source = @"
namespace Tester
{
    public class A
    {
        public B b;
    }

    public class B
    {
        public A a;
    }
}";
            var syntaxTree = CSharpSyntaxTree.ParseText(source);
            CSharpCompilation compilation = CSharpCompilation.Create(
                "assemblyName",
                new[] { syntaxTree },
                new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using(var ms = new MemoryStream())
            {
                var result = compilation.Emit(ms);
                if (result.Success)
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                }
            }
        }

        [Test]
        public void ConversionTest()
        {
            IExtendedXmlSerializer serializer = new ConfigurationContainer()
                .UseAutoFormatting()
                .UseOptimizedNamespaces()
                .EnableReferences()
                .Create();
            var converter = new AssemblyConverter();
            var assemblyInfo = converter.Convert(assembly);
            var str = serializer.Serialize(assemblyInfo);
            Assert.AreEqual(1, assemblyInfo.Modules.Count);
            Assert.AreEqual(2, assemblyInfo.Modules[0].Types.Count);
            var typeA = assemblyInfo.Modules[0].Types[0];
            var typeB = assemblyInfo.Modules[0].Types[1];
            Assert.AreEqual("Tester.A", typeA.Name);
            Assert.AreEqual("Tester.B", typeB.Name);
            Assert.AreEqual(typeB, typeA.Fields[0].Type);
            Assert.AreEqual(typeA, typeA.Fields[0].DeclaringType);
            Assert.AreEqual(typeA, typeB.Fields[0].Type);
            Assert.AreEqual(typeB, typeB.Fields[0].DeclaringType);
        }
    }
}