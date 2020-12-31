using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Model.Services.Interfaces;
using Model.Services;
using MvvmDialogs;
using GalaSoft.MvvmLight.Ioc;
using MvvmDialogs.FrameworkDialogs.SaveFile;

namespace ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectService;
        private IIoService _ioService;
        private readonly IDialogService dialogService; 

        public MenuViewModel(IProjectsService projectsService, IIoService ioService, IDialogService dialogService)
        {
            _projectService = projectsService;
            _ioService = ioService;
            this.dialogService = dialogService;
            ExitCommand = new RelayCommand(ExitCommandExecute, () => true);
            OpenCommand = new RelayCommand(OpenCommandExecute, () => true);
            CloseCommand = new RelayCommand(CloseCommandExecute, () => true);
            ExportXmlCommand = new RelayCommand(ExportXmlCommandExecute, () => true);
            ImportXmlCommand = new RelayCommand(ImportXmlCommandExecute, () => true);
        }

        public RelayCommand ExitCommand
        {
            get;
            private set;
        }

        protected void ExitCommandExecute()
        {
            
        }

        public RelayCommand OpenCommand
        {
            get;
            private set;
        }

        protected void OpenCommandExecute()
        {
            var settings = new SaveFileDialogSettings
            {
                Title = "Select assembly file to load",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Filter = "Dll files (*.dll)|*.dll",
                CheckFileExists = true
            };
            var success = dialogService.ShowSaveFileDialog(this, settings);
            if (success == true)
            {
                _projectService.Import(new DllFileAssemblyImporter(settings.FileName));
            }
        }

        public RelayCommand CloseCommand
        {
            get;
            private set;
        }

        protected void CloseCommandExecute()
        {
            
        }

        public RelayCommand ExportXmlCommand
        {
            get;
            private set;
        }

        protected void ExportXmlCommandExecute()
        {
            if (_projectService.Projects.IsEmpty())
            {
                dialogService.ShowMessageBox(this, "No projects opened", "No available projects to select", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var vm = SimpleIoc.Default.GetInstance<ProjectSelectDialogViewModel>();
            var success = dialogService.ShowDialog(this, vm);
            var project = vm.SelectedItem;
            var settings = new SaveFileDialogSettings
            {
                Title = "This Is The Title",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Text Documents (*.txt)|*.txt|All Files (*.*)|*.*",
                CheckFileExists = false
            };
            var success1 = dialogService.ShowSaveFileDialog(this, settings);
            if (success == true)
            {
                _projectService.Export(project.Guid, new XmlAssemblyExporter(settings.FileName));
            }
            dialogService.ShowMessageBox(
                this,
                "This is the text.",
                "This Is The Caption",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        public RelayCommand ImportXmlCommand
        {
            get;
            private set;
        }

        protected void ImportXmlCommandExecute()
        {
            var filename = _ioService.OpenFileDialog();
            if (filename == null) return;
            _projectService.Import(new XmlAssemblyImporter(filename));
        }
    }
}
