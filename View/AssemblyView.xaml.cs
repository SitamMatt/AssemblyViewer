using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    public partial class AssemblyView : UserControl
    {
        public AssemblyView()
        {
            InitializeComponent();
        }
        
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