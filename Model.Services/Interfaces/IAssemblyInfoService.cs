using System;
using Model.VisitorPattern;

namespace Model.Services.Interfaces
{
    public interface IAssemblyInfoService
    {
        void AcceptRoot(IVisitor visitor);
        void Accept(Guid key, IVisitor visitor);
    }
}