using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    public static class MessageService
    {
        public static void ShowSnackMessage(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IWindowBase window = Application.Current.MainWindow as IWindowBase;

                if (window != null)
                {
                    window.AddSnackMessage(message);
                }
            });
        }

        public static void ShowSnackMessageWithNotice(string message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IWindowBase window = Application.Current.MainWindow as IWindowBase;

                if (window != null)
                {
                    window.AddSnackMessage($"提示!  [" + DateTime.Now.ToString("HH:mm:ss]  " + message));
                }
            });
        }

        /// <summary> 输出消息和操作按钮 </summary>
        public static void ShowSnackMessage(string message, object actionContent, Action actionHandler)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IWindowBase window = Application.Current.MainWindow as IWindowBase;

                if (window != null)
                {
                    window.AddSnackMessage(message, actionContent, actionHandler);
                }
            });

        }

        /// <summary> 输出消息、按钮和参数 </summary>
        public static void ShowSnackMessage<TArgument>(string message, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IWindowBase window = Application.Current.MainWindow as IWindowBase;

                if (window != null)
                {
                    window.AddSnackMessage(message, actionContent, actionHandler, actionArgument);
                }
            });

        }

        public static async Task ShowWaittingMessge(Action action, Action closeAction = null)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
             {
                 var view = new WaittingMessageDialog();

                 //show the dialog
                 return await DialogHost.ShowWithOpen(view, "windowDialog", (l, e) =>
                 {
                     Task.Run(action).ContinueWith(m =>
                     {
                         Application.Current.Dispatcher.Invoke(() =>
                         {
                             e.Session.Close(false);

                             closeAction?.Invoke();
                         });
                     });
                 });
             });

        }
        public static async Task ShowSumitMessge(string message)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
             {
                 var view = new SampleMessageDialog();

                 view.MessageStr = message;

                 return await DialogHost.Show(view, "windowDialog");
             });

        }

        public static async void ShowResultMessge(string message, Action<object, DialogClosingEventArgs> action)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
             {
                 var view = new ResultMessageDialog();

                 view.MessageStr = message;

                 //show the dialog
                 return await DialogHost.ShowWithClose(view, "windowDialog", (l, e) =>
                  {
                      action?.Invoke(l, e);
                  });
             });

        }

        /// <summary> 显示蒙版 </summary>
        public static void ShowWithLayer(Uri uri, int layerIndex = 0)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IWindowBase window = Application.Current.MainWindow as IWindowBase;

                window?.ShowWithLayer(uri);
            });
        }

        /// <summary> 关闭蒙版 </summary>
        public static void CloseWithLayer(int layerIndex = 0)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IWindowBase window = Application.Current.MainWindow as IWindowBase;

                window?.CloseWithLayer();
            });
        }

    }
}
