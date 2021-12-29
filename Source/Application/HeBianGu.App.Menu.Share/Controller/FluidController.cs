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
    [Controller("Fluid")]
    internal class FluidController : Controller<CustomViewModel>
    {
        public async Task<IActionResult> FluidHome()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FluidStack()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> FluidUniform()
        {
            return await ViewAsync();
        }
    }
}
