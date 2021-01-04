﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Services;
using Services.Interfaces;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime.Loader;

namespace ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IProjectsService projectService;
        private readonly IDialogService dialogService;
        private readonly ILifetimeService lifetimeService;
        private readonly IFileSystem fileSystem;
        private readonly IAssemblyConverterFactory assemblyConverterFactory;

        public MenuViewModel(IProjectsService projectsService, IDialogService dialogService,
            ILifetimeService lifetimeService, IFileSystem fileSystem,
            IAssemblyConverterFactory assemblyConverterFactory)
        {
            this.projectService = projectsService;
            this.dialogService = dialogService;
            this.lifetimeService = lifetimeService;
            this.fileSystem = fileSystem;
            this.assemblyConverterFactory = assemblyConverterFactory;
            ExitCommand = new RelayCommand(ExitCommandExecute, () => true);
            OpenCommand = new RelayCommand(OpenCommandExecute, () => true);
            ExportXmlCommand = new RelayCommand(ExportXmlCommandExecute, () => true);
            ImportXmlCommand = new RelayCommand(ImportXmlCommandExecute, () => true);
        }

        public RelayCommand ExitCommand { get; }

        public RelayCommand ExportXmlCommand { get; }

        public RelayCommand ImportXmlCommand { get; }

        public RelayCommand OpenCommand { get; }

        protected void ExitCommandExecute() => lifetimeService.Exit(0);

        protected void ExportXmlCommandExecute()
        {
            if (projectService.Projects.IsEmpty())
            {
                dialogService.ShowWarning("No available projects to select", "No projects opened");
                return;
            }
            var vm = new ProjectSelectDialogViewModel(projectService);
            var success = dialogService.ShowDialog(vm);
            if (!success) return;
            var project = vm.SelectedItem;
            var filename = dialogService.SaveFile("Select xml file to save", "XML files (*.xml)|*.xml");
            if (!string.IsNullOrEmpty(filename))
            {
                using (var fs = fileSystem.File.Open(filename, FileMode.Create))
                {
                    projectService.Export(project.Guid, new XmlAssemblyExporter(fs));
                }
                dialogService.ShowInfo("Project succesfully exported", "Success");
            }
        }

        protected void ImportXmlCommandExecute()
        {
            var success = dialogService.OpenFile("Select xml file to load", "XML files (*.xml)|*.xml");
            if (!string.IsNullOrEmpty(success))
            {
                using (var file = fileSystem.File.OpenRead(success))
                {
                    projectService.Import(new XmlAssemblyImporter(file));
                }
            }
        }

        protected void OpenCommandExecute()
        {
            var success = dialogService.OpenFile("Select assembly file to load", "Dll files (*.dll)|*.dll");
            if (!string.IsNullOrEmpty(success))
            {
                using (var file = fileSystem.File.OpenRead(success))
                {
                    var stream = AssemblyLoadContext.Default.LoadFromStream(file);
                    projectService.Import(new DllAssemblyImporter(
                    stream,
                    assemblyConverterFactory.Create()));
                }
            }
        }
    }
}
