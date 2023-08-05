// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.Mvp
{
    /// <summary>
    /// 状态栏
    /// </summary>
    public interface IWindowStatusViewPresenter : IItemsViewPresenter
    {

    }

    public interface IWindowStatusViewPresenterOption : IItemsSettingOption
    {
        double Height { get; set; }
    }

    [Displayer(Name = "状态栏", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class WindowStatusViewPresenter : ItemsViewPresenterBase<WindowStatusViewPresenter, IWindowStatusViewPresenter>, IWindowStatusViewPresenter, IWindowStatusViewPresenterOption
    {
        private double _height;
        [DefaultValue(35.0)]
        [Display(Name = "高度")]
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged();
            }
        }

    }
}
