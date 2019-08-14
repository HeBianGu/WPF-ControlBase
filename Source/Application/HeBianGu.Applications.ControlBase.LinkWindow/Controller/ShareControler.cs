using HeBianGu.Base.WpfBase;
using HeBianGu.Common.PublicTool;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [Route("Share")]
    class ShareController : Controller
    {
        public async Task<IActionResult> Bottom()
        {
            return View();
        }

    }
}
