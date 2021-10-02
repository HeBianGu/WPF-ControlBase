using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Vedio")]
    class VedioController : Controller
    {
        public async Task<IActionResult> VedioCenter()
        {
            return await ViewAsync();
        }
    }
}
