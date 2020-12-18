using System;
using Model.Data;

namespace Model.Services.Data
{
    public class Project
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        
        public AssemblyInfo AssemblyInfo { get; set; }
    }
}