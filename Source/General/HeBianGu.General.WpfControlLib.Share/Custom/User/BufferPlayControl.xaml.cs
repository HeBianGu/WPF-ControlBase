// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// BufferPlayControl.xaml 的交互逻辑
    /// </summary>
    public partial class BufferPlayControl : UserControl
    {
        public BufferPlayControl()
        {
            InitializeComponent();
        }

        /// <summary> 绑定最小值 </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(BufferPlayControl), new PropertyMetadata(0.0, (d, e) =>
             {
                 BufferPlayControl control = d as BufferPlayControl;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 绑定最大值 </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(BufferPlayControl), new PropertyMetadata(100.0, (d, e) =>
             {
                 BufferPlayControl control = d as BufferPlayControl;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 绑定最小偏移量 </summary>
        public double SmallChange
        {
            get { return (double)GetValue(SmallChangeProperty); }
            set { SetValue(SmallChangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register("SmallChange", typeof(double), typeof(BufferPlayControl), new PropertyMetadata(0.1, (d, e) =>
             {
                 BufferPlayControl control = d as BufferPlayControl;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 设置当前播放值 </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(BufferPlayControl), new PropertyMetadata(30.0, (d, e) =>
             {
                 BufferPlayControl control = d as BufferPlayControl;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        /// <summary> 设置当前缓冲值 </summary>
        public double BufferValue
        {
            get { return (double)GetValue(BufferValueProperty); }
            set { SetValue(BufferValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BufferValueProperty =
            DependencyProperty.Register("BufferValue", typeof(double), typeof(BufferPlayControl), new PropertyMetadata(50.0, (d, e) =>
             {
                 BufferPlayControl control = d as BufferPlayControl;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

    }

    /// <summary> 缓冲播放引擎 </summary>
    public class BufferPlayEngine
    {
        /// <summary> 可播放容器量 </summary>
        public int Capacity { get; set; } = 10;

        /// <summary> 总缓冲容器量 </summary>
        public int CapacityTotal { get; set; } = 10;

        /// <summary> 同时下载的任务数量 </summary>
        public int TaskCount { get; set; } = 5;

        //  Message：所有的文件列表
        private List<IBufferPlayEntity> _entitys = new List<IBufferPlayEntity>();

        //  Message：当前播放的节点
        private IBufferPlayEntity _current;

        public BufferPlayEngine(List<IBufferPlayEntity> entitys)
        {
            _entitys = entitys;

            _current = entitys.First();
        }

        /// <summary> 刷新缓冲数量 </summary>
        public void RefreshCapacity(int count)
        {
            //  Do：可播放队列设置15s
            this.Capacity = count * 5;

            ////Do：后台缓存最多队列设置成5分钟
            this.CapacityTotal = count * 2 * 10;
        }

        private CancellationTokenSource cts = new CancellationTokenSource();
        private Semaphore _semaphore1 = new Semaphore(1, 1);
        /// <summary> 开始播放 </summary>
        public void Start()
        {
            if (cts != null)
            {
                cts.Cancel();
                _semaphore1.WaitOne();
            }

            cts = new CancellationTokenSource();

            //  Message：启动当前位置的顺序下载任务
            Task.Run(() =>
            {
                //  Message：并行运行
                ParallelLoopResult result = Parallel.For(0, this.TaskCount, k =>
                {
                    while (true)
                    {

                        if (cts.IsCancellationRequested) break;

                        int index = this._entitys.FindIndex(l => l == _current);

                        IEnumerable<IBufferPlayEntity> downs = _entitys.Skip(index).Take(this.CapacityTotal).Where(l => l.IsLoaded == 0);

                        //  Message：超出最大下载缓存数量则等待
                        if (downs == null || downs.Count() == 0)
                        {
                            Thread.Sleep(1000);
                            continue;
                        }

                        downs.FirstOrDefault()?.Start();

                    }
                }
                );

                _semaphore1.Release();

            }, cts.Token);
        }

        /// <summary> 停止引擎 </summary>
        public void Stop()
        {
            if (cts != null)
            {
                cts.Cancel();
            }

            flag = false;
        }

        private bool flag = true;
        private Semaphore _semaphore = new Semaphore(1, 1);

        /// <summary> 获取下好的文件 返回null则需要等待 </summary>
        public IBufferPlayEntity GetWaitCurrent(Predicate<IBufferPlayEntity> match, Action<bool, int, int> action)
        {
            IBufferPlayEntity result = this._entitys.Find(match);

            int now = this._entitys.FindIndex(match);

            _current = result;

            if (result.IsLoaded == 2)
            {
                return result;
            }
            else
            {
                //  Message：停止上一个获取任务
                flag = false;

                _semaphore.WaitOne();

                flag = true;

                List<IBufferPlayEntity> waitCache = _entitys.Skip(now).Take(this.Capacity).ToList();

                while (!waitCache.TrueForAll(l => l.IsLoaded == 2))
                {
                    if (!flag)
                    {
                        _semaphore.Release();
                        return null;
                    }

                    Thread.Sleep(500);

                    action(false, waitCache.FindAll(l => l.IsLoaded == 2).Count, waitCache.Count);
                }

                action(true, waitCache.FindAll(l => l.IsLoaded == 2).Count, waitCache.Count);

                _semaphore.Release();
                return result;
            }
        }

        /// <summary> 获取下好的文件 返回null则需要等待 </summary>
        public IBufferPlayEntity GetWaitCurrent(int index, Action<bool, int, int> action)
        {
            IBufferPlayEntity result = this._entitys[index];

            return this.GetWaitCurrent(l => l == result, action);
        }

        /// <summary> 获取当前缓存完的位置 </summary>
        public int GetBufferSize(Predicate<IBufferPlayEntity> match)
        {
            int index = this._entitys.FindIndex(l => l == _current);

            return this.GetBufferSize(index);
        }

        /// <summary> 获取当前缓存完的位置 </summary>
        public int GetBufferSize(int index)
        {
            IBufferPlayEntity isdown = _entitys.Skip(index).LastOrDefault(l => l.IsLoaded == 2);

            if (isdown == null) return 0;

            return this._entitys.FindIndex(l => l == isdown);
        }

        /// <summary> 清理缓存数据 </summary>
        public void Clear()
        {

        }

        //  Message：是否是向前播放
        private bool _isForward = true;

        /// <summary> 反向播放 </summary>
        public void RefreshPlayMode(bool forward)
        {
            if (_isForward = forward) return;

            _isForward = forward;

            _entitys.Reverse();
        }
    }

    /// <summary> 缓冲任务接口 </summary>
    public interface IBufferPlayEntity
    {
        /// <summary> 是否执行完成 </summary>
        int IsLoaded
        {
            get;
            set;
        }

        /// <summary> 开始任务 </summary>
        void Start();
    }

    /// <summary> 缓冲任务抽象基类 </summary>
    public abstract class BufferPlayEntityBase : IBufferPlayEntity
    {
        /// <summary> 执行状态 1=正在执行 2=执行完成  0=未执行 -1=执行错误 </summary>
        public int IsLoaded { get; set; }

        public void Start()
        {
            this.IsLoaded = 1;

            try
            {
                this.DoStart();
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);

                this.IsLoaded = -1;
            }

            this.IsLoaded = 2;
        }

        public abstract void DoStart();
    }
}
