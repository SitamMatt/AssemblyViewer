using Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IAssemblyImporter
    {
        AssemblyInfo Import();
    }
}
