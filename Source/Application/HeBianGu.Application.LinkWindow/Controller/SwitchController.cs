using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Switch")]
    class SwitchController : Controller<SwitchViewModel>
    {
        public async Task<IActionResult> SwitchCenter()
        {
            return await ViewAsync();
        }

    }
}
