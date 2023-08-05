// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public interface IDragAdorner
    {
        Point Offset { get; set; }
        void UpdatePosition(Point location);
        object GetData();
    }
}