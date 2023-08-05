// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HeBianGu.Control.PasswordBox
{
    public interface IPasswordDialogServiceOption
    {
        string Notice { get; set; }
        string Password { get; set; }
        string Title { get; set; }
    }

    [Displayer(Name = "密码设置", GroupName = SystemSetting.GroupApp)]
    public class PasswordDialogService : ServiceSettingInstance<PasswordDialogService, IPasswordDialogService>, IPasswordDialogService, IPasswordDialogServiceOption
    {
        private string _password;
        [DefaultValue("123456")]
        [Display(Name = "系统密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }


        private string _title;
        [DefaultValue("密码")]
        [Display(Name = "标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _notice;
        [DefaultValue("如果忘记请联系管理员，或尝试回复默认配置")]
        [Display(Name = "密码提示")]
        public string Notice
        {
            get { return _notice; }
            set
            {
                _notice = value;
                RaisePropertyChanged();
            }
        }

        private bool _useRemeber;
        [Browsable(false)]
        [DefaultValue(false)]
        [Display(Name = "启用记住密码设置")]
        public bool UseRemeber
        {
            get { return _useRemeber; }
            set
            {
                _useRemeber = value;
                RaisePropertyChanged();
            }
        }


        private bool _remeber;
        [Display(Name = "启用记住密码")]
        public bool Remeber
        {
            get { return _remeber; }
            set
            {
                _remeber = value;
                RaisePropertyChanged();
            }
        }


        public async Task<bool> ShowPasswordDialog()
        {
            PasswordPresenter password = new PasswordPresenter()
            {
                Title = this.Title,
                Notice = this.Notice
            };
            if (this.Remeber && this.UseRemeber)
            {
                password.Value = this.Password;
            }
            var r = await MessageProxy.Presenter.Show(password, x =>
             {
                 if (!(password.Value?.Trim() == this.Password))
                 {
                     MessageProxy.Snacker.ShowTime("密码错误");
                     return false;
                 }
                 return true;
             }, "请输入密码", x =>
             {
                 x.Padding = new System.Windows.Thickness(10);
                 x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                 x.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
             });

            if (!r) this.Remeber = false;
            return r;
        }
    }

    public interface IPasswordDialogService
    {
        Task<bool> ShowPasswordDialog();
    }

    //public class PasswordDialogger : ServiceInstance<IPasswordDialogService>
    //{

    //}

    //[Displayer(Name = "密码设置", GroupName = SystemSetting.GroupApp)]
    //public class PasswordSetting : LazySettingInstance<PasswordSetting>
    //{


    //}

    public class PasswordPresenter : NotifyPropertyChangedBase
    {
        #region - 属性 -


        private string _title;
        /// <summary> 说明  </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _notice;
        /// <summary> 说明  </summary>
        public string Notice
        {
            get { return _notice; }
            set
            {
                _notice = value;
                RaisePropertyChanged();
            }
        }


        private string _value;
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}
