using Model.Data;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Model.Services.Interfaces
{
    public interface IAssemblyConverter
    {
        AssemblyInfo Convert(Assembly assembly);
    }
}
