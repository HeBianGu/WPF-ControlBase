using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.ManagerWindow
{
    [Route("Panel")]
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
