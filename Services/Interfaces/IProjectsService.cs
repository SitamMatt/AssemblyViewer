﻿using System;
using System.Collections.ObjectModel;
using Services.Data;

namespace Services.Interfaces
{
    public interface IProjectsService
    {
        ObservableCollection<Project> Projects { get; }
        void CloseProject(Guid projectGuid);
        void Export(Guid projectGuid, IAssemblyExporter exporter);
        void Import(IAssemblyImporter importer);
    }
}