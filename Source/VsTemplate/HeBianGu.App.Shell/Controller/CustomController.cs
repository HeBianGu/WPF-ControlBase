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

namespace HeBianGu.App.Shell
{
    [Controller("Custom", Logo = "\xe7fd")]
    internal class CustomController : Controller<CustomViewModel>
    {
        [Action(DisplayName ="Custom",Logo = "\xe7fd")]
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }
    }
}
