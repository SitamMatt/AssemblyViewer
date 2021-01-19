using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Moq;
using NUnit.Framework;
using Services;
using Services.Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace ViewModel.Tests
{
    public class MenuViewModelTests
    {
        private Stream assemblyStream;

        [SetUp]
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
            var ms = new MemoryStream();
            var result = compilation.Emit(ms);
            if (result.Success)
            {
                ms.Seek(0, SeekOrigin.Begin);
                assemblyStream = ms;
            }
        }

        [Test]
        public void ExitCommandTest()
        {
            var lifetimeMock = new Mock<ILifetimeService>();
            var vm = new MenuViewModel(null, lifetimeMock.Object, null, null, null, null, null, null, null);
            Assert.AreEqual(true, vm.ExitCommand.CanExecute(null));
            vm.ExitCommand.Execute(null);
            lifetimeMock.Verify(x => x.Exit(It.Is<int>(y => y == 0)));
        }
        
        [Test]
        public void OpenCommandTest()
        {
            var dialogServiceMock = new Mock<IDialogService>();
            var filesystemMock = Mock.Of<IFileSystem>();
            var fileMock = Mock.Of<IFile>();
            var projectServiceMock = new Mock<IProjectsService>();
            var assemblyConverterFactoryMock = new Mock<IAssemblyConverterFactory>();

            var openFileMock = new Mock<OpenFile>();

            dialogServiceMock.Setup(
                x => x.OpenFile(
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .Returns(@"F:\file.dll");

            var a = new OpenFile((x, y) => @"F:\file.dll");

            Mock.Get(filesystemMock)
                .Setup(x => x.File)
                .Returns(fileMock);

            Mock.Get(fileMock)
                .Setup(x => x.OpenRead(It.IsAny<string>()))
                .Returns(assemblyStream);


            var vm = new MenuViewModel(projectServiceMock.Object, null, filesystemMock, assemblyConverterFactoryMock.Object, null, null, null, a, null);
            
            Assert.AreEqual(true, vm.OpenCommand.CanExecute(null));

            vm.OpenCommand.Execute(null);

            //dialogServiceMock.Verify(x => x.OpenFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            
            Mock.Get(filesystemMock).Verify(
                x => x.File.OpenRead(It.Is<string>(y => y == @"F:\file.dll")),
                Times.Once);

            projectServiceMock.Verify(
                x => x.Import(
                    It.IsAny<DllAssemblyImporter>()),
                Times.Once);
        }

        [Test]
        public void ExportXmlCommandNoProjectsTest()
        {
            var dialogServiceMock = new Mock<IDialogService>();
            var filesystemMock = Mock.Of<IFileSystem>();
            var fileMock = Mock.Of<IFile>();
            var projectServiceMock = new Mock<IProjectsService>();

            var informMock = new Mock<Warn>();

            projectServiceMock.Setup(x => x.Projects)
                .Returns(new ObservableCollection<Project>());

            var vm = new MenuViewModel(projectServiceMock.Object, null, filesystemMock, null, null, null, null, null, null);

            Assert.AreEqual(true, vm.ExportXmlCommand.CanExecute(null));

            vm.ExportXmlCommand.Execute(null);

            projectServiceMock.Verify(x => x.Projects);

            //dialogServiceMock.Verify(x => x.ShowWarning(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void ExportXmlCommandTest()
        {
            var dialogServiceMock = new Mock<IDialogService>();
            var filesystemMock = Mock.Of<IFileSystem>();
            var fileMock = Mock.Of<IFile>();
            var projectServiceMock = new Mock<IProjectsService>();

            var warneMock = new Mock<Warn>();

            var saveFileMock = new Mock<SaveFile>();
            var b = new SaveFile((x, y) => @"F:\file.xml");


            Project project = new Project
            {
                Guid = Guid.Parse("199d0520-758b-4cf4-ab6a-4484d0b6fc0a")
            };

            var informMock = new Mock<Inform>();

            var showSelectBoxMock = new Mock<ShowSelectBox>();
            var d = new ShowSelectBox(x => project);

            projectServiceMock.Setup(x => x.Projects)
                .Returns(new ObservableCollection<Project>() { project });

            dialogServiceMock.Setup(
                x => x.ShowDialog(
                    It.IsAny<ProjectSelectDialogViewModel>())
                )
                .Callback(new InvocationAction(x => {
                    var b = x.Arguments[0] as ProjectSelectDialogViewModel;
                    b.SelectedItem = project;
                }))
                .Returns(true);

            dialogServiceMock.Setup(
                x => x.SaveFile(
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .Returns(@"F:\file.xml");

            Mock.Get(filesystemMock)
                .Setup(x => x.File)
                .Returns(fileMock);

            Mock.Get(fileMock)
                .Setup(x => x.Open(It.IsAny<string>(), It.IsAny<FileMode>()))
                .Returns(assemblyStream);

            var vm = new MenuViewModel(projectServiceMock.Object, null, filesystemMock, null, b, warneMock.Object.Invoke,
                informMock.Object.Invoke, null, d);

            Assert.AreEqual(true, vm.ExportXmlCommand.CanExecute(null));

            vm.ExportXmlCommand.Execute(null);

            projectServiceMock.Verify(x => x.Projects);

            //dialogServiceMock.Verify(x => x.ShowDialog(It.IsAny<ProjectSelectDialogViewModel>()), Times.Once);

            //dialogServiceMock.Verify(x => x.SaveFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            Mock.Get(filesystemMock).Verify(
                x => x.File.Open(It.Is<string>(y => y == @"F:\file.xml"), It.Is<FileMode>(y => y == FileMode.Create)),
                Times.Once);

            projectServiceMock.Verify(
                x => x.Export(
                    It.Is<Guid>(y => y == project.Guid),
                    It.IsAny<XmlAssemblyExporter>()),
                Times.Once);
        }

        [Test]
        public void ImportXmlCommandTest()
        {
            var dialogServiceMock = new Mock<IDialogService>();
            var filesystemMock = Mock.Of<IFileSystem>();
            var fileMock = Mock.Of<IFile>();
            var projectServiceMock = new Mock<IProjectsService>();

            var openFileMock = new Mock<OpenFile>();
            var a = new OpenFile((x, y) => @"F:\file.dll");

            dialogServiceMock.Setup(
                x => x.OpenFile(
                    It.IsAny<string>(),
                    It.IsAny<string>())
                )
                .Returns(@"F:\file.dll");

            Mock.Get(filesystemMock)
                .Setup(x => x.File)
                .Returns(fileMock);

            Mock.Get(fileMock)
                .Setup(x => x.OpenRead(It.IsAny<string>()))
                .Returns(assemblyStream);


            var vm = new MenuViewModel(projectServiceMock.Object, null, filesystemMock, null, null, null, null, a, null);

            Assert.AreEqual(true, vm.ImportXmlCommand.CanExecute(null));

            vm.ImportXmlCommand.Execute(null);

            //dialogServiceMock.Verify(x => x.OpenFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once);

            Mock.Get(filesystemMock).Verify(
                x => x.File.OpenRead(It.Is<string>(y => y == @"F:\file.dll")),
                Times.Once);

            projectServiceMock.Verify(
                x => x.Import(
                    It.IsAny<XmlAssemblyImporter>()),
                Times.Once);
        }
    }
}