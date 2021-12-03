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

namespace HeBianGu.Application.ManagerWindow
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller
    { 
        public async Task<IActionResult> RealData()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> History()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> HomeA()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Monitor()
        {
            return await ViewAsync();
        }
    }
}
