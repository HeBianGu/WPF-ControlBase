// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Message;
using HeBianGu.Control.MessageContainer;
using HeBianGu.Control.Panel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.Window.Ribbon
{
    public partial class RibbonWindowBase : RibbonWindow
    {
        static RibbonWindowBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonWindowBase), new FrameworkPropertyMetadata(typeof(RibbonWindowBase)));
        }

        public RibbonWindowBase()
        {
            this.ShowAnimation = l =>
            {
                this.Show(true);
            };

            this.CloseAnimation = l =>
            {
                this.Show(false);
            };


            this.Loaded += (l, k) =>
            {
                this.ShowAnimation?.Invoke(this);


                //if (this.IsUseBlur)
                //{
                //    this.EnableBlur();
                //}
            };

            this.CloseWindowCommand = new RoutedUICommand();
            //this.MaximizeWindowCommand = new RoutedUICommand();
            //this.MinimizeWindowCommand = new RoutedUICommand();

            this.BindCommand(CloseWindowCommand, this.CloseCommand_Execute);
            //this.BindCommand(MaximizeWindowCommand, this.MaxCommand_Execute);
            //this.BindCommand(MinimizeWindowCommand, this.MinCommand_Execute);

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
        }

        public ICommand CloseWindowCommand { get; protected set; }
        public ICommand NotifyWindowCommand { get; protected set; }
        private void CloseCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            this.OnCloseAnimation();
        }

        /// <summary> 显示时的动画效果 </summary>
        public Action<RibbonWindowBase> ShowAnimation
        {
            get { return (Action<RibbonWindowBase>)GetValue(ShowAnimationProperty); }
            set { SetValue(ShowAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowAnimationProperty =
            DependencyProperty.Register("ShowAnimation", typeof(Action<RibbonWindowBase>), typeof(RibbonWindowBase), new PropertyMetadata(default(Action<RibbonWindowBase>), (d, e) =>
            {
                RibbonWindowBase control = d as RibbonWindowBase;

                if (control == null) return;

                Action<RibbonWindowBase> config = e.NewValue as Action<RibbonWindowBase>;

            }));

        /// <summary> 关闭时的动画效果 </summary>

        public Action<RibbonWindowBase> CloseAnimation
        {
            get { return (Action<RibbonWindowBase>)GetValue(CloseAnimationProperty); }
            set { SetValue(CloseAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseAnimationProperty =
            DependencyProperty.Register("CloseAnimation", typeof(Action<RibbonWindowBase>), typeof(RibbonWindowBase), new PropertyMetadata(default(Action<RibbonWindowBase>), (d, e) =>
            {
                RibbonWindowBase control = d as RibbonWindowBase;

                if (control == null) return;

                Action<RibbonWindowBase> config = e.NewValue as Action<RibbonWindowBase>;

            }));

        public ImageSource NotifyIconSource
        {
            get { return (ImageSource)GetValue(NotifyIconSourceProperty); }
            set { SetValue(NotifyIconSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotifyIconSourceProperty =
            DependencyProperty.Register("NotifyIconSource", typeof(ImageSource), typeof(RibbonWindowBase), new PropertyMetadata(default(ImageSource), (d, e) =>
            {
                MainWindowBase control = d as MainWindowBase;

                if (control == null) return;

                ImageSource config = e.NewValue as ImageSource;

            }));


        public Brush CaptionForeground
        {
            get { return (Brush)GetValue(CaptionForegroundProperty); }
            set { SetValue(CaptionForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionForegroundProperty =
            DependencyProperty.Register("CaptionForeground", typeof(Brush), typeof(RibbonWindowBase), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 RibbonWindowBase control = d as RibbonWindowBase;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        public Brush CaptionBackground
        {
            get { return (Brush)GetValue(CaptionBackgroundProperty); }
            set { SetValue(CaptionBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionBackgroundProperty =
            DependencyProperty.Register("CaptionBackground", typeof(Brush), typeof(RibbonWindowBase), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 RibbonWindowBase control = d as RibbonWindowBase;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));


        //public virtual void Show(bool value)
        //{
        //    var animation = ServiceRegistry.Instance.GetInstance<IWindowAnimationService>();

        //    if (animation == null)
        //    {
        //        if (value)
        //        {
        //            this.Show();
        //        }
        //        else
        //        {
        //            this.Close();
        //        }
        //    }
        //    else
        //    {
        //        if (value)
        //        {
        //            animation?.ShowAnimation(this);
        //        }
        //        else
        //        {
        //            animation?.CloseAnimation(this);
        //        }
        //    }

        //}


        protected virtual async void OnCloseAnimation()
        {
            if (Application.Current.MainWindow == this)
            {

                string message = null;

                //  Do ：保存主题
                IThemeSaveService localConfig = ServiceRegistry.Instance.GetInstance<IThemeSaveService>();

                localConfig?.Save(out message);

                //  Do ：保存系统配置
                ISystemSettingService setting = ServiceRegistry.Instance.GetInstance<ISystemSettingService>();

                setting?.Save(out message);

                //  Do ：保存工程
                IProjectService project = ServiceRegistry.Instance.GetInstance<IProjectService>();

                if (project?.Save(out message) == false)
                {
                    bool r = await MessageProxy.Messager.ShowResult(message + "，确定要退出系统？");

                    if (r)
                    {
                        this.CloseAnimation?.Invoke(this);
                    }
                }
                else
                {
                    bool r = await MessageProxy.Messager.ShowResult("确定要退出系统？");

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
    [TemplatePart(Name = "PART_SnackBar", Type = typeof(Snackbar))]
    [TemplatePart(Name = "PART_Message", Type = typeof(MessageContainer))]
    [TemplatePart(Name = "PART_LAYERGROUP", Type = typeof(ContainPanel))]
    public partial class RibbonWindowBase : IMainWindow
    {
        private ContainPanel _layerGroup;
        private MessageContainer _messageContainer;
        private Snackbar _snackbar;
        private NotifyIcon _notifyIcon;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._layerGroup = Template.FindName("PART_LAYERGROUP", this) as ContainPanel;
            this._messageContainer = Template.FindName("PART_Message", this) as MessageContainer;
            this._snackbar = Template.FindName("PART_SnackBar", this) as Snackbar;

            this._notifyIcon = Template.FindName("PART_NotifyIcon", this) as NotifyIcon;

            if (this._notifyIcon != null)
            {
                this._notifyIcon.MouseDoubleClick += (l, k) =>
                {
                    this.RefreshHide();
                };
            }
        }

        public virtual void Show(bool value)
        {
            bool ss = TransitionService.GetIsClose(this);
            TransitionService.SetIsClose(this, value);
        }

        public virtual void ShowContainer(FrameworkElement element, Predicate<Panel> predicate = null)
        {
            this._layerGroup.Add(element);
        }

        public virtual void CloseContainer()
        {
            this._layerGroup.Remove();
        }

        public virtual void ShowWindowNotifyMessage(INotifyMessageItem message)
        {
            this.Dispatcher.Invoke(() =>
            {
                _messageContainer.Add(message);

            });
        }

        public virtual void AddSnackMessage(object message)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Run(() => queue.Enqueue(message));
        }

        public virtual void AddSnackMessage(object message, object actionContent, Action actionHandler)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Run(() => queue.Enqueue(message, actionContent, actionHandler));
        }

        public virtual void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Run(() => queue.Enqueue(message, actionContent, actionHandler, actionArgument));
        }

        public virtual void RefreshHide()
        {
            TransitionService.SetIsVisible(this, !TransitionService.GetIsVisible(this));
        }

        public void ShowNotify(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_notifyIcon == null) return;

                _notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);

            });
        }
    }
}
