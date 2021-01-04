using System;
using System.Linq;
using Model.Data;
using NUnit.Framework;
using ViewModel.Visitors;

namespace ViewModel.Tests
{
    public class TreeConverterVisitorTests
    {
        [Test]
        public void AssemblyInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var asminfo = new AssemblyInfo
            {
                Name = "Asm name",
                Modules = null,
                Guid = Guid.NewGuid()
            };
            vis.Handle(asminfo);
            Assert.AreEqual("Asm name", vis.Result.Name);
            Assert.AreEqual(asminfo.Guid, vis.Result.Guid);
            Assert.IsNull(vis.Result.Children);
        }    
        
        [Test]
        public void ModuleInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var moduleInfo = new ModuleInfo()
            {
                Name = "Module name",
                Types = null,
                Guid = Guid.NewGuid()
            };
            vis.Handle(moduleInfo);
            Assert.AreEqual("Module name", vis.Result.Name);
            Assert.AreEqual(moduleInfo.Guid, vis.Result.Guid);
            Assert.IsNull(vis.Result.Children);
        }  
        
        [Test]
        public void TypeInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var typeInfo = new TypeInfo()
            {
                Name = "Type name",
                Guid = Guid.NewGuid()
            };
            vis.Handle(typeInfo);
            Assert.AreEqual("Type name", vis.Result.Name);
            Assert.AreEqual(typeInfo.Guid, vis.Result.Guid);
            Assert.IsNull(vis.Result.Children);
        }  
        
        [Test]
        public void FieldInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var fieldInfo = new FieldInfo()
            {
                Name = "Field name",
                Guid = Guid.NewGuid()
            };
            vis.Handle(fieldInfo);
            Assert.AreEqual("Field name", vis.Result.Name);
            Assert.AreEqual(fieldInfo.Guid, vis.Result.Guid);
            Assert.AreEqual(1, vis.Result.Children.Count);
            Assert.AreEqual("Dummy", vis.Result.Children.Single().Name);
        }  
    }
}