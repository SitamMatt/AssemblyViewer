using Model.Data;

namespace Model.Visitors
{
    public interface IVisitor
    {
        void Handle(AssemblyInfo assemblyInfo);
        void Handle(TypeInfo typeInfo);
        void Handle(MethodInfo assemblyInfo);
        void Handle(FieldInfo fieldInfo);
        void Handle(ConstructorInfo assemblyInfo);
        void Handle(PropertyInfo assemblyInfo);
        void Handle(ModuleInfo moduleInfo);
    }
}