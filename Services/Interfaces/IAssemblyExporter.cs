using Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IAssemblyExporter
    {
        void Export(AssemblyInfo assemblyInfo);
    }
}
