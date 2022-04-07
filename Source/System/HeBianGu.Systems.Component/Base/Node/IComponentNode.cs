// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.Diagram;
using System.Collections.Generic;

namespace HeBianGu.Systems.Component
{
    public interface IComponentNode : IAction, INodeData
    {
        List<IComponentPort> PortDatas { get; set; }
    }
}
