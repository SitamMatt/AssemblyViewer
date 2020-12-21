using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AvalonDock;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for NavigationWindow.xaml
    /// </summary>
    public partial class NavigationWindow : Window
    {
        public NavigationWindow()
        {
            InitializeComponent();
        }

        private void DockingManager_OnDocumentClosing(object? sender, DocumentClosingEventArgs e)
        {
            // MainViewModel mvm = e.Document.Content as MainViewModel;
            // mvm.Name
            // e.Cancel = true;
        }
    }
}
