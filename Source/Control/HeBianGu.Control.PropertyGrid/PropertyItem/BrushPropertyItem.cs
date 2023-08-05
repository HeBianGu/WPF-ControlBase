// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;
using System.Windows.Media;

namespace HeBianGu.Control.PropertyGrid
{
    public class BrushPropertyItem : ObjectPropertyItem<SolidColorBrush>
    {
        public BrushPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {
        }
    }
}
