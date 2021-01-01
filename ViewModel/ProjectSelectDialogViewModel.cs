using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmDialogs;
using Services.Data;
using Services.Interfaces;
using System.Collections.ObjectModel;

namespace ViewModel
{
    public class ProjectSelectDialogViewModel : ViewModelBase, IModalDialogViewModel
    {
        private readonly IProjectsService _projectsService;
        private bool? _dialogResult;
        private Project _selectedItem;

        public ProjectSelectDialogViewModel(IProjectsService projectsService)
        {
            _projectsService = projectsService;
            SubmitCommand = new RelayCommand(SubmitCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
        }

        public bool? DialogResult
        {
            get => _dialogResult;
            private set
            {
                _dialogResult = value;
                RaisePropertyChanged(nameof(DialogResult));
            }
        }

        public ObservableCollection<Project> Projects => _projectsService.Projects;

        public Project SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public RelayCommand CancelCommand { get; }

        public RelayCommand SubmitCommand { get; }

        public void SubmitCommandExecute() => DialogResult = true;

        private void CancelCommandExecute() => DialogResult = false;
    }
}