using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Extensions;
using Model.Data;
using Model.VisitorPattern;
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

        public object Handle(AssemblyInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Modules", info.Modules));
            children.AddIfNotNull(CreateCategory("Custom Attributes", info.CustomAttributes));

            Result = children;
            return children;
        }

        public TreeNode CreateCategory(string name, IEnumerable<AsmComponent> items)
        {
            if (items.IsEmpty()) return null;
            var category = new TreeNode
            {
                Name = name,
                Children = new ObservableCollection<TreeNode>()
            };
            foreach (var item in items)
            {
                category.Children.Add(
                    item.Accept(_treeItemConverter) as TreeNode
                    );
            }
            return category;
        }

        public TreeNode CreateCategory(string name, AsmComponent item)
        {
            if (item == null) return null;
            
            var category = new TreeNode
            {
                Name = name,
                Children = new ObservableCollection<TreeNode>
                {
                    item.Accept(_treeItemConverter) as TreeNode
                }
            };
            return category;
        }

        public TreeNode CreateCategory(string name, string item)
        {
            if (item == null) return null;
            var category = new TreeNode
            {
                Name = name,
                Children = new ObservableCollection<TreeNode>
                {
                    new TreeNode
                    {
                        Name = item
                    }
                }
            };
            return category;
        }

        public object Handle(TypeInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Attributes", info.Attributes.ToString()));
            children.AddIfNotNull(CreateCategory("Methods", info.Methods));
            children.AddIfNotNull(CreateCategory("Fields", info.Fields));
            children.AddIfNotNull(CreateCategory("Properties", info.Properties));
            children.AddIfNotNull(CreateCategory("Constructors", info.Constructors));
            children.AddIfNotNull(CreateCategory("Custom Attributes", info.CustomAttributes));
            children.AddIfNotNull(CreateCategory("Nested Types", info.NestedTypes));

            Result = children;
            return children;
        }

        public object Handle(MethodInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Attributes", info.Attributes.ToString()));

            children.AddIfNotNull(CreateCategory("Declaring Type", info.DeclaringType));
            children.AddIfNotNull(CreateCategory("Return Type", info.ReturnType));
            children.AddIfNotNull(CreateCategory("Parameters", info.Parameters));
            children.AddIfNotNull(CreateCategory("Custom Attributes", info.CustomAttributes));

            Result = children;
            return children;
        }

        public object Handle(FieldInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Attributes", info.Attributes.ToString()));
            children.AddIfNotNull(CreateCategory("Type", info.Type));

            Result = children;
            return children;
        }

        public object Handle(ConstructorInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Attributes", info.Attributes.ToString()));

            info.DeclaringType.Accept(_treeItemConverter);
            children.Add(_treeItemConverter.Result);
            if(info.Parameters.Count != 0)
            {
                var parameters = new ObservableCollection<TreeNode>();
                foreach (var parameter in info.Parameters)
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
            return children;
        }

        public object Handle(ParameterInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Attributes", info.Attributes.ToString()));

            info.Type.Accept(_treeItemConverter);
            children.Add(new TreeNode
            {
                Name = "Type",
                Children = new ObservableCollection<TreeNode> { _treeItemConverter.Result }
            });

            Result = children;
            return children;
        }

        public object Handle(PropertyInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Attributes", info.Attributes.ToString()));

            info.Type.Accept(_treeItemConverter);
            children.Add(_treeItemConverter.Result);
            children.AddIfNotNull(CreateCategory("Custom Attributes", info.CustomAttributes));
            Result = children;
            return children;
        }

        public object Handle(ModuleInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            children.AddIfNotNull(CreateCategory("Defined Types", info.Types));
            Result = children;
            return children;
        }

        public object Handle(AttributeInfo info)
        {
            var children = new ObservableCollection<TreeNode>();
            info.TypeInfo.Accept(_treeItemConverter);
            children.Add(_treeItemConverter.Result);
            info.ConstructorInfo.Accept(_treeItemConverter);
            children.Add(_treeItemConverter.Result);
            Result = children;
            return children;
        }
    }
}