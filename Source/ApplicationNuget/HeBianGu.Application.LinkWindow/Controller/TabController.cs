using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [Controller("Tab")]
    internal class TabController : Controller<TabViewModel>
    {
        public async Task<IActionResult> TabCenter()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> AnimatedTab()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> DTabItem()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> MetroTab()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> WebCenter()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TabLeft()
        {
            return await ViewAsync();
        }

    }
}
