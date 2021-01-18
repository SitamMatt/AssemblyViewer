using Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public delegate string SaveFile(string title, string filter);
    public delegate string OpenFile(string title, string filter);
    public delegate void Warn(string title);
    public delegate void Inform(string title);
    public delegate Project ShowSelectBox(ProjectSelectDialogViewModel viewModel);
}
