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
                Modules = null
            };
            vis.Handle(asminfo);
            Assert.AreEqual("Asm name", vis.Result.Name);
            Assert.AreEqual(asminfo.GetHashCode(), vis.Result.Hash);
            Assert.IsNull(vis.Result.Children);
        }    
        
        [Test]
        public void ModuleInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var moduleInfo = new ModuleInfo()
            {
                Name = "Module name",
                Types = null
            };
            vis.Handle(moduleInfo);
            Assert.AreEqual("Module name", vis.Result.Name);
            Assert.AreEqual(moduleInfo.GetHashCode(), vis.Result.Hash);
            Assert.IsNull(vis.Result.Children);
        }  
        
        [Test]
        public void TypeInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var typeInfo = new TypeInfo()
            {
                Name = "Type name",
            };
            vis.Handle(typeInfo);
            Assert.AreEqual("Type name", vis.Result.Name);
            Assert.AreEqual(typeInfo.GetHashCode(), vis.Result.Hash);
            Assert.IsNull(vis.Result.Children);
        }  
        
        [Test]
        public void FieldInfoHandlingTest()
        {
            var vis = new TreeConverterVisitor();
            var fieldInfo = new FieldInfo()
            {
                Name = "Field name",
            };
            vis.Handle(fieldInfo);
            Assert.AreEqual("Field name", vis.Result.Name);
            Assert.AreEqual(fieldInfo.GetHashCode(), vis.Result.Hash);
            Assert.AreEqual(1, vis.Result.Children.Count);
            Assert.AreEqual("Dummy", vis.Result.Children.Single().Name);
        }  
    }
}