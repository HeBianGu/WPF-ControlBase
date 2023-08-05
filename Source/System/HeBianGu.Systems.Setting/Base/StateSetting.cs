// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HeBianGu.Systems.Setting
{
    [Displayer(Name = "状态", GroupName = SystemSetting.GroupBase)]
    public class StateSetting : Setting<StateSetting>
    {
        private int _doNotingMinite;
        [DefaultValue(3)]
        [Display(Name = "鼠标键盘动作(分钟)")]
        public int DoNotingMinite
        {
            get { return _doNotingMinite; }
            set
            {
                _doNotingMinite = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabled;
        [DefaultValue(false)]
        [Display(Name = "是否启用")]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }

        private StateDoNotingMode _stateDoNotingMode;
        [DefaultValue(StateDoNotingMode.Lock)]
        [Display(Name = "鼠标键盘动作(分钟)")]
        public StateDoNotingMode StateDoNotingMode
        {
            get { return _stateDoNotingMode; }
            set
            {
                _stateDoNotingMode = value;
                RaisePropertyChanged();
            }
        }
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum StateDoNotingMode
    {
        [Display(Name = "自动锁定程序")]
        Lock = 0,
        [Display(Name = "使用屏保界面")]
        Screen,
        [Display(Name = "退出登录")]
        Logout,
        [Display(Name = "退出程序")]
        Exit
    }

}
