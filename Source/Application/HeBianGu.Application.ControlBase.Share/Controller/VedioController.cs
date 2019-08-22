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
    [Route("Vedio")]
    class VedioController : Controller
    {
        public async Task<IActionResult> VedioCenter()
        {
            return View();
        }
    }
}
