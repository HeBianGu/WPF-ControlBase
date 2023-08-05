using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HeBianGu.Systems.WinTool
{
    public interface IProcessViewPresenter
    {
        string FileName { get; set; }
    }

    public abstract class ProcessViewPresenter : InvokePresenterBase
    {
        private string _fileName;
        [Display(Name = "文件路径")]
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        public override bool Invoke(out string message)
        {
            message = null;
            //Process.Start(this.FileName);
            Process.Start(new ProcessStartInfo(this.FileName) { UseShellExecute = true });
            return true;
        }
    }

    [Displayer(Name = "我的电脑", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开我的电脑")]
    public class MyComputerViewPresenter : ProcessViewPresenter
    {
        public MyComputerViewPresenter()
        {
            this.FileName = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
        }
    }

    [Displayer(Name = "控制面板", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开控制面板")]
    public class ControlViewPresenter : ProcessViewPresenter
    {
        public ControlViewPresenter()
        {
            this.FileName = "control";
        }
    }

    [Displayer(Name = "回收站", GroupName = SystemSetting.GroupSystem, Icon ="\xe8d7", Description = "应用此功能可以回收站")]
    public class RecycleViewPresenter : ProcessViewPresenter
    {
        public RecycleViewPresenter()
        {
            this.FileName = "::{645FF040-5081-101B-9F08-00AA002F954E}";
        }
    }

    [Displayer(Name = "网上邻居", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开网上邻居")]
    public class NetNeiborhoodViewPresenter : ProcessViewPresenter
    {
        public NetNeiborhoodViewPresenter()
        {
            this.FileName = "::{645FF040-5081-101B-9F08-00AA002F954E}";
        }
    }

    [Displayer(Name = "定时关机", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以设置定时关机")]
    public class ShutdownViewPresenter : ProcessViewPresenter
    {
        public ShutdownViewPresenter()
        {
            this.FileName = "::{645FF040-5081-101B-9F08-00AA002F954E}";
        }
    }

    [Displayer(Name = "资源管理器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开资源管理器")]
    public class ExplorerViewPresenter : ProcessViewPresenter
    {
        public ExplorerViewPresenter()
        {
            this.FileName = "explorer";
        }
    }

    [Displayer(Name = "本机用户和组", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以房屋内本机用户和组")]
    public class UserGroupViewPresenter : ProcessViewPresenter
    {
        public UserGroupViewPresenter()
        {
            this.FileName = "lusrmgr.msc";
        }
    }

    [Displayer(Name = "记事本", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开记事本")]
    public class NotepadViewPresenter : ProcessViewPresenter
    {
        public NotepadViewPresenter()
        {
            this.FileName = "notepad";
        }
    }

    [Displayer(Name = "计算机管理器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以访问计算机管理器")]
    public class CompmgmtViewPresenter : ProcessViewPresenter
    {
        public CompmgmtViewPresenter()
        {
            this.FileName = "compmgmt.msc";
        }
    }

    [Displayer(Name = "磁盘管理器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以访问磁盘管理器")]
    public class DiskmgmtViewPresenter : ProcessViewPresenter
    {
        public DiskmgmtViewPresenter()
        {
            this.FileName = "diskmgmt.msc";
        }
    }

    [Displayer(Name = "计算器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开计算器")]
    public class CalcViewPresenter : ProcessViewPresenter
    {
        public CalcViewPresenter()
        {
            this.FileName = "calc";
        }
    }

    [Displayer(Name = "设备管理器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开设备管理器")]
    public class DevmgmtViewPresenter : ProcessViewPresenter
    {
        public DevmgmtViewPresenter()
        {
            this.FileName = "devmgmt.msc";
        }
    }

    [Displayer(Name = "立即关机", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以设置立即关机")]
    public class Shoutdown15ViewPresenter : ProcessViewPresenter
    {
        public Shoutdown15ViewPresenter()
        {
            this.FileName = "rononce -p";
        }
    }

    [Displayer(Name = "DirectX诊断工具", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以显示DirectX信息")]
    public class DirectXViewPresenter : ProcessViewPresenter
    {
        public DirectXViewPresenter()
        {
            this.FileName = "dxdiag";
        }
    }

    [Displayer(Name = "注册表", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以访问注册表")]
    public class Regedt32ViewPresenter : ProcessViewPresenter
    {
        public Regedt32ViewPresenter()
        {
            this.FileName = "regedt32";
        }
    }

    [Displayer(Name = "性能监测", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以方位性能检测")]
    public class PerfmonViewPresenter : ProcessViewPresenter
    {
        public PerfmonViewPresenter()
        {
            this.FileName = "perfmon.msc";
        }
    }

    [Displayer(Name = "Windows版本", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以查看Windows版本")]
    public class WinverViewPresenter : ProcessViewPresenter
    {
        public WinverViewPresenter()
        {
            this.FileName = "winver";
        }
    }

    [Displayer(Name = "任务管理器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以访问任务管理器")]
    public class TaskmgrViewPresenter : ProcessViewPresenter
    {
        public TaskmgrViewPresenter()
        {
            this.FileName = "taskmgr";
        }
    }

    [Displayer(Name = "写字板", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开写字板")]
    public class WriteViewPresenter : ProcessViewPresenter
    {
        public WriteViewPresenter()
        {
            this.FileName = "write";
        }
    }

    [Displayer(Name = "画图板", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开画图板")]
    public class MspaintViewPresenter : ProcessViewPresenter
    {
        public MspaintViewPresenter()
        {
            this.FileName = "mspaint";
        }
    }

    [Displayer(Name = "放大镜", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开放大镜")]
    public class MagnifyViewPresenter : ProcessViewPresenter
    {
        public MagnifyViewPresenter()
        {
            this.FileName = "magnify";
        }
    }

    [Displayer(Name = "键盘", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开系统键盘")]
    public class OskViewPresenter : ProcessViewPresenter
    {
        public OskViewPresenter()
        {
            this.FileName = "osk";
        }
    }

    [Displayer(Name = "事件查看器", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开事件查看器")]
    public class EventvwrViewPresenter : ProcessViewPresenter
    {
        public EventvwrViewPresenter()
        {
            this.FileName = "eventvwr";
        }
    }

    [Displayer(Name = "系统信息", GroupName = SystemSetting.GroupSystem, Icon = "\xe8d7", Description = "应用此功能可以打开事件查看器")]
    public class Msinfo32ViewPresenter : ProcessViewPresenter
    {
        public Msinfo32ViewPresenter()
        {
            this.FileName = "msinfo32";
        }
    }
}
