using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

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
