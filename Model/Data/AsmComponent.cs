﻿using System;
using Model.VisitorPattern;

namespace Model.Data
{
    public abstract class AsmComponent : IVisitable
    {
        public virtual Guid Guid { get; set; }
        public string Name { get; set; }
        public abstract void Accept(IVisitor visitor);
    }
}