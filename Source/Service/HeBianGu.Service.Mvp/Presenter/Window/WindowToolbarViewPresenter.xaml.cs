// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowToolbarViewPresenter : IItemsViewPresenter
    {

    }

    public interface IWindowToolbarViewPresenterOption : IItemsSettingOption
    {
        double Height { get; set; }
    }

    [Displayer(Name = "工具栏", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class WindowToolbarViewPresenter : ItemsViewPresenterBase<WindowToolbarViewPresenter, IWindowToolbarViewPresenter>, IWindowToolbarViewPresenter, IWindowToolbarViewPresenterOption
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
