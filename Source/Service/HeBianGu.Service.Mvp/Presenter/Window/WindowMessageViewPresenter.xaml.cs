// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Service.Mvp
{
    /// <summary>
    /// 信息栏
    /// </summary>
    public interface IWindowMessageViewPresenter : IItemsViewPresenter
    {

    }

    public interface IWindowMessageViewPresenterOption : IItemsSettingOption
    {
        double Height { get; set; }

        bool UseToolInStatus { get; set; }

        bool UseTool { get; set; }
    }

    [Displayer(Name = "信息栏", GroupName = SystemSetting.GroupMessage, Icon = IconAll.Menu, Description = "点击显示其他功能")]
    public class WindowMessageViewPresenter : ItemsViewPresenterBase<WindowMessageViewPresenter, IWindowMessageViewPresenter>, IWindowMessageViewPresenter, IWindowMessageViewPresenterOption
    {
        public override void Load()
        {
            base.Load();
            this.IsVisible = false;
        }
        private double _height;
        [DefaultValue(150.0)]
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

        private bool _useToolInStatus;
        [DefaultValue(true)]
        [Display(Name = "启用状态栏控制按钮")]
        public bool UseToolInStatus
        {
            get { return _useToolInStatus; }
            set
            {
                _useToolInStatus = value;
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



        public virtual bool Invoke(out string message)
        {
            message = null;
            return true;
        }
    }

    public interface IWindowMessageToolViewPresenter : ITogglePresenter
    {

    }

    public interface IWindowMessageToolViewPresenterOption : IMvpSettingOption
    {

    }

    [Displayer(Name = "信息栏控制按钮", GroupName = SystemSetting.GroupSystem, Icon = "\xe8ef", Description = "显示和隐藏消息列表")]
    public class WindowMessageToolViewPresenter : TogglePresenterBase<WindowMessageToolViewPresenter, IWindowMessageToolViewPresenter>, IWindowMessageToolViewPresenter
    {
        public WindowMessageToolViewPresenter()
        {
            Dock = System.Windows.Controls.Dock.Right;
        }
        public override bool Invoke(bool value, out string message)
        {
            message = null;
            return true;
        }
    }
}
