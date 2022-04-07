// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Systems.Component
{
    public class StartComponentNode : ComponentNode
    {
        public override IActionResult Invoke(IAction previors)
        {
            Thread.Sleep(2000);

            return OK();
        }

        public override async Task<IActionResult> InvokeAsync(IAction lapreviorsst)
        {
            await Task.Run(() =>
             {
                 Thread.Sleep(1000);
             });

            return OK();
        }
    }

    public class SuccessComponentNode : ComponentNode
    {
        public override IActionResult Invoke(IAction previors)
        {
            return OK();
        }

        public override async Task<IActionResult> InvokeAsync(IAction lapreviorsst)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(100);
            });

            return OK();
        }
    }

    public class ErrorComponentNode : ComponentNode
    {
        public override IActionResult Invoke(IAction previors)
        {
            return Error();
        }

        public override async Task<IActionResult> InvokeAsync(IAction lapreviorsst)
        {
            await Task.Run(() =>
            {
                Thread.Sleep(100);
            });
            return Error();
        }
    }
}
