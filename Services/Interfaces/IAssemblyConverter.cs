using Model.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Services.Interfaces
{
    public interface IAssemblyConverter
    {
        AssemblyInfo Convert(Assembly assembly);
    }
}
