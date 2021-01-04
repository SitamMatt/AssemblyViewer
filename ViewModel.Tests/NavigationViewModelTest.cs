using Model.Data;
using Moq;
using NUnit.Framework;
using Services;
using Services.Data;
using Services.Factory;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModel.Visitors;

namespace ViewModel.Tests
{
    public class NavigationViewModelTest
    {
        [Test]
        public void CloseCommandExecute()
        {
            var projectServiceMock = new Mock<IProjectsService>();
            var mainViewModelMock = new Mock<MainViewModel>();

            var guid = Guid.NewGuid();

            projectServiceMock.Setup(x => x.Projects)
                .Returns(new ObservableCollection<Project>());

            mainViewModelMock.Setup(x => x.Guid)
                .Returns(guid);

            var vm = new NavigationViewModel(projectServiceMock.Object,null,null,null);
            Assert.False(vm.CloseTabCommand.CanExecute(null));
            Assert.True(vm.CloseTabCommand.CanExecute(mainViewModelMock.Object));
            vm.CloseTabCommand.Execute(mainViewModelMock.Object);

            projectServiceMock.Verify(
                x => x.CloseProject(
                    It.Is<Guid>(y => y == guid)));
        }

        [Test]
        public void ProjectsChangedTest()
        {
            var projectServiceMock = new Mock<IProjectsService>();
            var assemblyInfoServiceCreatorMock = new Mock<IAssemblyInfoServiceCreator>();
            var treeConverterVisitorMock = new Mock<ITreeConverterVisitor>();
            var treeItemsConverterVisitorMock = new Mock<ITreeItemsConverterVisitor>();
            var assemblyInfoServiceMock = new Mock<IAssemblyInfoService>();
            var mainViewModelMock = new Mock<MainViewModel>();

            var guid = Guid.NewGuid();
            var collection = new ObservableCollection<Project>();
            var project = new Project
            {
                Guid = guid
            };

            projectServiceMock.Setup(x => x.Projects)
                .Returns(collection);

            assemblyInfoServiceCreatorMock.Setup(x => x.Create(
                It.IsAny<AssemblyInfo>()))
                .Returns(assemblyInfoServiceMock.Object);

            mainViewModelMock.Setup(x => x.Guid)
                .Returns(guid);

            var vm = new NavigationViewModel(
                projectServiceMock.Object,
                assemblyInfoServiceCreatorMock.Object,
                treeConverterVisitorMock.Object,
                treeItemsConverterVisitorMock.Object);

            Assert.AreEqual(0, vm.Tabs.Count);

            collection.Add(project);

            Assert.AreEqual(1, vm.Tabs.Count);

            assemblyInfoServiceCreatorMock.Verify(
                x => x.Create(It.IsAny<AssemblyInfo>()));
        }
    }
}
