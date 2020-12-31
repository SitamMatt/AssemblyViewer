using Model.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Services
{
    public class LifetimeService : ILifetimeService
    {
        public void Exit(int code)
        {
            Environment.Exit(code);
        }
    }
}
