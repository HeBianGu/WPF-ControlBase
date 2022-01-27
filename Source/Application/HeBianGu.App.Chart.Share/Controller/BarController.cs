using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
{
    [Controller("Bar")]
    internal class BarController : Controller<BarViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Bar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Stack()
        {
            return await ViewAsync();
        }
        public async Task<IActionResult> yBar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> yStackBar()
        {
            return await ViewAsync();
        }
    }
}
