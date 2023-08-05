using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Systems.WinTool
{
    public interface ICounterCategory
    {

    }

    public abstract class CounterCategoryBase : DisplayerViewModelBase
    {
        Timer _timer = new Timer();
        public CounterCategoryBase()
        {
            _timer.Interval = 1000;
            _timer.Elapsed += Timer_Elapsed;
        }

        public virtual void LoadCounters(string categoryName)
        {
            {
                var types = GetType().Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract).Where(x => typeof(CounterValuePresenterBase).IsAssignableFrom(x)).Where(x => x.GetCustomAttribute<CounterAttribute>()?.CategoryName == categoryName);
                this.Defineds = types.Select(x => Activator.CreateInstance(x)).OfType<CounterValuePresenterBase>().OrderBy(x => x.Order).ToObservable();
            }
            {
                var types = GetType().Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract).Where(x => typeof(CounterLinePresenterBase).IsAssignableFrom(x)).Where(x => x.GetCustomAttribute<CounterAttribute>()?.CategoryName == categoryName);
                this.Lines = types.Select(x => Activator.CreateInstance(x)).OfType<CounterLinePresenterBase>().OrderBy(x => x.Order).ToObservable();
            }
            var instances = PerforemanceCounterService.Instance.GetInstanceNames(categoryName);
            if (instances.Count() == 0)
            {
                var values = PerforemanceCounterService.Instance.GetCounterPresenters(categoryName, (ca, counter) => new CounterValuePresenter(ca, string.Empty, counter));
                Values.Load(values);
            }
            else
            {
                var values = PerforemanceCounterService.Instance.GetCounterPresenters(categoryName, (ca, instance, counter) => new CounterValuePresenter(ca, instance, counter));
                Values.Load(values);
            }

        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var item in Defineds)
            {
                item.NextValue();
            }
            foreach (var item in Lines)
            {
                item.NextValue();
            }
            foreach (var item in Values.Source.ToList())
            {
                item.NextValue();
            }
        }

        private ObservableCollection<CounterValuePresenterBase> _defineds = new ObservableCollection<CounterValuePresenterBase>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<CounterValuePresenterBase> Defineds
        {
            get { return _defineds; }
            set
            {
                _defineds = value;
                RaisePropertyChanged();
            }
        }

        private ObservableSource<CounterValuePresenter> _values = new ObservableSource<CounterValuePresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableSource<CounterValuePresenter> Values
        {
            get { return _values; }
            set
            {
                _values = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<CounterLinePresenterBase> _lines = new ObservableCollection<CounterLinePresenterBase>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableCollection<CounterLinePresenterBase> Lines
        {
            get { return _lines; }
            set
            {
                _lines = value;
                RaisePropertyChanged();
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

    }


    public class CounterCategory : CounterCategoryBase
    {
        public CounterCategory()
        {
            this.Icon = Icons.Add;
            this.Name = null;
        }
        private string _name;
        [Required]
        [Display(Name = "显示名称")]
        //[XmlIgnore]
        [Browsable(true)]
        public override string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _categoryName;
        [Required]
        [BindingGetSelectSourceMethodAttribute(nameof(GetCategoryNames))]
        [PropertyItemType(Type = typeof(ComboBoxSelectSourcePropertyItem))]
        [Display(Name = "监视器类型")]
        public string CategoryName
        {
            get { return _categoryName; }
            set
            {
                _categoryName = value;
                RaisePropertyChanged();
            }
        }

        public List<string> GetCategoryNames()
        {
            return PerforemanceCounterService.Instance.GetCategoryNames().OrderBy(x => x).ToList();
        }

    }

    [Displayer(Name = "CPU监视", Icon = "\xe751", GroupName = "Processor", Description = "处理器相关数据监控")]
    public class CpuCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }

    [Displayer(Name = "内存监视", Icon = "\xe696", GroupName = "Memory", Description = "内存相关数据监控")]
    public class RamCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }

    [Displayer(Name = "磁盘监视", Icon = "\xe61b", GroupName = "PhysicalDisk", Description = "磁盘相关数据监控")]
    public class PhysicalDiskCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }

    [Displayer(Name = "网络监视", Icon = "\xe619", GroupName = "Network Interface", Description = "网络相关数据监控")]
    public class NetworkCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }

    //[Displayer(Name = "进程监视", Icon = "\xe921", GroupName = "Process", Description = "进程相关数据监控")]
    //public class ProcessCounterCategory : CounterCategoryBase, ICounterCategory
    //{

    //}

    //[Displayer(Name = "线程监视", Icon = Icons.Clock, GroupName = "Thread", Description = "线程监视相关数据监控")]
    //public class ThreadCounterCategory : CounterCategoryBase, ICounterCategory
    //{

    //}

    [Displayer(Name = "对象监视", Icon = "\xe661", GroupName = "Objects", Description = "对象监视相关数据监控")]
    public class ObjectsCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }


    [Displayer(Name = "打印监视", Icon = "\xe7a2", GroupName = "Print Queue", Description = "打印监视相关数据监控")]
    public class PrintCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }

    [Displayer(Name = "浏览器监视", Icon = "\xeb2e", GroupName = "Browser", Description = "打印监视相关数据监控")]
    public class BrowserCounterCategory : CounterCategoryBase, ICounterCategory
    {

    }

    [Displayer(Name = "IPv4", Icon = "\xe7d5", GroupName = "IPv4", Description = "打印监视相关数据监控")]
    public class IPv4CounterCategory : CounterCategoryBase, ICounterCategory
    {

    }
}
