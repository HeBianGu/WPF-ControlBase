using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;

namespace HeBianGu.App.Chart
{
    [Controller("K")]
    internal class KController : Controller<KViewModel>
    {
        public async Task<IActionResult> KBasic()
        {
            return await ViewAsync();
        }
    }
}
