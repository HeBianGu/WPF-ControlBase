using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
   public class SettingViewModel : NotifyPropertyChanged
    {
        string _ip;
        /// <summary> 数据库ip地址 </summary>
        public string IP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
                RaisePropertyChanged();
            }
        }

        string _port;
        /// <summary> 数据库端口号 </summary>
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
                RaisePropertyChanged();
            }
        }

        string _name;
        /// <summary> 数据库名称 </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        string _user;
        /// <summary> 数据库登录名称 </summary>
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        string _psd;
        /// <summary> 数据库登录密码 </summary>
        public string PSD
        {
            get
            {
                return _psd;
            }
            set
            {
                _psd = value;
                RaisePropertyChanged();
            }
        }

        internal bool IsaVailable()
        {
            return !this.GetType().GetProperties().ToList().Any(l => l.GetValue(this) == null || l.GetValue(this).ToString() == string.Empty);
        }
    }
}
