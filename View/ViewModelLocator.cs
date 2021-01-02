using GalaSoft.MvvmLight.Ioc;
using ViewModel.Visitors;
using ViewModel;
using MvvmDialogs;
using Services;
using Services.Interfaces;
using Services.Factory;
using System.IO.Abstractions;

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

            // services
            SimpleIoc.Default.Register<IAssemblyInfoService, AssemblyInfoService>();
            SimpleIoc.Default.Register<IProjectsService, ProjectsService>();
            SimpleIoc.Default.Register<IAssemblyInfoServiceCreator, AssemblyInfoServiceCreator>();
            SimpleIoc.Default.Register<ITreeConverterVisitor, TreeConverterVisitor>();
            SimpleIoc.Default.Register<ITreeItemsConverterVisitor, TreeItemsConvertersVisitor>();
            SimpleIoc.Default.Register<ILifetimeService, LifetimeService>();
            SimpleIoc.Default.Register<IAssemblyConverter, AssemblyConverter>();
            SimpleIoc.Default.Register<IFileSystem, FileSystem>();
            SimpleIoc.Default.Register<IDialogService>(() => new DialogService(dialogTypeLocator: new DialogLocator()));
        }

        public MainViewModel MainViewModel
        {
            get => SimpleIoc.Default.GetInstance<MainViewModel>();
        }

        public MenuViewModel MenuViewModel
        {
            get => SimpleIoc.Default.GetInstance<MenuViewModel>();
        }

        public NavigationViewModel NavigationViewModel
        {
            get => SimpleIoc.Default.GetInstance<NavigationViewModel>();
        }

        public ProjectSelectDialogViewModel ProjectSelectDialogViewModel
        {
            get => SimpleIoc.Default.GetInstance<ProjectSelectDialogViewModel>();
        }
    }
}
