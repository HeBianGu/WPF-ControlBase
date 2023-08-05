// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowSideEditViewPresenter : IViewPresenter
    {

    }

    public interface IWindowSideEditViewPresenterOption : IItemsSettingOption
    {

    }

    [Displayer(Name = "信息栏", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class WindowSideEditViewPresenter : ItemsViewPresenterBase<WindowSideEditViewPresenter, IWindowSideEditViewPresenter>, IWindowSideEditViewPresenter, IWindowSideEditViewPresenterOption
    {
        
    }

    public interface IPropertyGridViewPresenter : IViewPresenter
    {

    }

    public interface IPropertyViewViewPresenter : IViewPresenter
    {

    }
}
