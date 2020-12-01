using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using Model.Services;
using ViewModel.Visitors;

namespace ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IAssemblyInfoService, AssemblyInfoService>();
            SimpleIoc.Default.Register<ITreeConverterVisitor, TreeConverterVisitor>();
            SimpleIoc.Default.Register<ITreeItemsConverterVisitor, TreeItemsConvertersVisitor>();
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
