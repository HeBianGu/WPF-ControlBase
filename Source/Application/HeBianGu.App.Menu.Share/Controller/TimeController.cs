using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;

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
