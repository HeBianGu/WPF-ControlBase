using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
{
    [Controller("Line")]
    internal class LineController : Controller<LineViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Line()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> LineArea()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> LinePolar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Smooth()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Step()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Candlestick()
        {
            return await ViewAsync();
        }
    }
}
