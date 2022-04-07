using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Disk
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Near()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Explorer()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Space()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Share()
        {
            return await ViewAsync();
        }
    }
}
