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
using System.Windows.Threading;

namespace HeBianGu.Control.Message
{
    public class PresenterMessage : IPresenterMessage
    {
        public MessageCloseLayerCommand CloseLayerCommand { get; } = new MessageCloseLayerCommand();

        /// <summary> 显示蒙版 </summary>
        public void Show(FrameworkElement element, Predicate<System.Windows.Controls.Panel> predicate = null)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    window.ShowContainer(element);
                }
            }));
        }

        public void Close()
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    window.CloseContainer();
                }
            }));
        }

        private ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);

        public async Task<bool> ShowClearly<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null)
        {
            return await this.Show<T>(value, match, title, builder, ObjectContentDialog.ClearKey);
        }

        public async Task<bool> ShowClose<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null)
        {
            return await this.Show<T>(value, match, title, builder, ObjectContentDialog.CloseKey);
        }

        public async Task<bool> ShowLeftClose<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null)
        {
            Action<ContentControl> action = x =>
              {
                  var trans = TranslateTransition.GetDockLeft();
                  ContainPanel.SetTransition(x, trans);
                  x.HorizontalAlignment = HorizontalAlignment.Right;
                  Cattach.SetCornerRadius(x, new CornerRadius(0));
                  builder?.Invoke(x);
              };

            return await this.Show<T>(value, match, title, action, ObjectContentDialog.CloseKey);
        }

        public async Task<bool> ShowRightClose<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null)
        {
            Action<ContentControl> action = x =>
            {
                var trans = TranslateTransition.GetDockRight();
                ContainPanel.SetTransition(x, trans);
                x.HorizontalAlignment = HorizontalAlignment.Right;
                Cattach.SetCornerRadius(x, new CornerRadius(0));
                builder?.Invoke(x);
            };
            return await this.Show<T>(value, match, title, action, ObjectContentDialog.CloseKey);
        }

        public async Task<bool> ShowTopClose<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null)
        {
            Action<ContentControl> action = x =>
            {
                var trans = TranslateTransition.GetDockTop();
                ContainPanel.SetTransition(x, trans);
                x.VerticalAlignment = VerticalAlignment.Top;
                Cattach.SetCornerRadius(x, new CornerRadius(0));
                builder?.Invoke(x);
            };
            return await this.Show<T>(value, match, title, action, ObjectContentDialog.CloseKey);
        }


        public async Task<bool> ShowBottomClose<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null)
        {
            Action<ContentControl> action = x =>
            {
                var trans = TranslateTransition.GetDockBottm();
                ContainPanel.SetTransition(x, trans);
                x.VerticalAlignment = VerticalAlignment.Bottom;
                Cattach.SetCornerRadius(x, new CornerRadius(0));
                builder?.Invoke(x);
            };
            return await this.Show<T>(value, match, title, action, ObjectContentDialog.CloseKey);
        }

        /// <summary> 显示蒙版 </summary>
        public async Task<bool> Show<T>(T value, Predicate<T> match = null, string title = null, Action<ContentControl> builder = null, ComponentResourceKey key = null)
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

                        Close();
                        _asyncShowWaitHandle.Set();
                        result = true;
                    };

                    content.Closed += (l, k) =>
                    {
                        Close();
                        _asyncShowWaitHandle.Set();
                        result = false;
                    };

                    window.ShowContainer(content);

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
    }

    public static class PresenterMessageExtension
    {
        public static async Task<Tuple<bool, string>> ShowInput(this IPresenterMessage message, Action<InputTextPresenter> pbuilder, Predicate<InputTextPresenter> match = null, string title = null, Action<ContentControl> builder = null, ComponentResourceKey key = null)
        {
            InputTextPresenter input = new InputTextPresenter();
            pbuilder?.Invoke(input);
            var r = await message.Show(input, match, title, builder, key);
            return Tuple.Create(r, input.Value);
        }
    }
}
