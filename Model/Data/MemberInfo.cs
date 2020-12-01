namespace Model.Data
{
    public abstract class MemberInfo : AsmComponent
    {
        public TypeInfo DeclaringType { get; set; }
    }
}