using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Model.Services;
using ViewModel.Visitors;
using Model.Services.Data;
using Model.Services.Wpf;
using Model.Services.Interfaces;
using ViewModel;
using MvvmDialogs;

namespace View
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NavigationViewModel>();
            SimpleIoc.Default.Register<IAssemblyInfoService, AssemblyInfoService>();
            SimpleIoc.Default.Register<MenuViewModel>();
            SimpleIoc.Default.Register<IProjectsService, ProjectsService>();
            SimpleIoc.Default.Register<IIoService, IOService>();
            SimpleIoc.Default.Register<IAssemblyInfoServiceCreator, AssemblyInfoServiceCreator>();
            SimpleIoc.Default.Register<ITreeConverterVisitor, TreeConverterVisitor>();
            SimpleIoc.Default.Register<ITreeItemsConverterVisitor, TreeItemsConvertersVisitor>();
            SimpleIoc.Default.Register<IDialogService>(() => new DialogService(dialogTypeLocator: new DialogLocator()));
            SimpleIoc.Default.Register<ProjectSelectDialogViewModel>();
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
