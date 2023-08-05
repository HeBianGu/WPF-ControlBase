// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using HeBianGu.Systems.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddMultiIdentity(this IServiceCollection service)
        {
            service.AddSingleton<IUserViewPresenter, UserViewPresenter>();
            service.AddSingleton<IIdentityInitService, IMultidentityInitService>();
            service.AddSingleton<ILoginService, LoginService>();
            service.AddSingleton<IRegisterService, RegisterService>();
            //SystemSetting.Instance?.Add(IdentifySetting.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddXmlMultiIdentity(this IServiceCollection service)
        {
            service.AddSingleton<ILoginService, XmlLoginService>();
            service.AddSingleton<IRegisterService, XmlRegisterService>();
            service.AddSingleton<IIdentityInitService, IMultidentityInitService>();
            //SystemSetting.Instance?.Add(IdentifySetting.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddIdentity(this IServiceCollection service, Action<ILoginOption> action = null)
        {
            service.AddSingleton<IUserViewPresenter, UserViewPresenter>();
            service.AddSingleton<ILoginService, LoginService>();
            service.AddSingleton<IIdentityInitService, IdentityLoginService>();
            action?.Invoke(LoginSetting.Instance);
            SystemSetting.Instance.Add(LoginSetting.Instance);
            //SystemSetting.Instance?.Add(IdentifySetting.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLoginViewPresenter(this IServiceCollection service, Action<ILoginViewPresenterOption> action = null)
        {
            //service.AddWindowCaptionViewPresenter();
            service.AddSingleton<ILoginViewPresenter, LoginViewPresenter>();
            action?.Invoke(LoginViewPresenter.Instance);
            //WindowCaptionViewPresenter.Instance.AddPersenter(LoginViewPresenter.Instance);
            SystemSetting.Instance.Add(LoginViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLoginManagerViewPresenter(this IServiceCollection service, Action<ILoginManagerViewPresenterOption> action = null)
        {
            //service.AddLoginViewPresenter();
            service.AddSingleton<ILoginManagerViewPresenter, LoginManagerViewPresenter>();
            action?.Invoke(LoginManagerViewPresenter.Instance);
            //WindowCaptionViewPresenter.Instance.AddPersenter(LoginViewPresenter.Instance);
            SystemSetting.Instance.Add(LoginManagerViewPresenter.Instance);
        }

        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddLoginManagerDefaultViewPresenter(this IServiceCollection service, Action<ILoginManagerViewPresenterOption> action = null)
        //{
        //    service.AddLoginViewPresenter();
        //    service.AddSingleton<ILoginManagerViewPresenter, LoginManagerViewPresenter>();
        //    action?.Invoke(LoginManagerViewPresenter.Instance);
        //    //WindowCaptionViewPresenter.Instance.AddPersenter(LoginViewPresenter.Instance);
        //    SystemSetting.Instance.Add(LoginManagerViewPresenter.Instance);

        //    service.AddSingleton<IUserViewPresenter, UserViewPresenter>();
        //    LoginManagerViewPresenter.Instance.AddPersenter(UserViewPresenter.Instance);
        //    service.AddUserViewPresenter();
        //    service.AddLoginManagerRoleViewPresenter();
        //    service.AddLoginManagerPasswordEditViewPresenter();
        //    service.AddLoginManagerLogoutViewPresenter();
        //}

        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddRoleViewPresenter(this IServiceCollection service, Action<IRoleViewPresenterOption> action = null)
        //{
        //    //service.AddLoginManagerViewPresenter();
        //    service.AddSingleton<IRoleViewPresenter, RoleViewPresenter>();
        //    action?.Invoke(RoleViewPresenter.Instance);
        //    SystemSetting.Instance.Add(RoleViewPresenter.Instance);
        //    //LoginManagerViewPresenter.Instance.AddPersenter(RoleViewPresenter.Instance);
        //}

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddPasswordEditViewPresenter(this IServiceCollection service, Action<IPasswordEditViewPresenterOption> action = null)
        {
            service.AddSingleton<IPasswordEditViewPresenter, PasswordEditViewPresenter>();
            action?.Invoke(PasswordEditViewPresenter.Instance);
            SystemSetting.Instance.Add(PasswordEditViewPresenter.Instance);
        }

        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddLogoutViewPresenter(this IServiceCollection service, Action<ILogoutViewPresenterOption> action = null)
        //{
        //    //service.AddLoginManagerViewPresenter();
        //    service.AddSingleton<ILogoutViewPresenter, LogoutViewPresenter>();
        //    action?.Invoke(LogoutViewPresenter.Instance);
        //    SystemSetting.Instance.Add(LogoutViewPresenter.Instance);
        //    //LoginManagerViewPresenter.Instance.AddPersenter(LogoutViewPresenter.Instance);
        //}

        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddLogoutViewPresenter(this IServiceCollection service, Action<ILogoutViewPresenterOption> action = null)
        {
            //service.AddMoreViewPresenter();
            service.AddSingleton<ILogoutViewPresenter, LogoutViewPresenter>();
            action?.Invoke(LogoutViewPresenter.Instance);
            //MoreViewPresenter.Instance.AddPersenter(LogoutViewPresenter.Instance);
            SystemSetting.Instance.Add(LogoutViewPresenter.Instance);
        }



        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddUserViewPresenter(this IServiceCollection service, Action<IUserViewPresenterOption> action = null)
        {
            service.AddSingleton<IUserViewPresenter, UserViewPresenter>();
            action?.Invoke(UserViewPresenter.Instance);
            SystemSetting.Instance.Add(UserViewPresenter.Instance);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="service"></param>
        public static void AddRoleViewPresenter(this IServiceCollection service, Action<IRoleViewPresenterOption> action = null)
        {
            service.AddSingleton<IRoleViewPresenter, RoleViewPresenter>();
            action?.Invoke(RoleViewPresenter.Instance);
            SystemSetting.Instance.Add(RoleViewPresenter.Instance);
        }

        ///// <summary>
        ///// 注册
        ///// </summary>
        ///// <param name="service"></param>
        //public static void AddAuthority(this IServiceCollection service, Action<IAuthorityOption> action = null)
        //{
        //    service.AddSingleton<IAuthorityService, AuthorityService>();
        //    action?.Invoke(AuthorityService.Instance);
        //    SystemSetting.Instance.Add(AuthorityService.Instance);
        //}

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="service"></param>
        [Obsolete]
        public static void UseIdentity(this IApplicationBuilder service, Action<IdentifySetting> action)
        {
            action?.Invoke(IdentifySetting.Instance);
            SystemSetting.Instance?.Add(IdentifySetting.Instance);
        }
    }


    [Displayer(Name = "登录", GroupName = SystemSetting.GroupAuthority)]
    public class IdentifySetting : LazySettingInstance<IdentifySetting>
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this._title = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>()?.Product;
        }


        private string _title;
        [Display(Name = "登录标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _userName;
        [DefaultValue("admin")]
        [Required]
        [Display(Name = "用户")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }

        private string _password;
        [DefaultValue("123456")]
        [Required]
        [Display(Name = "密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        private string _lastUserName;
        [DefaultValue("admin")]
        [Required]
        [Display(Name = "上次登录用户")]
        public string LastUserName
        {
            get { return _lastUserName; }
            set
            {
                _lastUserName = value;
                RaisePropertyChanged();
            }
        }

        private string _lastPassword;
        [DefaultValue("123456")]
        [Required]
        [Display(Name = "上次登录密码")]
        public string LastPassword
        {
            get { return _lastPassword; }
            set
            {
                _lastPassword = value;
                RaisePropertyChanged();
            }
        }

        private bool _remember = true;
        [Display(Name = "记住密码")]
        public bool Remember
        {
            get { return _remember; }
            set
            {
                _remember = value;
                RaisePropertyChanged();
            }
        }

        private string _serviceAgreementUri;
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "应用许可")]
        public string ServiceAgreementUri
        {
            get { return _serviceAgreementUri; }
            set
            {
                _serviceAgreementUri = value;
                RaisePropertyChanged();
            }
        }

        private string _privacypolicyUri;
        [DefaultValue("https://github.com/HeBianGu/WPF-ControlBase")]
        [Display(Name = "隐私策略")]
        public string PrivacypolicyUri
        {
            get { return _privacypolicyUri; }
            set
            {
                _privacypolicyUri = value;
                RaisePropertyChanged();
            }
        }

        private string _passwordRegular;
        [Display(Name = "密码规则")]
        public string PasswordRegular
        {
            get { return _passwordRegular; }
            set
            {
                _passwordRegular = value;
                RaisePropertyChanged();
            }
        }

        private string _logo;
        [DefaultValue("pack://application:,,,/HeBianGu.General.WpfControlLib;component/Resources/logo.png")]
        [Display(Name = "登录图标")]
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged();
            }
        }

        private bool _useVisitor = false;
        [DefaultValue(false)]
        [Display(Name = "启用访客", Description = "启用访客模式后登录不成功也可以进入主窗口")]
        public bool UseVisitor
        {
            get { return _useVisitor; }
            set
            {
                _useVisitor = value;
                RaisePropertyChanged();
            }
        }

    }
}
