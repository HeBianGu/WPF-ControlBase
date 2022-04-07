using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Disk
{
    [Controller("Extend")]
    internal class ExtendController : Controller<ExtendViewModel>
    {
        public async Task<IActionResult> Extend()
        {
            return await ViewAsync();
        }
    }
}
