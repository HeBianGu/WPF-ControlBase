// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading;

namespace HeBianGu.Systems.Component
{
    public class ComponentLink : ComponentLinkBase, IComponentLink
    {
        public override IActionResult Invoke(IAction previors)
        {
            Thread.Sleep(100);

            return this.OK();
        }
    }
}
