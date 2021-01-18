using GalaSoft.MvvmLight.Ioc;
using ViewModel.Visitors;
using ViewModel;
using Services;
using Services.Interfaces;
using Services.Factory;
using System.IO.Abstractions;
using Services.Wpf;
using Microsoft.Win32;
using static ViewModel.MenuViewModel;
using System.Windows;

namespace View
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            // view models
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NavigationViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<ProjectSelectDialogViewModel>();

            SimpleIoc.Default.Register<SaveFile>(() =>
            {
                return (t, f) =>
                {
                    var dialog = new SaveFileDialog
                    {
                        Title = t,
                        Filter = f,
                    };
                    var result = dialog.ShowDialog();
                    return result == true ? dialog.FileName : null;
                };
            });
            SimpleIoc.Default.Register<OpenFile>(() =>
            {
                return (t, f) =>
                {
                    var dialog = new OpenFileDialog
                    {
                        Title = t,
                        Filter = f,
                        CheckFileExists = true
                    };
                    var result = dialog.ShowDialog();
                    return result == true ? dialog.FileName : null;
                };
            });
            SimpleIoc.Default.Register<Warn>(() =>
            {
                return (t) => MessageBox.Show(
                    t,
                    "Warning",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            });
            SimpleIoc.Default.Register<Inform>(() =>
            {
                return (t) => MessageBox.Show(
                    t,
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            });
            SimpleIoc.Default.Register<ShowSelectBox>(() =>
            {
                return vm =>
                {
                    var dialog = new SelectBoxDialog();
                    dialog.DataContext = vm;
                    vm.window = dialog;
                    dialog.ShowDialog();
                    return vm.SelectedItem;
                };
            });

            // services
            SimpleIoc.Default.Register<IAssemblyInfoService, AssemblyInfoService>();
            SimpleIoc.Default.Register<IProjectsService, ProjectsService>();
            SimpleIoc.Default.Register<IAssemblyInfoServiceCreator, AssemblyInfoServiceCreator>();
            SimpleIoc.Default.Register<ITreeConverterVisitor, TreeConverterVisitor>();
            SimpleIoc.Default.Register<ITreeItemsConverterVisitor, TreeItemsConvertersVisitor>();
            SimpleIoc.Default.Register<ILifetimeService, LifetimeService>();
            SimpleIoc.Default.Register<IAssemblyConverter, AssemblyConverter>();
            SimpleIoc.Default.Register<IFileSystem, FileSystem>();
            SimpleIoc.Default.Register<IAssemblyConverterFactory, AssemblyConverterFactory>();
            SimpleIoc.Default.Register<IDialogService>(() => {
                var service = new WpfDialogService();
                service.RegisterDialog<ProjectSelectDialog, ProjectSelectDialogViewModel>();
                return service;
            });
        }

        public MainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MainViewModel>();

        public MenuViewModel MenuViewModel => SimpleIoc.Default.GetInstance<MenuViewModel>();

        public NavigationViewModel NavigationViewModel => SimpleIoc.Default.GetInstance<NavigationViewModel>();

        public ProjectSelectDialogViewModel ProjectSelectDialogViewModel => SimpleIoc.Default.GetInstance<ProjectSelectDialogViewModel>();
    }
}
