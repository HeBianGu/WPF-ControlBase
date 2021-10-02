using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("TreeList")]
    public class TreeListController : Controller
    {
        [Controller("TreeList")]
        public async Task<IActionResult> TreeList()
        {
            await MessageService.ShowWaittingMessge(() => Thread.Sleep(1000));

            return await ViewAsync();
        }
    }
}
