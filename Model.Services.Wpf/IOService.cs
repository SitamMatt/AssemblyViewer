using Microsoft.Win32;
using Model.Services.Interfaces;

namespace Model.Services.Wpf
{
    public class IOService : IIoService
    {
        public string OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() ?? false)
                return dialog.FileName;

            return null;
        }
    }
}