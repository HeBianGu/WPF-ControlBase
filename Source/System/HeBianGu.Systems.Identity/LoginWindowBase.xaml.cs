// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Systems.Identity
{
    [TemplatePart(Name = "PART_BUTTON_SUMIT", Type = typeof(ProgressButton))]
    public partial class LoginWindowBase : DialogWindowBase
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LoginWindowBase), "S.Window.Login.Default");

        static LoginWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LoginWindowBase), new FrameworkPropertyMetadata(typeof(LoginWindowBase)));
        }

        private ProgressButton _button;
        public LoginWindowBase()
        {
            //this.ShowAnimation = l =>
            //{

            //    //l.RenderTransformOrigin = new Point(0.5, 0.5);

            //    //var engine2 = DoubleStoryboardEngine.Create(0.5, 1, 0.5, "Opacity");
            //    //var engine = DoubleStoryboardEngine.Create(0.1, 0.98, 1, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
            //    //var engine1 = DoubleStoryboardEngine.Create(0.1, 0.98, 1, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

            //    //engine.Start(l);
            //    //engine1.Start(l);
            //    //engine2.Start(l);

            //    this.Show(true);
            //};

            this.UseName = IdentifySetting.Instance.LastUserName;
            this.PassWord = IdentifySetting.Instance.LastPassword;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._button = Template.FindName("PART_BUTTON_SUMIT", this) as ProgressButton;
            if (this._button == null) return;

            this._button.Click += async (l, k) =>
            {
                this._button.IsBusy = true;
                this._button.Message = "登录中...";
                string account = this.UseName;
                string password = this.PassWord;
                string message = null;
                bool r = await Task.Run(() =>
                {
                    Thread.Sleep(1000);
                    return Loginner.Instance.Login(account, password, out message);
                });

                if (r)
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
                    await Task.Delay(1000).ContinueWith(t =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            this._button.IsBusy = false;
                        });
                    });
                }

            };
        }

        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LoginWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public string UseName
        {
            get { return (string)GetValue(UseNameProperty); }
            set { SetValue(UseNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseNameProperty =
            DependencyProperty.Register("UseName", typeof(string), typeof(LoginWindowBase), new PropertyMetadata(default(string), (d, e) =>
            {
                LoginWindowBase control = d as LoginWindowBase;

                if (control == null) return;

                string config = e.NewValue as string;

            }));


        public string PassWord
        {
            get { return (string)GetValue(PassWordProperty); }
            set { SetValue(PassWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.Register("PassWord", typeof(string), typeof(LoginWindowBase), new PropertyMetadata(default(string), (d, e) =>
            {
                LoginWindowBase control = d as LoginWindowBase;

                if (control == null) return;

                string config = e.NewValue as string;

            }));


        public string Product
        {
            get { return (string)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(string), typeof(LoginWindowBase), new FrameworkPropertyMetadata(default(string), (d, e) =>
             {
                 LoginWindowBase control = d as LoginWindowBase;

                 if (control == null) return;

                 if (e.OldValue is string o)
                 {

                 }

                 if (e.NewValue is string n)
                 {

                 }
             }));



        //public void InitAccount(Func<Tuple<string, string, bool>> func)
        //{
        //    this.IsLoginEnbled = false;

        //    this.LoginMessage = "正在加载";

        //    Task.Run(() =>
        //    {
        //        Tuple<string, string, bool> t = func?.Invoke();

        //        if (t == null)
        //        {

        //        }
        //        else
        //        {
        //            Application.Current.Dispatcher.Invoke(() =>
        //            {
        //                this.UseName = t.Item1;
        //                this.PassWord = t.Item2;
        //                this.Remenber = t.Item3;
        //                this.IsLoginEnbled = true;
        //                this.LoginMessage = "登陆";
        //            });
        //        }
        //    });
        //}
    }
}