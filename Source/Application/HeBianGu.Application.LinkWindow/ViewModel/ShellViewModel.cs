using HeBianGu.Window.Link;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HeBianGu.Application.LinkWindow
{
    [ViewModel("Shell")]
    class ShellViewModel : WindowLinkViewModel
    {
        Random random = new Random();

        protected async override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                this.IsBuzy = true;

                await Task.Run(() =>
                {
                    Thread.Sleep(500);

                    string err;

                    var result = AssemblyDomain.Instance.Login(this.LoginUseName, this.LoginPassWord, this.Remenber, out err);

                    if (result)
                    {
                        this.LoginMessage = "登录成功";
                        this.IsBuzy = false;

                        Task.Delay(500).ContinueWith(l =>
                        {
                            this.IsLogined = true;

                        });
                    }
                    else
                    {
                        this.IsBuzy = false;
                        this.IsEnbled = false;

                        //this.LoginMessage = "网络错误，登录失败";
                        this.LoginMessage = err;

                        Task.Delay(1000).ContinueWith(l =>
                        {
                            this.LoginMessage = "登录";
                            this.IsEnbled = true;
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
                var result = AssemblyDomain.Instance.GetAccountConfig();

                if (result != null)
                {
                    if (result.Remenber)
                    {
                        this.LoginPassWord = result.LoginPassWord;
                        this.Remenber = true;
                    }
                    this.LoginUseName = result.LoginUseName;

                }

                //this.CurrentLink = this.TabLinks.FirstOrDefault();
            }
        }
    }



}
