using HeBianGu.Service.Mvc;
using HeBianGu.Window.Link;
using System;
using System.Linq;

namespace HeBianGu.App.Touch
{
    [ViewModel("Shell")]
    internal class ShellViewModel : WindowLinkViewModel
    {
        protected override void Init()
        {
            Links.Add(new LinkAction() { Controller = "Login", Action = "Login", DisplayName = "登录页面" });
            Links.Add(new LinkAction() { Controller = "Loyout", Action = "Home", DisplayName = "测量页面" });
            Links.Add(new LinkAction() { Controller = "Setting", Action = "Setting", DisplayName = "设置页面" });
            Links.Add(new LinkAction() { Controller = "Report", Action = "Report", DisplayName = "报告页面" });

            CurrentLink = Links.FirstOrDefault();

            base.Init();


        }

        protected override async void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Sumit")
            {
                //this.IsBuzy = true;

                //await Task.Run(() =>
                //{
                //    Thread.Sleep(500);

                //    string err;

                //    var result = AssemblyDomain.Instance.Login(this.LoginUseName, this.LoginPassWord,this.Remenber, this.IsOffLine, out err);

                //    if (result)
                //    {
                //        this.LoginMessage = "登录成功";
                //        this.IsBuzy = false;

                //        Task.Delay(500).ContinueWith(l =>
                //        {
                //            this.IsLogined = true;

                //        });
                //    }
                //    else
                //    {
                //        this.IsBuzy = false;

                //        this.IsEnbled = false;

                //        this.LoginMessage = err;

                //        Task.Delay(2000).ContinueWith(l =>
                //        {
                //            this.LoginMessage = "登录";
                //            this.IsEnbled = true;
                //        });
                //    }
                //    //});

                //});

            }
            //  Do：取消
            else if (command == "init")
            {
                ////  Do：加载记住账号和密码
                //string error;

                //var result = AssemblyDomain.Instance.GetAccountConfig(out error);

                //if (result != null)
                //{
                //    if (result.Remenber)
                //    {
                //        this.LoginPassWord = result.LoginPassWord;
                //        this.Remenber = true;
                //    }

                //    this.LoginUseName = result.LoginUseName;
                //}
            }
        }
    }



}
