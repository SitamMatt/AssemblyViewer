using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Data;
using Model.Services.Interfaces;
using Model.VisitorPattern;
using FieldInfo = Model.Data.FieldInfo;
using TypeInfo = Model.Data.TypeInfo;

namespace Model.Services
{
    public class AssemblyConverter : IAssemblyConverter
    {
        protected AssemblyInfo _assemblyInfo;
        protected Dictionary<string, TypeInfo> _typesLookup = new Dictionary<string, TypeInfo>();
        public Dictionary<Guid, AsmComponent> NodesLookup { get; } = new Dictionary<Guid, AsmComponent>();

        public AssemblyInfo Convert(Assembly assembly)
        {
            _assemblyInfo = new AssemblyInfo
            {
                Name = assembly.FullName,
                Modules = assembly.Modules.Select(ConvertModule).ToList(),
                Guid = Guid.NewGuid()
            };
            NodesLookup[_assemblyInfo.Guid] = _assemblyInfo;
            _assemblyInfo.Lookup = NodesLookup;
            return _assemblyInfo;
        }

        protected ModuleInfo ConvertModule(Module module)
        {
            var moduleInfo = new ModuleInfo
            {
                Name = module.Name,
                Types = module.GetTypes().Select(ConvertType).ToList(),
                Guid = Guid.NewGuid()
            };
            NodesLookup[moduleInfo.Guid] = moduleInfo;
            return moduleInfo;
        }

        protected TypeInfo ConvertType(Type type)
        {
            if (type.FullName != null && _typesLookup.ContainsKey(type.FullName))
                return _typesLookup[type.FullName];
            var typeInfo = new TypeInfo
            {
                Name = type.FullName,
                Guid = Guid.NewGuid()
            };
            _typesLookup[typeInfo.Name] = typeInfo;
            NodesLookup[typeInfo.Guid] = typeInfo;
            typeInfo.Fields = type.GetFields().Select(ConvertField).ToList();
            return typeInfo;
        }

        protected FieldInfo ConvertField(System.Reflection.FieldInfo field)
        {
            var fieldInfo = new FieldInfo
            {
                Name = field.Name,
                Attributes = field.Attributes,
                DeclaringType = _typesLookup[field.DeclaringType.FullName],
                Type = ConvertType(field.FieldType),
                Guid = Guid.NewGuid()
            };
            NodesLookup[fieldInfo.Guid] = fieldInfo;
            return fieldInfo;
        }
    }
}