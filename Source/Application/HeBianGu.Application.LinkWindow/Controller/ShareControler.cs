using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Share")]
    class ShareController : Controller
    {
        public async Task<IActionResult> Bottom()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Top()
        {
            return await ViewAsync();
        }
    }
}
