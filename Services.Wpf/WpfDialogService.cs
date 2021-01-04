using Microsoft.Win32;
using MvvmDialogs;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using IDialogService = Services.Interfaces.IDialogService;

namespace Services.Wpf
{
    public class WpfDialogService : IDialogService
    {
        private readonly Dictionary<Type, Type> registeredDialogs = new Dictionary<Type, Type>();

        public void RegisterDialog<TView, TViewModel>()
        {
            registeredDialogs[typeof(TViewModel)] = typeof(TView);
        }

        public bool ShowDialog(INotifyPropertyChanged viewModel)
        {
            var dialogViewType = registeredDialogs[viewModel.GetType()];
            var window  = (Window)Activator.CreateInstance(dialogViewType);
            window.DataContext = viewModel;
            var result = window.ShowDialog();
            return result == true;
        }
        public string OpenFile(string title, string filter)
        {
            var dialog = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                CheckFileExists = true
            };
            var result = dialog.ShowDialog();
            return result == true ? dialog.FileName : null;
        }

        public string SaveFile(string title, string filter)
        {
            var dialog = new SaveFileDialog
            {
                Title = title,
                Filter = filter,
                CheckFileExists = true
            };
            var result = dialog.ShowDialog();
            return result == true ? dialog.FileName : null;
        }

        public void ShowError(Exception Error, string Title)
        {
            throw new NotImplementedException();
        }

        public void ShowError(string Message, string Title)
        {
            throw new NotImplementedException();
        }

        public void ShowInfo(string Message, string Title)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string Message, string Title)
        {
            throw new NotImplementedException();
        }

        public bool ShowQuestion(string Message, string Title)
        {
            throw new NotImplementedException();
        }

        public void ShowWarning(string Message, string Title)
        {
            MessageBox.Show(Message, Title, MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
