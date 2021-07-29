using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    [TemplatePart(Name = "PART_BUTTON_SUMIT", Type = typeof(Button))]
    public partial class LoginWindowBase : DialogWindowBase
    {
        Button _button;
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
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._button = Template.FindName("PART_BUTTON_SUMIT", this) as Button;

            if (this._button == null) return;

            this._button.Click += async (l, k) =>
              {
                  this.IsBuzy = true;
                  this.IsLoginEnbled = false;

                  bool r = this.IsMatch == null ? true : await this.IsMatch();

                  if (r)
                  {
                      this.IsBuzy = false;
                      this.LoginMessage = "登录成功";

                      await Task.Delay(1000).ContinueWith(t =>
                       {
                           Application.Current.Dispatcher.Invoke(() =>
                           {
                               this.DialogResult = true;
                               this.Close();
                           });

                       });
                  }
                  else
                  {
                      this.IsBuzy = false;
                      this.IsLoginEnbled = false;

                      await Task.Delay(1000).ContinueWith(t =>
                       {
                           Application.Current.Dispatcher.Invoke(() =>
                           {
                               this.LoginMessage = "登录";
                               this.IsLoginEnbled = true;
                           });

                       });
                  }
              };
        }

        public Func<Task<bool>> IsMatch { get; set; }

        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LoginWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        [Obsolete]
        public bool IsLogined
        {
            get { return (bool)GetValue(IsLoginedProperty); }
            set { SetValue(IsLoginedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoginedProperty =
            DependencyProperty.Register("IsLogined", typeof(bool), typeof(LoginWindowBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                LoginWindowBase control = d as LoginWindowBase;

                if (control == null) return;

                bool config = (bool)e.NewValue;

                if (config)
                {
                    control.Result = true;
                    control.Show(false);
                }

            }));
        [Obsolete]
        public bool Result { get; set; }


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


        public bool Remenber
        {
            get { return (bool)GetValue(RemenberProperty); }
            set { SetValue(RemenberProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemenberProperty =
            DependencyProperty.Register("Remenber", typeof(bool), typeof(LoginWindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 LoginWindowBase control = d as LoginWindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public string LoginMessage
        {
            get { return (string)GetValue(LoginMessageProperty); }
            set { SetValue(LoginMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoginMessageProperty =
            DependencyProperty.Register("LoginMessage", typeof(string), typeof(LoginWindowBase), new PropertyMetadata("登陆", (d, e) =>
             {
                 LoginWindowBase control = d as LoginWindowBase;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        public bool IsLoginEnbled
        {
            get { return (bool)GetValue(IsLoginEnbledProperty); }
            set { SetValue(IsLoginEnbledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoginEnbledProperty =
            DependencyProperty.Register("IsLoginEnbled", typeof(bool), typeof(LoginWindowBase), new PropertyMetadata(true, (d, e) =>
             {
                 LoginWindowBase control = d as LoginWindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public bool IsBuzy
        {
            get { return (bool)GetValue(IsBuzyProperty); }
            set { SetValue(IsBuzyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBuzyProperty =
            DependencyProperty.Register("IsBuzy", typeof(bool), typeof(LoginWindowBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 LoginWindowBase control = d as LoginWindowBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        public void InitAccount(Func<Tuple<string, string, bool>> func)
        {
            this.IsLoginEnbled = false;

            this.LoginMessage = "正在加载";

            Task.Run(() =>
            {
                var t = func?.Invoke();

                if (t == null)
                {

                }
                else
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.UseName = t.Item1;
                        this.PassWord = t.Item2;
                        this.Remenber = t.Item3;
                        this.IsLoginEnbled = true;
                        this.LoginMessage = "登陆";
                    });
                }



            });
        }
    }


}
