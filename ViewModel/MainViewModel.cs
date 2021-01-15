using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Services.Interfaces;
using ViewModel.Data;
using ViewModel.Visitors;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAssemblyInfoService _assemblyInfoService;
        private readonly ITreeItemsConverterVisitor _childrenConverter;

        private readonly List<TreeNode> _nodesList;

        private ObservableCollection<TreeNode> _root;

        public MainViewModel(IAssemblyInfoService assemblyInfoService, ITreeConverterVisitor treeConverterVisitor,
            ITreeItemsConverterVisitor itemsConverter)
        {
            _assemblyInfoService = assemblyInfoService;
            _childrenConverter = itemsConverter;
            _assemblyInfoService.AcceptRoot(treeConverterVisitor);
            Root = new ObservableCollection<TreeNode>
            {
                treeConverterVisitor.Result
            };
            _nodesList = new List<TreeNode>
            {
                treeConverterVisitor.Result
            };
            ExpandCommand = new RelayCommand<TreeNode>(ExecuteExpandCommand, param => true);
        }

        public string Name => Root[0].Name;
        public virtual Guid Guid => Root[0].Guid;

        public ObservableCollection<TreeNode> Root
        {
            get => _root;
            set
            {
                _root = value;
                RaisePropertyChanged(nameof(Root));
            }
        }

        public RelayCommand<TreeNode> ExpandCommand { get; }

        private void ExecuteExpandCommand(TreeNode node)
        {
            var h = _nodesList.Contains(node);
            if (!_nodesList.Contains(node)) return;
            if (node.Guid != Guid.Empty)
            {
                _assemblyInfoService.Accept(node.Guid, _childrenConverter);
                node.Children.Clear();
                node.Children.AddRange(_childrenConverter.Result);
            }
            _nodesList.AddRange(node.Children);
        }
    }
}