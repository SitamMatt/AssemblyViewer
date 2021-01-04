using Model.Data;
using Model.VisitorPattern;
using Moq;
using NUnit.Framework;
using Services.Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Services.Tests
{
    class ProjectServiceTests
    {
        [Test]
        public void ExportTest()
        {
            var aInfo = new Mock<AssemblyInfo>();
            var assembly = new Mock<Assembly>();

            var pService = new ProjectsService();
            var exporter = new Mock<IAssemblyExporter>();
            exporter.Setup(x => x.Export(aInfo.Object));

            var importer = new Mock<IAssemblyImporter>();
            importer.Setup(x => x.Import()).Returns(aInfo.Object);

            pService.Import(importer.Object);

            pService.Export(pService.Projects[0].Guid, exporter.Object);
            exporter.Verify(x => x.Export(aInfo.Object), Times.Once);

        }

        [Test]
        public void ImportTest()
        {
            var aInfo = new Mock<AssemblyInfo>();
            var assembly = new Mock<Assembly>();

            var pService = new ProjectsService();
            var importer = new Mock<IAssemblyImporter>();
            importer.Setup(x => x.Import()).Returns(aInfo.Object);
            Assert.IsEmpty(pService.Projects);
            pService.Import(importer.Object);
            Assert.IsNotEmpty(pService.Projects);
            Assert.AreEqual(pService.Projects.Count, 1);
            importer.Verify(x => x.Import(), Times.Once);
            
        }

        [Test]
        public void CloseProjectTest()
        {
            var assembly = new Mock<Assembly>();
            var aInfo = new Mock<AssemblyInfo>();

            var pService = new ProjectsService();
            var importer = new Mock<IAssemblyImporter>();
            importer.Setup(x => x.Import()).Returns(aInfo.Object);

            pService.Import(importer.Object);

            Assert.AreEqual(pService.Projects.Count, 1);
            String pName = pService.Projects[0].Name;

            pService.CloseProject(pName);

            Assert.IsEmpty(pService.Projects);
        }
    }
}
