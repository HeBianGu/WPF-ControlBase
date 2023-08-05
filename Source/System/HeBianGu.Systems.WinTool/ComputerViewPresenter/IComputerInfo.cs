using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Management;
using System.Xml.Serialization;

namespace HeBianGu.Systems.WinTool
{
    public interface IComputerInfo
    {
        int Order { get; set; }
        string Name { get; set; }
        string Error { get; set; }
        string Title { get; set; }
        bool IsVisble { get; set; }
        bool IsBuzy { get; set; }
        IEnumerable<ManagementObjectPresenter> CreateData();
        void Load(IEnumerable<ManagementObjectPresenter> infos);
    }


    public abstract class ComputerInfoBase : DisplayerViewModelBase, IComputerInfo
    {
        public ComputerInfoBase()
        {
            this.Title = this.Name;
        }

        private string _error;
        [Browsable(false)]
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                RaisePropertyChanged();
            }
        }


        private string _title;
        [Browsable(false)]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private bool _isVisble;
        [Browsable(false)]
        public bool IsVisble
        {
            get { return _isVisble; }
            set
            {
                _isVisble = value;
                RaisePropertyChanged();
            }
        }

        private bool _isBuzy;
        [Browsable(false)]
        public bool IsBuzy
        {
            get { return _isBuzy; }
            set
            {
                _isBuzy = value;
                RaisePropertyChanged();
            }
        }

        private ObservableSource<ManagementObjectPresenter> _source = new ObservableSource<ManagementObjectPresenter>();
        [XmlIgnore]
        [Browsable(false)]
        public ObservableSource<ManagementObjectPresenter> Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged();
            }
        }

        public virtual IEnumerable<ManagementObjectPresenter> CreateData()
        {
            //return ComputerService.Instance.GetManagementClassInfos(this.Name, this.GroupName);
            return ComputerService.Instance.GetManagementObjects(this.Name, this.GroupName).Select(x => new ManagementObjectPresenter(x));

        }

        protected virtual void Build()
        {

        }

        public void Load(IEnumerable<ManagementObjectPresenter> infos)
        {
            this.Source.Load(infos);
            this.Build();
        }

        //protected string GetValue(string name)
        //{
        //    var first = this.Source.FirstOrDefault(x => true);
        //    if (first == null)
        //        return null;
        //    return null;
        //}
    }

    public class ComputerInfo : ComputerInfoBase
    {
        public ComputerInfo()
        {
            this.Icon = Icons.Add;
            this.Name = null;
        }
        private string _name;
        [Required]
        [Display(Name = "显示名称")]
        [XmlIgnore]
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

        private string _groupName;
        [Browsable(true)]
        [Required]
        [BindingGetSelectSourceMethodAttribute(nameof(GetGroupNames))]
        [PropertyItemType(Type = typeof(ComboBoxSelectSourcePropertyItem))]
        [Display(Name = "参数类型")]
        public override string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }
#if NET
        public List<string> GetGroupNames() => new Lazy<List<string>>(ComputerService.Instance.GetWin32Names().ToList()).Value;

#else
        public List<string> GetGroupNames() => new Lazy<List<string>>(() => ComputerService.Instance.GetWin32Names().ToList()).Value;

#endif
        //{
        //    return ComputerService.Instance.GetWin32Names().ToList();
        //}
    }


    [Displayer(Name = "总览", Icon = "\xeb1b", Description = "包括全部参数信息列表，点击更多加载详情", Order = 0)]
    public partial class HomeComputerInfo : ComputerInfoBase
    {
        public HomeComputerInfo()
        {
            this.Counters.Add(new CpuCounter() { MaxValue = 90 });
            this.Counters.Add(new RamCounter());
            this.Counters.Add(new ReadPhysicalDiskCounter());
            this.Counters.Add(new ReceivedNetworkCounter());
        }
        public override IEnumerable<ManagementObjectPresenter> CreateData()
        {
            return new List<ManagementObjectPresenter>();
        }


        private string _cpu;
        /// <summary> 说明  </summary>
        public string Cpu
        {
            get { return _cpu; }
            set
            {
                _cpu = value;
                RaisePropertyChanged();
            }
        }


        private string _ram;
        /// <summary> 说明  </summary>
        public string Ram
        {
            get { return _ram; }
            set
            {
                _ram = value;
                RaisePropertyChanged();
            }
        }

        protected override void Build()
        {
            base.Build();

            //PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        cpuCounter.NextValue();
            //        Thread.Sleep(1000);
            //        var cpuUsage = cpuCounter.NextValue();
            //        string cpuUsageStr = string.Format("{0:f2} %", cpuUsage);
            //        var ramAvailable = ramCounter.NextValue();
            //        string ramAvaiableStr = string.Format("{0} MB", ramAvailable);
            //        //Console.WriteLine($"CPU:{cpuUsageStr}    RAM:{ramAvaiableStr}");
            //        this.Cpu = cpuUsageStr;
            //        this.Ram = ramAvaiableStr;
            //    }
            //});


            foreach (var item in this.Counters)
            {
                item.Start();
            }
        }


        private ObservableCollection<ICounterPresenter> _counters = new ObservableCollection<ICounterPresenter>();
        /// <summary> 说明  </summary>
        public ObservableCollection<ICounterPresenter> Counters
        {
            get { return _counters; }
            set
            {
                _counters = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "计算机信息", GroupName = "Win32_ComputerSystem", Icon = "\xe81e", Description = "显示计算机信息具体参数，点击更多获取详细参数", Order = 0)]
    public class Win32_ComputerSystem : ComputerInfoBase
    {

    }

    [Displayer(Name = "处理器", GroupName = "Win32_Processor", Icon = "\xe751", Description = "显示具体参数，点击更多获取详细参数", Order = 2)]
    public class Win32_Processor : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var name = x.Model.Properties["Name"]?.Value?.ToString();
                var cores = x.Model.Properties["NumberOfCores"]?.Value?.ToString();
                var count = x.Model.Properties["ThreadCount"]?.Value?.ToString();
                x.Caption = $"{name} {cores}核";
                x.Tag = $"线程数 - {count} {x.Tag}";
            });


        }


    }

    [Displayer(Name = "打印机", GroupName = "Win32_Printer", Icon = "\xe8cc", Description = "显示具体参数，点击更多获取详细参数", Order = 11)]
    public class Win32_Printer : ComputerInfoBase
    {

    }
    [Displayer(Name = "网络适配器", GroupName = "Win32_NetworkAdapter", Icon = "\xe619", Description = "显示具体参数，点击更多获取详细参数", Order = 9)]
    public class Win32_NetworkAdapter : ComputerInfoBase
    {

    }

    [Displayer(Name = "网络适配器配置", GroupName = "Win32_NetworkAdapterConfiguration", Icon = "\xe619", Description = "显示网络适配器配置设备具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_NetworkAdapterConfiguration : ComputerInfoBase
    {

    }
    //[Displayer(Name = "登录用户", GroupName = "Win32_LoggedOnUser", Icon = "\xe8d7", Description = "显示具体参数，点击更多获取详细参数", Order = 11)]
    //public class Win32_LoggedOnUser : ComputerInfoBase
    //{
    //    protected override void Build()
    //    {
    //        this.Source.Foreach(x =>
    //        {
    //            var caption = x.Model.Properties["Antecedent"]?.Value?.ToString();//系统名
    //            x.Caption = $"{caption}";
    //            var desc = x.Model.Properties["Dependent"]?.Value?.ToString();//系统名
    //            x.Description = $"{desc}";
    //        });
    //    }
    //}
    [Displayer(Name = "系统机箱", GroupName = "Win32_SystemEnclosure", Icon = "\xe628", Description = "显示具体参数，点击更多获取详细参数", Order = 12)]
    public class Win32_SystemEnclosure : ComputerInfoBase
    {

    }
    //[Displayer(Name = "BIOS", GroupName = "Win32_Bios", Icon = "\xe650", Description = "显示具体参数，点击更多获取详细参数", Order = 12)]
    //public class Win32_Bios : ComputerInfoBase
    //{

    //}
    [Displayer(Name = "操作系统", GroupName = "Win32_OperatingSystem", Icon = "\xe616", Description = "显示具体参数，点击更多获取详细参数", Order = 0)]
    public class Win32_OperatingSystem : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var caption = x.Model.Properties["Caption"]?.Value?.ToString();//系统名
                var osarchitecture = x.Model.Properties["OSArchitecture"]?.Value?.ToString(); //64位
                var user = x.Model.Properties["RegisteredUser"]?.Value?.ToString();//注册用户
                var version = x.Model.Properties["Version"]?.Value?.ToString();//系统版本
                var csName = x.Model.Properties["CSName"]?.Value?.ToString();//计算机名
                                                                             //x.Description= $" Version：{version}";
                                                                             //x.Version = osarchitecture;
                                                                             //x.Manufacturer = x.Model.Properties["Manufacturer"]?.Value?.ToString();
                x.Caption = $"{caption}  {osarchitecture}";
            });
        }
    }

    //[Displayer(Name = "开机启动", GroupName = "Win32_StartupCommand", Icon = "\xe6f3", Description = "显示具体参数，点击更多获取详细参数", Order = 14)]
    //public class Win32_StartupCommand : ComputerInfoBase
    //{
    //    protected override void Build()
    //    {
    //        this.Source.Foreach(x =>
    //        {
    //            var desc = x.Model.Properties["Command"]?.Value?.ToString();//系统名
    //            x.Description = $"{desc}";
    //        });
    //    }
    //}

    //[Displayer(Name = "共享", GroupName = "Win32_Share", Icon = "\xe670", Description = "显示具体参数，点击更多获取详细参数", Order = 15)]
    //public class Win32_Share : ComputerInfoBase
    //{
    //    protected override void Build()
    //    {
    //        this.Source.Foreach(x =>
    //        {
    //            var desc = x.Model.Properties["Path"]?.Value?.ToString();//系统名
    //            x.Description = $"{desc}";
    //        });
    //    }
    //}
    //[Displayer(Name = "网络客户端", GroupName = "Win32_NetworkClient", Icon = "\xe8d7", Description = "显示具体参数，点击更多获取详细参数", Order = 16)]
    //public class Win32_NetworkClient : ComputerInfoBase
    //{

    //}
    //[Displayer(Name = "服务", GroupName = "Win32_Service", Icon = "\xe760", Description = "显示具体参数，点击更多获取详细参数", Order = 17)]
    //public class Win32_Service : ComputerInfoBase
    //{

    //}
    //[Displayer(Name = "用户帐号", GroupName = "Win32_UserAccount", Icon = "\xe88d", Description = "显示具体参数，点击更多获取详细参数", Order = 18)]
    //public class Win32_UserAccount : ComputerInfoBase
    //{

    //}
    //[Displayer(Name = "组", GroupName = "Win32_Group", Icon = "\xe88b", Description = "显示具体参数，点击更多获取详细参数", Order = 19)]
    //public class Win32_Group : ComputerInfoBase
    //{

    //}
    [Displayer(Name = "电脑型号", GroupName = "Win32_ComputerSystemProduct", Icon = "\xe814", Description = "显示具体参数，点击更多获取详细参数", Order = 1)]
    public class Win32_ComputerSystemProduct : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var caption = x.Model.Properties["Version"]?.Value?.ToString();//系统名
                var uuid = x.Model.Properties["UUID"]?.Value?.ToString(); //64位
                x.Caption = $"{caption}";
                x.Description = uuid;
            });
        }


    }

    //[Displayer(Name = "计算机系统处理器", GroupName = "Win32_ComputerSystemProcessor", Icon = "\xe68a", Description = "显示具体参数，点击更多获取详细参数", Order = 20)]
    //public class Win32_ComputerSystemProcessor : ComputerInfoBase
    //{
    //    protected override void Build()
    //    {
    //        this.Source.Foreach(x =>
    //        {
    //            var caption = x.Model.Properties["PartComponent"]?.Value?.ToString();//系统名
    //            x.Caption = $"{caption}";
    //            var desc = x.Model.Properties["GroupComponent"]?.Value?.ToString();//系统名
    //            x.Description = $"{desc}";
    //        });
    //    }
    //}

    [Displayer(Name = "内存", GroupName = "Win32_PhysicalMemory", Icon = "\xe696", Description = "显示具体参数，点击更多获取详细参数", Order = 3)]
    public class Win32_PhysicalMemory : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var caption = x.Model.Properties["Version"]?.Value?.ToString();//系统名
                var uuid = x.Model.Properties["SerialNumber"]?.Value?.ToString(); //64位
                var speed = x.Model.Properties["Speed"]?.Value?.ToString(); //当前频率
                var capacity = x.Model.Properties["Capacity"]?.Value?.ToString();
                var part = x.Model.Properties["PartNumber"]?.Value?.ToString();
                var total = x.Model.Properties["TotalWidth"]?.Value?.ToString();
                long l = Convert.ToInt64(capacity);
                capacity = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
                x.Caption = $"{part}  {capacity}";
                x.Description = uuid;
                x.Tag = $"频率 - {speed} 总线位 - {total} {x.Tag}";

            });
        }
    }
    [Displayer(Name = "硬盘", GroupName = "Win32_DiskDrive", Icon = "\xe61b", Description = "显示具体参数，点击更多获取详细参数", Order = 4)]
    public class Win32_DiskDrive : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var size = x.Model.Properties["Size"]?.Value?.ToString();
                long l = Convert.ToInt64(size);
                size = XConverter.ByteSizeDisplayConverter.Convert(l, null, null, null)?.ToString();
                var caption = x.Model.Properties["Caption"]?.Value?.ToString();
                x.Caption = $"{caption}  {size}";
            });
        }
    }
    //[Displayer(Name = "网络登录信息", GroupName = "Win32_NetworkLoginProfile", Icon = "\xe61f", Description = "显示网络登录信息具体参数，点击更多获取详细参数", Order = 21)]
    //public class Win32_NetworkLoginProfile : ComputerInfoBase
    //{

    //}
    //[Displayer(Name = "安装软件", GroupName = "Win32_Product", Icon = "\xe921", Description = "显示安装软件具体参数，点击更多获取详细参数", Order = 22)]
    //public class Win32_Product : ComputerInfoBase
    //{

    //}
    [Displayer(Name = "主板", GroupName = "Win32_BaseBoard", Icon = "\xe827", Description = "显示主板具体参数，点击更多获取详细参数", Order = 5)]
    public class Win32_BaseBoard : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var caption = x.Model.Properties["Product"]?.Value?.ToString();
                x.Caption = $"{caption}";
            });
        }
    }

    [Displayer(Name = "显卡", GroupName = "Win32_VideoController", Icon = "\xe7ec", Description = "显示显卡具体参数，点击更多获取详细参数", Order = 6)]
    public class Win32_VideoController : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(x =>
            {
                var ram = x.Model.Properties["AdapterRAM"]?.Value?.ToString();
                x.Tag = $"显存 - {ram} {x.Tag}";
            });
        }
    }


    [Displayer(Name = "即插即用监视器", GroupName = "Win32_DesktopMonitor", Icon = "\xe7ec", Description = "显示即插即用监视器具体参数，点击更多获取详细参数", Order = 6)]
    public class Win32_DesktopMonitor : ComputerInfoBase
    {
        protected override void Build()
        {
            this.Source.Foreach(l =>
            {
                var ram = l.Collection.FirstOrDefault(x => x.Model.Name == "AdapterRAM")?.Model?.Value?.ToString();
                if (!string.IsNullOrWhiteSpace(ram))
                    l.Tag = $"显存 - {ram} {l.Tag}";
            });
        }
    }

    [Displayer(Name = "声卡", GroupName = "Win32_SoundDevice", Icon = "\xe664", Description = "显示声卡具体参数，点击更多获取详细参数", Order = 7)]
    public class Win32_SoundDevice : ComputerInfoBase
    {

    }

    [Displayer(Name = "键盘", GroupName = "Win32_Keyboard", Icon = "\xe727", Description = "显示键盘具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_Keyboard : ComputerInfoBase
    {

    }

    [Displayer(Name = "鼠标", GroupName = "Win32_PointingDevice", Icon = "\xe73a", Description = "显示键盘具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_PointingDevice : ComputerInfoBase
    {

    }

    [Displayer(Name = "系统插槽", GroupName = "Win32_SystemSlot", Icon = "\xeb9a", Description = "显示系统插槽具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_SystemSlot : ComputerInfoBase
    {

    }

    [Displayer(Name = "USB控制器", GroupName = "Win32_USBController", Icon = "\xe639", Description = "显示USB控制器具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_USBController : ComputerInfoBase
    {

    }

    //[Displayer(Name = "USB控制器设备", GroupName = "Win32_USBControllerDevice", Icon = "\xe664", Description = "显示USB控制器设备具体参数，点击更多获取详细参数", Order = 10)]
    //public class Win32_USBControllerDevice : ComputerInfoBase
    //{

    //}

    [Displayer(Name = "USB集线器", GroupName = "Win32_USBHub", Icon = "\xe639", Description = "显示USB集线器设备具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_USBHub : ComputerInfoBase
    {

    }

    [Displayer(Name = "电池", GroupName = "Win32_Battery", Icon = "\xe9f3", Description = "显示电池具体参数，点击更多获取详细参数", Order = 10)]
    public class Win32_Battery : ComputerInfoBase
    {

    }
}
