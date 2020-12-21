using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Model.Services;
using ViewModel.Visitors;
using Model.Services.Data;
using Model.Services.Wpf;

namespace ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IAssemblyInfoService, AssemblyInfoService>();
            SimpleIoc.Default.Register<MenuViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get => SimpleIoc.Default.GetInstance<MainViewModel>();
        }

        public MenuViewModel MenuViewModel
        {
            get => SimpleIoc.Default.GetInstance<MenuViewModel>();
        }
    }
}
