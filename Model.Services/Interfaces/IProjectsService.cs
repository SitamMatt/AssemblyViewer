using System;
using System.Collections.ObjectModel;
using Model.Services.Data;

namespace Model.Services.Interfaces
{
    public interface IProjectsService
    {
        ObservableCollection<Project> Projects { get; }
        void CloseProject(string projectName);
        void Export(Guid projectGuid, IAssemblyExporter exporter);
        void Import(IAssemblyImporter importer);
    }
}