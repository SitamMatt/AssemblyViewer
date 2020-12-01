using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Extensions;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Model.Services;
using ViewModel.Visitors;

namespace ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAssemblyInfoService _assemblyInfoService;
        private readonly TreeItemsConvertersVisitor _childrenConverter;

        private readonly List<TreeNode> _nodesList;

        private ObservableCollection<TreeNode> _root;

        public MainViewModel(IAssemblyInfoService assemblyInfoService)
        {
            _assemblyInfoService = assemblyInfoService;
            var converter = new TreeConverterVisitor();
            _childrenConverter = new TreeItemsConvertersVisitor(converter);
            _assemblyInfoService.AcceptRoot(converter);
            Root = new ObservableCollection<TreeNode>
            {
                converter.Result
            };
            _nodesList = new List<TreeNode>
            {
                converter.Result
            };
            ExpandCommand = new RelayCommand<TreeNode>(ExecuteExpandCommand, param => true);
        }

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
            if (!_nodesList.Contains(node)) return;
            _assemblyInfoService.Accept(node.Hash, _childrenConverter);
            node.Children.Clear();
            node.Children.AddRange(_childrenConverter.Result);
            _nodesList.AddRange(_childrenConverter.Result);
        }
    }
}