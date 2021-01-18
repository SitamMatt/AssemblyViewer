using GalaSoft.MvvmLight;
using Services.Data;
using Services.Interfaces;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ProjectSelectDialogViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectsService;
        private Project _selectedItem;

        public ProjectSelectDialogViewModel(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        public ObservableCollection<Project> Projects => _projectsService.Projects;

        public IWindow window;

        public Project SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
                if (_selectedItem != null) window?.Close();
            }
        }
    }
}