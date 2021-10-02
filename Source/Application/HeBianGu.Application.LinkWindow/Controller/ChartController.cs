using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Chart")]
    class ChartController : Controller
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
