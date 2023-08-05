
using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;

namespace HeBianGu.Systems.WinTool
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [Displayer(Name = "参数设置", GroupName = SystemSetting.GroupBase)]
    public class Setting : LazySettingInstance<Setting>
    {

    }

  
}
