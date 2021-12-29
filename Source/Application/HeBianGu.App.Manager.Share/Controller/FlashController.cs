using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;

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
