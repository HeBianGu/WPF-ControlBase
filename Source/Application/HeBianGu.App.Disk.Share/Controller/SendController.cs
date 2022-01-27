using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Disk
{
    [Controller("Send")]
    internal class SendController : Controller<SendViewModel>
    {
        public async Task<IActionResult> Send()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Down()
        {
            return await ViewAsync();
        }
    }
}
