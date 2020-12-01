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
            var col = new ObservableCollection<TreeNode>();
            foreach (var moduleInfo in typeInfo.Fields)
            {
                moduleInfo.Accept(_treeItemConverter);
                col.Add(_treeItemConverter.Result);
            }

            Result = col;
        }

        public void Handle(MethodInfo methodInfo)
        {
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
        }

        public void Handle(ParameterInfo parameterInfo)
        {
        }

        public void Handle(PropertyInfo propertyInfo)
        {
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