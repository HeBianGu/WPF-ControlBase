// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Window.Ribbon
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [Displayer(Name = "参数设置", GroupName = SystemSetting.GroupStyle)]
    public class Setting : LazySettingInstance<Setting>
    {

    }
}
