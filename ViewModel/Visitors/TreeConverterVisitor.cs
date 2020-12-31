using System;
using System.Collections.ObjectModel;
using Model.Data;
using Model.VisitorPattern;
using ViewModel.Data;

namespace ViewModel.Visitors
{
    public class TreeConverterVisitor : ITreeConverterVisitor
    {
        protected TreeNode DummyTreeNode = new TreeNode {Name = "Dummy"};

        protected TreeNode createBasicNode(AsmComponent component)
        {
            return new TreeNode {Name = component.Name, Guid = component.Guid};
        }

        public void Handle(AssemblyInfo assemblyInfo)
        {
            var result = createBasicNode(assemblyInfo);
            if (assemblyInfo.Modules != null && assemblyInfo.Modules.Count != 0) result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public void Handle(TypeInfo typeInfo)
        {
            var result = createBasicNode(typeInfo);
            if(typeInfo.Fields != null && typeInfo.Fields.Count != 0) result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public void Handle(MethodInfo methodInfo)
        {
            
        }

        public void Handle(FieldInfo fieldInfo)
        {
            var result = createBasicNode(fieldInfo);
            result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public void Handle(ConstructorInfo constructorInfo)
        {
            
        }

        public void Handle(ParameterInfo parameterInfo)
        {
            
        }

        public void Handle(PropertyInfo propertyInfo)
        {
            
        }

        public void Handle(ModuleInfo moduleInfo)
        {
            var result = createBasicNode(moduleInfo);
            if(moduleInfo.Types != null && moduleInfo.Types.Count != 0) result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
        }

        public TreeNode Result { get; set; }
    }
}