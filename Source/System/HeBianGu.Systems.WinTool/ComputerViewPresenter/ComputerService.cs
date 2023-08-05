using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace HeBianGu.Systems.WinTool
{
    public partial class ComputerService
    {
#if NET
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OSDescription { get; } = System.Runtime.InteropServices.RuntimeInformation.OSDescription; 

        /// <summary>
        /// 操作系统架构（<see cref="Architecture">）
        /// </summary>
        public string OSArchitecture { get; } = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

        /// <summary>
        /// 是否为Windows操作系统
        /// </summary>
        public bool IsOSPlatform { get; } = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);

#else
        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OSDescription { get; } = Environment.OSVersion.ToString();

        /// <summary>
        /// 操作系统架构（<see cref="Architecture">）
        /// </summary>
        public string OSArchitecture { get; } = Environment.OSVersion.VersionString;

        /// <summary>
        /// 是否为Windows操作系统
        /// </summary>
        public bool IsOSPlatform { get; } = Environment.OSVersion.Platform.ToString().StartsWith("win");

#endif
        /// <summary>
        /// 计算机名称
        /// </summary>
        public string ComputerName { get; } = System.Environment.GetEnvironmentVariable("ComputerName");
        /// <summary>
        /// 计算机用户
        /// </summary>
        public string UserName { get; set; } = System.Environment.UserName;

        /// <summary>
        /// 电脑型号
        // Caption:计算机系统产品
        // Description:计算机系统产品
        // ElementName:
        // IdentifyingNumber:CND0162XZ1
        // InstanceID:
        // Name:HP ENVY x360 Convertible 15-ed0xxx
        // SKUNumber:
        // UUID:A6C31C0E-997C-EA11-8104-B05CDA905B6C
        // Vendor:HP
        // Version:Type1ProductConfigId
        // WarrantyDuration:
        // WarrantyStartDate:
        /// </summary>
        /// <returns></returns>
        public string GetComputerVersion()
        {
            var version = new StringBuilder();
            var moc = new ManagementClass("Win32_ComputerSystemProduct").GetInstances();
            foreach (ManagementObject mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    version.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return version.ToString(); ;
        }

        /// <summary>
        /// 主板信息
        // Caption:简短说明
        // ConfigOptions:数组，表示位于在底板上跳线和开关的配置
        // CreationClassName:表示类的名称(就是Win32_baseboard类)
        // Depth:对象的描述（底板）
        // Description:基板
        // Height:
        // HostingBoard:如果为TRUE，该卡是一个主板，或在一个机箱中的基板。
        // HotSwappable:如果为TRUE，就是支持热插拔（判断是否支持热插拔）
        // InstallDate:
        // Manufacturer:表示制造商的名称
        // Model:
        // Name:对象的名称标签
        // OtherIdentifyingInfo:
        // PartNumber:
        // PoweredOn:如果为真，物理元素处于开机状态
        // Product:产品的型号
        // Removable:判断是否可拆卸的
        // Replaceable:判断是否可更换的
        // RequirementsDescription:
        // RequiresDaughterBoard:False
        // SerialNumber:制造商分配的用于识别所述物理元件数目
        // SKU:
        // SlotLayout:
        // SpecialRequirements:
        // Status:对象的当前状态
        // Tag:符系统的基板唯一标识
        // Version:08.32
        // Weight:
        // Width:
        /// </summary>
        /// <returns></returns>
        public string GetBaseBoardInfo()
        {
            var baseBoard = new StringBuilder();
            var moc = new ManagementClass("Win32_BaseBoard").GetInstances();
            foreach (ManagementObject mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    baseBoard.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return baseBoard.ToString();
        }

        /// <summary>
        /// 硬盘驱动器信息（<see cref="https://www.cnblogs.com/zhesong/p/wmiid.html">）
        //  Access:0
        //  Availability:
        //  BlockSize:
        //  Caption:硬盘描述，例如“C:"
        //  Compressed:False
        //  ConfigManagerErrorCode:Windows配置管理器错误代码
        //  ConfigManagerUserConfig:如果为True，该设备使用用户定义的配置
        //  CreationClassName:Win32_LogicalDisk
        //  Description:本地固定磁盘
        //  DeviceID:磁盘驱动器与系统中的其他设备的唯一标识符，例如“C:"
        //  DriveType:3
        //  ErrorCleared:如果为True，报告LastErrorCode错误现已清除
        //  ErrorDescription:关于可能采取的纠正措施记录在LastErrorCode错误，和信息的详细信息
        //  ErrorMethodology:误差检测和校正的类型被此设备支持
        //  FileSystem:NTFS
        //  FreeSpace:可使用硬盘大小
        //  InstallDate:
        //  LastErrorCode:
        //  MaximumComponentLength:255
        //  MediaType:由该设备使用或访问的媒体类型
        //  Name:硬盘名字
        //  NumberOfBlocks:
        //  PNPDeviceID:即插即用逻辑设备的播放设备标识符
        //  PowerManagementCapabilities:
        //  PowerManagementSupported:
        //  ProviderName:
        //  Purpose:
        //  QuotasDisabled:True
        //  QuotasIncomplete:False
        //  QuotasRebuilding:False
        //  Size:硬盘总大小
        //  Status:对象的当前状态
        //  StatusInfo:逻辑设备的状态
        //  SupportsDiskQuotas:True
        //  SupportsFileBasedCompression:True
        //  SystemCreationClassName:Win32_ComputerSystem
        //  SystemName:DESKTOP-OLA70V5
        //  VolumeDirty:False
        //  VolumeName:Windows
        //  VolumeSerialNumber:硬盘的序列号
        /// </summary>
        /// <returns></returns>
        public string GetDiskInfo()
        {
            var disk = new StringBuilder();
            var moc = new ManagementClass("Win32_LogicalDisk").GetInstances();
            foreach (ManagementObject mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    disk.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return disk.ToString();
        }

        /// <summary>
        /// 处理器信息（<see cref="https://www.cnblogs.com/zhesong/p/wmiid.html">）
        //  AddressWidth:在32位操作系统，该值是32，在64位操作系统是64
        //  Architecture:所使用的平台的处理器架构
        //  AssetTag:代表该处理器的资产标签
        //  Availability:设备的状态
        //  Caption:设备的简短描述
        //  Characteristics:处理器支持定义的功能
        //  ConfigManagerErrorCode:Windows API的配置管理器错误代码
        //  ConfigManagerUserConfig:如果为TRUE，该装置是使用用户定义的配置
        //  CpuStatus:处理器的当前状态
        //  CreationClassName:出现在用来创建一个实例继承链的第一个具体类的名称
        //  CurrentClockSpeed:处理器的当前速度，以MHz为单位
        //  CurrentVoltage:处理器的电压。如果第八位被设置，位0-6包含电压乘以10，如果第八位没有置位，则位在VoltageCaps设定表示的电压值。 CurrentVoltage时SMBIOS指定的电压值只设置
        //  DataWidth:在32位处理器，该值是32，在64位处理器是64
        //  Description:描述
        //  DeviceID:在系统上的处理器的唯一标识符
        //  ErrorCleared:如果为真，报上一个错误代码的被清除
        //  ErrorDescription:错误的代码描述
        //  ExtClock:外部时钟频率，以MHz为单位
        //  Family:处理器系列类型
        //  InstallDate:安装日期
        //  L2CacheSize:二级缓存大小
        //  L2CacheSpeed:二级缓存处理器的时钟速度
        //  L3CacheSize:三级缓存大小
        //  L3CacheSpeed:三级缓存处理器的时钟速度
        //  LastErrorCode:报告的逻辑设备上一个错误代码
        //  Level:处理器类型的定义。该值取决于处理器的体系结构
        //  LoadPercentage:每个处理器的负载能力，平均到最后一秒
        //  Manufacturer:处理器的制造商
        //  MaxClockSpeed:处理器的最大速度，以MHz为单位
        //  Name:处理器的名称
        //  NumberOfCores:处理器的当前实例的数目。核心是在集成电路上的物理处理器
        //  NumberOfEnabledCore:每个处理器插槽启用的内核数
        //  NumberOfLogicalProcessors:用于处理器的当前实例逻辑处理器的数量
        //  OtherFamilyDescription:处理器系列类型
        //  PartNumber:这款处理器的产品编号制造商所设置
        //  PNPDeviceID:即插即用逻辑设备的播放设备标识符
        //  PowerManagementCapabilities:逻辑设备的特定功率相关的能力阵列
        //  PowerManagementSupported:如果为TRUE，该装置的功率可以被管理，这意味着它可以被放入挂起模式
        //  ProcessorId:描述处理器功能的处理器的信息
        //  ProcessorType:处理器的主要功能
        //  Revision:系统修订级别取决于体系结构
        //  Role:所述处理器的作用
        //  SecondLevelAddressTranslationExtensions:如果为True，该处理器支持用于虚拟地址转换扩展
        //  SerialNumber:处理器的序列号
        //  SocketDesignation:芯片插座的线路上使用的类型
        //  Status:对象的当前状态
        //  StatusInfo:对象的当前状态信息
        //  Stepping:在处理器家族处理器的版本
        //  SystemCreationClassName:创建类名属性的作用域计算机的价值
        //  SystemName:系统的名称
        //  ThreadCount:每个处理器插槽的线程数
        //  UniqueId:全局唯一标识符的处理器
        //  UpgradeMethod:CPU插槽的信息
        //  Version:依赖于架构处理器的版本号
        //  VirtualizationFirmwareEnabled:如果真，固件可以虚拟化扩展
        //  VMMonitorModeExtensions:如果为True，该处理器支持Intel或AMD虚拟机监控器扩展。
        //  VoltageCaps:该处理器的电压的能力
        /// </summary>
        /// <returns></returns>
        public string GetCPUInfo()
        {
            var cpu = new StringBuilder();
            var moc = new ManagementClass("Win32_Processor").GetInstances();
            foreach (var mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    cpu.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return cpu.ToString();
        }

        /// <summary>
        /// 内存信息（<see cref="https://www.cnblogs.com/zhesong/p/wmiid.html">）
        //  Attributes:1
        //  BankLabel:BANK 2
        //  Capacity:获取内存容量（单位KB）
        //  Caption:物理内存还虚拟内存
        //  ConfiguredClockSpeed:配置时钟速度
        //  ConfiguredVoltage:配置电压
        //  CreationClassName:创建类名
        //  DataWidth:获取内存带宽
        //  Description:描述
        //  DeviceLocator:获取设备定位器
        //  FormFactor:构成因素
        //  HotSwappable:是否支持热插拔
        //  InstallDate:安装日期
        //  InterleaveDataDepth:数据交错深度
        //  InterleavePosition:数据交错的位置
        //  Manufacturer:生产商
        //  MaxVoltage:最大电压
        //  MemoryType:内存类型
        //  MinVoltage:最小电压
        //  Model:型号
        //  Name:名字
        //  OtherIdentifyingInfo:其他识别信息
        //  PartNumber:零件编号
        //  PositionInRow:行位置
        //  PoweredOn:是否接通电源
        //  Removable:是否可拆卸
        //  Replaceable:是否可更换
        //  SerialNumber:编号
        //  SKU:SKU号
        //  SMBIOSMemoryType:SMBIOS内存类型
        //  Speed:速率
        //  Status:状态
        //  Tag:唯一标识符的物理存储器
        //  TotalWidth:总宽
        //  TypeDetail:类型详细信息
        //  Version:版本信息
        //  AvailableBytes:可利用内存大小（B）
        //  AvailableKBytes:可利用内存大小（KB）
        //  AvailableMBytes:可利用内存大小（MB）
        //  CacheBytes:125460480
        //  CacheBytesPeak:392294400
        //  CacheFaultsPersec:70774721
        //  Caption:
        //  CommitLimit:31939616768
        //  CommittedBytes:20280020992
        //  DemandZeroFaultsPersec:759274721
        //  Description:
        //  FreeAndZeroPageListBytes:2097152
        //  FreeSystemPageTableEntries:12528527
        //  Frequency_Object:0
        //  Frequency_PerfTime:10000000
        //  Frequency_Sys100NS:10000000
        //  LongTermAverageStandbyCacheLifetimes:14400
        //  ModifiedPageListBytes:41500672
        //  Name:
        //  PageFaultsPersec:1560432075
        //  PageReadsPersec:19173703
        //  PagesInputPersec:98834167
        //  PagesOutputPersec:25921396
        //  PagesPersec:124755563
        //  PageWritesPersec:103362
        //  PercentCommittedBytesInUse:2727084283
        //  PercentCommittedBytesInUse_Base:4294967295
        //  PoolNonpagedAllocs:0
        //  PoolNonpagedBytes:798519296
        //  PoolPagedAllocs:0
        //  PoolPagedBytes:709898240
        //  PoolPagedResidentBytes:496873472
        //  StandbyCacheCoreBytes:247545856
        //  StandbyCacheNormalPriorityBytes:847036416
        //  StandbyCacheReserveBytes:0
        //  SystemCacheResidentBytes:125460480
        //  SystemCodeResidentBytes:0
        //  SystemCodeTotalBytes:0
        //  SystemDriverResidentBytes:17592179236864
        //  SystemDriverTotalBytes:16953344
        //  Timestamp_Object:0
        //  Timestamp_PerfTime:5838028983825
        //  Timestamp_Sys100NS:132532052633540000
        //  TransitionFaultsPersec:792343233
        //  TransitionPagesRePurposedPersec:78554340
        //  WriteCopiesPersec:17253788
        /// </summary>
        /// <returns></returns>
        public string GetRAMInfo()
        {
            var ram = new StringBuilder();
            var searcher = new ManagementObjectSearcher()
            {
                Query = new SelectQuery("Win32_PhysicalMemory"),
            }.Get().GetEnumerator();

            while (searcher.MoveNext())
            {
                ManagementBaseObject baseObj = searcher.Current;
                foreach (var item in baseObj.Properties)
                {
                    ram.Append($"{item.Name}:{item.Value}\r\n");
                }
            }

            searcher = new ManagementObjectSearcher()
            {
                Query = new SelectQuery("Win32_PerfRawData_PerfOS_Memory"),
            }.Get().GetEnumerator();

            while (searcher.MoveNext())
            {
                ManagementBaseObject baseObj = searcher.Current;
                foreach (var item in baseObj.Properties)
                {
                    ram.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return ram.ToString();
        }

        /// <summary>
        /// 显卡信息
        //  AcceleratorCapabilities:
        //  AdapterCompatibility:Intel Corporation
        //  AdapterDACType:Internal
        //  AdapterRAM:1073741824
        //  Availability:3
        //  CapabilityDescriptions:
        //  Caption:Intel(R) UHD Graphics
        //  ColorTableEntries:
        //  ConfigManagerErrorCode:0
        //  ConfigManagerUserConfig:False
        //  CreationClassName:Win32_VideoController
        //  CurrentBitsPerPixel:32
        //  CurrentHorizontalResolution:1920
        //  CurrentNumberOfColors:4294967296
        //  CurrentNumberOfColumns:0
        //  CurrentNumberOfRows:0
        //  CurrentRefreshRate:60
        //  CurrentScanMode:4
        //  CurrentVerticalResolution:1080
        //  Description:Intel(R) UHD Graphics
        //  DeviceID:VideoController1
        //  DeviceSpecificPens:
        //  DitherType:0
        //  DriverDate:20200109000000.000000-000
        //  DriverVersion:26.20.100.7755
        //  ErrorCleared:
        //  ErrorDescription:
        //  ICMIntent:
        //  ICMMethod:
        //  InfFilename:oem41.inf
        //  InfSection:iCML_w10_DS
        //  InstallDate:
        //  InstalledDisplayDrivers:C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igdumdim64.dll,C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igd10iumd64.dll,C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igd10iumd64.dll,C:\windows\System32\DriverStore\FileRepository\iigd_dch.inf_amd64_d512f7a0dbcb7a2f\igd12umd64.dll
        //  LastErrorCode:
        //  MaxMemorySupported:
        //  MaxNumberControlled:
        //  MaxRefreshRate:75
        //  MinRefreshRate:50
        //  Monochrome:False
        //  Name:Intel(R) UHD Graphics
        //  NumberOfColorPlanes:
        //  NumberOfVideoPages:
        //  PNPDeviceID:PCI\VEN_8086&DEV_9B41&SUBSYS_8757103C&REV_02\3&11583659&2&10
        //  PowerManagementCapabilities:
        //  PowerManagementSupported:
        //  ProtocolSupported:
        //  ReservedSystemPaletteEntries:
        //  SpecificationVersion:
        //  Status:OK
        //  StatusInfo:
        //  SystemCreationClassName:Win32_ComputerSystem
        //  SystemName:DESKTOP-OLA70V5
        //  SystemPaletteEntries:
        //  TimeOfLastReset:
        //  VideoArchitecture:5
        //  VideoMemoryType:2
        //  VideoMode:
        //  VideoModeDescription:屏幕描述
        //  VideoProcessor:Intel(R) UHD Graphics Family
        //  AcceleratorCapabilities:
        //  AdapterCompatibility:NVIDIA
        //  AdapterDACType:Integrated RAMDAC
        //  AdapterRAM:4293918720
        //  Availability:8
        //  CapabilityDescriptions:
        //  Caption:显卡描述
        //  ColorTableEntries:
        //  ConfigManagerErrorCode:0
        //  ConfigManagerUserConfig:False
        //  CreationClassName:Win32_VideoController
        //  CurrentBitsPerPixel:
        //  CurrentHorizontalResolution:
        //  CurrentNumberOfColors:
        //  CurrentNumberOfColumns:
        //  CurrentNumberOfRows:
        //  CurrentRefreshRate:
        //  CurrentScanMode:
        //  CurrentVerticalResolution:
        //  Description:NVIDIA GeForce MX330
        //  DeviceID:VideoController2
        //  DeviceSpecificPens:
        //  DitherType:
        //  DriverDate:20200923000000.000000-000
        //  DriverVersion:27.21.14.5241
        //  ErrorCleared:
        //  ErrorDescription:
        //  ICMIntent:
        //  ICMMethod:
        //  InfFilename:oem123.inf
        //  InfSection:Section043
        //  InstallDate:
        //  InstalledDisplayDrivers:C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll,C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll,C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll,C:\windows\System32\DriverStore\FileRepository\nvhm.inf_amd64_c87780efe1918cc5\nvldumdx.dll
        //  LastErrorCode:
        //  MaxMemorySupported:
        //  MaxNumberControlled:
        //  MaxRefreshRate:
        //  MinRefreshRate:
        //  Monochrome:False
        //  Name:NVIDIA GeForce MX330
        //  NumberOfColorPlanes:
        //  NumberOfVideoPages:
        //  PNPDeviceID:PCI\VEN_10DE&DEV_1D16&SUBSYS_8757103C&REV_A1\4&24375CB2&0&00E0
        //  PowerManagementCapabilities:
        //  PowerManagementSupported:
        //  ProtocolSupported:
        //  ReservedSystemPaletteEntries:
        //  SpecificationVersion:
        //  Status:OK
        //  StatusInfo:
        //  SystemCreationClassName:Win32_ComputerSystem
        //  SystemName:DESKTOP-OLA70V5
        //  SystemPaletteEntries:
        //  TimeOfLastReset:
        //  VideoArchitecture:5
        //  VideoMemoryType:2
        //  VideoMode:
        //  VideoModeDescription:
        //  VideoProcessor:GeForce MX330
        /// </summary>
        /// <returns></returns>
        public string GetGPUInfo()
        {
            var gpu = new StringBuilder();
            var moc = new ManagementObjectSearcher("select * from Win32_VideoController").Get();

            foreach (var mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    gpu.Append($"{item.Name}:{item.Value}\r\n");
                }
            }
            return gpu.ToString(); ;
        }
    }

    partial class ComputerService : LazyInstance<ComputerService>
    {
        public IEnumerable<ComputerPropertyInfo> GetSystemInfos(string name = "操作系统")
        {
            yield return new ComputerPropertyInfo() { Name = "操作系统版本", Value = this.OSDescription, Description = this.OSDescription, TabName = name, GroupName = "基础信息", Icon = Icons.Clock };
            yield return new ComputerPropertyInfo() { Name = "操作系统架构", Value = this.OSArchitecture, Description = this.OSArchitecture, TabName = name, GroupName = "基础信息", Icon = Icons.Clock };
            yield return new ComputerPropertyInfo() { Name = "是否为Windows系统", Value = this.OSArchitecture, Description = this.OSArchitecture, TabName = name, GroupName = "基础信息", Icon = Icons.Clock };
            yield return new ComputerPropertyInfo() { Name = "计算机名称", Value = this.ComputerName, Description = this.ComputerName, TabName = name, GroupName = "基础信息", Icon = Icons.Clock };
            yield return new ComputerPropertyInfo() { Name = "计算机用户", Value = this.UserName, Description = this.UserName, TabName = name, GroupName = "基础信息", Icon = Icons.Clock };
        }

        public IEnumerable<ComputerPropertyInfo> GetVersionInfos(string name = "电脑型号")
        {
            return this.GetManagementClassInfos(name, "Win32_ComputerSystemProduct");
        }

        public IEnumerable<ComputerPropertyInfo> GetBaseBoardInfos(string name = "主板信息")
        {
            return this.GetManagementClassInfos(name, "Win32_BaseBoard");
        }


        public IEnumerable<ComputerPropertyInfo> GetCPUInfos(string name = "主板信息")
        {
            return this.GetManagementClassInfos(name, "Win32_Processor");
        }

        public IEnumerable<ComputerPropertyInfo> GetRAMInfos(string name = "内存信息")
        {
            var ram = new StringBuilder();
            var searcher = new ManagementObjectSearcher()
            {
                Query = new SelectQuery("Win32_PhysicalMemory"),
            }.Get().GetEnumerator();

            while (searcher.MoveNext())
            {
                ManagementBaseObject baseObj = searcher.Current;
                foreach (var item in baseObj.Properties)
                {
                    yield return new ComputerPropertyInfo() { Name = item.Name, ShortName = item.Name, Data = item, Description = item.Value?.ToString(), TabName = name, GroupName = item.Origin, Icon = Icons.Clock };

                }
            }

            searcher = new ManagementObjectSearcher()
            {
                Query = new SelectQuery("Win32_PerfRawData_PerfOS_Memory"),
            }.Get().GetEnumerator();

            while (searcher.MoveNext())
            {
                ManagementBaseObject baseObj = searcher.Current;
                foreach (var item in baseObj.Properties)
                {
                    yield return new ComputerPropertyInfo() { Name = item.Name, ShortName = item.Name, Data = item, Description = item.Value?.ToString(), TabName = name, GroupName = item.Origin, Icon = Icons.Clock };
                }
            }
        }

        public IEnumerable<ComputerPropertyInfo> GetManagementClassInfos(string name = "系统信息", string managementClass = "Win32_ComputerSystemProduct")
        {
            var moc = new ManagementClass(managementClass).GetInstances();
            foreach (ManagementObject mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    yield return new ComputerPropertyInfo() { Name = item.Name, ShortName = item.Name, Value = item.Value?.ToString(), Data = item, Description = item.Value?.ToString(), TabName = name, GroupName = item.Origin, Icon = Icons.Clock };
                }
            }
        }

        public IEnumerable<PropertyData> GetManagementClassPropertyDatas(string name = "系统信息", string managementClass = "Win32_ComputerSystemProduct")
        {
            var baseBoard = new StringBuilder();
            var moc = new ManagementClass(managementClass).GetInstances();
            foreach (ManagementObject mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<ManagementObject> GetManagementObjects(string name = "系统信息", string managementClass = "Win32_ComputerSystemProduct")
        {
            var moc = new ManagementClass(managementClass).GetInstances();
            foreach (ManagementObject mo in moc)
            {
                yield return mo;
            }
        }


        public IEnumerable<ComputerPropertyInfo> GetGpuInfos(string name = "显卡信息")
        {
            return this.GetManagementObjectSearcherInfos(name, "select * from Win32_VideoController");
        }

        public IEnumerable<ComputerPropertyInfo> GetManagementObjectSearcherInfos(string name = "显卡信息", string managementClass = "select * from Win32_VideoController")
        {
            var gpu = new StringBuilder();
            var moc = new ManagementObjectSearcher(managementClass).Get();
            foreach (var mo in moc)
            {
                foreach (var item in mo.Properties)
                {
                    yield return new ComputerPropertyInfo() { Name = item.Name, ShortName = item.Name, Data = item, Description = item.Value?.ToString(), TabName = name, GroupName = item.Origin, Icon = Icons.Clock };

                }
            }
        }

        //public IEnumerable<ComputerInfoItem> GetSelectQueryInfos(string name = "显卡信息", string managementClass = "select * from Win32_VideoController")
        //{
        //    var gpu = new StringBuilder();
        //    var moc = new ManagementObjectSearcher("select * from Win32_VideoController").Get();
        //    foreach (var mo in moc)
        //    {
        //        foreach (var item in mo.Properties)
        //        {
        //            yield return new ComputerInfoItem() { Name = item.Name, ShortName = item.Name, Description = item.Value.ToString(), TabName = name, GroupName = "基础信息", Icon = Icons.Clock };

        //        }
        //    }
        //}


        public IEnumerable<string> GetWin32Names()
        {
            //"    冷却类别
            yield return "Win32_Fan";//风扇
            yield return "Win32_HeatPipe";//热管
            yield return "Win32_Refrigeration";//致冷
            yield return "Win32_TemperatureProbe";//温度传感
                                                  //"输入设备类别
            yield return "Win32_Keyboard";//键盘
            yield return "Win32_PointingDevice";//指示设备（如鼠标）　
                                                //"大容量存储类别
            yield return "Win32_AutochkSetting";//磁盘自动检查操作设置
            yield return "Win32_CDROMDrive";//光盘驱动器
            yield return "Win32_DiskDrive";//硬盘驱动器
            yield return "Win32_FloppyDrive";//软盘驱动器
            yield return "Win32_PhysicalMedia";//物理媒体
            yield return "Win32_TapeDrive";//磁带驱动器
                                           //"主板、控制器、端口类别
            yield return "Win32_1394Controller";//1394控制器
            yield return "Win32_1394ControllerDevice";//1394控制器设备
            yield return "Win32_AllocatedResource";//已分配的资源
            yield return "Win32_AssociatedProcessorMemory";//处理器和高速缓冲存储器
            yield return "Win32_BaseBoard";//主板
            yield return "Win32_BIOS";//BIOS（基本输入输出系统）
            yield return "Win32_Bus";//总线
            yield return "Win32_CacheMemory";//缓存内存
            yield return "Win32_ControllerHasHub";//USB控制器
            yield return "Win32_DeviceBus";//设备总线
            yield return "Win32_DeviceMemoryAddress";//设备存储器地址
            yield return "Win32_DeviceSettings";//设备设置
            yield return "Win32_DMAChannel";//DMA通道
            yield return "Win32_FloppyController";//软盘控制器
            yield return "Win32_IDEController";//IDE控制器
            yield return "Win32_IDEControllerDevice";//IDE控制器设备
            yield return "Win32_InfraredDevice";//红外线设备
            yield return "Win32_IRQResource";//中断（IRQ）资源
            yield return "Win32_MemoryArray";//内存数组
            yield return "Win32_MemoryArrayLocation";//内存数组位置
            yield return "Win32_MemoryDevice";//内存设备
            yield return "Win32_MemoryDeviceArray";//内存设备数组
            yield return "Win32_MemoryDeviceLocation";//内存设备位置
            yield return "Win32_MotherboardDevice";//主板设备
            yield return "Win32_OnBoardDevice";//插件设备
            yield return "Win32_ParallelPort";//并行端口
            yield return "Win32_PCMCIAController";//PCMCIA控制器
            yield return "Win32_PhysicalMemory";//物理内存
            yield return "Win32_PhysicalMemoryArray";//物理内存数组
            yield return "Win32_PhysicalMemoryLocation";//物理内存位置
            yield return "Win32_PNPAllocatedResource";//PNP保留资源
            yield return "Win32_PNPDevice";//PNP设备
            yield return "Win32_PNPEntity";//PNP实体
            yield return "Win32_PortConnector";//端口连接器
            yield return "Win32_PortResource";//端口资源
            yield return "Win32_Processor";//（CPU）处理器
            yield return "Win32_SCSIController";//SCSI控制器
            yield return "Win32_SCSIControllerDevice";//SCSI控制器设备
            yield return "Win32_SerialPort";//串行端口
            yield return "Win32_SerialPortConfiguration";//串行端口配置
            yield return "Win32_SerialPortSetting";//串行端口设置
            yield return "Win32_SMBIOSMemory";//内存有关的设备的管理
            yield return "Win32_SoundDevice";//声卡
            yield return "Win32_SystemBIOS";//系统BIOS
            yield return "Win32_SystemDriverPNPEntity";//系统驱动器PNP实体
            yield return "Win32_SystemEnclosure";//系统封闭
            yield return "Win32_SystemMemoryResource";//系统内存资源
            yield return "Win32_SystemSlot";//系统插槽
            yield return "Win32_USBController";//USB控制器
            yield return "Win32_USBControllerDevice";//USB控制器设备
            yield return "Win32_USBHub";//USB集线器


            //"建网设备类别
            yield return "Win32_NetworkAdapter";//网络适配器
            yield return "Win32_NetworkAdapterConfiguration";//网络适配器配置
            yield return "Win32_NetworkAdapterSetting";//网络适配器设置


            //"电源类别
            yield return "Win32_AssociatedBattery";//联合电池组
            yield return "Win32_Battery";//电池
            yield return "Win32_CurrentProbe";//当前传感
            yield return "Win32_PortableBattery";//便携式电池
            yield return "Win32_PowerManagementEvent";//电池事件管理
            yield return "Win32_UninterruptiblePowerSupply";//UPS电源
            yield return "Win32_VoltageProbe";//电压探测


            //"打印类别
            yield return "Win32_DriverForDevice";//驱动器设备
            yield return "Win32_Printer";//打印机
            yield return "Win32_PrinterConfiguration";//打印机配置
            yield return "Win32_PrinterController";//打印机控制器
            yield return "Win32_PrinterDriver";//打印机驱动器
            yield return "Win32_PrinterDriverDll";//打印机驱动器DLL
            yield return "Win32_PrinterSetting";//打印机设置
            yield return "Win32_PrintJob";//打印工作
            yield return "Win32_TCPIPPrinterPort";//TCPIP打印机端口


            //"电话类别
            yield return "Win32_POTSModem";//POTS调制解调器（Modem）
            yield return "Win32_POTSModemToSerialPort";//POTS调制解调器串行端口


            //"视频监视器类别
            yield return "Win32_DesktopMonitor";//即插即用监视器
            yield return "Win32_DisplayConfiguration";//显示配置
            yield return "Win32_DisplayControllerConfiguration";//显示控制器配置
            yield return "Win32_VideoConfiguration";//视频配置
            yield return "Win32_VideoController";//视频控制器
            yield return "Win32_VideoSettings";//视频设置


            //"2、操作系统类
            //"COM类别
            yield return "Win32_ClassicCOMApplicationClasses";//
            yield return "Win32_ClassicCOMClass";//
            yield return "Win32_ClassicCOMClassSettings";//
            yield return "Win32_ClientApplicationSetting";// 
            yield return "Win32_COMApplication";//COM应用
            yield return "Win32_COMApplicationClasses";//
            yield return "Win32_COMApplicationSettings";//
            yield return "Win32_COMClass";//
            yield return "Win32_ComClassAutoEmulator";//
            yield return "Win32_ComClassEmulator";//
            yield return "Win32_ComponentCategory";//
            yield return "Win32_COMSetting";//
            yield return "Win32_DCOMApplication";//DCOM应用
            yield return "Win32_DCOMApplicationAccessAllowedSetting";//
            yield return "Win32_DCOMApplicationSetting";//
            yield return "Win32_ImplementedCategory";//


            //"桌面类别
            yield return "Win32_Desktop";//桌面
            yield return "Win32_Environment";//环境
            yield return "Win32_TimeZone";//时区
            yield return "Win32_UserDesktop";//使用者桌面


            //"驱动程序类别
            yield return "Win32_DriverVXD";//
            yield return "Win32_SystemDriver";//系统驱动程序


            //"文件系统类别
            yield return "Win32_CIMLogicalDeviceCIMDataFile";//
            yield return "Win32_Directory";//
            yield return "Win32_DirectorySpecification";//
            yield return "Win32_DiskDriveToDiskPartition";//
            yield return "Win32_DiskPartition";//磁盘逻辑分区
            yield return "Win32_DiskQuota";//NTFS磁盘分区定额
            yield return "Win32_LogicalDisk";//逻辑磁盘分区
            yield return "Win32_LogicalDiskRootDirectory";//
            yield return "Win32_LogicalDiskToPartition";//
            yield return "Win32_MappedLogicalDisk";//映射逻辑磁盘
            yield return "Win32_OperatingSystemAutochkSetting";//
            yield return "Win32_QuotaSetting";//
            yield return "Win32_ShortcutFile";//
            yield return "Win32_SubDirectory";//
            yield return "Win32_SystemPartitions";//
            yield return "Win32_Volume";// 
            yield return "Win32_VolumeQuota";// 
            yield return "Win32_VolumeQuotaSetting";//
            yield return "Win32_VolumeUserQuota";//


            //"作业对象类别
            yield return "Win32_CollectionStatistics";//
            yield return "Win32_LUID";//
            yield return "Win32_LUIDandAttributes";//
            yield return "Win32_NamedJobObject";//
            yield return "Win32_NamedJobObjectActgInfo";//
            yield return "Win32_NamedJobObjectLimit";//
            yield return "Win32_NamedJobObjectLimitSetting";//
            yield return "Win32_NamedJobObjectProcess";//
            yield return "Win32_NamedJobObjectSecLimit";//
            yield return "Win32_NamedJobObjectSecLimitSetting";//
            yield return "Win32_NamedJobObjectStatistics";//
            yield return "Win32_SIDandAttributes";//
            yield return "Win32_TokenGroups";//
            yield return "Win32_TokenPrivileges";//


            //"存储页面文件类别
            yield return "Win32_LogicalMemoryConfiguration";//逻辑内存配置
            yield return "Win32_PageFile";//页面文件
            yield return "Win32_PageFileElementSetting";//
            yield return "Win32_PageFileSetting";//页面文件设置
            yield return "Win32_PageFileUsage";//页面文件使用
            yield return "Win32_SystemLogicalMemoryConfiguration";//


            //"多媒体视听类别
            yield return "Win32_CodecFile";//编解码器文件


            //"建网类别
            yield return "Win32_ActiveRoute";//活动路由
            yield return "Win32_IP4PersistedRouteTable";//
            yield return "Win32_IP4RouteTable";//路由表
            yield return "Win32_IP4RouteTableEvent";//
            yield return "Win32_NetworkClient";//
            yield return "Win32_NetworkConnection";//
            yield return "Win32_NetworkProtocol";//网络协议
            yield return "Win32_NTDomain";//
            yield return "Win32_PingStatus";//
            yield return "Win32_ProtocolBinding";//协议绑定


            //"操作系统事件类别
            yield return "Win32_ComputerShutdownEvent";//
            yield return "Win32_ComputerSystemEvent";//
            yield return "Win32_DeviceChangeEvent";//
            yield return "Win32_ModuleLoadTrace";//
            yield return "Win32_ModuleTrace";//
            yield return "Win32_ProcessStartTrace";//
            yield return "Win32_ProcessStopTrace";//
            yield return "Win32_ProcessTrace";//
            yield return "Win32_SystemConfigurationChangeEvent";//
            yield return "Win32_SystemTrace";//
            yield return "Win32_ThreadStartTrace";//
            yield return "Win32_ThreadStopTrace";//
            yield return "Win32_ThreadTrace";//
            yield return "Win32_VolumeChangeEvent";//


            yield return "Win32_BootConfiguration";//引导配置
            yield return "Win32_ComputerSystem";//计算机系统
            yield return "Win32_ComputerSystemProcessor";//计算机系统处理器
            yield return "Win32_ComputerSystemProduct";//计算机系统产品
            yield return "Win32_DependentService";//信任的服务
            yield return "Win32_LoadOrderGroup";//装载顺序组
            yield return "Win32_LoadOrderGroupServiceDependencies";//
            yield return "Win32_LoadOrderGroupServiceMembers";//
            yield return "Win32_OperatingSystem";//操作系统
            yield return "Win32_OperatingSystemQFE";//
            yield return "Win32_OSRecoveryConfiguration";//操作系统恢复配置
            yield return "Win32_QuickFixEngineering";//
            yield return "Win32_StartupCommand";//启动命令
            yield return "Win32_SystemBootConfiguration";//
            yield return "Win32_SystemDesktop";//
            yield return "Win32_SystemDevices";//
            yield return "Win32_SystemLoadOrderGroups";//
            yield return "Win32_SystemNetworkConnections";//
            yield return "Win32_SystemOperatingSystem";//
            yield return "Win32_SystemProcesses";//
            yield return "Win32_SystemProgramGroups";//Windows开始程序组
            yield return "Win32_SystemResources";//
            yield return "Win32_SystemServices";//系统服务
            yield return "Win32_SystemSetting";//
            yield return "Win32_SystemSystemDriver";//
            yield return "Win32_SystemTimeZone";//系统时区
            yield return "Win32_SystemUsers";//系统用户


            //"进程类别
            yield return "Win32_Process";//进程
            yield return "Win32_ProcessStartup";//
            yield return "Win32_Thread";//线程


            //"注册类别
            yield return "Win32_Registry";//注册表
                                          //"调试程序作业类别
            yield return "Win32_CurrentTime";//当前时间
            yield return "Win32_ScheduledJob";//


            // "安全类别
            yield return "Win32_AccountSID";//
            yield return "Win32_ACE";//
            yield return "Win32_LogicalFileAccess";//
            yield return "Win32_LogicalFileAuditing";//
            yield return "Win32_LogicalFileGroup";//
            yield return "Win32_LogicalFileOwner";//
            yield return "Win32_LogicalFileSecuritySetting";//
            yield return "Win32_LogicalShareAccess";//
            yield return "Win32_LogicalShareAuditing";//
            yield return "Win32_LogicalShareSecuritySetting";//
            yield return "Win32_PrivilegesStatus";//
            yield return "Win32_SecurityDescriptor";//
            yield return "Win32_SecuritySetting";//
            yield return "Win32_SecuritySettingAccess";//
            yield return "Win32_SecuritySettingAuditing";//
            yield return "Win32_SecuritySettingGroup";//
            yield return "Win32_SecuritySettingOfLogicalFile";//
            yield return "Win32_SecuritySettingOfLogicalShare";//
            yield return "Win32_SecuritySettingOfObject";//
            yield return "Win32_SecuritySettingOwner";//
            yield return "Win32_SID";//
            yield return "Win32_Trustee";//
                                         //"服务类别
            yield return "Win32_BaseService";//基本服务
            yield return "Win32_Service";//服务


            //"共享类别
            yield return "Win32_DFSNode";//
            yield return "Win32_DFSNodeTarget";//
            yield return "Win32_DFSTarget";//
            yield return "Win32_ServerConnection";//
            yield return "Win32_ServerSession";//
            yield return "Win32_ConnectionShare";//
            yield return "Win32_PrinterShare";//
            yield return "Win32_SessionConnection";//
            yield return "Win32_SessionProcess";//
            yield return "Win32_ShareToDirectory";//
            yield return "Win32_Share";//共享文件夹


            //"开始菜单类别
            yield return "Win32_LogicalProgramGroup";//Windows开始逻辑程序组
            yield return "Win32_LogicalProgramGroupDirectory";//Windows开始逻辑程序组目录Win32_LogicalProgramGroupItem";//Windows开始逻辑程序组项
            yield return "Win32_LogicalProgramGroupItemDataFile";//Windows开始逻辑程序组项数据文件
            yield return "Win32_ProgramGroup";//Windows程序组
            yield return "Win32_ProgramGroupContents";//Windows程序组内容
            yield return "Win32_ProgramGroupOrItem";//Windows程序组或项
                                                    //"存储类别
            yield return "Win32_ShadowBy";//
            yield return "Win32_ShadowContext";//
            yield return "Win32_ShadowCopy";//
            yield return "Win32_ShadowDiffVolumeSupport";//
            yield return "Win32_ShadowFor";//
            yield return "Win32_ShadowOn";//
            yield return "Win32_ShadowProvider";//
            yield return "Win32_ShadowStorage";//
            yield return "Win32_ShadowVolumeSupport";//
            yield return "Win32_Volume";//
            yield return "Win32_VolumeUserQuota";//


            //"用户类别
            yield return "Win32_Account";//帐户
            yield return "Win32_Group";//组
            yield return "Win32_GroupInDomain";//域中的组
            yield return "Win32_GroupUser";//组用户
            yield return "Win32_LogonSession";//登录会话
            yield return "Win32_LogonSessionMappedDisk";//
            yield return "Win32_NetworkLoginProfile";//
            yield return "Win32_SystemAccount";//系统账户
            yield return "Win32_UserAccount";//使用账户
            yield return "Win32_UserInDomain";//域中的用户
                                              //"Windows NT的事件日志类别
            yield return "Win32_NTEventlogFile";//事件日志文件
            yield return "Win32_NTLogEvent";//日志事件
            yield return "Win32_NTLogEventComputer";//日志事件计算机
            yield return "Win32_NTLogEventLog";//日志事件日志
            yield return "Win32_NTLogEventUser";//


            //"Windows产品激活类别
            yield return "Win32_ComputerSystemWindowsProductActivationSetting";//
            yield return "Win32_Proxy";//代理
            yield return "Win32_WindowsProductActivation";//Windows产品激活


            //"3、安装应用程序类
            yield return "Win32_ActionCheck";//
            yield return "Win32_ApplicationCommandLine";//
            yield return "Win32_ApplicationService";//
            yield return "Win32_Binary";//
            yield return "Win32_BindImageAction";//
            yield return "Win32_CheckCheck";//
            yield return "Win32_ClassInfoAction";//
            yield return "Win32_CommandLineAccess";//
            yield return "Win32_Condition";//
            yield return "Win32_CreateFolderAction";//
            yield return "Win32_DuplicateFileAction";//
            yield return "Win32_EnvironmentSpecification";//
            yield return "Win32_ExtensionInfoAction";//
            yield return "Win32_FileSpecification";//
            yield return "Win32_FontInfoAction";//
            yield return "Win32_IniFileSpecification";//
            yield return "Win32_InstalledSoftwareElement";//
            yield return "Win32_LaunchCondition";//
            yield return "Win32_ManagedSystemElementResource";//
            yield return "Win32_MIMEInfoAction";//
            yield return "Win32_MoveFileAction";//
            yield return "Win32_MSIResource";//
            yield return "Win32_ODBCAttribute";//
            yield return "Win32_ODBCDataSourceAttribute";//
            yield return "Win32_ODBCDataSourceSpecification";//
            yield return "Win32_ODBCDriverAttribute";//
            yield return "Win32_ODBCDriverSoftwareElement";//
            yield return "Win32_ODBCDriverSpecification";//
            yield return "Win32_ODBCSourceAttribute";//
            yield return "Win32_ODBCTranslatorSpecification";//
            yield return "Win32_Patch";//
            yield return "Win32_PatchFile";//
            yield return "Win32_PatchPackage";//
            yield return "Win32_Product";//
            yield return "Win32_ProductCheck";//
            yield return "Win32_ProductResource";//
            yield return "Win32_ProductSoftwareFeatures";//
            yield return "Win32_ProgIDSpecification";//
            yield return "Win32_Property";//
            yield return "Win32_PublishComponentAction";//
            yield return "Win32_RegistryAction";//
            yield return "Win32_RemoveFileAction";//
            yield return "Win32_RemoveIniAction";//
            yield return "Win32_ReserveCost";//
            yield return "Win32_SelfRegModuleAction";//
            yield return "Win32_ServiceControl";//
            yield return "Win32_ServiceSpecification";//
            yield return "Win32_ServiceSpecificationService";//
            yield return "Win32_SettingCheck";//
            yield return "Win32_ShortcutAction";//
            yield return "Win32_ShortcutSAP";//
            yield return "Win32_SoftwareElement";//
            yield return "Win32_SoftwareElementAction";//
            yield return "Win32_SoftwareElementCheck";//
            yield return "Win32_SoftwareElementCondition";//
            yield return "Win32_SoftwareElementResource";//
            yield return "Win32_SoftwareFeature";//
            yield return "Win32_SoftwareFeatureAction";//
            yield return "Win32_SoftwareFeatureCheck";//
            yield return "Win32_SoftwareFeatureParent";//
            yield return "Win32_SoftwareFeatureSoftwareElements";//
            yield return "Win32_TypeLibraryAction";//
                                                   //"4、WMI服务管理类
                                                   //"WMI配置类别
            yield return "Win32_MethodParameterClass";//方法参数类
                                                      //"WMI管理类别
            yield return "Win32_WMISetting";//WMI设置
            yield return "Win32_WMIElementSetting";//WMI单元设置


            //"5、性能计数器类
            //"格式化性能计数器类别
            yield return "Win32_PerfFormattedData";//
            yield return "Win32_PerfFormattedData_ASP_ActiveServerPages";//
            yield return "Win32_PerfFormattedData_ContentFilter_IndexingServiceFilter";//
            yield return "Win32_PerfFormattedData_ContentIndex_IndexingService";//
            yield return "Win32_PerfFormattedData_InetInfo_InternetInformationServicesGlobal";//
            yield return "Win32_PerfFormattedData_ISAPISearch_HttpIndexingService";//
            yield return "Win32_PerfFormattedData_MSDTC_DistributedTransactionCoordinator";//
            yield return "Win32_PerfFormattedData_NTFSDRV_SMTPNTFSStoreDriver";//
            yield return "Win32_PerfFormattedData_PerfDisk_LogicalDisk";//
            yield return "Win32_PerfFormattedData_PerfDisk_PhysicalDisk";//
            yield return "Win32_PerfFormattedData_PerfNet_Browser";//
            yield return "Win32_PerfFormattedData_PerfNet_Redirector";//
            yield return "Win32_PerfFormattedData_PerfNet_Server";//
            yield return "Win32_PerfFormattedData_PerfNet_ServerWorkQueues";//
            yield return "Win32_PerfFormattedData_PerfOS_Cache";//
            yield return "Win32_PerfFormattedData_PerfOS_Memory";//
            yield return "Win32_PerfFormattedData_PerfOS_Objects";//
            yield return "Win32_PerfFormattedData_PerfOS_PagingFile";//
            yield return "Win32_PerfFormattedData_PerfOS_Processor";//
            yield return "Win32_PerfFormattedData_PerfOS_System";//
            yield return "Win32_PerfFormattedData_PerfProc_FullImage_Costly";//
            yield return "Win32_PerfFormattedData_PerfProc_Image_Costly";//Win32_PerfFormattedData_PerfProc_JobObject";//Win32_PerfFormattedData_PerfProc_JobObjectDetails";//Win32_PerfFormattedData_PerfProc_Process";//Win32_PerfFormattedData_PerfProc_ProcessAddressSpace_Costly";//Win32_PerfFormattedData_PerfProc_Thread";//Win32_PerfFormattedData_PerfProc_ThreadDetails_Costly";//Win32_PerfFormattedData_PSched_PSchedFlow";//
            yield return "Win32_PerfFormattedData_PSched_PSchedPipe";//Win32_PerfFormattedData_RemoteAccess_RASPort";//Win32_PerfFormattedData_RemoteAccess_RASTotal";//Win32_PerfFormattedData_RSVP_ACSRSVPInterfaces";//Win32_PerfFormattedData_RSVP_ACSRSVPService";//Win32_PerfFormattedData_SMTPSVC_SMTPServer";//Win32_PerfFormattedData_Spooler_PrintQueue";//
            yield return "Win32_PerfFormattedData_TapiSrv_Telephony";//
            yield return "Win32_PerfFormattedData_Tcpip_ICMP";//
            yield return "Win32_PerfFormattedData_Tcpip_IP";//
            yield return "Win32_PerfFormattedData_Tcpip_NBTConnection";//Win32_PerfFormattedData_Tcpip_NetworkInterface";//
            yield return "Win32_PerfFormattedData_Tcpip_TCP";//
            yield return "Win32_PerfFormattedData_Tcpip_UDP";//Win32_PerfFormattedData_TermService_TerminalServices";//Win32_PerfFormattedData_TermService_TerminalServicesSession";//Win32_PerfFormattedData_W3SVC_WebService";//


            //"原始性能计数器类别
            yield return "Win32_PerfRawData";//Win32_PerfRawData_ASP_ActiveServerPages";//Win32_PerfRawData_ContentFilter_IndexingServiceFilter";//Win32_PerfRawData_ContentIndex_IndexingService";//Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal";//Win32_PerfRawData_ISAPISearch_HttpIndexingService";//Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator";//Win32_PerfRawData_NTFSDRV_SMTPNTFSStoreDriver";//
            yield return "Win32_PerfRawData_PerfDisk_LogicalDisk";//
            yield return "Win32_PerfRawData_PerfDisk_PhysicalDisk";//
            yield return "Win32_PerfRawData_PerfNet_Browser";//
            yield return "Win32_PerfRawData_PerfNet_Redirector";//
            yield return "Win32_PerfRawData_PerfNet_Server";//
            yield return "Win32_PerfRawData_PerfNet_ServerWorkQueues";//
            yield return "Win32_PerfRawData_PerfOS_Cache";//
            yield return "Win32_PerfRawData_PerfOS_Memory";//
            yield return "Win32_PerfRawData_PerfOS_Objects";//
            yield return "Win32_PerfRawData_PerfOS_PagingFile";//
            yield return "Win32_PerfRawData_PerfOS_Processor";//
            yield return "Win32_PerfRawData_PerfOS_System";//
            yield return "Win32_PerfRawData_PerfProc_FullImage_Costly";//
            yield return "Win32_PerfRawData_PerfProc_Image_Costly";//
            yield return "Win32_PerfRawData_PerfProc_JobObject";//
            yield return "Win32_PerfRawData_PerfProc_JobObjectDetails";//
            yield return "Win32_PerfRawData_PerfProc_Process";//Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly";//Win32_PerfRawData_PerfProc_Thread";//
            yield return "Win32_PerfRawData_PerfProc_ThreadDetails_Costly";//
            yield return "Win32_PerfRawData_PSched_PSchedFlow";//
            yield return "Win32_PerfRawData_PSched_PSchedPipe";//
            yield return "Win32_PerfRawData_RemoteAccess_RASPort";//
            yield return "Win32_PerfRawData_RemoteAccess_RASTotal";//
            yield return "Win32_PerfRawData_RSVP_ACSRSVPInterfaces";//
            yield return "Win32_PerfRawData_RSVP_ACSRSVPService";//
            yield return "Win32_PerfRawData_SMTPSVC_SMTPServer";//
            yield return "Win32_PerfRawData_Spooler_PrintQueue";//
            yield return "Win32_PerfRawData_TapiSrv_Telephony";//
            yield return "Win32_PerfRawData_Tcpip_ICMP";//
            yield return "Win32_PerfRawData_Tcpip_IP";//
            yield return "Win32_PerfRawData_Tcpip_NBTConnection";//
            yield return "Win32_PerfRawData_Tcpip_NetworkInterface";//
            yield return "Win32_PerfRawData_Tcpip_TCP";//
            yield return "Win32_PerfRawData_Tcpip_UDP";//
            yield return "Win32_PerfRawData_TermService_TerminalServices";//Win32_PerfRawData_TermService_TerminalServicesSession";//Win32_PerfRawData_W3SVC_WebService
        }

    }

    /// <summary> 说明</summary>
    public class ComputerPropertyInfo : DisplayerViewModelBase
    {
        private string _value;
        /// <summary> 说明  </summary>
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        private PropertyData _data;
        /// <summary> 说明  </summary>
        public PropertyData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }
    }


    //    冷却类别
    //Win32_Fan--风扇
    //Win32_HeatPipe--热管
    //Win32_Refrigeration--致冷
    //Win32_TemperatureProbe--温度传感
    //输入设备类别
    //Win32_Keyboard--键盘
    //Win32_PointingDevice--指示设备（如鼠标）　
    //大容量存储类别
    //Win32_AutochkSetting--磁盘自动检查操作设置
    //Win32_CDROMDrive--光盘驱动器
    //Win32_DiskDrive--硬盘驱动器
    //Win32_FloppyDrive--软盘驱动器
    //Win32_PhysicalMedia--物理媒体
    //Win32_TapeDrive--磁带驱动器
    //主板、控制器、端口类别
    //Win32_1394Controller--1394控制器
    //Win32_1394ControllerDevice--1394控制器设备
    //Win32_AllocatedResource--已分配的资源
    //Win32_AssociatedProcessorMemory--处理器和高速缓冲存储器
    //Win32_BaseBoard--主板
    //Win32_BIOS--BIOS（基本输入输出系统）
    //Win32_Bus--总线
    //Win32_CacheMemory--缓存内存
    //Win32_ControllerHasHub--USB控制器
    //Win32_DeviceBus--设备总线
    //Win32_DeviceMemoryAddress--设备存储器地址
    //Win32_DeviceSettings--设备设置
    //Win32_DMAChannel--DMA通道
    //Win32_FloppyController--软盘控制器
    //Win32_IDEController--IDE控制器
    //Win32_IDEControllerDevice--IDE控制器设备
    //Win32_InfraredDevice--红外线设备
    //Win32_IRQResource--中断（IRQ）资源
    //Win32_MemoryArray--内存数组
    //Win32_MemoryArrayLocation--内存数组位置
    //Win32_MemoryDevice--内存设备
    //Win32_MemoryDeviceArray--内存设备数组
    //Win32_MemoryDeviceLocation--内存设备位置
    //Win32_MotherboardDevice--主板设备
    //Win32_OnBoardDevice--插件设备
    //Win32_ParallelPort--并行端口
    //Win32_PCMCIAController--PCMCIA控制器
    //Win32_PhysicalMemory--物理内存
    //Win32_PhysicalMemoryArray--物理内存数组
    //Win32_PhysicalMemoryLocation--物理内存位置
    //Win32_PNPAllocatedResource--PNP保留资源
    //Win32_PNPDevice--PNP设备
    //Win32_PNPEntity--PNP实体
    //Win32_PortConnector--端口连接器
    //Win32_PortResource--端口资源
    //Win32_Processor--（CPU）处理器
    //Win32_SCSIController--SCSI控制器
    //Win32_SCSIControllerDevice--SCSI控制器设备
    //Win32_SerialPort--串行端口
    //Win32_SerialPortConfiguration--串行端口配置
    //Win32_SerialPortSetting--串行端口设置
    //Win32_SMBIOSMemory--内存有关的设备的管理
    //Win32_SoundDevice--声卡
    //Win32_SystemBIOS--系统BIOS
    //Win32_SystemDriverPNPEntity--系统驱动器PNP实体
    //Win32_SystemEnclosure--系统封闭
    //Win32_SystemMemoryResource--系统内存资源
    //Win32_SystemSlot--系统插槽
    //Win32_USBController--USB控制器
    //Win32_USBControllerDevice--USB控制器设备
    //Win32_USBHub--USB集线器


    //建网设备类别
    //Win32_NetworkAdapter--网络适配器
    //Win32_NetworkAdapterConfiguration--网络适配器配置
    //Win32_NetworkAdapterSetting--网络适配器设置


    //电源类别
    //Win32_AssociatedBattery--联合电池组
    //Win32_Battery--电池
    //Win32_CurrentProbe--当前传感
    //Win32_PortableBattery--便携式电池
    //Win32_PowerManagementEvent--电池事件管理
    //Win32_UninterruptiblePowerSupply--UPS电源
    //Win32_VoltageProbe--电压探测


    //打印类别
    //Win32_DriverForDevice--驱动器设备
    //Win32_Printer--打印机
    //Win32_PrinterConfiguration--打印机配置
    //Win32_PrinterController--打印机控制器
    //Win32_PrinterDriver--打印机驱动器
    //Win32_PrinterDriverDll--打印机驱动器DLL
    //Win32_PrinterSetting--打印机设置
    //Win32_PrintJob--打印工作
    //Win32_TCPIPPrinterPort--TCPIP打印机端口


    //电话类别
    //Win32_POTSModem--POTS调制解调器（Modem）
    //Win32_POTSModemToSerialPort--POTS调制解调器串行端口


    //视频监视器类别
    //Win32_DesktopMonitor--即插即用监视器
    //Win32_DisplayConfiguration--显示配置
    //Win32_DisplayControllerConfiguration--显示控制器配置
    //Win32_VideoConfiguration--视频配置
    //Win32_VideoController--视频控制器
    //Win32_VideoSettings--视频设置


    //2、操作系统类
    //COM类别
    //Win32_ClassicCOMApplicationClasses--
    //Win32_ClassicCOMClass--
    //Win32_ClassicCOMClassSettings--
    //Win32_ClientApplicationSetting-- 
    //Win32_COMApplication--COM应用
    //Win32_COMApplicationClasses--
    //Win32_COMApplicationSettings--
    //Win32_COMClass--
    //Win32_ComClassAutoEmulator--
    //Win32_ComClassEmulator--
    //Win32_ComponentCategory--
    //Win32_COMSetting--
    //Win32_DCOMApplication--DCOM应用
    //Win32_DCOMApplicationAccessAllowedSetting--
    //Win32_DCOMApplicationSetting--
    //Win32_ImplementedCategory--


    //桌面类别
    //Win32_Desktop--桌面
    //Win32_Environment--环境
    //Win32_TimeZone--时区
    //Win32_UserDesktop--使用者桌面


    //驱动程序类别
    //Win32_DriverVXD--
    //Win32_SystemDriver--系统驱动程序


    //文件系统类别
    //Win32_CIMLogicalDeviceCIMDataFile--
    //Win32_Directory--
    //Win32_DirectorySpecification--
    //Win32_DiskDriveToDiskPartition--
    //Win32_DiskPartition--磁盘逻辑分区
    //Win32_DiskQuota--NTFS磁盘分区定额
    //Win32_LogicalDisk--逻辑磁盘分区
    //Win32_LogicalDiskRootDirectory--
    //Win32_LogicalDiskToPartition--
    //Win32_MappedLogicalDisk--映射逻辑磁盘
    //Win32_OperatingSystemAutochkSetting--
    //Win32_QuotaSetting--
    //Win32_ShortcutFile--
    //Win32_SubDirectory--
    //Win32_SystemPartitions--
    //Win32_Volume-- 
    //Win32_VolumeQuota-- 
    //Win32_VolumeQuotaSetting--
    //Win32_VolumeUserQuota--


    //作业对象类别
    //Win32_CollectionStatistics--
    //Win32_LUID--
    //Win32_LUIDandAttributes--
    //Win32_NamedJobObject--
    //Win32_NamedJobObjectActgInfo--
    //Win32_NamedJobObjectLimit--
    //Win32_NamedJobObjectLimitSetting--
    //Win32_NamedJobObjectProcess--
    //Win32_NamedJobObjectSecLimit--
    //Win32_NamedJobObjectSecLimitSetting--
    //Win32_NamedJobObjectStatistics--
    //Win32_SIDandAttributes--
    //Win32_TokenGroups--
    //Win32_TokenPrivileges--


    //存储页面文件类别
    //Win32_LogicalMemoryConfiguration--逻辑内存配置
    //Win32_PageFile--页面文件
    //Win32_PageFileElementSetting--
    //Win32_PageFileSetting--页面文件设置
    //Win32_PageFileUsage--页面文件使用
    //Win32_SystemLogicalMemoryConfiguration--


    //多媒体视听类别
    //Win32_CodecFile--编解码器文件


    //建网类别
    //Win32_ActiveRoute--活动路由
    //Win32_IP4PersistedRouteTable--
    //Win32_IP4RouteTable--路由表
    //Win32_IP4RouteTableEvent--
    //Win32_NetworkClient--
    //Win32_NetworkConnection--
    //Win32_NetworkProtocol--网络协议
    //Win32_NTDomain--
    //Win32_PingStatus--
    //Win32_ProtocolBinding--协议绑定


    //操作系统事件类别
    //Win32_ComputerShutdownEvent--
    //Win32_ComputerSystemEvent--
    //Win32_DeviceChangeEvent--
    //Win32_ModuleLoadTrace--
    //Win32_ModuleTrace--
    //Win32_ProcessStartTrace--
    //Win32_ProcessStopTrace--
    //Win32_ProcessTrace--
    //Win32_SystemConfigurationChangeEvent--
    //Win32_SystemTrace--
    //Win32_ThreadStartTrace--
    //Win32_ThreadStopTrace--
    //Win32_ThreadTrace--
    //Win32_VolumeChangeEvent--


    //Win32_BootConfiguration--引导配置
    //Win32_ComputerSystem--计算机系统
    //Win32_ComputerSystemProcessor--计算机系统处理器
    //Win32_ComputerSystemProduct--计算机系统产品
    //Win32_DependentService--信任的服务
    //Win32_LoadOrderGroup--装载顺序组
    //Win32_LoadOrderGroupServiceDependencies--
    //Win32_LoadOrderGroupServiceMembers--
    //Win32_OperatingSystem--操作系统
    //Win32_OperatingSystemQFE--
    //Win32_OSRecoveryConfiguration--操作系统恢复配置
    //Win32_QuickFixEngineering--
    //Win32_StartupCommand--启动命令
    //Win32_SystemBootConfiguration--
    //Win32_SystemDesktop--
    //Win32_SystemDevices--
    //Win32_SystemLoadOrderGroups--
    //Win32_SystemNetworkConnections--
    //Win32_SystemOperatingSystem--
    //Win32_SystemProcesses--
    //Win32_SystemProgramGroups--Windows开始程序组
    //Win32_SystemResources--
    //Win32_SystemServices--系统服务
    //Win32_SystemSetting--
    //Win32_SystemSystemDriver--
    //Win32_SystemTimeZone--系统时区
    //Win32_SystemUsers--系统用户


    //进程类别
    //Win32_Process--进程
    //Win32_ProcessStartup--
    //Win32_Thread--线程


    //注册类别
    //Win32_Registry--注册表
    //调试程序作业类别
    //Win32_CurrentTime--当前时间
    //Win32_ScheduledJob--


    //安全类别
    //Win32_AccountSID--
    //Win32_ACE--
    //Win32_LogicalFileAccess--
    //Win32_LogicalFileAuditing--
    //Win32_LogicalFileGroup--
    //Win32_LogicalFileOwner--
    //Win32_LogicalFileSecuritySetting--
    //Win32_LogicalShareAccess--
    //Win32_LogicalShareAuditing--
    //Win32_LogicalShareSecuritySetting--
    //Win32_PrivilegesStatus--
    //Win32_SecurityDescriptor--
    //Win32_SecuritySetting--
    //Win32_SecuritySettingAccess--
    //Win32_SecuritySettingAuditing--
    //Win32_SecuritySettingGroup--
    //Win32_SecuritySettingOfLogicalFile--
    //Win32_SecuritySettingOfLogicalShare--
    //Win32_SecuritySettingOfObject--
    //Win32_SecuritySettingOwner--
    //Win32_SID--
    //Win32_Trustee--
    //服务类别
    //Win32_BaseService--基本服务
    //Win32_Service--服务


    //共享类别
    //Win32_DFSNode--
    //Win32_DFSNodeTarget--
    //Win32_DFSTarget--
    //Win32_ServerConnection--
    //Win32_ServerSession--
    //Win32_ConnectionShare--
    //Win32_PrinterShare--
    //Win32_SessionConnection--
    //Win32_SessionProcess--
    //Win32_ShareToDirectory--
    //Win32_Share--共享文件夹


    //开始菜单类别
    //Win32_LogicalProgramGroup--Windows开始逻辑程序组
    //Win32_LogicalProgramGroupDirectory--Windows开始逻辑程序组目录Win32_LogicalProgramGroupItem--Windows开始逻辑程序组项
    //Win32_LogicalProgramGroupItemDataFile--Windows开始逻辑程序组项数据文件
    //Win32_ProgramGroup--Windows程序组
    //Win32_ProgramGroupContents--Windows程序组内容
    //Win32_ProgramGroupOrItem--Windows程序组或项
    //存储类别
    //Win32_ShadowBy--
    //Win32_ShadowContext--
    //Win32_ShadowCopy--
    //Win32_ShadowDiffVolumeSupport--
    //Win32_ShadowFor--
    //Win32_ShadowOn--
    //Win32_ShadowProvider--
    //Win32_ShadowStorage--
    //Win32_ShadowVolumeSupport--
    //Win32_Volume--
    //Win32_VolumeUserQuota--


    //用户类别
    //Win32_Account--帐户
    //Win32_Group--组
    //Win32_GroupInDomain--域中的组
    //Win32_GroupUser--组用户
    //Win32_LogonSession--登录会话
    //Win32_LogonSessionMappedDisk--
    //Win32_NetworkLoginProfile--
    //Win32_SystemAccount--系统账户
    //Win32_UserAccount--使用账户
    //Win32_UserInDomain--域中的用户
    //Windows NT的事件日志类别
    //Win32_NTEventlogFile--事件日志文件
    //Win32_NTLogEvent--日志事件
    //Win32_NTLogEventComputer--日志事件计算机
    //Win32_NTLogEventLog--日志事件日志
    //Win32_NTLogEventUser--


    //Windows产品激活类别
    //Win32_ComputerSystemWindowsProductActivationSetting--
    //Win32_Proxy--代理
    //Win32_WindowsProductActivation--Windows产品激活


    //3、安装应用程序类
    //Win32_ActionCheck--
    //Win32_ApplicationCommandLine--
    //Win32_ApplicationService--
    //Win32_Binary--
    //Win32_BindImageAction--
    //Win32_CheckCheck--
    //Win32_ClassInfoAction--
    //Win32_CommandLineAccess--
    //Win32_Condition--
    //Win32_CreateFolderAction--
    //Win32_DuplicateFileAction--
    //Win32_EnvironmentSpecification--
    //Win32_ExtensionInfoAction--
    //Win32_FileSpecification--
    //Win32_FontInfoAction--
    //Win32_IniFileSpecification--
    //Win32_InstalledSoftwareElement--
    //Win32_LaunchCondition--
    //Win32_ManagedSystemElementResource--
    //Win32_MIMEInfoAction--
    //Win32_MoveFileAction--
    //Win32_MSIResource--
    //Win32_ODBCAttribute--
    //Win32_ODBCDataSourceAttribute--
    //Win32_ODBCDataSourceSpecification--
    //Win32_ODBCDriverAttribute--
    //Win32_ODBCDriverSoftwareElement--
    //Win32_ODBCDriverSpecification--
    //Win32_ODBCSourceAttribute--
    //Win32_ODBCTranslatorSpecification--
    //Win32_Patch--
    //Win32_PatchFile--
    //Win32_PatchPackage--
    //Win32_Product--
    //Win32_ProductCheck--
    //Win32_ProductResource--
    //Win32_ProductSoftwareFeatures--
    //Win32_ProgIDSpecification--
    //Win32_Property--
    //Win32_PublishComponentAction--
    //Win32_RegistryAction--
    //Win32_RemoveFileAction--
    //Win32_RemoveIniAction--
    //Win32_ReserveCost--
    //Win32_SelfRegModuleAction--
    //Win32_ServiceControl--
    //Win32_ServiceSpecification--
    //Win32_ServiceSpecificationService--
    //Win32_SettingCheck--
    //Win32_ShortcutAction--
    //Win32_ShortcutSAP--
    //Win32_SoftwareElement--
    //Win32_SoftwareElementAction--
    //Win32_SoftwareElementCheck--
    //Win32_SoftwareElementCondition--
    //Win32_SoftwareElementResource--
    //Win32_SoftwareFeature--
    //Win32_SoftwareFeatureAction--
    //Win32_SoftwareFeatureCheck--
    //Win32_SoftwareFeatureParent--
    //Win32_SoftwareFeatureSoftwareElements--
    //Win32_TypeLibraryAction--
    //4、WMI服务管理类
    //WMI配置类别
    //Win32_MethodParameterClass--方法参数类
    //WMI管理类别
    //Win32_WMISetting--WMI设置
    //Win32_WMIElementSetting--WMI单元设置


    //5、性能计数器类
    //格式化性能计数器类别
    //Win32_PerfFormattedData--
    //Win32_PerfFormattedData_ASP_ActiveServerPages--
    //Win32_PerfFormattedData_ContentFilter_IndexingServiceFilter--
    //Win32_PerfFormattedData_ContentIndex_IndexingService--
    //Win32_PerfFormattedData_InetInfo_InternetInformationServicesGlobal--
    //Win32_PerfFormattedData_ISAPISearch_HttpIndexingService--
    //Win32_PerfFormattedData_MSDTC_DistributedTransactionCoordinator--
    //Win32_PerfFormattedData_NTFSDRV_SMTPNTFSStoreDriver--
    //Win32_PerfFormattedData_PerfDisk_LogicalDisk--
    //Win32_PerfFormattedData_PerfDisk_PhysicalDisk--
    //Win32_PerfFormattedData_PerfNet_Browser--
    //Win32_PerfFormattedData_PerfNet_Redirector--
    //Win32_PerfFormattedData_PerfNet_Server--
    //Win32_PerfFormattedData_PerfNet_ServerWorkQueues--
    //Win32_PerfFormattedData_PerfOS_Cache--
    //Win32_PerfFormattedData_PerfOS_Memory--
    //Win32_PerfFormattedData_PerfOS_Objects--
    //Win32_PerfFormattedData_PerfOS_PagingFile--
    //Win32_PerfFormattedData_PerfOS_Processor--
    //Win32_PerfFormattedData_PerfOS_System--
    //Win32_PerfFormattedData_PerfProc_FullImage_Costly--
    //Win32_PerfFormattedData_PerfProc_Image_Costly--Win32_PerfFormattedData_PerfProc_JobObject--Win32_PerfFormattedData_PerfProc_JobObjectDetails--Win32_PerfFormattedData_PerfProc_Process--Win32_PerfFormattedData_PerfProc_ProcessAddressSpace_Costly--Win32_PerfFormattedData_PerfProc_Thread--Win32_PerfFormattedData_PerfProc_ThreadDetails_Costly--Win32_PerfFormattedData_PSched_PSchedFlow--
    //Win32_PerfFormattedData_PSched_PSchedPipe--Win32_PerfFormattedData_RemoteAccess_RASPort--Win32_PerfFormattedData_RemoteAccess_RASTotal--Win32_PerfFormattedData_RSVP_ACSRSVPInterfaces--Win32_PerfFormattedData_RSVP_ACSRSVPService--Win32_PerfFormattedData_SMTPSVC_SMTPServer--Win32_PerfFormattedData_Spooler_PrintQueue--
    //Win32_PerfFormattedData_TapiSrv_Telephony--
    //Win32_PerfFormattedData_Tcpip_ICMP--
    //Win32_PerfFormattedData_Tcpip_IP--
    //Win32_PerfFormattedData_Tcpip_NBTConnection--Win32_PerfFormattedData_Tcpip_NetworkInterface--
    //Win32_PerfFormattedData_Tcpip_TCP--
    //Win32_PerfFormattedData_Tcpip_UDP--Win32_PerfFormattedData_TermService_TerminalServices--Win32_PerfFormattedData_TermService_TerminalServicesSession--Win32_PerfFormattedData_W3SVC_WebService--


    //原始性能计数器类别
    //Win32_PerfRawData--Win32_PerfRawData_ASP_ActiveServerPages--Win32_PerfRawData_ContentFilter_IndexingServiceFilter--Win32_PerfRawData_ContentIndex_IndexingService--Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal--Win32_PerfRawData_ISAPISearch_HttpIndexingService--Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator--Win32_PerfRawData_NTFSDRV_SMTPNTFSStoreDriver--
    //Win32_PerfRawData_PerfDisk_LogicalDisk--
    //Win32_PerfRawData_PerfDisk_PhysicalDisk--
    //Win32_PerfRawData_PerfNet_Browser--
    //Win32_PerfRawData_PerfNet_Redirector--
    //Win32_PerfRawData_PerfNet_Server--
    //Win32_PerfRawData_PerfNet_ServerWorkQueues--
    //Win32_PerfRawData_PerfOS_Cache--
    //Win32_PerfRawData_PerfOS_Memory--
    //Win32_PerfRawData_PerfOS_Objects--
    //Win32_PerfRawData_PerfOS_PagingFile--
    //Win32_PerfRawData_PerfOS_Processor--
    //Win32_PerfRawData_PerfOS_System--
    //Win32_PerfRawData_PerfProc_FullImage_Costly--
    //Win32_PerfRawData_PerfProc_Image_Costly--
    //Win32_PerfRawData_PerfProc_JobObject--
    //Win32_PerfRawData_PerfProc_JobObjectDetails--
    //Win32_PerfRawData_PerfProc_Process--Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly--Win32_PerfRawData_PerfProc_Thread--
    //Win32_PerfRawData_PerfProc_ThreadDetails_Costly--
    //Win32_PerfRawData_PSched_PSchedFlow--
    //Win32_PerfRawData_PSched_PSchedPipe--
    //Win32_PerfRawData_RemoteAccess_RASPort--
    //Win32_PerfRawData_RemoteAccess_RASTotal--
    //Win32_PerfRawData_RSVP_ACSRSVPInterfaces--
    //Win32_PerfRawData_RSVP_ACSRSVPService--
    //Win32_PerfRawData_SMTPSVC_SMTPServer--
    //Win32_PerfRawData_Spooler_PrintQueue--
    //Win32_PerfRawData_TapiSrv_Telephony--
    //Win32_PerfRawData_Tcpip_ICMP--
    //Win32_PerfRawData_Tcpip_IP--
    //Win32_PerfRawData_Tcpip_NBTConnection--
    //Win32_PerfRawData_Tcpip_NetworkInterface--
    //Win32_PerfRawData_Tcpip_TCP--
    //Win32_PerfRawData_Tcpip_UDP--
    //Win32_PerfRawData_TermService_TerminalServices--Win32_PerfRawData_TermService_TerminalServicesSession--Win32_PerfRawData_W3SVC_WebService
}
