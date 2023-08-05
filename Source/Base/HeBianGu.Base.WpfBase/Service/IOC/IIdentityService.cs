// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// 认证接口
    /// </summary>
    //public interface IIdentityService
    //{
    //    IUser User { get; }

    //    bool Login(string name, string password, out string message);
    //}

    public interface ILoginService
    {
        IUser User { get; }

        bool Login(string name, string password, out string message);

        bool Logout(out string message);
    }

    public interface IRegisterService
    {
        bool Register(string phone, string password, out string message);

        string GetPhoneVerificationCode(string phone, out string message);

        string GetVerificationCode(out string message);

        bool ResetPassword(string phone, string password, out string message);
    }

    public class Loginner : ServiceInstance<ILoginService>
    {

    }

    public class Register : ServiceInstance<IRegisterService>
    {

    }

    public interface IUser
    {
        string ID { get; }
        string Account { get; set; }
        string Password { get; set; }
        string Name { get; set; }
        //string RoleID { get; set; }
        //IRole Role { get; set; }
        bool IsValid(string authorId);
    }

    public interface IAuthor
    {
        string ID { get; }
        string Name { get; }
    }

    public interface IRole
    {
        string ID { get; }
        string Name { get; set; }
        //ObservableCollection<IAuthor> Authors { get; set; }

        bool IsValid(string authorId);
    }

    public interface IAuthority
    {
        string ID { get; }
        string Name { get; }
        string GroupName { get; }
        //string AuthorPredefinePath { get;}
        bool IsAuthority { get; }
    }

    public class Authority : IAuthority
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        //public string AuthorPredefinePath { get; set; }
        public bool IsAuthority { get; set; }
    }

    //public class AuthorityAttribute : System.Attribute
    //{
    //    public string ID { get; set; }
    //    public string Name { get; set; }
    //    public string GroupName { get; set; }
    //}

    [Displayer(Name = "权限设置", GroupName = SystemSetting.GroupSystem)]
    public class AuthoritySetting : LazyNotifyInstance<AuthoritySetting>
    {
        private bool _useAuthority = true;
        [Display(Name = "启用权限控制")]
        public bool UseAuthority
        {
            get { return _useAuthority; }
            set
            {
                _useAuthority = value;
                RaisePropertyChanged();
            }
        }

        private bool _useAllIfNoLogin = true;
        [Display(Name = "启用如果没有登录用户加载所有权限")]
        public bool UseAllIfNoLggin
        {
            get { return _useAllIfNoLogin; }
            set
            {
                _useAllIfNoLogin = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<IAuthority> _authoritys = new ObservableCollection<IAuthority>();
        public ObservableCollection<IAuthority> Authoritys
        {
            get { return _authoritys; }
            set
            {
                _authoritys = value;
                RaisePropertyChanged();
            }
        }

    }

    //public class AuthorityProxy : ServiceInstance<IAuthorityService>
    //{

    //}

    //public interface IAuthorityService
    //{
    //    void Add(params IAuthority[] settings);

    //    bool IsValid(string id);
    //}
}