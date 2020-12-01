using Model.Data;

namespace Model.VisitorPattern
{
    public interface IVisitor
    {
        void Handle(AssemblyInfo assemblyInfo);
        void Handle(TypeInfo typeInfo);
        void Handle(MethodInfo methodInfo);
        void Handle(FieldInfo fieldInfo);
        void Handle(ConstructorInfo constructorInfo);
        void Handle(ParameterInfo parameterInfo);
        void Handle(PropertyInfo propertyInfo);
        void Handle(ModuleInfo moduleInfo);
    }
}