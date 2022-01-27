using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Mvc")]
    class MvcController : Controller<MvcViewModel>
    {
        public async Task<IActionResult> MvcCenter()
        {
            return await ViewAsync();
        }
    }
}
