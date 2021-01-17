using Model.Data;

namespace Model.VisitorPattern
{
    public interface IVisitor
    {
        object Handle(AssemblyInfo assemblyInfo);
        object Handle(TypeInfo typeInfo);
        object Handle(MethodInfo methodInfo);
        object Handle(FieldInfo fieldInfo);
        object Handle(ConstructorInfo constructorInfo);
        object Handle(ParameterInfo parameterInfo);
        object Handle(PropertyInfo propertyInfo);
        object Handle(ModuleInfo moduleInfo);
        object Handle(AttributeInfo moduleInfo);
    }
}