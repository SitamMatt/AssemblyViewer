using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Model.Converters;
using Model.Services.Data;
using Model.Services.Interfaces;

namespace Model.Services
{
    public class ProjectsService : IProjectsService
    {
        public ObservableCollection<Project> Projects { get; }

        public void CloseProject(string projectName)
        {
            Projects.Remove(
                Projects
                    .FirstOrDefault(x => x.Name == projectName));
        }

        public ProjectsService()
        {
            Projects = new ObservableCollection<Project>();
        }

        public void OpenDll(string path)
        {
            var assembly = Assembly.LoadFile(path);
            // todo make it DI'able
            var converter = new Converter();
            var asmInfo = converter.Convert(assembly);
            var project = new Project
            {
                Name = asmInfo.Name,
                Guid = Guid.NewGuid(),
                AssemblyInfo = asmInfo
            };
            Projects.Add(project);
        }

        public void Export(Guid projectGuid, IAssemblyExporter exporter)
        {
            //var project = Projects.FirstOrDefault(x => x.Guid == projectGuid);
            var project = Projects.FirstOrDefault();
            if(project == null)
            {
                // ex
            }
            //TODO : export project instead of AssemblyInfo
            exporter.Export(project.AssemblyInfo);
        }

        
    }
}