using System;
using System.Collections.Generic;
using System.Linq;
using Model.Data;
using Services.Interfaces;
using FieldInfo = Model.Data.FieldInfo;
using TypeInfo = Model.Data.TypeInfo;

namespace Services
{
    public class AssemblyConverter : IAssemblyConverter
    {
        protected Dictionary<Type, TypeInfo> typesLookup = new Dictionary<Type, TypeInfo>();
        protected Dictionary<Guid, AsmComponent> localNodesLookup = new Dictionary<Guid, AsmComponent>();
        protected Dictionary<Guid, AsmComponent> nodesLookup = new Dictionary<Guid, AsmComponent>();
        protected System.Reflection.Assembly localAssembly;


        public AssemblyInfo Convert(System.Reflection.Assembly assembly)
        {
            localAssembly = assembly;
            AssemblyInfo info = new AssemblyInfo
            {
                Name = assembly.FullName,
                Guid = Guid.NewGuid(),
                Lookup = localNodesLookup,
                Modules = assembly.Modules.Select(ConvertModule).ToList(),
            };
            localNodesLookup[info.Guid] = info;
            info.CustomAttributes = assembly.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public ModuleInfo ConvertModule(System.Reflection.Module module)
        {
            var info = new ModuleInfo
            {
                Name = module.ScopeName,
                Types = module.GetTypes().Select(ConvertType).ToList(),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            info.CustomAttributes = module.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public TypeInfo ConvertType(Type type)
        {
            if (typesLookup.ContainsKey(type)) return typesLookup[type];
            if (type.Assembly != localAssembly) return new TypeInfo { Name = type.Name };
            var info = new TypeInfo
            {
                Name = type.Name,
                Guid = Guid.NewGuid()
            };
            typesLookup[type] = info;
            localNodesLookup[info.Guid] = info;
            info.Attributes = type.Attributes;
            info.Namespace = type.Namespace;
            info.Fields = type.GetFields().Select(ConvertField).ToList();
            info.Properties = type.GetProperties().Select(ConvertProperty).ToList();
            info.Methods = type.GetMethods().Select(ConvertMethod).ToList();
            info.Constructors = type.GetConstructors().Select(ConvertConstructor).ToList();
            info.CustomAttributes = type.CustomAttributes.Select(ConvertAttribute).ToList();
            info.NestedTypes = type.GetNestedTypes().Select(ConvertType).ToList();
            return info;
        }

        private ConstructorInfo ConvertConstructor(System.Reflection.ConstructorInfo constructor)
        {
            var info = new ConstructorInfo
            {
                Name = constructor.Name,
                Attributes = constructor.Attributes,
                DeclaringType = ConvertType(constructor.DeclaringType),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            info.Attributes = constructor.Attributes;
            info.Parameters = constructor.GetParameters().Select(ConvertParameter).ToList();
            info.CustomAttributes = constructor.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public MethodInfo ConvertMethod(System.Reflection.MethodInfo method)
        {
            var info = new MethodInfo
            {
                Name = method.Name,
                Attributes = method.Attributes,
                DeclaringType = ConvertType(method.DeclaringType),
                ReturnType = ConvertType(method.ReturnType),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            info.Attributes = method.Attributes;
            info.Parameters = method.GetParameters().Select(ConvertParameter).ToList();
            info.CustomAttributes = method.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public ParameterInfo ConvertParameter(System.Reflection.ParameterInfo parameter)
        {
            var info = new ParameterInfo
            {
                Name = parameter.Name,
                Type = ConvertType(parameter.ParameterType),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            info.Attributes = parameter.Attributes;
            info.CustomAttributes = parameter.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public FieldInfo ConvertField(System.Reflection.FieldInfo field)
        {
            var info = new FieldInfo
            {
                Name = field.Name,
                Attributes = field.Attributes,
                DeclaringType = ConvertType(field.DeclaringType),
                Type = ConvertType(field.FieldType),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            info.Attributes = field.Attributes;
            info.CustomAttributes = field.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public PropertyInfo ConvertProperty(System.Reflection.PropertyInfo property)
        {
            var info = new PropertyInfo
            {
                Name = property.Name,
                Attributes = property.Attributes,
                DeclaringType = ConvertType(property.DeclaringType),
                HasGetter = property.CanRead,
                HasSetter = property.CanWrite,
                Type = ConvertType(property.PropertyType),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            info.Attributes = property.Attributes;
            info.CustomAttributes = property.CustomAttributes.Select(ConvertAttribute).ToList();
            return info;
        }

        public AttributeInfo ConvertAttribute(System.Reflection.CustomAttributeData attribute)
        {
            if (attribute.AttributeType.Assembly != localAssembly) return new AttributeInfo { Name = attribute.AttributeType.Name };
            var info = new AttributeInfo
            {
                Name = attribute.AttributeType.Name,
                Type = ConvertType(attribute.AttributeType),
                ConstructorInfo = ConvertConstructor(attribute.Constructor),
                Guid = Guid.NewGuid()
            };
            localNodesLookup[info.Guid] = info;
            return info;
        }
    }
}