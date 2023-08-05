using HeBianGu.App.Touch.View.Share;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvc;
using System;

namespace HeBianGu.App.Touch
{
    [ViewModel("Login")]
    internal class LoginViewModel : MvcViewModelBase
    {
        private IAssemblyDomain _domain = null;

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


        private string _passWord = "111111";
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


        public RelayCommand RelayCommand => new RelayCommand(RelayMethod);
        /// <summary> 命令通用方法 </summary>
        protected async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：登录
            if (command == "Button.Click.Login")
            {
                if (PassWord == "111111")
                {
                    _domain.GoToLinkAction("Loyout", "Home"); return;
                }

                MessageProxy.Snacker.ShowTime("身份证号正确，请输入111111");
            }
            //  Do：设置
            else if (command == "Button.Click.Setting")
            {
                Stop();

                _domain.GoToLinkAction("Setting", "Setting");

                AdminLoginControl adminLogin = new AdminLoginControl();

                bool result = await Messager.Instance.ShowDialog<bool>(adminLogin);

                if (result) return;

                _domain.GoToLinkAction("Login", "Login");


            }
            /// <summary> 退出 </summary>
            else if (command == "Button.Click.ShutDown")
            {
                Stop();

                (System.Windows.Application.Current.MainWindow as WindowBase)?.Close();
            }
            /// <summary> 超时屏保 </summary>
            else if (command == "Button.Click.Awaitting")
            {
                Stop();

                ShowWaitting();
            }

        }

        private void Stop()
        {
            IsActive = false;
        }

        public void ShowWaitting()
        {
            AwaitControl awaitControl = AwaitControl.Create();

            awaitControl.Closed += (l, k) =>
            {
                IsActive = true;
            };

            MessageProxy.Container.Show(awaitControl);
        }

    }
}
