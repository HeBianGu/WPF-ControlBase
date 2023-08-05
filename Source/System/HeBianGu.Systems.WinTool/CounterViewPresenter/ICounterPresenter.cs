using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Chart2D;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;

namespace HeBianGu.Systems.WinTool
{
    public interface ICounterPresenter
    {
        void Start();
        void Stop();
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class CounterAttribute : Attribute
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CounterName { get; set; } = string.Empty;
        public string InstanceName { get; set; } = string.Empty;
        public bool UseSum { get; set; }
    }

    public abstract class CounterPresenterBase : DisplayerViewModelBase, ICounterPresenter
    {
        Timer _timer = new Timer();
        List<PerformanceCounter> _performanceCounters = new List<PerformanceCounter>();

        public CounterPresenterBase()
        {
            var attribute = GetType().GetCustomAttribute<CounterAttribute>();
            CategoryName = attribute.CategoryName;
            CounterName = attribute.CounterName;
            InstanceName = attribute.InstanceName;
            _performanceCounters = CreatePerformanceCounters().ToList();
            _timer.Interval = 1000;
            _timer.Elapsed += Timer_Elapsed;
        }

        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public CounterPresenterBase(string categoryName, string instanceName, string counterName)
        {
            CategoryName = categoryName;
            CounterName = counterName;
            InstanceName = instanceName;

            _performanceCounters = CreatePerformanceCounters().ToList();
            _timer.Interval = 1000;
            _timer.Elapsed += Timer_Elapsed;
        }

        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }

        private float _minValue = float.MinValue;
        /// <summary> 说明  </summary>
        public float MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                RaisePropertyChanged();
                this.OnMaxMinValueChanged();
            }
        }

        private float _maxValue = float.MaxValue;
        /// <summary> 说明  </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                RaisePropertyChanged();
                this.OnMaxMinValueChanged();
            }
        }

        protected virtual void OnMaxMinValueChanged()
        {


        }

        private bool _useAlarm;
        /// <summary> 说明  </summary>
        public bool UseAlarm
        {
            get { return _useAlarm; }
            set
            {
                _useAlarm = value;
                RaisePropertyChanged();
            }
        }


        protected virtual IEnumerable<PerformanceCounter> CreatePerformanceCounters()
        {
            if (!string.IsNullOrEmpty(InstanceName))
            {
                yield return new PerformanceCounter(CategoryName, CounterName, InstanceName);
            }
            else
            {
                var instances = PerforemanceCounterService.Instance.GetInstanceNames(CategoryName);
                if (instances.Count() == 0)
                {
                    yield return new PerformanceCounter(CategoryName, CounterName);
                }
                else
                {
                    foreach (var item in PerforemanceCounterService.Instance.GetInstanceNames(CategoryName))
                    {
                        yield return new PerformanceCounter(CategoryName, CounterName, item);
                    }
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NextValue();
        }

        public virtual string NextValue()
        {
            try
            {
                return ConvertToValues(_performanceCounters.Select(x => x.NextValue()).ToList());
            }
            catch
            {
                return null;
            }
        }

        protected virtual string ConvertToValue(float value)
        {
            return value.ToString();
        }

        protected virtual bool CheckValue(float value)
        {
            if (this.UseAlarm == false)
                return true;
            string v = this.ConvertToValue(value);

            if (value > this.MaxValue)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageProxy.Notify.ShowInfoSystem($"{this.Name}参数值过高:{v}");
                });
                return false;
            }
            if (value < this.MinValue)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageProxy.Notify.ShowInfoSystem($"{this.Name}参数值过低:{v}");
                });
                return false;
            }
            return true;
        }

        protected virtual string ConvertToValues(IEnumerable<float> values)
        {
            var sum = values.Sum(x => x);
            this.CheckValue(sum);
            return ConvertToValue(sum);
        }
        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }


        public RelayCommand StartCommand => new RelayCommand((s, e) =>
        {
            s.IsBusy = true;
            Start();
        });


        public RelayCommand StopCommand => new RelayCommand((s, e) =>
        {
            Stop();
        });
    }

    public abstract class CounterValuePresenterBase : CounterPresenterBase, ICounterPresenter
    {
        protected override string ConvertToValue(float value)
        {
            return this.Value = value.ToString();
        }

        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            protected set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }

    public abstract class CounterLinePresenterBase : CounterPresenterBase, ICounterPresenter
    {
        List<double> _cache = new List<double>();

        public CounterLinePresenterBase()
        {
            _dynimacLinePresenter = Application.Current.Dispatcher.Invoke(() => new DynimacLinePresenter());
            _dynimacLinePresenter.UsexAxis = false;
            _dynimacLinePresenter.UseyAxis = false;
            _dynimacLinePresenter.UseAverageMarkLine = false;
            _dynimacLinePresenter.UseLastMarkPositon = true;
            _dynimacLinePresenter.Padding = new Thickness(10, 0, 10, 0);
        }
        protected override string ConvertToValue(float value)
        {
            _cache.Add(value);
            if (_cache.Count > this.Take)
                _cache = _cache.Skip(_cache.Count - this.Take).Take(this.Take).ToList();
            else
            {
                _cache.InsertRange(0, Enumerable.Range(0, this.Take - _cache.Count).Select(x => 0.0));
            }

            //this.DelayInvoke(() =>
            //{
            //    _dynimacLinePresenter.RefreshData(_cache);
            //    _dynimacLinePresenter.LoadyAxis(_cache, 100.0, 0.0);
            //});

            //Application.Current?.Dispatcher?.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            //          {
            //              _dynimacLinePresenter.RefreshData(_cache);
            //              //_dynimacLinePresenter.LoadyAxis(_cache); 
            //              _dynimacLinePresenter.LoadyAxis(_cache, 100.0, 0.0);
            //          }));

            this.DrawData(_cache);
            return this.Value = value.ToString();
        }

        protected virtual void DrawData(List<double> data)
        {
            this.DelayInvoke(() =>
            {
                _dynimacLinePresenter.RefreshData(_cache);
                _dynimacLinePresenter.LoadyAxis(_cache);
            });
        }

        protected override void OnMaxMinValueChanged()
        {
            _dynimacLinePresenter.MaxValue = this.MaxValue == float.MaxValue ? double.NaN : this.MaxValue;
            _dynimacLinePresenter.MinValue = this.MinValue == float.MinValue ? double.NaN : this.MinValue;
        }

        private int _take = 50;
        /// <summary> 说明  </summary>
        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
                RaisePropertyChanged();
            }
        }

        private DynimacLinePresenter _dynimacLinePresenter;
        /// <summary> 说明  </summary>
        public DynimacLinePresenter DynimacLinePresenter
        {
            get { return _dynimacLinePresenter; }
            set
            {
                _dynimacLinePresenter = value;
                RaisePropertyChanged();
            }
        }

        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            protected set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }

    public class CounterValuePresenter : CounterPresenterBase
    {
        public CounterValuePresenter(string categoryName, string instanceName, string counterName) : base(categoryName, instanceName, counterName)
        {

        }

        protected override string ConvertToValue(float value)
        {
            return Value = value.ToString();
        }

        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            private set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "CPU使用率", GroupName = "CPU", Icon = Icons.Clock)]
    [Counter(CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total")]
    public class CpuCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            //this.Value = string.Format("{0:f2} %", value);
            //if (value > 50)
            //    Application.Current.Dispatcher.Invoke(() =>
            //    {
            //        MessageProxy.Notify.ShowInfoSystem("CPU使用过高:" + this.Value);
            //    });
            return this.Value = string.Format("{0:f2} %", value);
        }
    }

    [Displayer(Name = "CPU使用率", GroupName = "CPU", Icon = Icons.Clock)]
    [Counter(CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total")]
    public class CpuLineCounter : CounterLinePresenterBase
    {
        public CpuLineCounter()
        {
            this.MaxValue = 80.0f;
            this.UseAlarm = true;
        }

        protected override void DrawData(List<double> data)
        {
            //Application.Current?.Dispatcher?.BeginInvoke(DispatcherPriority.Input, new Action(() =>
            //{
            //    this.DynimacLinePresenter.RefreshData(data);
            //    this.DynimacLinePresenter.LoadyAxis(data, 100.0, 0.0);
            //}));

            this.DelayInvoke(() =>
            {
                this.DynimacLinePresenter.RefreshData(data);
                this.DynimacLinePresenter.LoadyAxis(data, 100.0, 0.0);
            });

        }
    }


    [Displayer(Name = "CPU速度", GroupName = "CPU", Icon = Icons.Clock)]
    [Counter(CategoryName = "Processor", CounterName = "DPC Rate", InstanceName = "_Total")]
    public class CpuDPCRateCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            return Value = value.ToString();
        }
    }

    [Displayer(Name = "CPU中断", GroupName = "CPU", Icon = Icons.Clock)]
    [Counter(CategoryName = "Processor", CounterName = "Interrupts/sec", InstanceName = "_Total")]
    public class CpuInterruptsRateCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}/s", d);
        }
    }

    [Displayer(Name = "CPU队列", GroupName = "CPU", Icon = Icons.Clock)]
    [Counter(CategoryName = "Processor", CounterName = "DPCs Queued/sec", InstanceName = "_Total")]
    public class CpuDPCsRateCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}/s", d);
        }
    }

    [Displayer(Name = "可用内存")]
    [Counter(CategoryName = "Memory", CounterName = "Available MBytes")]
    public class RamCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            return Value = XConverter.ByteSizeDisplayConverter.Convert(l * 1024 * 1024, null, null, null)?.ToString();
        }
    }

    [Displayer(Name = "已提交内存")]
    [Counter(CategoryName = "Memory", CounterName = "Commit Limit")]
    public class CommitCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            return Value = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();

            //return Value = string.Format("{0} MB", value);
        }
    }

    [Displayer(Name = "分页缓冲池")]
    [Counter(CategoryName = "Memory", CounterName = "Pool Paged Bytes")]
    public class PoolPagedCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            return Value = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
        }
    }

    [Displayer(Name = "非分页缓冲池")]
    [Counter(CategoryName = "Memory", CounterName = "Pool Nonpaged Bytes")]
    public class PoolNonpagedCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            return Value = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
        }
    }

    [Displayer(Name = "磁盘读取速度")]
    [Counter(CategoryName = "PhysicalDisk", CounterName = "Disk Reads/sec", InstanceName = "_Total")]
    public class ReadPhysicalDiskCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}/s", d);
        }
    }


    [Displayer(Name = "磁盘写入速度")]
    [Counter(CategoryName = "PhysicalDisk", CounterName = "Disk Writes/sec", InstanceName = "_Total")]
    public class WritePhysicalDiskCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}/s", d);
        }
    }

    [Displayer(Name = "磁盘平均写入时间")]
    [Counter(CategoryName = "PhysicalDisk", CounterName = "Avg. Disk sec/Write", InstanceName = "_Total")]
    public class AvgWritePhysicalDiskCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            return Value = string.Format("{0}/s", value);
        }
    }


    [Displayer(Name = "磁盘平均读取时间")]
    [Counter(CategoryName = "PhysicalDisk", CounterName = "Avg. Disk sec/Read", InstanceName = "_Total")]
    public class AvgReadPhysicalDiskCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            return Value = string.Format("{0}/s", value);
        }
    }

    //[Displayer(Name = "GPU使用率")]
    //[Counter(CategoryName = "GPU Engine", CounterName = "Utilization Percentage")]
    //public class GPUCounter : PerformanceCounterPresenterBase
    //{
    //    protected override string ConvertToValue(float value)
    //    {
    //        return string.Format("{0} Bytes/sec", value);
    //    }
    //}

    [Displayer(Name = "网络下载速度")]
    [Counter(CategoryName = "Network Interface", CounterName = "Bytes Received/sec")]
    public class ReceivedNetworkCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}/s", d);
        }
    }

    [Displayer(Name = "网络下载速度", GroupName = "网络")]
    [Counter(CategoryName = "Network Interface", CounterName = "Bytes Received/sec")]
    public class LineReceivedNetworkCounter : CounterLinePresenterBase
    {

    }

    [Displayer(Name = "网络上传速度")]
    [Counter(CategoryName = "Network Interface", CounterName = "Bytes Sent/sec")]
    public class SendNetworkCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}/s", d);
        }
    }

    [Displayer(Name = "网络上传速度", GroupName = "网络")]
    [Counter(CategoryName = "Network Interface", CounterName = "Bytes Sent/sec")]
    public class SendReceivedNetworkCounter : CounterLinePresenterBase
    {

    }


    [Displayer(Name = "网络带宽")]
    [Counter(CategoryName = "Network Interface", CounterName = "Current BandWidth")]
    public class BandWidthNetworkCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            long l = (long)value;
            var d = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
            return Value = string.Format("{0}", d);
        }
    }

    [Displayer(Name = "数据包")]
    [Counter(CategoryName = "Network Interface", CounterName = "Packets/sec")]
    public class PacketsNetworkCounter : CounterValuePresenterBase
    {
        protected override string ConvertToValue(float value)
        {
            return Value = string.Format("{0:f2}/s", value);
        }
    }
}
