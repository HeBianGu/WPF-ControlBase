// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;

namespace HeBianGu.Systems.Component
{
    public class NodeActionResult : ActionResult, INodeActionResult
    {
        public List<IComponentPort> Ports { get; set; } = new List<IComponentPort>();
    }
}
