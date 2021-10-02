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

namespace HeBianGu.Application.ManagerWindow
{
    [Controller("Drawer")]
    internal class DrawerController : Controller
    {
        public async Task<IActionResult> Translate()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Scale()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Geomotry()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Other()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Opacity()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Angle()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> GitTop()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Expander()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Panel()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Scroll()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Filter()
        {
            return await ViewAsync();
        }
    }
}
