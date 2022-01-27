using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Manager
{
    [Controller("Flash")]
    internal class FlashController : Controller
    {

        public async Task<IActionResult> Wave()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Heart()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Flash()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Text()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Arc()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Hex()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Area()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Animation()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Drag()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Panel()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Page()
        {
            return await ViewAsync();
        }

    }
}
