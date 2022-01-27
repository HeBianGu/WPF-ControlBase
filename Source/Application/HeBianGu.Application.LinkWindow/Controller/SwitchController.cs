using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Switch")]
    class SwitchController : Controller<SwitchViewModel>
    {
        public async Task<IActionResult> SwitchCenter()
        {
            return await ViewAsync();
        }

    }
}
