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
    [Route("Switch")]
    class SwitchController : Controller<SwitchViewModel>
    {
        public async Task<IActionResult> SwitchCenter()
        {
            return View();
        }

    }
}
