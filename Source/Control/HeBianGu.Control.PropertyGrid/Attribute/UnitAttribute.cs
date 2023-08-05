// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Control.PropertyGrid
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class UnitAttribute : Attribute
    {
        public UnitAttribute(string unit)
        {
            this.Unit = unit;
        }
        public string Unit { get;}
    }
}
