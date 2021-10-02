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
    [Controller("Custom")]
    internal class CustomController : Controller<CustomViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Rain()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> AQI()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> GDP()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Down()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Visit()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Age()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Area()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Wave()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> SinPolar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Osc()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Parallel()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Rich()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> CMap()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Thinning()
        {
            return await ViewAsync();
        }


        
    }
}
