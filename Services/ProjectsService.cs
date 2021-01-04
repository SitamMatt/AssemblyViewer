using System;
using System.Collections.ObjectModel;
using System.Linq;
using Services.Data;
using Services.Interfaces;

namespace Services
{
    public class ProjectsService : IProjectsService
    {
        public ObservableCollection<Project> Projects { get; }

        public void CloseProject(Guid projectGuid)
        {
            Projects.Remove(
                Projects
                    .FirstOrDefault(x => x.AssemblyInfo.Guid == projectGuid));
        }

        public ProjectsService()
        {
            Projects = new ObservableCollection<Project>();
        }

        public void Export(Guid projectGuid, IAssemblyExporter exporter)
        {
            var project = Projects.FirstOrDefault(x => x.Guid == projectGuid);
            if (project != null)
            {
                exporter.Export(project.AssemblyInfo);
            }
        }

        public void Import(IAssemblyImporter importer)
        {
            var info = importer.Import();
            var project = new Project
            {
                Guid = Guid.NewGuid(),
                AssemblyInfo = info
            };
            Projects.Add(project);
        }
    }
}