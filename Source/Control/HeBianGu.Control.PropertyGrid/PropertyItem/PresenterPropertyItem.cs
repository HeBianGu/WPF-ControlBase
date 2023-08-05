// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public class PresenterPropertyItem : ObjectPropertyItem<object>
    {
        public PresenterPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }
}
