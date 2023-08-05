// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public static readonly DependencyProperty ViewBoxWidthProperty =
            DependencyProperty.Register("ViewBoxWidth", typeof(double), typeof(MainWindowBase), new PropertyMetadata(1920.0, (d, e) =>
             {
                 MainWindowBase control = d as MainWindowBase;

                 if (control == null) return;
             }));


        public double ViewBoxHeight
        {
            get { return (double)GetValue(ViewBoxHeightProperty); }
            set { SetValue(ViewBoxHeightProperty, value); }
        }

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

        }

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
            if (Application.Current.MainWindow == this)
            {
                List<string> messages = new List<string>();
                var result = await MessageProxy.Messager.ShowStringProgress(x =>
                  {
                      List<ISave> saves = new List<ISave>();
                      saves.Add(ServiceRegistry.Instance.GetInstance<IThemeSaveService>());
                      saves.Add(ServiceRegistry.Instance.GetInstance<ISystemSettingService>());
                      saves.Add(ServiceRegistry.Instance.GetInstance<IAppSaveService>());
                      saves.Add(ServiceRegistry.Instance.GetInstance<IProjectService>());
                      saves.Add(ServiceRegistry.Instance.GetInstance<IDataBaseSaveService>());
                      saves = saves.Where(l => l != null).ToList();

                      for (int i = 0; i < saves.Count; i++)
                      {
                          var save = saves[i];
                          x.MessageStr = $"{i + 1}/{saves.Count} 正在保存{save.Name}...";
                          var s = save.Save(out string message);
                          if (!s)
                          {
                              string error = $"[{save.Name}] {message.Trim('\n')}";
                              Logger.Instance?.Error(error);
                              messages.Add(error);
                          }
                          Thread.Sleep(100);
                      }
                      return true;
                  });

                if (result)
                {
                    string message = string.Join(Environment.NewLine, messages);
                    bool r = await MessageHost.ShowResult(messages.Count == 0 ? "确定要退出系统？" : message + Environment.NewLine + "确定要退出系统?");
                    if (r)
                    {
                        this.CloseAnimation?.Invoke(this);
                    }
                }
                else
                {
                    bool r = await MessageHost.ShowResult("确定要退出系统？");
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
    public abstract partial class MainWindowBase : IMainWindow
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
        public abstract void AddSnackMessage(object message);
        public abstract void AddSnackMessage(object message, object actionContent, Action actionHandler);
        public abstract void AddSnackMessage<TArgument>(object message, object actionContent, Action<TArgument> actionHandler, TArgument actionArgument);
        public abstract void ShowContainer(FrameworkElement element, Predicate<Panel> predicate = null);
        public abstract void CloseContainer();
        public void ShowNotify(string tipTitle, string tipText, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (_notifyIcon == null) return;

                _notifyIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);

            });
        }
        public abstract void ShowWindowNotifyMessage(INotifyMessageItem message);
        public FrameworkElement GetAdornerElement()
        {
            return this._notifyIcon;
        }
    }
}
