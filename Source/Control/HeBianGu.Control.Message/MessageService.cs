// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Panel;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.Animation;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shell;
using System.Windows.Threading;

namespace HeBianGu.Control.Message
{
    public class MessageService : IMessageService
    {
        public async Task ShowWaitter(Action action, Action closeAction = null)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
             {
                 if (CheckOpen()) return;

                 WaittingMessageDialog view = new WaittingMessageDialog();

                 //show the dialog
                 await DialogHost.ShowWithOpen(view, "windowDialog", (l, e) =>
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

        /// <summary> 带有返回结果的等待消息窗口 </summary>
        public async Task<T> ShowWaitter<T>(Func<T> action)
        {
            if (CheckOpen() || action == null) return default(T);

            T result = default(T);

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                WaittingMessageDialog view = new WaittingMessageDialog();

                //show the dialog
                await DialogHost.ShowWithOpen(view, "windowDialog", (l, e) =>
                 {
                     Task.Run(() => result = action.Invoke()).ContinueWith(m =>
                          {
                              Application.Current.Dispatcher.Invoke(() =>
                              {
                                  e.Session.Close(false);
                              });
                          });
                 });
            });

            return result;
        }

        public async Task ShowPercentProgress(Action<IPercentProgress> action, Action closeAction = null)
        {
            Func<PercentProgressDialog, object> func = l =>
            {
                action(l);
                return null;
            };

            await ShowProgress(func, closeAction);
        }

        public async Task<T> ShowPercentProgress<T>(Func<IPercentProgress, T> action, Action closeAction = null)
        {
            Func<PercentProgressDialog, T> func = l =>
            {
                return action(l);
            };

            return await ShowProgress(func, closeAction);
        }

        /// <summary> 带有结果的进度消息 </summary>
        public async Task<T> ShowStringProgress<T>(Func<IStringProgress, T> action, Action closeAction = null)
        {
            Func<StringProgressDialog, T> func = l =>
            {
                return action(l);
            };

            return await ShowProgress(func, closeAction);
        }

        /// <summary> 进度消息 </summary>
        public async Task ShowStringProgress(Action<IStringProgress> action, Action closeAction = null)
        {
            Func<StringProgressDialog, object> func = l =>
               {
                   action(l);
                   return null;
               };

            await ShowProgress(func, closeAction);
        }

        public async Task<R> ShowProgress<T, R>(Func<T, R> action, Action closeAction = null) where T : new()
        {
            if (CheckOpen()) return default(R);

            R result = default(R);

            await Application.Current.Dispatcher.Invoke(async () =>
              {
                  T view = new T();

                  //show the dialog
                  await DialogHost.ShowWithOpen(view, "windowDialog", (l, e) =>
                    {
                        Task.Run(() => result = action.Invoke(view)).ContinueWith(m =>
                         {
                             Application.Current.Dispatcher.Invoke(() =>
                             {
                                 e.Session.Close(false);

                                 closeAction?.Invoke();
                             });
                         });
                    });

              });

            return result;
        }

        public async Task ShowSumit(string message, bool isLog = true)
        {
            if (CheckOpen()) return;

            await Application.Current.Dispatcher.Invoke(async () =>
             {
                 SampleMessageDialog view = new SampleMessageDialog();

                 view.MessageStr = message;

                 await DialogHost.Show(view, "windowDialog");
             });
        }

        /// <summary> 显示自定义窗口 </summary>
        public async Task<T> ShowDialog<T>(UIElement element, Action<object, RoutedEventArgs> action = null)
        {
            if (CheckOpen()) return default(T);

            T result = default(T);

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                //show the dialog
                result = (T)await DialogHost.ShowWithClose(element, "windowDialog", (l, e) =>
                  {
                      action?.Invoke(l, e);
                  });
            });

            return result;
        }

        private bool CheckOpen()
        {
            if (IsOpened())
            {
                //string message = LanguageService.Instance.GetMessageByCode("M00002");
                MessageProxy.Snacker.ShowTime("存在未关闭的窗口，请等待或关闭窗口再执行此操作");
                return true;
            }

            return false;
        }

        public bool IsOpened()
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                return DialogHost.IsOpened();
            });

        }

        public async Task ShowResult(string message, Action<object, RoutedEventArgs> action)
        {
            if (CheckOpen()) return;

            await Application.Current.Dispatcher.Invoke(async () =>
             {
                 ResultMessageDialog view = new ResultMessageDialog();

                 view.MessageStr = message;

                 //show the dialog
                 await DialogHost.ShowWithClose(view, "windowDialog", (l, e) =>
                 {
                     action?.Invoke(l, e);
                 });
             });

        }

        /// <summary> 检查显示主窗口模糊效果 </summary>
        public void BeginDefaultBlurEffect(bool isuse)
        {
            if (isuse)
            {
                if (Application.Current.MainWindow is WindowBase window)
                {
                    window.AdornerDecoratorEffect = window.DefaultBlurEffect;
                }
            }
            else
            {
                if (Application.Current.MainWindow is WindowBase window)
                {
                    window.AdornerDecoratorEffect = null;
                }
            }


        }

        public async Task<bool> ShowResult(string message)
        {
            if (CheckOpen()) return false;

            bool result = false;

            Action<object, DialogClosingEventArgs> action = (l, k) =>
             {
                 result = (bool)k.Parameter;
             };

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                ResultMessageDialog view = new ResultMessageDialog();

                view.MessageStr = message;

                //show the dialog
                await DialogHost.ShowWithClose(view, "windowDialog", (l, e) =>
                {
                    action?.Invoke(l, e);
                });
            });

            return result;

        }
    }
}
