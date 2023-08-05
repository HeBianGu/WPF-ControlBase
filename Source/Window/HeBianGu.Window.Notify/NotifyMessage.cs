// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.MessageContainer;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Mvp;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Window.Notify
{
    [Displayer(Name = "通知提示", GroupName = SystemSetting.GroupMessage)]
    public class NotifyMessage : ServiceSettingInstance<NotifyMessage, INotifyMessage>, INotifyMessage
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

        #region - 列表消息 -

        private NotifyMessageWindow _notifyMessage;

        /// <summary> 显示自定义气泡消息 </summary>
        public void ShowNotifyDialogMessage(string message, string title = null, int closeTime = -1)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                NotifyDialogWindow.ShowMessage(message, title, closeTime);
                if (this.UseLogger)
                    Logger.Instance?.Info(message?.ToString());
            });
        }

        public void ShowSystem(INotifyMessageItem message)
        {
            if (_notifyMessage == null)
            {
                _notifyMessage = new NotifyMessageWindow();
                Application.Current.MainWindow.Closed += (l, k) =>
                {
                    _notifyMessage.Close();
                };
                _notifyMessage.Show();
            }
            _notifyMessage.Add(message);
        }

        public void Show(INotifyMessageItem message)
        {
            IMainWindow window = Application.Current.MainWindow as IMainWindow;
            window?.ShowWindowNotifyMessage(message);
        }

        public void ShowSuccessSystem(string message)
        {
            ShowSystem(new SuccessMessage
            {
                Message = message
            });

            if (this.UseLogger)
                Logger.Instance?.Info(message?.ToString());
        }

        public void ShowInfoSystem(string message)
        {
            ShowSystem(new InfoMessage
            {
                Message = message
            });
            if (this.UseLogger)
                Logger.Instance?.Info(message?.ToString());
        }

        public void ShowErrorSystem(string message)
        {
            ShowSystem(new ErrorMessage
            {
                Message = message
            });
            if (this.UseLogger)
                Logger.Instance?.Error(message?.ToString());
        }

        public void ShowWarnSystem(string message)
        {
            ShowSystem(new WarnMessage
            {
                Message = message
            });

            if (this.UseLogger)
                Logger.Instance?.Warn(message?.ToString());
        }

        public void ShowFatalSystem(string message)
        {
            ShowSystem(new FatalMessage
            {
                Message = message
            });
            if (this.UseLogger)
                Logger.Instance?.Fatal(message?.ToString());
        }

        /// <summary> 显示等待效果的 </summary>
        public async Task<T> ShowWaittingSystem<T>(Func<INotifyMessageItem, T> action)
        {
            WaittingMessage control = new WaittingMessage();
            ShowSystem(control);
            T result = await Task<T>.Run(() => action.Invoke(control));
            control.Close();
            return result;
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowProgressSystem<T>(Func<IPercentProgressMessage, T> action)
        {
            PercentProgressMessage control = new PercentProgressMessage();
            ShowSystem(control);
            T result = await Task<T>.Run(() => action.Invoke(control));
            control.Close();
            return result;
        }

        public async Task<T> ShowStringSystem<T>(Func<INotifyMessageItem, T> action)
        {
            StringProgressMessage control = new StringProgressMessage();
            ShowSystem(control);
            T result = await Task<T>.Run(() => action.Invoke(control));
            control.Close();
            return result;
        }

        public void ShowSuccess(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show(new SuccessMessage
                {
                    Message = message
                });
            });

            if (this.UseLogger)
                Logger.Instance?.Info(message?.ToString());
        }

        public void ShowInfo(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show(new InfoMessage
                {
                    Message = message
                });
            });

            if (this.UseLogger)
                Logger.Instance?.Info(message?.ToString());
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowString<T>(Func<INotifyMessageItem, T> action)
        {
            //return await Application.Current.Dispatcher.InvokeAsync(() =>
            // {
            //     StringProgressMessage control = new StringProgressMessage();
            //     Show(control);
            //     T result = action.Invoke(control);
            //     control.Close();
            //     return result;
            // });


            StringProgressMessage control1 = Application.Current.Dispatcher.Invoke(() =>
            {
                StringProgressMessage control = new StringProgressMessage();
                Show(control);
                return control;
            });

            return await Task.Run(() =>
            {
                T result = action.Invoke(control1);
                control1.Close();
                return result;
            });
        }

        /// <summary> 显示等待效果的 </summary>
        public async Task<T> ShowWaitting<T>(Func<INotifyMessageItem, T> action)
        {
            //return await Application.Current.Dispatcher.InvokeAsync(() =>
            //  {
            //      WaittingMessage control = new WaittingMessage();
            //      Show(control);
            //      T result = action.Invoke(control);
            //      control.Close();
            //      return result;
            //  });

            WaittingMessage control1 = Application.Current.Dispatcher.Invoke(() =>
            {
                WaittingMessage control = new WaittingMessage();
                Show(control);
                return control;
            });

            return await Task.Run(() =>
            {
                T result = action.Invoke(control1);
                control1.Close();
                return result;
            });
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowProgress<T>(Func<IPercentProgressMessage, T> action)
        {
            //return await Application.Current.Dispatcher.InvokeAsync(() =>
            //  {
            //      PercentProgressMessage control = new PercentProgressMessage();
            //      Show(control);
            //      T result = action.Invoke(control);
            //      control.Close();
            //      return result;
            //  });

            PercentProgressMessage control1 = Application.Current.Dispatcher.Invoke(() =>
            {
                PercentProgressMessage control = new PercentProgressMessage();
                Show(control);
                return control;
            });

            return await Task.Run(() =>
            {
                T result = action.Invoke(control1);
                control1.Close();
                return result;
            });
        }

        public void ShowError(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show(new ErrorMessage
                {
                    Message = message
                });
            });

            if (this.UseLogger)
                Logger.Instance?.Error(message?.ToString());
        }

        public void ShowWarn(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show(new WarnMessage
                {
                    Message = message
                });

                if (this.UseLogger)
                    Logger.Instance?.Warn(message?.ToString());
            });
        }

        public void ShowFatal(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Show(new FatalMessage
                {
                    Message = message
                });

                if (this.UseLogger)
                    Logger.Instance?.Fatal(message?.ToString());
            });
        }

        #endregion
    }
}
