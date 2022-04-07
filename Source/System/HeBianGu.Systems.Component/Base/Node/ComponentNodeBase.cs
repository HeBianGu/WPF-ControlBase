// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System.ComponentModel;

namespace HeBianGu.Systems.Component
{
    public abstract class ComponentNodeBase : Action, INodeData
    {
        [Browsable(false)]
        public double X { get; set; }

        [Browsable(false)]
        public double Y { get; set; }
    }
}
