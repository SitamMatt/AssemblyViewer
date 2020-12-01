using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        public MenuViewModel()
        {
            ExitCommand = new RelayCommand(ExitCommandExecute, () => true);
            OpenCommand = new RelayCommand(OpenCommandExecute, () => true);
            CloseCommand = new RelayCommand(CloseCommandExecute, () => true);
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
            
        }

        public RelayCommand CloseCommand
        {
            get;
            private set;
        }

        protected void CloseCommandExecute()
        {
            
        }
    }
}
