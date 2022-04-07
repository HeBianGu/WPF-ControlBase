// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{

    public partial class MainWindowBase : WindowBase
    {
        static MainWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindowBase), new FrameworkPropertyMetadata(typeof(MainWindowBase)));
        }

        public double ViewBoxWidth
        {
            get { return (double)GetValue(ViewBoxWidthProperty); }
            set { SetValue(ViewBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewBoxWidthProperty =
            DependencyProperty.Register("ViewBoxWidth", typeof(double), typeof(MainWindowBase), new PropertyMetadata(1920.0, (d, e) =>
             {
                 MainWindowBase control = d as MainWindowBase;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public double ViewBoxHeight
        {
            get { return (double)GetValue(ViewBoxHeightProperty); }
            set { SetValue(ViewBoxHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewBoxHeightProperty =
            DependencyProperty.Register("ViewBoxHeight", typeof(double), typeof(MainWindowBase), new PropertyMetadata(1055.0, (d, e) =>
             {
                 MainWindowBase control = d as MainWindowBase;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public ICommand NotifyWindowCommand { get; protected set; }

        public MainWindowBase()
        {
            this.NotifyWindowCommand = new RoutedUICommand();

            this.BindCommand(NotifyWindowCommand, (l, k) =>
            {
                //this._notifyIcon.ShowBalloonTip(1000, "提示！", "窗口即将隐藏至右下角，双击右下角图标显示窗口", NotifyBalloonIcon.Info);

                Task.Delay(100).ContinueWith(t =>
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        //this.ShowWindow = false;

                        this.RefreshHide();
                    });

                });
            });

            this.CloseAnimation = l =>
            {
                this.Show(false);

                //  Do ：释放托盘图标
                this._notifyIcon.Dispose();
            };

            //this.MouseDown +=(l, k) =>
            // {
            //     this.DragMove();

            // };

        }

        /// <summary> 托盘图标按钮图标 </summary>
        public ImageSource NotifyIconSource
        {
            get { return (ImageSource)GetValue(NotifyIconSourceProperty); }
            set { SetValue(NotifyIconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifyIconSourceProperty =
            DependencyProperty.Register("NotifyIconSource", typeof(ImageSource), typeof(MainWindowBase), new PropertyMetadata(default(ImageSource), (d, e) =>
            {
                MainWindowBase control = d as MainWindowBase;

                if (control == null) return;

                ImageSource config = e.NewValue as ImageSource;

            }));

        protected override async void OnCloseAnimation()
        {
            //Action<object, RoutedEventArgs> action = (l, k) =>
            //{
            //    if ((bool)k.Parameter)
            //    {
            //        this.CloseAnimation?.Invoke(this);
            //    }
            //};

            if (Application.Current.MainWindow == this)
            {

                string message = null;

                //  Do ：保存主题
                IThemeSaveService localConfig = ServiceRegistry.Instance.GetInstance<IThemeSaveService>();

                localConfig?.Save();

                //  Do ：保存系统配置
                ISystemSettingService setting = ServiceRegistry.Instance.GetInstance<ISystemSettingService>();

                setting?.Save(out message);

                //  Do ：保存工程
                IProjectService project = ServiceRegistry.Instance.GetInstance<IProjectService>();

                if (project?.Save(out message) == false)
                {

                    bool r = await MessageHost.ShowResultMessge(message + "，确定要退出系统？");

                    if (r)
                    {
                        this.CloseAnimation?.Invoke(this);
                    }
                }
                else
                {
                    bool r = await MessageHost.ShowResultMessge("确定要退出系统？");

                    if (r)
                    {
                        this.CloseAnimation?.Invoke(this);
                    }
                }
            }
            else
            {
                this.CloseAnimation?.Invoke(this);
            }
        }
    }

    [TemplatePart(Name = "PART_NotifyIcon", Type = typeof(NotifyIcon))]
    public
    //[TemplatePart(Name = "PART_Message", Type = typeof(MessageContainer))]

    abstract partial class MainWindowBase : IMainWindow
    {
        private NotifyIcon _notifyIcon;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._notifyIcon = Template.FindName("PART_NotifyIcon", this) as NotifyIcon;

            if (this._notifyIcon != null)
            {
                this._notifyIcon.MouseDoubleClick += (l, k) =>
                {
                    this.RefreshHide();
                };
            }
        }

        /// <summary> 输出消息 </summary>
        public abstract void AddSnackMessage(object message);

        /// <summary> 输出消息和操作按钮 </summary>
        public abstract void AddSnackMessage(object message, object actionContent, Action actionHandler);

        /// <summary> 输出消息、按钮和参数 </summary>
        public abstract void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument);

        public abstract void ShowLayer(FrameworkElement element);

        public abstract void CloseLayer();

        public void ShowNotifyMessage(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_notifyIcon == null) return;

                _notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);

            });
        }

        public abstract void ShowWindowNotifyMessage(INotifyMessage message);

        public FrameworkElement GetAdornerElement()
        {
            return this._notifyIcon;
        }
    }
}
