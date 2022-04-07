// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Control.MessageContainer;
using HeBianGu.General.WpfControlLib;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Window.Notify
{
    public class NotifyMessage : INotifyMessage
    {
        #region - 列表消息 -

        private NotifyMessageWindow _notifyMessage;

        /// <summary> 显示自定义气泡消息 </summary>
        public void ShowNotifyDialogMessage(string message, string title = null, int closeTime = -1)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                NotifyDialogWindow.ShowMessage(message, title, closeTime);
            });
        }


        /// <summary> 显示自定义气泡消息 </summary>
        public void ShowSysMessage(MessageBase message)
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

        /// <summary> 输出消息、按钮和参数 </summary>
        public void ShowWinMessage(MessageBase message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.ShowWindowNotifyMessage(message);
                }
            });
        }

        public void ShowSysSuccessMessage(string message)
        {
            ShowSysMessage(new SuccessMessage
            {
                Message = message
            });
        }

        public void ShowSysInfoMessage(string message)
        {
            ShowSysMessage(new InfoMessage
            {
                Message = message
            });
        }

        public void ShowSysErrorMessage(string message)
        {
            ShowSysMessage(new ErrorMessage
            {
                Message = message
            });
        }

        public void ShowSysWarnMessage(string message)
        {
            ShowSysMessage(new WarnMessage
            {
                Message = message
            });
        }

        public void ShowSysFatalMessage(string message)
        {
            ShowSysMessage(new FatalMessage
            {
                Message = message
            });
        }

        /// <summary> 显示等待效果的 </summary>
        public async Task<T> ShowSysWaittingMessage<T>(Func<WaittingMessage, T> action)
        {
            WaittingMessage control = new WaittingMessage();

            ShowSysMessage(control);

            T result = await Task<T>.Run(() => action.Invoke(control));

            control.Close();

            return result;
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowSysProgressBarMessage<T>(Func<PercentProgressMessage, T> action)
        {
            PercentProgressMessage control = new PercentProgressMessage();

            ShowSysMessage(control);

            T result = await Task<T>.Run(() => action.Invoke(control));

            control.Close();

            return result;
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowSysProgressStrMessage<T>(Func<StringProgressMessage, T> action)
        {
            StringProgressMessage control = new StringProgressMessage();

            ShowSysMessage(control);

            T result = await Task<T>.Run(() => action.Invoke(control));

            control.Close();

            return result;
        }

        public void ShowWinSuccessMessage(string message)
        {
            ShowWinMessage(new SuccessMessage
            {
                Message = message
            });
        }

        public void ShowWinInfoMessage(string message)
        {
            ShowWinMessage(new InfoMessage
            {
                Message = message
            });
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowWinProgressStrMessage<T>(Func<StringProgressMessage, T> action)
        {
            StringProgressMessage control = new StringProgressMessage();

            ShowWinMessage(control);

            T result = await Task<T>.Run(() => action.Invoke(control));

            control.Close();

            return result;
        }

        /// <summary> 显示等待效果的 </summary>
        public async Task<T> ShowWinWaittingMessage<T>(Func<WaittingMessage, T> action)
        {
            WaittingMessage control = new WaittingMessage();

            ShowWinMessage(control);

            T result = await Task<T>.Run(() => action.Invoke(control));

            control.Close();

            return result;
        }

        /// <summary> 显示带进度的消息 </summary>
        public async Task<T> ShowWinProgressBarMessage<T>(Func<PercentProgressMessage, T> action)
        {
            PercentProgressMessage control = new PercentProgressMessage();

            ShowWinMessage(control);

            T result = await Task<T>.Run(() => action.Invoke(control));

            control.Close();

            return result;
        }

        public void ShowWinErrorMessage(string message)
        {
            ShowWinMessage(new ErrorMessage
            {
                Message = message
            });
        }

        public void ShowWinWarnMessage(string message)
        {
            ShowWinMessage(new WarnMessage
            {
                Message = message
            });
        }

        public void ShowWinFatalMessage(string message)
        {
            ShowWinMessage(new FatalMessage
            {
                Message = message
            });
        }

        #endregion
    }
}
