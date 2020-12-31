﻿using System;
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

        public void Export(Guid projectGuid, IAssemblyExporter exporter)
        {
            var project = Projects.FirstOrDefault(x => x.Guid == projectGuid);
            if(project == null)
            {
                // ex
            }
            exporter.Export(project.AssemblyInfo);
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