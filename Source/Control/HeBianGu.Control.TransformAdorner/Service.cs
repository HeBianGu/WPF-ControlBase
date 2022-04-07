// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Control.TransformAdorner
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [SettingConfig(Name = "参数设置", Group = "基本设置")]
    public class Setting : LazySettingInstance<Setting>
    {

    }
}
