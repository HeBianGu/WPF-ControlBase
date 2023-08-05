// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HeBianGu.Control.Message
{
    [Displayer(Name = "运行提示", GroupName = SystemSetting.GroupMessage)]
    public class SnackMessage : ServiceSettingInstance<SnackMessage, ISnackMessage>, ISnackMessage
    {
        private bool _useLogger;
        [DefaultValue(true)]
        [Display(Name = "启用提示消息输出到消息日志")]
        public bool UseLogger
        {
            get { return _useLogger; }
            set
            {
                _useLogger = value;
                RaisePropertyChanged();
            }
        }

        public void Show(object message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.AddSnackMessage(message);
                    if (this.UseLogger)
                        Logger.Instance?.Info(message?.ToString());
                }
            });
        }

        public void ShowTime(object message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    string info = $"提示：[{DateTime.Now.ToString("HH:mm:ss")}] {message}";
                    window.AddSnackMessage(info);
                    if (this.UseLogger)
                        Logger.Instance?.Info(message?.ToString());
                }
            });
        }

        /// <summary> 输出消息和操作按钮 </summary>
        public void Show(object message, object actionContent, Action actionHandler)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.AddSnackMessage(message, actionContent, actionHandler);
                    if (this.UseLogger)
                        Logger.Instance?.Info(message?.ToString());
                }
            });

        }

        /// <summary> 输出消息、按钮和参数 </summary>
        public void Show<TArgument>(string message, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.AddSnackMessage(message, actionContent, actionHandler, actionArgument);
                    if (this.UseLogger)
                        Logger.Instance?.Info(message?.ToString());
                }
            });

        }
    }
}
