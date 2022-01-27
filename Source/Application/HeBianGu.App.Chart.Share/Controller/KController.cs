using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Chart
{
    [Controller("K")]
    internal class KController : Controller<KViewModel>
    {
        public async Task<IActionResult> KBasic()
        {
            return await ViewAsync();
        }
    }
}
