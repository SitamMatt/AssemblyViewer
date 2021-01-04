using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Services.Interfaces
{
    public interface IDialogService
    {
        void ShowError(Exception Error, string Title);
        void ShowError(string Message, string Title);
        void ShowInfo(string Message, string Title);
        void ShowMessage(string Message, string Title);
        bool ShowQuestion(string Message, string Title);
        void ShowWarning(string Message, string Title);

        string SaveFile(string title, string filter);

        string OpenFile(string title, string filter);
        bool ShowDialog(INotifyPropertyChanged viewModel);
    }
}
