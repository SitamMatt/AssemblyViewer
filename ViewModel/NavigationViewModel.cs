using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GalaSoft.MvvmLight;
using Model.Services;
using Model.Services.Data;
using Tester;

namespace ViewModel
{
    public class NavigationViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectService;
        private readonly IAssemblyInfoServiceCreator _assemblyInfoServiceCreator;

        public NavigationViewModel(IProjectsService projectsService, IAssemblyInfoServiceCreator assemblyInfoServiceCreator)
        {
            _projectService = projectsService;
            _assemblyInfoServiceCreator = assemblyInfoServiceCreator;
            _projectService.Projects.CollectionChanged += ProjectsOnCollectionChanged;
            Tabs = new ObservableCollection<MainViewModel>();
        }

        private void ProjectsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems == null) return;
            foreach (var newItem in e.NewItems)
            {
                if (newItem is Project project)
                    Tabs.Add(new MainViewModel(_assemblyInfoServiceCreator.Create(project.AssemblyInfo)));
            }
        }

        public ObservableCollection<MainViewModel> Tabs { get; set; }
    }
}