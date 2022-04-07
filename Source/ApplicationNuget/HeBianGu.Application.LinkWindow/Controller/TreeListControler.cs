using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("TreeList")]
    public class TreeListController : Controller
    {
        [Controller("TreeList")]
        public async Task<IActionResult> TreeList()
        {
            await Message.Instance.ShowWaittingMessge(() => Thread.Sleep(1000));

            return await ViewAsync();
        }
    }
}
