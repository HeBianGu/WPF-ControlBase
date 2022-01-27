using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
{
    [Controller("Radar")]
    internal class RadarController : Controller<RadarViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Basic()
        {
            return await ViewAsync();
        }
    }
}
