
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Threading.Tasks;

namespace HeBianGu.App.Touch
{
    [Controller("Login")]
    internal class LoginController : Controller<LoginViewModel>
    {
        public async Task<IActionResult> Login()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                ViewModel.ShowWaitting();
            });

            return await ViewAsync();
        }

        public async Task<IActionResult> Login(bool isAwatting)
        {
            if (isAwatting)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                 {
                     ViewModel.ShowWaitting();
                 });
            }
            return await ViewAsync();
        }
    }
}
