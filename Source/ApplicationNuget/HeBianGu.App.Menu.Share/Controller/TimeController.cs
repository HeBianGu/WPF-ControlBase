using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Menu
{
    [Controller("Time")]
    internal class TimeController : Controller
    {
        public async Task<IActionResult> TimeFountain()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TimeUniform()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TimeStack()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TimeHStack()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> Time()
        {
            return await ViewAsync();
        }


        public async Task<IActionResult> TimeEasing()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TimeBack()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TimeBounce()
        {
            return await ViewAsync();
        }

        public async Task<IActionResult> TimeElastic()
        {
            return await ViewAsync();
        }
    }
}
