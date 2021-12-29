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
