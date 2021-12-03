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

namespace HeBianGu.Application.MenuWindow
{
    [Controller("Fluid")]
    internal class FluidController : Controller<CustomViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Stack()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Uniform()
        {
            return await ViewAsync();
        }
    }
}
