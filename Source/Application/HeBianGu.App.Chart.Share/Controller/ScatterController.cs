using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
{
    [Controller("Scatter")]
    internal class ScatterController : Controller<ScatterViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Scatter()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Bubble()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> ScatterPolar()
        {
            return await ViewAsync();
        }
    }
}
