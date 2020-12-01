using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Model.Services;

namespace ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IAssemblyInfoService, AssemblyInfoService>();
        }

        public MainViewModel MainViewModel
        {
            get => SimpleIoc.Default.GetInstance<MainViewModel>();
        }

    }
}
