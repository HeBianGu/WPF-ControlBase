using HeBianGu.Window.Link;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [ViewModel("Shell")]
    internal class ShellViewModel : WindowLinkViewModel
    {
        private Random random = new Random();

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                IsBuzy = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(500);

                    string err;

                    bool result = AssemblyDomain.Instance.Login(LoginUseName, LoginPassWord, Remenber, out err);

                    if (result)
                    {
                        LoginMessage = "登录成功";
                        IsBuzy = false;

                        Task.Delay(500).ContinueWith(l =>
                        {
                            IsLogined = true;

                        });
                    }
                    else
                    {
                        IsBuzy = false;
                        IsEnbled = false;

                        //this.LoginMessage = "网络错误，登录失败";
                        LoginMessage = err;

                        Task.Delay(1000).ContinueWith(l =>
                        {
                            LoginMessage = "登录";
                            IsEnbled = true;
                        });
                    }
                    //});

                });

            }
            //  Do：取消
            else if (command == "Cancel")
            {

            }

            //  Do：取消
            else if (command == "init")
            {
                //  Do：加载记住账号和密码
                General.WpfControlLib.LoginViewModel result = AssemblyDomain.Instance.GetAccountConfig();

                if (result != null)
                {
                    if (result.Remenber)
                    {
                        LoginPassWord = result.LoginPassWord;
                        Remenber = true;
                    }
                    LoginUseName = result.LoginUseName;

                }

                //this.CurrentLink = this.TabLinks.FirstOrDefault();
            }
        }
    }



}
