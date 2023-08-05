// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace HeBianGu.Service.Mvp
{
    public interface IHomeViewPresenter : ITreeViewItemPresenter
    {

    }

    public interface IHomeViewPresenterOption : IItemsSettingOption
    {
        string PredefinePath { get; set; }
    }

    [Displayer(Name = "分析与报告", GroupName = SystemSetting.GroupData, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class HomeViewPresenter : ItemsViewPresenterBase<HomeViewPresenter, IHomeViewPresenter>, IHomeViewPresenter, IHomeViewPresenterOption
    {
        public HomeViewPresenter()
        {
            this.PredefinePath = this.GroupName;
        }
        [Browsable(false)]
        public string PredefinePath { get; set; }

        public override bool Invoke(out string message, object param)
        {
            message = null;
            MessageProxy.Presenter.Show(this, null, this.Name, x =>
            {
                x.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                //x.MinWidth = 600;
                //x.MinHeight = 400;
                //x.Margin = new Thickness(10);
                //x.HorizontalAlignment = HorizontalAlignment.Stretch;
                //x.VerticalAlignment = VerticalAlignment.Stretch;
            });
            return true;
        }
    }

    public class HomeViewPresenterProxy : ServiceInstance<IHomeViewPresenter>
    {

    }
}
