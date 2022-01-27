using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Menu
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Loyout()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Fluid()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> MouseDrag()
        {
            return await ViewAsync();
        }
    }
}
