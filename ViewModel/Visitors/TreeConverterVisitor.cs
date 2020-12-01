using System;
using System.Collections.ObjectModel;
using Model.Data;
using Model.Visitors;

namespace ViewModel.Visitors
{
    public class TreeConverterVisitor : IVisitor
    {
        protected TreeNode DummyTreeNode = new TreeNode {Name = "Dummy"};
        public TreeNode Result { get; private set; }

        protected TreeNode createBasicNode(AsmComponent component)
        {
            return new TreeNode {Name = component.Name, Hash = component.GetHashCode()};
        }

        public void Handle(AssemblyInfo assemblyInfo)
        {
            var result = createBasicNode(assemblyInfo);
            if (assemblyInfo.Modules.Count != 0) result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public void Handle(TypeInfo typeInfo)
        {
            var result = createBasicNode(typeInfo);
            if(typeInfo.Fields.Count != 0) result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public void Handle(MethodInfo assemblyInfo)
        {
            throw new NotImplementedException();
        }

        public void Handle(FieldInfo fieldInfo)
        {
            var result = createBasicNode(fieldInfo);
            result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public void Handle(ConstructorInfo assemblyInfo)
        {
            throw new NotImplementedException();
        }

        public void Handle(PropertyInfo assemblyInfo)
        {
            throw new NotImplementedException();
        }

        public void Handle(ModuleInfo moduleInfo)
        {
            var result = createBasicNode(moduleInfo);
            if(moduleInfo.Types.Count != 0) result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }
    }
}