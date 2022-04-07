// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    public class TimeButton : Button
    {
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(TimeButton), new PropertyMetadata(default(bool), (d, e) =>
            {
                TimeButton control = d as TimeButton;

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
        private TaskQueue _taskEngines = new TaskQueue();

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

                     Application.Current.Dispatcher.Invoke(() =>
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

                 Application.Current.Dispatcher.Invoke(() =>
                 {
                     //  Do ：结束触发点击事件
                     this.Content = this.Content?.ToString().Split('(')[0];
                 });


             };

            _taskEngines.Enqueue(engine.Start(startOne));
        }

        private int count = -1;

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
            DependencyProperty.Register("Count", typeof(int), typeof(TimeButton), new PropertyMetadata(5, (d, e) =>
            {
                TimeButton control = d as TimeButton;

                if (control == null) return;

            }));


        public TimeButton()
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

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private ManualResetEvent resetEvent = new ManualResetEvent(true);

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public bool IsCancellationRequested { get => _cancellationTokenSource.IsCancellationRequested; }

        public TaskEngine Start(Action action)
        {

            Task.Run(action, _cancellationTokenSource.Token);

            // 初始化为true时执行WaitOne不阻塞
            resetEvent.WaitOne();

            return this;
        }

        /// <summary> 暂停 </summary>
        public void Sleep()
        {
            resetEvent.Reset();
        }

        /// <summary> 继续 </summary>
        public void Continue()
        {
            resetEvent.Set();
        }
    }



}
