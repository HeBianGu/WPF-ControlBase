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

namespace HeBianGu.Application.OfficeWindow
{
    [Controller("Loyout")]
    internal class LoyoutController : Controller
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Account()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Connect()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Open()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> New()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Info()
        {
            return await ViewAsync();
        }
    }
}
