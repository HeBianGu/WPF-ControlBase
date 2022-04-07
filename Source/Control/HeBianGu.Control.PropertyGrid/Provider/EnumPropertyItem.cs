// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    /// <summary> 枚举类型 </summary>
    public class EnumPropertyItem : ObjectPropertyItem<Enum>
    {
        public EnumPropertyItem(PropertyInfo property, object obj)
            : base(property, obj)
        {
        }
    }

}
