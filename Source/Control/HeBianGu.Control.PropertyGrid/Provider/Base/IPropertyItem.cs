// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Reflection;

namespace HeBianGu.Control.PropertyGrid
{
    public interface IPropertyItem
    {
        string Name { get; set; }

        string TabGroup { get; set; }

        string Group { get; set; }

        PropertyInfo PropertyInfo { get; set; }

        object Obj { get; set; }

    }
}