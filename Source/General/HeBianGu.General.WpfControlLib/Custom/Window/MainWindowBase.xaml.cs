using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    public partial class MainWindowBase : WindowBase
    {
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

            //this.ShowAnimation = l =>
            //{
            //    // Todo ：初始化淡出初始效果 
            //    //this.OpacityMask = this.TryFindResource("S.WindowOpMack.LoadBrush") as Brush;

            //    Cattach.SetIsClose(this, true);
            //};

            //this.CloseAnimation = l =>
            //{
            //    //this.BegionStoryClose();
            //    Cattach.SetIsClose(this, false);
            //};

            this.BindCommand(CommandService.Close, async (s, e) =>
            {
                Action<object, DialogClosingEventArgs> action = (l, k) =>
                {
                    if ((bool)k.Parameter)
                    {
                        this.CloseAnimation?.Invoke(this);
                    }
                };

                if (Application.Current.MainWindow == this)
                {
                    await MessageService.ShowResultMessge("确认要退出系统?", action);
                }
                else
                {
                    this.CloseAnimation?.Invoke(this);
                }
            });
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



        //public bool ShowWindow
        //{
        //    get { return (bool)GetValue(ShowWindowProperty); }
        //    set { SetValue(ShowWindowProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ShowWindowProperty =
        //    DependencyProperty.Register("ShowWindow", typeof(bool), typeof(MainWindowBase), new PropertyMetadata(true, (d, e) =>
        //    {
        //        MainWindowBase control = d as MainWindowBase;

        //        if (control == null) return;

        //        bool config = (bool)e.NewValue;

        //        if (config)
        //        {
        //            control.ShowOfScaleEnlarge();

        //            control.ScaleEnlarge(new Point(1, 1), 0.5, 5);
        //        }
        //        else
        //        {
        //            control.HideOfScaleReduce();
        //        }

        //    }));


        ///// <summary> 用于重写关闭到那个花 </summary>
        //public virtual void BegionStoryClose()
        //{
        //    if (this._notifyIcon != null)
        //    {
        //        this._notifyIcon.Visibility = Visibility.Collapsed;
        //        this._notifyIcon.Dispose();
        //    }

        //    this.CloseDownToUpOps();
        //}
    }

    [TemplatePart(Name = "PART_SnackBar", Type = typeof(Snackbar))]
    [TemplatePart(Name = "PART_NotifyIcon", Type = typeof(NotifyIcon))]
    [TemplatePart(Name = "PART_LAYERGROUP", Type = typeof(ContainPanel))]
    [TemplatePart(Name = "PART_Message", Type = typeof(MessageContainer))]

    partial class MainWindowBase : IWindowBase
    {
        Snackbar _snackbar;

        NotifyIcon _notifyIcon;

        ContainPanel _layerGroup;

        MessageContainer _messageContainer;

        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this._snackbar = Template.FindName("PART_SnackBar", this) as Snackbar;
            this._notifyIcon = Template.FindName("PART_NotifyIcon", this) as NotifyIcon;
            this._layerGroup = Template.FindName("PART_LAYERGROUP", this) as ContainPanel;
            this._messageContainer = Template.FindName("PART_Message", this) as MessageContainer;

            if (this._notifyIcon != null)
            {
                this._notifyIcon.MouseDoubleClick += (l, k) =>
                {
                    //this.ShowWindow = !this.ShowWindow;

                    this.RefreshHide();
                };
            }
        }
   
        /// <summary> 输出消息 </summary>
        public void AddSnackMessage(object message)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Factory.StartNew(() => queue.Enqueue(message));
        }

        /// <summary> 输出消息和操作按钮 </summary>
        public void AddSnackMessage(object message, object actionContent, Action actionHandler)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Factory.StartNew(() => queue.Enqueue(message, actionContent, actionHandler));
        }

        /// <summary> 输出消息、按钮和参数 </summary>
        public void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument)
        {
            SnackbarMessageQueue queue = _snackbar.MessageQueue;

            Task.Factory.StartNew(() => queue.Enqueue(message, actionContent, actionHandler, actionArgument));
        }

        public void ShowLayer(FrameworkElement element)
        {
            this._layerGroup.Add(element);
        }

        public void CloseLayer()
        {
            this._layerGroup.Remove();
        }


        public void ShowNotifyMessage(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_notifyIcon == null) return;

                _notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);

            });
        }

        public void ShowWindowNotifyMessage(MessageBase message)
        {
            this.Dispatcher.Invoke(() =>
            {
                _messageContainer.Add(message);

            });
        }
    }


    public interface IWindowBase
    {
        /// <summary> 输出消息 </summary>
        void AddSnackMessage(object message);

        /// <summary> 输出消息和操作按钮 </summary>
        void AddSnackMessage(object message, object actionContent, Action actionHandler);

        /// <summary> 输出消息、按钮和参数 </summary>
        void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);

        void ShowLayer(FrameworkElement element);

        /// <summary> 关闭蒙版 </summary>
        void CloseLayer();

        /// <summary> 显示气泡消息 </summary>
        void ShowNotifyMessage(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000);

        /// <summary> 显示气泡消息 </summary>
        void ShowWindowNotifyMessage(MessageBase message);

    }
}
