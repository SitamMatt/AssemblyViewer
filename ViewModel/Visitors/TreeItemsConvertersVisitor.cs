using System;
using System.Collections.ObjectModel;
using Model.Data;
using ViewModel.Data;

namespace ViewModel.Visitors
{
    public class TreeItemsConvertersVisitor : ITreeItemsConverterVisitor
    {
        private readonly ITreeConverterVisitor _treeItemConverter;

        public TreeItemsConvertersVisitor(ITreeConverterVisitor treeItemConverter)
        {
            _treeItemConverter = treeItemConverter;
        }

        public ObservableCollection<TreeNode> Result { get; set; }

        public void Handle(AssemblyInfo assemblyInfo)
        {
            var col = new ObservableCollection<TreeNode>();
            foreach (var moduleInfo in assemblyInfo.Modules)
            {
                moduleInfo.Accept(_treeItemConverter);
                col.Add(_treeItemConverter.Result);
            }

            Result = col;
        }

        public void Handle(TypeInfo typeInfo)
        {
            var children = new ObservableCollection<TreeNode>();
            foreach (var field in typeInfo.Fields)
            {
                field.Accept(_treeItemConverter);
                children.Add(_treeItemConverter.Result);
            }
            foreach (var property in typeInfo.Properties)
            {
                property.Accept(_treeItemConverter);
                children.Add(_treeItemConverter.Result);
            }
            foreach (var method in typeInfo.Methods)
            {
                method.Accept(_treeItemConverter);
                children.Add(_treeItemConverter.Result);
            }
            foreach (var constructor in typeInfo.Constructors)
            {
                constructor.Accept(_treeItemConverter);
                children.Add(_treeItemConverter.Result);
            }

            Result = children;
        }

        public void Handle(MethodInfo methodInfo)
        {
            var children = new ObservableCollection<TreeNode>();
            methodInfo.DeclaringType.Accept(_treeItemConverter);
            children.Add(_treeItemConverter.Result);
            methodInfo.ReturnType.Accept(_treeItemConverter);
            children.Add(new TreeNode
            {
                Name = "Return type",
                Children = new ObservableCollection<TreeNode> { _treeItemConverter.Result }
            });
            var parameters = new ObservableCollection<TreeNode>();
            foreach (var parameter in methodInfo.Parameters)
            {
                parameter.Accept(_treeItemConverter);
                parameters.Add(_treeItemConverter.Result);
            }
            children.Add(new TreeNode
            {
                Name = "Parameters",
                Children = parameters
            });

            Result = children;
        }

        public void Handle(FieldInfo fieldInfo)
        {
            var col = new ObservableCollection<TreeNode>();
            fieldInfo.Type.Accept(_treeItemConverter);
            col.Add(_treeItemConverter.Result);
            Result = col;
        }

        public void Handle(ConstructorInfo constructorInfo)
        {
            var children = new ObservableCollection<TreeNode>();
            constructorInfo.DeclaringType.Accept(_treeItemConverter);
            children.Add(_treeItemConverter.Result);
            if(constructorInfo.Parameters.Count != 0)
            {
                var parameters = new ObservableCollection<TreeNode>();
                foreach (var parameter in constructorInfo.Parameters)
                {
                    parameter.Accept(_treeItemConverter);
                    parameters.Add(_treeItemConverter.Result);
                }
                children.Add(new TreeNode
                {
                    Name = "Parameters",
                    Children = parameters
                });
            }
            Result = children;
        }

        public void Handle(ParameterInfo parameterInfo)
        {
            var children = new ObservableCollection<TreeNode>();
            parameterInfo.Type.Accept(_treeItemConverter);
            children.Add(new TreeNode
            {
                Name = "Type",
                Children = new ObservableCollection<TreeNode> { _treeItemConverter.Result }
            });

            Result = children;
        }

        public void Handle(PropertyInfo propertyInfo)
        {
            var col = new ObservableCollection<TreeNode>();
            propertyInfo.Type.Accept(_treeItemConverter);
            col.Add(_treeItemConverter.Result);
            Result = col;
        }

        public void Handle(ModuleInfo moduleInfo)
        {
            var col = new ObservableCollection<TreeNode>();
            foreach (var typeInfo in moduleInfo.Types)
            {
                typeInfo.Accept(_treeItemConverter);
                col.Add(_treeItemConverter.Result);
            }

            Result = col;
        }
    }
}