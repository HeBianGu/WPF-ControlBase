using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
{
    [Controller("Pie")]
    internal class PieController : Controller<PieViewModel>
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> PieBasic()
        {
            return await ViewAsync();
        }
    }
}
