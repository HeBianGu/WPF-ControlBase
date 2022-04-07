using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Chart")]
    internal class ChartController : Controller
    {
        public async Task<IActionResult> ChartCenter()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Curve()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Cardiogram()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> ChartLeft()
        {
            return await ViewAsync();
        }
    }
}
