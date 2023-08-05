// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;

namespace HeBianGu.Control.PropertyGrid
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class PropertyItemTypeAttribute : Attribute
    {
        public PropertyItemTypeAttribute()
        {

        }
        public PropertyItemTypeAttribute(Type type)
        {
            Type = type;
        }
        public Type Type { get; set; }
    }
}
