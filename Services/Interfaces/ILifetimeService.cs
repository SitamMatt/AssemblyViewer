using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface ILifetimeService
    {
        void Exit(int code);
    }
}
