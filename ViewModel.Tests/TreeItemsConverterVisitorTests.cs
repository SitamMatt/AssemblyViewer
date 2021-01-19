using System.Collections.Generic;
using Model.Data;
using Moq;
using NUnit.Framework;
using ViewModel.Visitors;

namespace ViewModel.Tests
{
    public class TreeItemsConverterVisitorTests
    {
        private AssemblyInfo asmInfo = new AssemblyInfo {Name = "ASM"};
        private ModuleInfo modInfo = new ModuleInfo {Name = "MOD"};
        private TypeInfo typeInfo = new TypeInfo {Name = "TYP"};
        private FieldInfo fieldInfo = new FieldInfo {Name = "FIL"};

        private Mock<ITreeConverterVisitor> _mock;

        [SetUp]
        public void Setup()
        {
            asmInfo.Modules = new List<ModuleInfo> {modInfo};
            modInfo.Types = new List<TypeInfo> {typeInfo};
            typeInfo.Fields = new List<FieldInfo> {fieldInfo};
            fieldInfo.Type = typeInfo;
            
            _mock = new Mock<ITreeConverterVisitor>();
        }
        [Test]
        public void AssemblyInfoConverterTest()
        {
            var vis = new TreeItemsConvertersVisitor(_mock.Object);
            vis.Handle(asmInfo);
            _mock.Verify(x => x.Handle(It.IsAny<AssemblyInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<ModuleInfo>()), Times.Once);
            _mock.Verify(x => x.Handle(It.IsAny<TypeInfo>()), Times.Never);
            Assert.AreEqual(1, vis.Result.Count);
        }
        
        [Test]
        public void ModuleInfoConverterTest()
        {
            var vis = new TreeItemsConvertersVisitor(_mock.Object);
            vis.Handle(modInfo);
            _mock.Verify(x => x.Handle(It.IsAny<AssemblyInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<ModuleInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<TypeInfo>()), Times.Once);
            _mock.Verify(x => x.Handle(It.IsAny<FieldInfo>()), Times.Never);
            Assert.AreEqual(1, vis.Result.Count);
        }
        
        [Test]
        public void TypeInfoConverterTest()
        {
            var vis = new TreeItemsConvertersVisitor(_mock.Object);
            vis.Handle(typeInfo);
            _mock.Verify(x => x.Handle(It.IsAny<AssemblyInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<ModuleInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<TypeInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<FieldInfo>()), Times.Once);
            Assert.AreEqual(2, vis.Result.Count);
        }
        
        [Test]
        public void FieldInfoConverterTest()
        {
            var vis = new TreeItemsConvertersVisitor(_mock.Object);
            vis.Handle(fieldInfo);
            _mock.Verify(x => x.Handle(It.IsAny<AssemblyInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<ModuleInfo>()), Times.Never);
            _mock.Verify(x => x.Handle(It.IsAny<TypeInfo>()), Times.Once);
            _mock.Verify(x => x.Handle(It.IsAny<FieldInfo>()), Times.Never);
            Assert.AreEqual(2, vis.Result.Count);
        }
    }
}