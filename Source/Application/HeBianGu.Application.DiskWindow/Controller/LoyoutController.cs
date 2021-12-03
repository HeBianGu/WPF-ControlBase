using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.DiskWindow
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
