// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Service.Mvp
{


    public interface IWindowMenuViewPresenter : IViewPresenter
    {

    }

    public interface IWindowMenuViewPresenterOption : ITreeSettingOption
    {

    }

    [Displayer(Name = "菜单栏", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class WindowMenuViewPresenter : TreeViewPresenterBase<WindowMenuViewPresenter, IWindowMenuViewPresenter>, IWindowMenuViewPresenter, IWindowMenuViewPresenterOption
    {

    }
}
