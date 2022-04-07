using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Manager
{
    [Controller("Panel")]
    internal class PanelController : Controller
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Circle()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Cover()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Arc()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Dock()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Cross()
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

        public async Task<IActionResult> Wave()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Linear()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Contain()
        {
            return await ViewAsync();
        }
    }
}
