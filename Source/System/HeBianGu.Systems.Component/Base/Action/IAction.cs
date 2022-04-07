// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Threading.Tasks;

namespace HeBianGu.Systems.Component
{
    public interface IAction
    {
        ActionState State { get; set; }

        IActionResult Invoke(IAction previors);

        Task<IActionResult> InvokeAsync(IAction previors);

        Task<IActionResult> TryInvokeAsync(IAction previors);

        void Clear();

    }
}
