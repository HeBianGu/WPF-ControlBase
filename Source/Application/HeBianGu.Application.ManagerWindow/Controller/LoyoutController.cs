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

namespace HeBianGu.Application.ManagerWindow
{
    [Route("Loyout")]
    internal class LoyoutController : Controller<LoyoutViewModel>
    {
        [Route("数据分析/实施数据")]
        public async Task<IActionResult> RealData()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> HistoryData()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Report()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Monitor()
        {
            return await ViewAsync();
        }
    }
}
