using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IAssemblyConverterFactory
    {
        IAssemblyConverter Create();
    }
}
