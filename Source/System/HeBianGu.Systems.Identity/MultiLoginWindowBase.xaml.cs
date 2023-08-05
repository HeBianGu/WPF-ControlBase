// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Systems.Identity
{
    [TemplatePart(Name = "PART_BUTTON_SUMIT", Type = typeof(ProgressButton))]

    public partial class MultiLoginWindowBase : DialogWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MultiLoginWindowBase), "S.Window.MultiLogin.Default");

        static MultiLoginWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(typeof(MultiLoginWindowBase)));
        }

        private ProgressButton _button;

        public MultiLoginWindowBase()
        {
            //  Do ：登录 
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.Login, async (l, k) =>
                {
                    this._button.IsBusy = true;
                    this._button.Message = "登录中...";
                    string message = null;
                    string account = this.LoginAccount;
                    string password = this.LoginPassword;

                    bool result = await Task.Run(() =>
                    {
                        return Loginner.Instance.Login(account, password, out message);
                    });

                    if (result)
                    {
                        this._button.Message = "登录成功";
                        await Task.Delay(1000).ContinueWith(t =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                this.Result = true;
                                this.BeginClose();
                            });
                        });
                    }
                    else
                    {
                        this._button.Message = message;
                    }

                    this._button.IsBusy = false;

                });

                this.CommandBindings.Add(binding);
            }

            //  Do ：注册/重置密码
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.Register, async (l, k) =>
                {
                    string message = null;
                    string phone = this.Phone;
                    string password = this.RegisterPassword;
                    string verify = this.Verifypassword;

                    if (this.RegisterPassword != this.Verifypassword)
                    {
                        this.RegistorErrorMessage = "两次输入的密码不匹配";
                        return;
                    }

                    if (string.IsNullOrEmpty(Verifypassword))
                    {
                        this.RegistorErrorMessage = "密码不能为空";
                        return;
                    }
                    this.IsBuzy = true;
                    bool isForget = this.IsForgetpassword;
                    bool result = await Task.Run(() =>
                    {
                        return isForget ? Register.Instance.Register(phone, password, out message)
                        : Register.Instance.Register(phone, password, out message);
                    });

                    if (result)
                    {
                        this.RegisterMessage = this.IsForgetpassword ? "重置成功" : "注册成功";

                        await Task.Delay(1000).ContinueWith(t =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
#if DEBUG
                                this.LoginAccount = this.Phone;
                                this.LoginPassword = this.RegisterPassword;
#endif

                                this.IsLoginPage = true;

                                this.IsBuzy = false;

                                this.ClearRegister();
                            });
                        });
                    }
                    else
                    {
                        this.RegistorErrorMessage = message;

                        this.IsBuzy = false;
                    }
                });

                this.CommandBindings.Add(binding);
            }

            //  Do ：下一页
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.NextPage, (l, k) =>
                {
                    this.RegistorErrorMessage = null;

                    if (this.InputVerificationCode != this.VerificationCode)
                    {
                        this.RegistorErrorMessage = "验证码不匹配"; return;
                    }

                    if (this.InputPhoneVerificationCode != this.PhoneVerificationCode)
                    {
                        this.RegistorErrorMessage = "短信验证码不匹配"; return;
                    }

                    if (string.IsNullOrEmpty(InputPhoneVerificationCode))
                    {
                        this.RegistorErrorMessage = "短信验证码不能为空"; return;
                    }

                    this.IsNextPage = true;
                });

                this.CommandBindings.Add(binding);
            }

            //  Do ：刷新验证码
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.VerificationCode, async (l, k) =>
                {
                    await this.RefreshVerificationCode();
                });

                this.CommandBindings.Add(binding);
            }

            //  Do ：获取短信验证码
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.PhoneVerificationCode, async (l, k) =>
                {
                    string message;

                    string phone = this.Phone;

                    string code = await Task.Run(() => Register.Instance.GetPhoneVerificationCode(phone, out message));

                    this.PhoneVerificationCode = code;

#if DEBUG
                    this.InputPhoneVerificationCode = code;
#endif
                });

                this.CommandBindings.Add(binding);
            }

            //  Do ：服务协议
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.ServiceAgreement, (l, k) =>
                {
                    string uri = IdentifySetting.Instance.ServiceAgreementUri;

                    Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
                    //Process.Start(uri);
                });

                this.CommandBindings.Add(binding);
            }

            //  Do ：隐私政策
            {
                CommandBinding binding = new CommandBinding(IdentifyCommands.Privacypolicy, (l, k) =>
                {
                    string uri = IdentifySetting.Instance.PrivacypolicyUri;
                    Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });

                    //Process.Start(uri);
                });

                this.CommandBindings.Add(binding);
            }

            this.Loaded += async (l, k) =>
            {
                await this.RefreshVerificationCode();
            };


            this.LoginAccount = IdentifySetting.Instance.LastUserName;
            this.LoginPassword = IdentifySetting.Instance.LastPassword;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._button = Template.FindName("PART_BUTTON_SUMIT", this) as ProgressButton;
        }
        #region - Method -

        public void ClearRegister()
        {
            this.Phone = null;
            this.RegisterPassword = null;
            this.RegistorErrorMessage = null;
            this.Verifypassword = null;
            this.InputVerificationCode = null;
            this.InputPhoneVerificationCode = null;
            this.PhoneVerificationCode = null;
            this.IsAgree = false;
            this.IsNextPage = false;
        }

        private async Task RefreshVerificationCode()
        {
            string message;

            this.IsBuzy = true;

            string code = await Task.Run(() => Register.Instance.GetVerificationCode(out message));

            this.IsBuzy = false;

            this.VerificationCode = code;

#if DEBUG
            this.InputVerificationCode = code;
#endif
        }

        #endregion

        #region - Propertys -

        /// <summary>
        /// 阅读并同意
        /// </summary>
        public bool IsAgree
        {
            get { return (bool)GetValue(IsAgreeProperty); }
            set { SetValue(IsAgreeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAgreeProperty =
            DependencyProperty.Register("IsAgree", typeof(bool), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));

        /// <summary>
        /// 是否是忘记密码页面
        /// </summary>
        public bool IsForgetpassword
        {
            get { return (bool)GetValue(IsForgetpasswordProperty); }
            set { SetValue(IsForgetpasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsForgetpasswordProperty =
            DependencyProperty.Register("IsForgetpassword", typeof(bool), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));

        /// <summary>
        /// 是否是登录页面
        /// </summary>
        public bool IsLoginPage
        {
            get { return (bool)GetValue(IsLoginPageProperty); }
            set { SetValue(IsLoginPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoginPageProperty =
            DependencyProperty.Register("IsLoginPage", typeof(bool), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));

        /// <summary>
        /// 是否注册页面切换到下一页
        /// </summary>
        public bool IsNextPage
        {
            get { return (bool)GetValue(IsNextPageProperty); }
            set { SetValue(IsNextPageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsNextPageProperty =
            DependencyProperty.Register("IsNextPage", typeof(bool), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));

        /// <summary>
        /// 注册的日志信息
        /// </summary>
        public string RegisterMessage
        {
            get { return (string)GetValue(RegisterMessageProperty); }
            set { SetValue(RegisterMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegisterMessageProperty =
            DependencyProperty.Register("RegisterMessage", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 注册的错误日志
        /// </summary>
        public string RegistorErrorMessage
        {
            get { return (string)GetValue(RegistorErrorMessageProperty); }
            set { SetValue(RegistorErrorMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegistorErrorMessageProperty =
            DependencyProperty.Register("RegistorErrorMessage", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 输入的验证码
        /// </summary>
        public string InputVerificationCode
        {
            get { return (string)GetValue(InputVerificationCodeProperty); }
            set { SetValue(InputVerificationCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputVerificationCodeProperty =
            DependencyProperty.Register("InputVerificationCode", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode
        {
            get { return (string)GetValue(VerificationCodeProperty); }
            set { SetValue(VerificationCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerificationCodeProperty =
            DependencyProperty.Register("VerificationCode", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 输入的短信验证码
        /// </summary>
        public string InputPhoneVerificationCode
        {
            get { return (string)GetValue(InputPhoneVerificationCodeProperty); }
            set { SetValue(InputPhoneVerificationCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputPhoneVerificationCodeProperty =
            DependencyProperty.Register("InputPhoneVerificationCode", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 短信验证码
        /// </summary>
        public string PhoneVerificationCode
        {
            get { return (string)GetValue(PhoneVerificationCodeProperty); }
            set { SetValue(PhoneVerificationCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhoneVerificationCodeProperty =
            DependencyProperty.Register("PhoneVerificationCode", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone
        {
            get { return (string)GetValue(PhoneProperty); }
            set { SetValue(PhoneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PhoneProperty =
            DependencyProperty.Register("Phone", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(MultiLoginWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        ///// <summary>
        ///// 登录按钮是否可用
        ///// </summary>
        //public bool IsLoginEnbled
        //{
        //    get { return (bool)GetValue(IsLoginEnbledProperty); }
        //    set { SetValue(IsLoginEnbledProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsLoginEnbledProperty =
        //    DependencyProperty.Register("IsLoginEnbled", typeof(bool), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        //     {
        //         MultiLoginWindowBase control = d as MultiLoginWindowBase;

        //         if (control == null) return;

        //         if (e.OldValue is bool o)
        //         {

        //         }

        //         if (e.NewValue is bool n)
        //         {

        //         }

        //     }));

        /// <summary>
        /// 登录账号
        /// </summary>
        public string LoginAccount
        {
            get { return (string)GetValue(LoginAccountProperty); }
            set { SetValue(LoginAccountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginAccountProperty =
            DependencyProperty.Register("LoginAccount", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 输入密码
        /// </summary>
        public string RegisterPassword
        {
            get { return (string)GetValue(RegisterPasswordProperty); }
            set { SetValue(RegisterPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegisterPasswordProperty =
            DependencyProperty.Register("RegisterPassword", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        /// <summary>
        /// 再次输入密码
        /// </summary>
        public string Verifypassword
        {
            get { return (string)GetValue(VerifypasswordProperty); }
            set { SetValue(VerifypasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerifypasswordProperty =
            DependencyProperty.Register("Verifypassword", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));


        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPassword
        {
            get { return (string)GetValue(LoginPasswordProperty); }
            set { SetValue(LoginPasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginPasswordProperty =
            DependencyProperty.Register("LoginPassword", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }

             }));

        ///// <summary>
        ///// 登录日志
        ///// </summary>
        //public string LoginMessage
        //{
        //    get { return (string)GetValue(LoginMessageProperty); }
        //    set { SetValue(LoginMessageProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty LoginMessageProperty =
        //    DependencyProperty.Register("LoginMessage", typeof(string), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata("登录", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
        //     {
        //         MultiLoginWindowBase control = d as MultiLoginWindowBase;

        //         if (control == null) return;

        //         if (e.OldValue is string o)
        //         {

        //         }

        //         if (e.NewValue is string n)
        //         {

        //         }

        //     }));


        /// <summary>
        /// 正在执行
        /// </summary>
        public bool IsBuzy
        {
            get { return (bool)GetValue(IsBuzyProperty); }
            set { SetValue(IsBuzyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBuzyProperty =
            DependencyProperty.Register("IsBuzy", typeof(bool), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public object IntroductionContent
        {
            get { return GetValue(IntroductionContentProperty); }
            set { SetValue(IntroductionContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntroductionContentProperty =
            DependencyProperty.Register("IntroductionContent", typeof(object), typeof(MultiLoginWindowBase), new FrameworkPropertyMetadata(default(object), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 MultiLoginWindowBase control = d as MultiLoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is object o)
                 {

                 }

                 if (e.NewValue is object n)
                 {

                 }

             }));



        #endregion

    }

}