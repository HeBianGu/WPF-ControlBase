// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.General.WpfControlLib
{
    public class LoginViewModel : NotifyPropertyChanged
    {
        private string _loginUseName;
        public string LoginUseName
        {
            get { return _loginUseName; }
            set
            {
                _loginUseName = value;
                RaisePropertyChanged("LoginUseName");
            }
        }


        private string _loginPassWord;
        public string LoginPassWord
        {
            get { return _loginPassWord; }
            set
            {
                _loginPassWord = value;
                RaisePropertyChanged("LoginPassWord");
            }
        }



        private bool _isLogined;
        public bool IsLogined
        {
            get { return _isLogined; }
            set
            {
                _isLogined = value;
                RaisePropertyChanged("IsLogined");
            }
        }


        private string _loginMessage = "登录";
        public string LoginMessage
        {
            get { return _loginMessage; }
            set
            {
                _loginMessage = value;
                RaisePropertyChanged("LoginMessage");
            }
        }

        private bool _remenber;
        public bool Remenber
        {
            get { return _remenber; }
            set
            {
                _remenber = value;
                RaisePropertyChanged("Remenber");
            }
        }

        private bool _isBuzy;
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }



        private bool _isEnbled = true;
        /// <summary> 说明  </summary>
        public bool IsEnbled
        {
            get { return _isEnbled; }
            set
            {
                _isEnbled = value;
                RaisePropertyChanged("IsEnbled");
            }
        }


    }
}
