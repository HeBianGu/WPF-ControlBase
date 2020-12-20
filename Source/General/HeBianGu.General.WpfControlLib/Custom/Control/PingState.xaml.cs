using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public class PingState : ContentControl
    {
        static PingState()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PingState), new FrameworkPropertyMetadata(typeof(PingState)));
        }

        /// <summary> 设置启动引擎 </summary>
        public bool Start
        {
            get { return (bool)GetValue(StartProperty); }
            set { SetValue(StartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartProperty =
            DependencyProperty.Register("Start", typeof(bool), typeof(PingState), new PropertyMetadata(false, (d, e) =>
             {
                 PingState control = d as PingState;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 if ((bool)e.NewValue)
                 {
                     control.StartEngine();
                 }
                 else
                 {
                     control.StopEngine();
                 }
             }));


        Queue<Tuple<CancellationTokenSource, Task>> _queue = new Queue<Tuple<CancellationTokenSource, Task>>();


        List<string> _cache = new List<string>();

        void StopEngine()
        {
            if (_queue.Count == 0) return;

            var item = _queue.Dequeue();

            item.Item1.Cancel();
        }

        void StartEngine()
        {
            Ping ping = new Ping();

            CancellationTokenSource source = new CancellationTokenSource();

            Task task = Task.Run(() =>
             {
                 while (true)
                 {
                     if (source.Token.IsCancellationRequested) break;
                     try
                     {

                         string ip = this.Dispatcher?.Invoke(() =>
                         {
                             return this.IP;
                         });

                         var reply = ping.Send(ip);

                         this.Dispatcher?.Invoke(() =>
                         {
                             this.Status = reply.Status;

                             if (reply.Status == IPStatus.Success)
                             {
                                 this.RoundtripTime = reply.RoundtripTime;

                                 _cache.Insert(0,$"[{DateTime.Now.ToString("HH:mm:ss")}] ping {this.IP} {this.RoundtripTime} ms");
                             }
                             else
                             {
                                 this.RoundtripTime = -1;

                                 _cache.Insert(0, $"[{DateTime.Now.ToString("HH:mm:ss")}][{this.Status}] {this.IP} {reply.Options?.Ttl}");
                             }
                         });
                     }
                     catch (Exception ex)
                     {
                         this?.Dispatcher?.Invoke(() =>
                         {
                             Status = IPStatus.Unknown;

                             _cache?.Insert(0,ex?.Message);
                         });

                         Debug.WriteLine(ex);
                     }
                     finally
                     {
                         //  Do ：只取最新十个
                         this._cache= this._cache.Take(10)?.ToList();

                         this.Dispatcher?.Invoke(() =>
                         {
                             this.Message = string.Join(Environment.NewLine, this._cache);
                         });
                     }

                     Thread.Sleep(Sleep);
                 }
             }, source.Token);

            _queue.Enqueue(Tuple.Create(source, task));
        }

        /// <summary> 显示交互信息 </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(PingState), new PropertyMetadata(default(string), (d, e) =>
             {
                 PingState control = d as PingState;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));


        /// <summary> 连接状态 </summary>
        public IPStatus Status
        {
            get { return (IPStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(IPStatus), typeof(PingState), new PropertyMetadata(IPStatus.Unknown, (d, e) =>
             {
                 PingState control = d as PingState;

                 if (control == null) return;

                 //IPStatus config = e.NewValue as IPStatus;

             }));

        /// <summary> 响应时间 </summary>
        public long RoundtripTime
        {
            get { return (long)GetValue(RoundtripTimeProperty); }
            set { SetValue(RoundtripTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoundtripTimeProperty =
            DependencyProperty.Register("RoundtripTime", typeof(long), typeof(PingState), new PropertyMetadata(default(long), (d, e) =>
             {
                 PingState control = d as PingState;

                 if (control == null) return;

                 //long config = e.NewValue as long;

             }));

        /// <summary> 设置的IP </summary>
        public string IP
        {
            get { return (string)GetValue(IPProperty); }
            set { SetValue(IPProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IPProperty =
            DependencyProperty.Register("IP", typeof(string), typeof(PingState), new PropertyMetadata(@"www.baidu.com", (d, e) =>
             {
                 PingState control = d as PingState;

                 if (control == null) return;

                 string config = e.NewValue as string;

             }));

        /// <summary> 刷新时间 </summary>
        int Sleep { get; set; } = 1000;
    }


}
