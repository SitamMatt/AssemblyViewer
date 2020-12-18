using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Model.Converters;
using Model.Services.Data;

namespace Model.Services
{
    public class ProjectsService : IProjectsService
    {
        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects => _projects;

        public ProjectsService()
        {
            _projects = new ObservableCollection<Project>();
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
                Guid = System.Guid.NewGuid(),
                AssemblyInfo = asmInfo
            };
            _projects.Add(project);
        }
    }
}