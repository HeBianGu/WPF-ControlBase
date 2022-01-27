using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Manager
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller
    {
        public async Task<IActionResult> RealData()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> History()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> HomeA()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Monitor()
        {
            return await ViewAsync();
        }
    }
}
