using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Model.Services;
using Model.Services.Data;
using Model.Services.Wpf;

namespace ViewModel
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
    }
}
