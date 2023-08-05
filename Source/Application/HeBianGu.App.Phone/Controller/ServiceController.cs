using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Phone
{
    [Controller("Service")]
    internal class ServiceController : Controller
    {
        public async Task<IActionResult> Home()
        {
            return await ViewAsync();
        }
    }
}
