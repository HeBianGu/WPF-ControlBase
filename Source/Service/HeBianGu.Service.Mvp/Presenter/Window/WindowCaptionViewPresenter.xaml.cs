// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowCaptionViewPresenter : IItemsViewPresenter
    {

    }

    public class WindowCaptionvper : ServiceInstance<IWindowCaptionViewPresenter>
    {

    }

    public interface IWindowCaptionViewPresenterOption : IItemsSettingOption
    {

    }

    [Displayer(Name = "窗口操作栏", GroupName = SystemSetting.GroupSystem, Icon = Icons.Eye_slash, Description = "显示窗口操作栏工具")]
    public class WindowCaptionViewPresenter : ItemsViewPresenterBase<WindowCaptionViewPresenter, IWindowCaptionViewPresenter>, IWindowCaptionViewPresenter, IWindowCaptionViewPresenterOption
    {

    }
}
