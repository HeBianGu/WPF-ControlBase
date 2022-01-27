using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Tool
{
    [Controller("Favorite")]
    internal class FavoriteController : Controller<FavoriteViewModel>
    {
        public async Task<IActionResult> Favorite()
        {
            return await ViewAsync();
        }
    }
}
