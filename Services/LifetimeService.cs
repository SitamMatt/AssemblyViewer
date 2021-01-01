using Services.Interfaces;
using System;

namespace Services
{
    public class LifetimeService : ILifetimeService
    {
        public void Exit(int code)
        {
            Environment.Exit(code);
        }
    }
}
