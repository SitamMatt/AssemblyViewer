using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Data;
using FieldInfo = Model.Data.FieldInfo;
using TypeInfo = Model.Data.TypeInfo;

namespace Model.Converters
{
    public class Converter
    {
        protected AssemblyInfo _assemblyInfo;
        protected Dictionary<string, TypeInfo> _typesLookup = new Dictionary<string, TypeInfo>();
        public Dictionary<int, IVisitable> NodesLookup { get; set; } = new Dictionary<int, IVisitable>();

        public AssemblyInfo Convert(Assembly assembly)
        {
            _assemblyInfo = new AssemblyInfo
            {
                Name = assembly.FullName,
                Modules = assembly.Modules.Select(ConvertModule).ToList()
            };
            NodesLookup[_assemblyInfo.GetHashCode()] = _assemblyInfo;
            return _assemblyInfo;
        }

        protected ModuleInfo ConvertModule(Module module)
        {
            var moduleInfo = new ModuleInfo
            {
                Name = module.Name,
                Types = module.GetTypes().Select(ConvertType).ToList()
            };
            NodesLookup[moduleInfo.GetHashCode()] = moduleInfo;
            return moduleInfo;
        }

        protected TypeInfo ConvertType(Type type)
        {
            if (type.FullName != null && _typesLookup.ContainsKey(type.FullName)) 
                return _typesLookup[type.FullName];
            var typeInfo = new TypeInfo
            {
                Name = type.FullName,
            };
            _typesLookup[typeInfo.Name] = typeInfo;
            NodesLookup[typeInfo.GetHashCode()] = typeInfo;
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
                Type = ConvertType(field.FieldType)
            };
            NodesLookup[fieldInfo.GetHashCode()] = fieldInfo;
            return fieldInfo;
        }
    }
}