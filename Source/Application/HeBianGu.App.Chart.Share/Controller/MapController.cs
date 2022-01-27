using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
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
