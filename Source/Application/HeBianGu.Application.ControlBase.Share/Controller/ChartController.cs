using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Controler
{
    [Route("Chart")]
    class ChartController : Controller
    {
        public async Task<IActionResult> ChartCenter()
        {
            return View();
        }

        public async Task<IActionResult> Curve()
        {
            return View();
        }


        public async Task<IActionResult> Cardiogram()
        {
            return View();
        }

        public async Task<IActionResult> ChartLeft()
        {
            return View();
        }
    }
}
