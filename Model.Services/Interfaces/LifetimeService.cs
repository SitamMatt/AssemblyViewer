using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Services.Interfaces
{
    public interface ILifetimeService
    {
        void Exit(int code);
    }
}
