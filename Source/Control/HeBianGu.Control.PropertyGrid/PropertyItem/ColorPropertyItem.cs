// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;
using System.Windows.Media;

namespace HeBianGu.Control.PropertyGrid
{
    public class ColorPropertyItem : ObjectPropertyItem<Color>
    {
        public ColorPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
