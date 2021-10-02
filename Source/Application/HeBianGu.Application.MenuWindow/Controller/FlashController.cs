using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.MenuWindow
{
    [Controller("Flash")]
    internal class FlashController : Controller
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Grid()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Uniform()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Index()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Origin()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Easing()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Back()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Bounce()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Elastic()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Flash()
        {
            return await ViewAsync();
        }
    }
}
