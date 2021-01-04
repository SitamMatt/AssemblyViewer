using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Data;
using Services.Interfaces;
using FieldInfo = Model.Data.FieldInfo;
using TypeInfo = Model.Data.TypeInfo;

namespace Services
{
    public class AssemblyConverter : IAssemblyConverter
    {
        protected Dictionary<string, TypeInfo> typesLookup = new Dictionary<string, TypeInfo>();
        protected Dictionary<Guid, AsmComponent> nodesLookup = new Dictionary<Guid, AsmComponent>();

        public AssemblyInfo Convert(Assembly assembly)
        {
            AssemblyInfo assemblyInfo = new AssemblyInfo
            {
                Name = assembly.FullName,
                Guid = Guid.NewGuid(),
                Lookup = nodesLookup,
                Modules = assembly.Modules.Select(ConvertModule).ToList()
            };
            nodesLookup[assemblyInfo.Guid] = assemblyInfo;
            return assemblyInfo;
        }

        public ModuleInfo ConvertModule(Module module)
        {
            var moduleInfo = new ModuleInfo
            {
                Name = module.Name,
                Types = module.GetTypes().Select(ConvertType).ToList(),
                Guid = Guid.NewGuid()
            };
            nodesLookup[moduleInfo.Guid] = moduleInfo;
            return moduleInfo;
        }

        public TypeInfo ConvertType(Type type)
        {
            if ((type.FullName != null || type.Name != null) && typesLookup.ContainsKey(type.FullName ?? type.Name))
                return typesLookup[type.FullName ?? type.Name];
            var typeInfo = new TypeInfo
            {
                Name = type.FullName ?? type.Name,
                Guid = Guid.NewGuid()
            };
            typesLookup[typeInfo.Name] = typeInfo;
            nodesLookup[typeInfo.Guid] = typeInfo;
            typeInfo.Fields = type.GetFields().Select(ConvertField).ToList();
            return typeInfo;
        }

        public FieldInfo ConvertField(System.Reflection.FieldInfo field)
        {
            var fieldInfo = new FieldInfo
            {
                Name = field.Name,
                Attributes = field.Attributes,
                DeclaringType = typesLookup[field.DeclaringType.FullName],
                Type = ConvertType(field.FieldType),
                Guid = Guid.NewGuid()
            };
            nodesLookup[fieldInfo.Guid] = fieldInfo;
            return fieldInfo;
        }
    }
}