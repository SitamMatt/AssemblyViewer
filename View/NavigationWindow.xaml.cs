using System.Windows;
using AvalonDock;

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

        private void DockingManager_DocumentClosing(object sender, DocumentClosingEventArgs e)
        {

        }

        private void DockingManager_DocumentClosed(object sender, DocumentClosedEventArgs e)
        {

        }
    }
}
