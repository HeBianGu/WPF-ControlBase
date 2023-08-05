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

    [Displayer(Name = "参数设置", GroupName = SystemSetting.GroupStyle)]
    public class ScreenSetting : LazySettingInstance<ScreenSetting>
    {

    }
}
