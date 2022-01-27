using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Office
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Account()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Connect()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Open()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> New()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Info()
        {
            return await ViewAsync();
        }
    }
}
