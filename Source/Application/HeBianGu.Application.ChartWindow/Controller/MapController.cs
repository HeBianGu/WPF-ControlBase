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

namespace HeBianGu.Application.ChartWindow
{
    [Controller("Map")]
    internal class MapController : Controller<MapViewModel>
    {
        public async Task<IActionResult> China()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> World()
        {
            return await ViewAsync();
        }
    }
}
