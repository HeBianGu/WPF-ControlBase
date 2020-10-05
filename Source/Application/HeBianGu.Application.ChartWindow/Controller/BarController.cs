using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
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
    [Route("Bar")]
    internal class BarController : Controller<BarViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Basic()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Stack()
        {
            return await ViewAsync();
        }
        public async Task<IActionResult> yBar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> yStackBar()
        {
            return await ViewAsync();
        }
    }
}
