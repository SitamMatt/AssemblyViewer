using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Services.Data;
using Services.Factory;
using Services.Interfaces;
using ViewModel.Visitors;

namespace ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectService;
        private readonly IAssemblyInfoServiceCreator _assemblyInfoServiceCreator;
        private readonly ITreeConverterVisitor treeConverterVisitor;
        private readonly ITreeItemsConverterVisitor treeItemsConverterVisitor;

        public NavigationViewModel(IProjectsService projectsService, IAssemblyInfoServiceCreator assemblyInfoServiceCreator,
            ITreeConverterVisitor treeConverterVisitor, ITreeItemsConverterVisitor treeItemsConverterVisitor)
        {
            _projectService = projectsService;
            _assemblyInfoServiceCreator = assemblyInfoServiceCreator;
            this.treeConverterVisitor = treeConverterVisitor;
            this.treeItemsConverterVisitor = treeItemsConverterVisitor;
            _projectService.Projects.CollectionChanged += ProjectsOnCollectionChanged;
            Tabs = new ObservableCollection<MainViewModel>();
            CloseTabCommand = new RelayCommand<MainViewModel>(ExecuteCloseTabCommand, CanExecuteCloseTabCommand);
        }

        private void ExecuteCloseTabCommand(MainViewModel viewModel)
        {
            _projectService.CloseProject(viewModel.Guid);
        }

        private bool CanExecuteCloseTabCommand(MainViewModel arg)
        {
            return arg != null;
        }

        private void ProjectsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (var newItem in e.NewItems)
            {
                if (newItem is Project project)
                    Tabs.Add(new MainViewModel(
                        _assemblyInfoServiceCreator.Create(project.AssemblyInfo),
                        treeConverterVisitor,
                        treeItemsConverterVisitor
                    ));
            }
        }

        public ObservableCollection<MainViewModel> Tabs { get; set; }
        
        public RelayCommand<MainViewModel> CloseTabCommand { get; }
    }
}