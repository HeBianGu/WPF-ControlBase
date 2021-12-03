using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Mvc")]
    class MvcController : Controller<MvcViewModel>
    {
        public async Task<IActionResult> MvcCenter()
        {
            return await ViewAsync();
        }
    }
}
