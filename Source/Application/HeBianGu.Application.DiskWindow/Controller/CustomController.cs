using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.DiskWindow
{
    [Controller("Extend")]
    internal class ExtendController : Controller<ExtendViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }
    }
}
