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
    [Route("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> xAxis()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> yAxis()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Grid()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Legend()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Series()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Title()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Polar()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> AngleAxis()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> RadiusAxis()
        {
            return await ViewAsync();
        }
    }
}
