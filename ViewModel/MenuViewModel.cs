using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using MvvmDialogs;
using GalaSoft.MvvmLight.Ioc;
using MvvmDialogs.FrameworkDialogs.SaveFile;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using Services.Interfaces;
using Services;
using System.IO.Abstractions;

namespace ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IProjectsService projectService;
        private readonly IDialogService dialogService;
        private readonly ILifetimeService lifetimeService;
        private readonly IFileSystem fileSystem;

        public MenuViewModel(IProjectsService projectsService, IDialogService dialogService, ILifetimeService lifetimeService, IFileSystem fileSystem)
        {
            this.projectService = projectsService;
            this.dialogService = dialogService;
            this.lifetimeService = lifetimeService;
            this.fileSystem = fileSystem;
            ExitCommand = new RelayCommand(ExitCommandExecute, () => true);
            OpenCommand = new RelayCommand(OpenCommandExecute, () => true);
            CloseCommand = new RelayCommand(CloseCommandExecute, () => true);
            ExportXmlCommand = new RelayCommand(ExportXmlCommandExecute, () => true);
            ImportXmlCommand = new RelayCommand(ImportXmlCommandExecute, () => true);
        }

        public RelayCommand CloseCommand { get; }

        public RelayCommand ExitCommand { get; }

        public RelayCommand ExportXmlCommand { get; }

        public RelayCommand ImportXmlCommand { get; }

        public RelayCommand OpenCommand { get; }

        protected void CloseCommandExecute() { }

        protected void ExitCommandExecute() => lifetimeService.Exit(0);

        protected void ExportXmlCommandExecute()
        {
            if (projectService.Projects.IsEmpty())
            {
                //dialogService.ShowMessageBox(this, "No projects opened", "No available projects to select", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                projectService.Export(project.Guid, new XmlAssemblyExporter(settings.FileName));
            }
            //dialogService.ShowMessageBox(
            //    this,
            //    "This is the text.",
            //    "This Is The Caption",
            //    MessageBoxButton.OK,
            //    MessageBoxImage.Information);
        }

        protected void ImportXmlCommandExecute()
        {
            var settings = new OpenFileDialogSettings
            {
                Title = "Select xml file to load",
                Filter = "XML files (*.xml)|*.xml",
                CheckFileExists = true,
            };
            var success = dialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                using (var file = fileSystem.File.OpenRead(settings.FileName))
                {
                    projectService.Import(new XmlAssemblyImporter(file));
                }
            }
        }

        protected void OpenCommandExecute()
        {
            var settings = new OpenFileDialogSettings
            {
                Title = "Select assembly file to load",
                Filter = "Dll files (*.dll)|*.dll",
                CheckFileExists = true,
            };
            var success = dialogService.ShowOpenFileDialog(this, settings);
            if (success == true)
            {
                projectService.Import(new DllFileAssemblyImporter(
                    settings.FileName,
                    SimpleIoc.Default.GetInstance<IAssemblyConverter>()));
            }
        }
    }
}
