// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class BoolPropertyItem : ObjectPropertyItem<bool>
    {
        public BoolPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }

    public class BoolNullablePropertyItem : ObjectPropertyItem<bool?>
    {
        public BoolNullablePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
