// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
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
        #region - 提示消息 -

        public void ShowSnackMessage(object message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.AddSnackMessage(message);
                }
            });
        }

        public void ShowSnackMessageWithNotice(object message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    string info = $"提示：[{DateTime.Now.ToString("HH:mm:ss")}] {message}";

                    window.AddSnackMessage(info);
                }
            });
        }

        /// <summary> 输出消息和操作按钮 </summary>
        public void ShowSnackMessage(object message, object actionContent, Action actionHandler)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.AddSnackMessage(message, actionContent, actionHandler);
                }
            });

        }

        /// <summary> 输出消息、按钮和参数 </summary>
        public void ShowSnackMessage<TArgument>(string message, object actionContent, Action<TArgument> actionHandler,
            TArgument actionArgument)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.AddSnackMessage(message, actionContent, actionHandler, actionArgument);
                }
            });

        }


        #endregion

        #region - 窗口消息 -

        public async Task ShowWaittingMessge(Action action, Action closeAction = null)
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
        public async Task<T> ShowWaittingResultMessge<T>(Func<T> action)
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

            await ShowProgressMessge(func, closeAction);
        }

        public async Task<T> ShowPercentProgress<T>(Func<IPercentProgress, T> action, Action closeAction = null)
        {
            Func<PercentProgressDialog, T> func = l =>
            {
                return action(l);
            };

            return await ShowProgressMessge(func, closeAction);
        }

        /// <summary> 带有结果的进度消息 </summary>
        public async Task<T> ShowStringProgress<T>(Func<IStringProgress, T> action, Action closeAction = null)
        {
            Func<StringProgressDialog, T> func = l =>
            {
                return action(l);
            };

            return await ShowProgressMessge(func, closeAction);
        }

        /// <summary> 进度消息 </summary>
        public async Task ShowStringProgress(Action<IStringProgress> action, Action closeAction = null)
        {
            Func<StringProgressDialog, object> func = l =>
               {
                   action(l);
                   return null;
               };

            await ShowProgressMessge(func, closeAction);
        }

        public async Task<R> ShowProgressMessge<T, R>(Func<T, R> action, Action closeAction = null) where T : new()
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

        public async Task ShowSumitMessge(string message, bool isLog = true)
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
        public async Task<T> ShowCustomDialog<T>(UIElement element, Action<object, RoutedEventArgs> action = null)
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

                ShowSnackMessageWithNotice("存在未关闭的窗口，请等待或关闭窗口再执行此操作");
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

        public async Task ShowResultMessge(string message, Action<object, RoutedEventArgs> action)
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

        public async Task<bool> ShowResultMessge(string message)
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

        #endregion

        #region - 蒙版消息 -

        public MessageCloseLayerCommand CloseLayerCommand { get; } = new MessageCloseLayerCommand();

        /// <summary> 显示蒙版 </summary>
        public void ShowLayer(FrameworkElement element)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    window.ShowLayer(element);
                }
            }));
        }

        /// <summary> 关闭蒙版 </summary>
        public void CloseLayer()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    window.CloseLayer();
                }
            }));
        }

        private ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);

        ///// <summary> 显示蒙版 </summary>
        //public async Task<bool> ShowObjectWithPropertyForm<T>(T value, Predicate<T> match = null, string title = null, int layerIndex = 0)
        //{
        //    bool result = false;

        //    await Application.Current.Dispatcher.Invoke(async () =>
        //     {
        //         if (Application.Current.MainWindow is IWindowBase window)
        //         {
        //             PropertyGrid form = new PropertyGrid();

        //             form.Title = title;

        //             form.Style = Application.Current.FindResource("S.PropertyGrid.Default.WithSumit") as Style;

        //             form.SelectObject = value;

        //             form.Close += (l, k) =>
        //             {
        //                 CloseLayer();
        //                 _asyncShowWaitHandle.Set();
        //                 result = false;
        //             };

        //             form.Sumit += (l, k) =>
        //             {
        //                 var check = form.ModelState(out List<string> errors);

        //                 if (!check)
        //                 {
        //                     Message.Instance.ShowSnackMessageWithNotice(errors.FirstOrDefault());
        //                     return;
        //                 }

        //                 if (match != null && !match(value))
        //                 {
        //                     return;
        //                 }

        //                 CloseLayer();
        //                 _asyncShowWaitHandle.Set();
        //                 result = true;

        //             };

        //             window.ShowLayer(form);

        //             _asyncShowWaitHandle.Reset();

        //             var task = new Task(() =>
        //             {
        //                 _asyncShowWaitHandle.WaitOne();
        //             });

        //             task.Start();

        //             await task;
        //         }
        //     });

        //    return result;

        //}

        /// <summary> 显示蒙版 </summary>
        public async Task<bool> ShowObjectWithContent<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null, ComponentResourceKey key = null)
        {
            bool result = false;

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    ObjectContentDialog content = new ObjectContentDialog();
                    content.Content = value;
                    content.Title = title;
                    content.Style = Application.Current.FindResource(key ?? ObjectContentDialog.DefaultKey) as Style;
                    builder?.Invoke(content);

                    content.Sumited += (l, k) =>
                    {
                        if (match != null && !match(value)) return;

                        CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = true;
                    };

                    content.Closed += (l, k) =>
                   {
                       CloseLayer();
                       _asyncShowWaitHandle.Set();
                       result = false;
                   };

                    window.ShowLayer(content);

                    _asyncShowWaitHandle.Reset();

                    Task task = new Task(() =>
                    {
                        _asyncShowWaitHandle.WaitOne();
                    });

                    task.Start();

                    await task;
                }
            });

            return result;

        }

        #endregion

        #region - 气泡消息 -

        /// <summary> 显示气泡消息 </summary>
        public void ShowNotifyMessage(string message, string title = null, NotifyBalloonIcon tipIcon = NotifyBalloonIcon.Info, int timeout = 1000)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IMainWindow window = Application.Current.MainWindow as IMainWindow;

                if (window != null)
                {
                    window.ShowNotifyMessage(title, message, tipIcon, timeout);
                }
            });

        }

        #endregion

        #region - 任务图标消息 -

        /// <summary> 调用主窗口任务栏缩略图 </summary>
        public void ShowTaskbar(Action<TaskbarItemInfo> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return;

            bar.ProgressValue = 1;

            action?.Invoke(bar);
        }

        /// <summary> 调用主窗口任务栏缩略图 显示任务完成百分比</summary>
        public void ShowTaskbarNormal(Action<TaskbarItemInfo> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return;

            bar.ProgressState = TaskbarItemProgressState.Normal;

            action?.Invoke(bar);
        }

        /// <summary> 调用主窗口任务栏缩略图 显示任务完成百分比</summary>
        public async Task ShowTaskbarPercent(Action<TaskbarItemInfo> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return;

            bar.ProgressState = TaskbarItemProgressState.Normal;

            await Task.Run(() =>
            {
                TaskbarItemInfo t = Application.Current.Dispatcher.Invoke(() =>
                {
                    return Application.Current.MainWindow.TaskbarItemInfo;
                });

                action?.Invoke(t);
            });

            //bar.ProgressValue = 0;

            bar.ProgressState = TaskbarItemProgressState.None;
        }

        /// <summary> 调用主窗口任务栏缩略图 显示等待效果 </summary>
        public async Task<bool> ShowTaskbarWaitting(Func<bool> action)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return false;
            if (action == null) return false;

            bar.ProgressState = TaskbarItemProgressState.Indeterminate;

            bool result = await Task.Run(() => action.Invoke());

            bar.ProgressValue = 1;

            if (result)
            {
                bar.ProgressState = TaskbarItemProgressState.Normal;
            }
            else
            {
                bar.ProgressState = TaskbarItemProgressState.Error;
            }

            Thread.Sleep(1000);

            bar.ProgressState = TaskbarItemProgressState.None;

            return result;
        }


        /// <summary> 调用主窗口任务栏缩略图 </summary>
        public void ShowTaskbarImage(ImageSource image)
        {
            TaskbarItemInfo bar = Application.Current.MainWindow.TaskbarItemInfo;

            if (bar == null) return;

            bar.Overlay = image;
        }

        #endregion

    }

}
