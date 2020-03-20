
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfMvc;

namespace HeBianGu.Application.TouchWindow
{
    [Route("Login")]
    internal class LoginController : Controller<LoginViewModel>
    {
        public async Task<IActionResult> Login()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                this.ViewModel.ShowWaitting();
            });

            return await ViewAsync();
        }

        public async Task<IActionResult> Login(bool isAwatting)
        {
            if (isAwatting)
            {
               System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    this.ViewModel.ShowWaitting();
                });
            }
            return await ViewAsync();
        }
    }
}
