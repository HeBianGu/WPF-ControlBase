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

namespace HeBianGu.App.Disk
{
    [Controller("Extend")]
    internal class ExtendController : Controller<ExtendViewModel>
    {
        public async Task<IActionResult> Extend()
        {
            return await ViewAsync();
        }
    }
}
