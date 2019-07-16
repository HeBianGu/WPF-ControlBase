using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfControlLib
{
   public class LoginViewModel : NotifyPropertyChanged
    {
        private string _loginUseName;
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public bool IsLogined
        {
            get { return _isLogined; }
            set
            {
                _isLogined = value;
                RaisePropertyChanged("IsLogined");
            }
        }


        private string _loginMessage="登录";
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged("IsBuzy");
            }
        }



        private bool _isEnbled=true;
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
