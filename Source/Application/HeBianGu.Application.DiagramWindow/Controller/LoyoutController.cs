using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Application.DiagramWindow
{
    [Route("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Tree()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Port()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> PortXml()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TypeTree()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Tool()
        {
            return await ViewAsync();
        }


    }
}
