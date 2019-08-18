using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Control
{
    [Route("Mvc")]
    class MvcController : Controller<MvcViewModel>
    {
        public async Task<IActionResult> MvcCenter()
        {
            return View();
        }
    }
}
