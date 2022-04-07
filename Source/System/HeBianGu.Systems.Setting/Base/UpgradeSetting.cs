// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HeBianGu.Systems.Setting
{
    /// <summary> 软件更新</summary>
    [SettingConfig(Name = "软件更新", Group = "基本设置")]
    public class UpgradeSetting : Setting<UpgradeSetting>
    {
        private bool _automaticUpgrade;
        [DefaultValue(true)]
        [Display(Name = "有更新时自动为我安装(推荐)", Description = "当有更新时，智能选择最佳的软件更新方案，自动完成更新的下载与安装，省心省力")]
        public bool AutomaticUpgrade
        {
            get { return _automaticUpgrade; }
            set
            {
                _automaticUpgrade = value;
                RaisePropertyChanged();
            }
        }

        private bool _notifyUpgrade;
        [DefaultValue(false)]
        [Display(Name = "有更新时不要安装，但提醒我", Description = "此方式不影响安全补丁等重要更新的自动安装")]
        public bool NotifyUpgrade
        {
            get { return _notifyUpgrade; }
            set
            {
                _notifyUpgrade = value;
                RaisePropertyChanged();
            }
        }
    }
}
