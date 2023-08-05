// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.Mvp
{
    public interface IWindowSideMenuPresenter : IViewPresenter
    {

    }

    public interface IWindowSideMenuPresenterOption : ISettingOption
    {
        double Width { get; set; }
        bool UseTool { get; set; }
    }

    [Displayer(Name = "侧栏菜单", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Menu2, Description = "点击显示其他功能")]
    public class WindowSideMenuPresenter : TreeViewPresenterBase<WindowSideMenuPresenter, IWindowSideMenuPresenter>, IWindowSideMenuPresenter, IWindowSideMenuPresenterOption
    {
        private double _width;
        /// <summary> 说明  </summary>
        [DefaultValue(250.0)]
        [Display(Name = "宽度")]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }

        private bool _useTool;
        [DefaultValue(true)]
        [Display(Name = "启用控制按钮")]
        public bool UseTool
        {
            get { return _useTool; }
            set
            {
                _useTool = value;
                RaisePropertyChanged();
            }
        }
    }
}
