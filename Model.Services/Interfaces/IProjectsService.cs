using System;
using System.Collections.ObjectModel;
using Model.Services.Data;

namespace Model.Services.Interfaces
{
    public interface IProjectsService
    {
        void OpenDll(string path);
        ObservableCollection<Project> Projects { get; }
        void CloseProject(string projectName);
        void Export(Guid projectGuid, IAssemblyExporter exporter);
    }
}