// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace HeBianGu.Control.PropertyGrid
{

    /// <summary> 自定义导航框架 </summary> 
    public partial class PropertyView : PropertyGrid
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PropertyView), "S.PropertyView.Default");
        public static new ComponentResourceKey SingleKey => new ComponentResourceKey(typeof(PropertyView), "S.PropertyView.Single");
        public static new ComponentResourceKey SumitKey => new ComponentResourceKey(typeof(PropertyView), "S.PropertyView.Default.WithSumit");
        public static new ComponentResourceKey SumitCloseKey => new ComponentResourceKey(typeof(PropertyView), "S.PropertyView.Default.WithSumitClose");

        static PropertyView()
        {
            //StyleLoader.Instance?.LoadDefault(typeof(PropertyView));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyView), new FrameworkPropertyMetadata(typeof(PropertyView)));
        }
    }

    public partial class PropertyView
    {
        private static ManualResetEvent _asyncShowWaitHandle = new ManualResetEvent(false);

        /// <summary> 显示蒙版 </summary>
        public static new async Task<bool> ShowObject<T>(T value, Predicate<T> match = null, string title = null, int layerIndex = 0)
        {
            bool result = false;

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                if (Application.Current.MainWindow is IMainWindow window)
                {
                    PropertyView form = new PropertyView();

                    form.Title = title;

                    form.Style = Application.Current.FindResource(PropertyView.SumitKey) as Style;

                    form.SelectObject = value;

                    form.Close += (l, k) =>
                    {
                        Message.Instance.CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = false;
                    };

                    form.Sumit += (l, k) =>
                    {
                        bool check = form.ModelState(out List<string> errors);

                        if (!check)
                        {
                            Message.Instance.ShowSnackMessageWithNotice(errors.FirstOrDefault());
                            return;
                        }

                        if (match != null && !match(value))
                        {
                            return;
                        }

                        Message.Instance.CloseLayer();
                        _asyncShowWaitHandle.Set();
                        result = true;

                    };

                    window.ShowLayer(form);

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


}
