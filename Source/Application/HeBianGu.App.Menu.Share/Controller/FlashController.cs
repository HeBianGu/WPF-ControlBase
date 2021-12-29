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

namespace HeBianGu.App.Menu
{
    [Controller("Flash")]
    internal class FlashController : Controller
    {
        public async Task<IActionResult> Normal()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashGrid()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashUniform()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Index()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashOrigin()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashEasing()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashBack()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashBounce()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FlashElastic()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Flash()
        {
            return await ViewAsync();
        }
    }
}
