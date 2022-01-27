using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Manager
{
    [Controller("Transition")]
    internal class TransitionController : Controller
    {
        public async Task<IActionResult> Default()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Scale()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Skew()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Opacity()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Rotate()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Translate()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> GitTop()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Expander()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Panel()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Linear()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Clip()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Image()
        {
            return await ViewAsync();
        }
    }
}
