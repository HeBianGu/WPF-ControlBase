
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Systems.Notification
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
