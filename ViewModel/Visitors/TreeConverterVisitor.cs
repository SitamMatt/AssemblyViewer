using System;
using System.Collections.ObjectModel;
using Common;
using Model.Data;
using Model.VisitorPattern;
using ViewModel.Data;

namespace ViewModel.Visitors
{
    public class TreeConverterVisitor : ITreeConverterVisitor
    {
        protected static readonly TreeNode DummyTreeNode = new TreeNode {Name = "Dummy"};

        protected TreeNode CreateBasicNode(AsmComponent component)
        {
            return new TreeNode {Name = component.Name, Guid = component.Guid};
        }

        public object Handle(AssemblyInfo assemblyInfo)
        {
            var result = CreateBasicNode(assemblyInfo);
            if(BoolHelper.AnyOf(
                assemblyInfo.Modules
                )) result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public object Handle(TypeInfo typeInfo)
        {
            var result = CreateBasicNode(typeInfo);
            if (BoolHelper.AnyOf(
                typeInfo.Constructors,
                typeInfo.CustomAttributes,
                typeInfo.Fields,
                typeInfo.Methods,
                typeInfo.NestedTypes,
                typeInfo.Properties
                )) result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public object Handle(MethodInfo methodInfo)
        {
            var result = CreateBasicNode(methodInfo);
            result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;

        }

        public object Handle(FieldInfo fieldInfo)
        {
            var result = CreateBasicNode(fieldInfo);
            result.Children = new ObservableCollection<TreeNode> {DummyTreeNode};
            Result = result;
            return result;
        }

        public object Handle(ConstructorInfo constructorInfo)
        {
            var result = CreateBasicNode(constructorInfo);
            result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public object Handle(ParameterInfo parameterInfo)
        {
            var result = CreateBasicNode(parameterInfo);
            result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public object Handle(PropertyInfo propertyInfo)
        {
            var result = CreateBasicNode(propertyInfo);
            result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public object Handle(ModuleInfo moduleInfo)
        {
            var result = CreateBasicNode(moduleInfo);
            if(BoolHelper.AnyOf(
                moduleInfo.Types
                )) result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public object Handle(AttributeInfo moduleInfo)
        {
            var result = CreateBasicNode(moduleInfo);
            result.Children = new ObservableCollection<TreeNode> { DummyTreeNode };
            Result = result;
            return result;
        }

        public TreeNode Result { get; set; }
    }
}