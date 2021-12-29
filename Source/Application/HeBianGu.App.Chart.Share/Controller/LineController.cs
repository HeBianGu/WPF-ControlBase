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
