using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HeBianGu.General.WpfControlLib;

namespace HeBianGu.App.Touch
{
    /// <summary> 带有倒计时效果的按钮控件 </summary>
    public class FButtonCountDown : FButton
    {
        /// <summary> 开始或停止倒计时 </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(FButtonCountDown), new PropertyMetadata(default(bool), (d, e) =>
            {
                FButtonCountDown control = d as FButtonCountDown;

                if (control == null) return;

                if ((bool)e.NewValue)
                {
                    control.count = control.Count;

                    control.StopAll();

                    control.StartOne();
                }
                else
                {
                    control.StopAll();
                }
            }));

        TaskQueue _taskEngines = new TaskQueue();

        public void StopAll()
        {
            _taskEngines.StopAll();
        }

        public void StartOne()
        {
            TaskEngine engine = new TaskEngine();

            Action startOne = () =>
            {
                while (true)
                {
                    //  Do ：调用取消退出循环
                    if (engine.IsCancellationRequested)
                    {
                        break;
                    }

                    Thread.Sleep(1000);

                    System.Windows.Application.Current?.Dispatcher.Invoke(() =>
                    {
                        this.Content = this.Content?.ToString().Split('(')[0] + $"({count = count - 1})";

                        if (count <= 0)
                        {
                            //  Do ：结束触发点击事件
                            this.Content = this.Content?.ToString().Split('(')[0];

                            this.Command?.Execute(this.CommandParameter);

                            this.IsActive = false;
                            //  Do ：结束触发点击事件
                            this.Content = this.Content?.ToString().Split('(')[0];
                        }
                    });
                }

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    //  Do ：结束触发点击事件
                    this.Content = this.Content?.ToString().Split('(')[0];
                });
            };

            _taskEngines.Enqueue(engine.Start(startOne));
        }


        int count = -1;

        public int Count
        {
            get
            {
                return this.Dispatcher.Invoke(() =>
                {
                    return (int)GetValue(CountProperty);
                });
            }
            set { SetValue(CountProperty, value); }
        }

        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(FButtonCountDown), new PropertyMetadata(6, (d, e) =>
            {
                FButtonCountDown control = d as FButtonCountDown;

                if (control == null) return;

            }));


        public FButtonCountDown()
        {
            //  Message：点击取消
            this.Click += (l, k) =>
            {
                this.IsActive = false;
            };

        }
    }


    public class TaskQueue : Queue<TaskEngine>
    {
        /// <summary> 停止所有任务 </summary>
        public void StopAll()
        {
            while (this.Count > 0)
            {
                this.Dequeue().Cancel();
            }
        }
    }

    public class TaskEngine
    {
        public Task Task { get; set; }

        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public bool IsCancellationRequested { get => _cancellationTokenSource.IsCancellationRequested; }

        public TaskEngine Start(Action action)
        {
            Task.Run(action, _cancellationTokenSource.Token);

            return this;
        }
    }
}
