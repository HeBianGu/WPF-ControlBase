// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HeBianGu.Service.Mvp
{
    /// <summary>
    /// 其他
    /// </summary>
    public interface IMoreViewPresenter : IViewPresenter
    {

    }

    public interface IMoreViewPresenterOption : IItemsSettingOption
    {

    }

    [Displayer(Name = "更多工具", GroupName = SystemSetting.GroupSystem, Icon = IconAll.List, Description = "点击显示其他功能")]
    public class MoreViewPresenter : ItemsViewPresenterBase<MoreViewPresenter, IMoreViewPresenter>, IMoreViewPresenter, IMoreViewPresenterOption
    {

    }
}


