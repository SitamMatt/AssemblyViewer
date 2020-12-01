using System.Collections.ObjectModel;
using System.Linq;
using Model.Data;
using Model.Visitors;

namespace ViewModel.Visitors
{
    public class TreeItemsConvertersVisitor : IVisitor
    {
        private TreeConverterVisitor _treeItemConverter;

        public TreeItemsConvertersVisitor(TreeConverterVisitor treeItemConverter)
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

        public void Handle(MethodInfo assemblyInfo)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(FieldInfo fieldInfo)
        {
            var col = new ObservableCollection<TreeNode>();
            fieldInfo.Type.Accept(_treeItemConverter);
            col.Add(_treeItemConverter.Result);
            Result = col;
        }

        public void Handle(ConstructorInfo assemblyInfo)
        {
            throw new System.NotImplementedException();
        }

        public void Handle(PropertyInfo assemblyInfo)
        {
            throw new System.NotImplementedException();
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