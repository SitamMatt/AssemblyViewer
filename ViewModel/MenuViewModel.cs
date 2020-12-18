using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public RelayCommand ExitCommand
        {
            get;
            private set;
        }

        public void ExitCommandExecute()
        {
            Environment.Exit(0);
        }

        public RelayCommand OpenCommand
        {
            get;
            private set;
        }

        public void OpenCommandExecute()
        {
            var filename = _ioService.OpenFileDialog();
            _projectService.OpenDll(filename);
        }

        public RelayCommand CloseCommand
        {
            get;
            private set;
        }

        public void CloseCommandExecute()
        {
            
        }
    }
}
