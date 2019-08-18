using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [Route("Tab")]
    class TabController : Controller<TabViewModel>
    {
        public async Task<IActionResult> TabCenter()
        {
            return View();
        }

        public async Task<IActionResult> AnimatedTab()
        {
            return View();
        }

        public async Task<IActionResult> DTabItem()
        {
            return View();
        }

        public async Task<IActionResult> MetroTab()
        {
            return View();
        }


        public async Task<IActionResult> WebCenter()
        {
            return View();
        }

        public async Task<IActionResult> TabLeft()
        {
            return View();
        }

    }
}
