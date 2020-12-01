using Model.VisitorPattern;

namespace Model.Services
{
    public interface IAssemblyInfoService
    {
        void AcceptRoot(IVisitor visitor);
        void Accept(int key, IVisitor visitor);
    }
}