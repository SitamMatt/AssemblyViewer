using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Services.Interfaces;
using Model.Services;

namespace ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly IProjectsService _projectService;
        private IIoService _ioService;

        public MenuViewModel(IProjectsService projectsService, IIoService ioService)
        {
            _projectService = projectsService;
            _ioService = ioService;
            ExitCommand = new RelayCommand(ExitCommandExecute, () => true);
            OpenCommand = new RelayCommand(OpenCommandExecute, () => true);
            CloseCommand = new RelayCommand(CloseCommandExecute, () => true);
            ExportXmlCommand = new RelayCommand(ExportXmlCommandExecute, () => true);
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
            var filename = _ioService.OpenFileDialog();
            _projectService.OpenDll(filename);
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
            var filename = _ioService.OpenFileDialog();
            if (filename == null) return;
            _projectService.Export(Guid.NewGuid(), new XmlAssemblyExporter(filename));
            //TODO: show result dialog
        }

        public RelayCommand ImportXmlCommand
        {
            get;
            private set;
        }

        protected void ImportXmlCommandExecute()
        {

        }
    }
}
