using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.ChartWindow
{
    [Controller("K")]
    internal class KController : Controller<KViewModel>
    {
        public async Task<IActionResult> Basic()
        {
            return await ViewAsync();
        }
    }
}
