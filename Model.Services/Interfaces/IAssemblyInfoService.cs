using Model.VisitorPattern;

namespace Model.Services.Interfaces
{
    public interface IAssemblyInfoService
    {
        void AcceptRoot(IVisitor visitor);
        void Accept(int key, IVisitor visitor);
    }
}