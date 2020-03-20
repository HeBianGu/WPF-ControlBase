
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfMvc;

namespace HeBianGu.Application.TouchWindow
{
    [Route("Setting")]
    internal class SettingController : Controller<SettingViewModel>
    {
        public async Task<IActionResult> Setting()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> Theme()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> UpLoad()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Advance()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Basic()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Device()
        {
            return await ViewAsync();
        } 
    }
}
