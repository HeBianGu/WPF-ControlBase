// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.Screen
{
    public class ScreenService : IScreenService
    {

    }

    public interface IScreenService
    {

    }

    [SettingConfig(Name = "参数设置", Group = "基本设置")]
    public class ScreenSetting : LazySettingInstance<ScreenSetting>
    {

    }
}
