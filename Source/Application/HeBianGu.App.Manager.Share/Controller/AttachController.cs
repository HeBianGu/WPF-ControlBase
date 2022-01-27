using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Manager
{
    [Controller("Attach")]
    internal class AttachController : Controller
    {
        public async Task<IActionResult> Visible()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Random()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Group()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Ping()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Step()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> LeftMenu()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> GitTop()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Expander()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Panel()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Scroll()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Filter()
        {
            return await ViewAsync();
        }
    }
}
