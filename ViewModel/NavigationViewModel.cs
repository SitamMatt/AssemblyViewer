using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Model.Services.Data;
using Model.Services.Interfaces;
using ViewModel.Data;
using ViewModel.Visitors;

namespace ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectService;
        private readonly IAssemblyInfoServiceCreator _assemblyInfoServiceCreator;
        private MainViewModel _activeVm;

        public NavigationViewModel(IProjectsService projectsService, IAssemblyInfoServiceCreator assemblyInfoServiceCreator)
        {
            _projectService = projectsService;
            _assemblyInfoServiceCreator = assemblyInfoServiceCreator;
            _projectService.Projects.CollectionChanged += ProjectsOnCollectionChanged;
            Tabs = new ObservableCollection<MainViewModel>();
            CloseTabCommand = new RelayCommand<Object>(ExecuteCloseTabCommand, CanExecuteCloseTabCommand);
        }

        private void ExecuteCloseTabCommand(Object projectName)
        {
            _projectService.CloseProject(_activeVm.Name);
        }

        private bool CanExecuteCloseTabCommand(Object arg)
        {
            return true;
        }

        private void ProjectsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (var newItem in e.NewItems)
            {
                if (newItem is Project project)
                    Tabs.Add(new MainViewModel(
                        _assemblyInfoServiceCreator.Create(project.AssemblyInfo),
                        SimpleIoc.Default.GetInstance<ITreeConverterVisitor>(),
                        SimpleIoc.Default.GetInstance<ITreeItemsConverterVisitor>()
                    ));
            }
        }

        public ObservableCollection<MainViewModel> Tabs { get; set; }
        
        public RelayCommand<Object> CloseTabCommand { get; }

        public MainViewModel ActiveVM
        {
            get => _activeVm;
            set
            {
                _activeVm = value;
                RaisePropertyChanged(nameof(ActiveVM));
            }
        }
    }
}