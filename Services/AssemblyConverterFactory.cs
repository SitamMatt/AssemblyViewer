using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AssemblyConverterFactory : IAssemblyConverterFactory
    {
        public IAssemblyConverter Create()
        {
            return new AssemblyConverter();
        }
    }
}
