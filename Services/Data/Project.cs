using System;
using Model.Data;

namespace Services.Data
{
    public class Project
    {
        public string Name => AssemblyInfo.Name;
        public Guid Guid { get; set; }

        public AssemblyInfo AssemblyInfo { get; set; }
    }
}