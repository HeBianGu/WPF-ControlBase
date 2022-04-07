// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Component
{
    public class WaitComponentNode : ComponentNode
    {
        public override IActionResult Invoke(IAction previors)
        {
            Thread.Sleep(2000);

            return OK();
        }

        public override async Task<IActionResult> InvokeAsync(IAction previors)
        {
            await Task.Run(() =>
             {
                 this.IsBuzy = true;

                 Thread.Sleep(5000);

                 this.IsBuzy = false;
             });

            return OK();
        }
    }
}
