using System.Collections.ObjectModel;
using System.Linq;
using Model.Data;
using Model.Services;
using Model.VisitorPattern;
using Moq;
using NUnit.Framework;
using ViewModel.Data;
using ViewModel.Visitors;

namespace ViewModel.Tests
{
    public class MainViewModelTests
    {
        private Mock<IAssemblyInfoService> _assemblyInfoServiceMock;
        private Mock<ITreeConverterVisitor> _treeConverterVisitorMock;
        private Mock<ITreeItemsConverterVisitor> _treeItemsConverterVisitorMock;
        private TreeNode rootNode;
        private TreeNode dummyNode;
        private TreeNode customNode;
        private TreeNode externalNode;

        [SetUp]
        public void Setup()
        {
            dummyNode = new TreeNode {Name = "Dummy"};
            rootNode = new TreeNode
            {
                Name = "Root",
                Hash = 5,
                Children = new ObservableCollection<TreeNode> {dummyNode}
            };
            externalNode = new TreeNode {Name = "External"};
            customNode = new TreeNode {Name = "Custom"}; 
            _assemblyInfoServiceMock = new Mock<IAssemblyInfoService>();
            _treeConverterVisitorMock = new Mock<ITreeConverterVisitor>();
            _treeItemsConverterVisitorMock = new Mock<ITreeItemsConverterVisitor>();
            _treeConverterVisitorMock.Setup(x => x.Result)
                .Returns(() => rootNode);
            _treeItemsConverterVisitorMock.Setup(x => x.Result)
                .Returns(() => new ObservableCollection<TreeNode>{customNode});
        }

        [Test]
        public void ViewModelInitialization()
        {
            var vm = new MainViewModel(_assemblyInfoServiceMock.Object,
                _treeConverterVisitorMock.Object,
                _treeItemsConverterVisitorMock.Object);
            Assert.AreEqual(1, vm.Root.Count);
            Assert.AreEqual(rootNode, vm.Root.Single());
            Assert.AreEqual(1, vm.Root.Single().Children.Count);
            Assert.AreEqual(dummyNode, vm.Root.Single().Children.Single());
        }

        [Test]
        public void ExpandCommandTest()
        {
            var vm = new MainViewModel(_assemblyInfoServiceMock.Object,
                _treeConverterVisitorMock.Object,
                _treeItemsConverterVisitorMock.Object);
            vm.ExpandCommand.Execute(rootNode);
            Assert.AreEqual(1, vm.Root.Count);
            Assert.AreEqual(rootNode, vm.Root.Single());
            Assert.AreEqual(1, vm.Root.Single().Children.Count);
            Assert.AreNotEqual(dummyNode, vm.Root.Single().Children.Single());
        }
        
        [Test]
        public void ExpandCommandExternalNodeTest()
        {
            var vm = new MainViewModel(_assemblyInfoServiceMock.Object,
                _treeConverterVisitorMock.Object,
                _treeItemsConverterVisitorMock.Object);
            vm.ExpandCommand.Execute(externalNode);
            Assert.AreEqual(1, vm.Root.Count);
            Assert.AreEqual(rootNode, vm.Root.Single());
            Assert.AreEqual(1, vm.Root.Single().Children.Count);
            Assert.AreEqual(dummyNode, vm.Root.Single().Children.Single());
        }
    }
}