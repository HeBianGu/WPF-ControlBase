using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Disk
{
    [Controller("Friend")]
    internal class FriendController : Controller<FriendViewModel>
    {
        public async Task<IActionResult> Friend()
        {
            return await ViewAsync();
        }
    }
}
