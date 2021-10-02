using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.ChartWindow
{
    [Controller("Scatter")]
    internal class ScatterController : Controller<ScatterViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Basic()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Bubble()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Polar()
        {
            return await ViewAsync();
        }
    }
}
