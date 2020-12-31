using MvvmDialogs.DialogTypeLocators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ViewModel;

namespace View
{
    class DialogLocator : IDialogTypeLocator
    {
        public Type Locate(INotifyPropertyChanged viewModel)
        {
            return viewModel switch
            {
                ProjectSelectDialogViewModel _ => typeof(ProjectSelectDialog),
                _ => null,
            };
        }
    }
}
