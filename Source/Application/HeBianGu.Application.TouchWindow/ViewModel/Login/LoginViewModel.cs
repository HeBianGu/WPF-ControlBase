using HeBianGu.Application.TouchWindow.View.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Application.TouchWindow
{
    [ViewModel("Login")]
    class LoginViewModel : MvcViewModelBase
    {
        IAssemblyDomain _domain = null;

        public LoginViewModel(IAssemblyDomain domain)
        {
            _domain = domain;
        }
        protected override void Init()
        {

        }


        protected override void Loaded(string args)
        {

        }


        private bool _isActive = false;
        /// <summary> 用于开始倒计时  </summary>
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                RaisePropertyChanged("IsActive");
            }
        }


        private string _passWord="111111";
        /// <summary> 说明  </summary>
        public string PassWord
        {
            get { return _passWord; }
            set
            {
                _passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：登录
            if (command == "Button.Click.Login")
            {
                if (this.PassWord == "111111")
                {
                    _domain.GoToLinkAction("Loyout", "Home"); return;
                }

                MessageService.ShowSnackMessageWithNotice("身份证号正确，请输入111111");
            }
            //  Do：设置
            else if (command == "Button.Click.Setting")
            {
                this.Stop();

                _domain.GoToLinkAction("Setting", "Setting");

                AdminLoginControl adminLogin = new AdminLoginControl();

                var result = await MessageService.ShowCustomDialog<bool>(adminLogin);

                if (result) return;

                _domain.GoToLinkAction("Login", "Login");


            }
            /// <summary> 退出 </summary>
            else if (command == "Button.Click.ShutDown")
            {
                this.Stop();

                (System.Windows.Application.Current.MainWindow as WindowBase)?.Close();
            }
            /// <summary> 超时屏保 </summary>
            else if (command == "Button.Click.Awaitting")
            {
                this.Stop();

                this.ShowWaitting();
            }

        }

        void Stop()
        {
            this.IsActive = false;
        }

        public void ShowWaitting()
        {
            AwaitControl awaitControl = AwaitControl.Create();

            awaitControl.Closed += (l, k) =>
            {
                this.IsActive = true;
            };

            MessageService.ShowLayer(awaitControl);
        }

    }
}
