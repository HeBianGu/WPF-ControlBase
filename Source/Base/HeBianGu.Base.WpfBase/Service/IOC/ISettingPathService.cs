// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    public interface ISettingPathService
    {
        string GetSetting();
        string GetConfigExtention();
    }

    public class SettingPath : ServiceInstance<ISettingPathService>
    {

    }
}