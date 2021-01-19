using GalaSoft.MvvmLight;
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
        private readonly ILifetimeService lifetimeService;
        private readonly IFileSystem fileSystem;
        private readonly IAssemblyConverterFactory assemblyConverterFactory;

        public MenuViewModel(IProjectsService projectsService,
            ILifetimeService lifetimeService, IFileSystem fileSystem,
            IAssemblyConverterFactory assemblyConverterFactory, SaveFile saveFileDelegate, Warn warnDelegate, Inform informDelegate, OpenFile openFileDelegate,
            ShowSelectBox showSelectBoxDelegate)
        {
            this.projectService = projectsService;
            this.lifetimeService = lifetimeService;
            this.fileSystem = fileSystem;
            this.assemblyConverterFactory = assemblyConverterFactory;
            this.saveFileDelegate = saveFileDelegate;
            this.warnDelegate = warnDelegate;
            this.informDelegate = informDelegate;
            this.openFileDelegate = openFileDelegate;
            this.showSelectBoxDelegate = showSelectBoxDelegate;
            ExitCommand = new RelayCommand(ExitCommandExecute, () => true);
            OpenCommand = new RelayCommand(OpenCommandExecute, () => true);
            ExportXmlCommand = new RelayCommand(ExportXmlCommandExecute, () => true);
            ImportXmlCommand = new RelayCommand(ImportXmlCommandExecute, () => true);
        }

        private readonly SaveFile saveFileDelegate;
        private readonly Warn warnDelegate;
        private readonly Inform informDelegate;
        private readonly OpenFile openFileDelegate;
        private readonly ShowSelectBox showSelectBoxDelegate;

        public RelayCommand ExitCommand { get; }

        public RelayCommand ExportXmlCommand { get; }

        public RelayCommand ImportXmlCommand { get; }

        public RelayCommand OpenCommand { get; }

        protected void ExitCommandExecute() => lifetimeService.Exit(0);

        protected void ExportXmlCommandExecute()
        {
            if (projectService.Projects.IsEmpty())
            {
                warnDelegate("No available projects to select");
                return;
            }
            var project = showSelectBoxDelegate(new ProjectSelectDialogViewModel(projectService));
            if (project == null) return;
            var filename = saveFileDelegate("Select xml file to save", "XML files (*.xml)|*.xml");
            if (!string.IsNullOrEmpty(filename))
            {
                using (var fs = fileSystem.File.Open(filename, FileMode.Create))
                {
                    projectService.Export(project.Guid, new XmlAssemblyExporter(fs));
                }
                informDelegate("Project succesfully exported");
            }
        }

        protected void ImportXmlCommandExecute()
        {
            var success = openFileDelegate("Select xml file to load", "XML files (*.xml)|*.xml");
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
            var success = openFileDelegate("Select assembly file to load", "Dll files (*.dll)|*.dll");
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
