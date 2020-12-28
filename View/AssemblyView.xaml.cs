using System.Windows;
using System.Windows.Controls;
using Model.Data;
using ViewModel;
using ViewModel.Data;

namespace View
{
    public partial class AssemblyView : UserControl
    {
        public AssemblyView()
        {
            InitializeComponent();
        }
        
        
        public AssemblyInfo Asm { get; set; }
        private void ItemExpanded(object sender, RoutedEventArgs e)
        {
            var item = e.OriginalSource as TreeViewItem;
            var param = (item.DataContext as TreeNode);
            var vm = DataContext as MainViewModel;
            if (vm?.ExpandCommand.CanExecute(param) ?? false)
            {
                vm.ExpandCommand.Execute(param);
            }
        }
    }
}