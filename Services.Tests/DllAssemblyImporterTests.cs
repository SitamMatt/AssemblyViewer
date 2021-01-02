using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Model.Data;
using Moq;
using NUnit.Framework;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace Services.Tests
{
    class DllAssemblyImporterTests
    {
        private Assembly assembly;


        public void SetUp()
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
            using (var ms = new MemoryStream())
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
        public void ImportTest()
        {
            var converterMock = new Mock<IAssemblyConverter>();
            converterMock.Setup(x => x.Convert(It.IsAny<Assembly>())).Returns(new AssemblyInfo());
            var importer = new DllAssemblyImporter(assembly, converterMock.Object);
            var result = importer.Import();
            converterMock.Verify(x => x.Convert(It.Is<Assembly>(x => x == assembly)), Times.Once);
        }
    }
}
