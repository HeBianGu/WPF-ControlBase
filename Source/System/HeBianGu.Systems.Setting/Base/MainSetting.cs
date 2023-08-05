// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Systems.Setting
{
    /// <summary> 主面板 </summary>
    [Displayer(Name = "主面板", GroupName = SystemSetting.GroupBase)]
    public class MainSetting : Setting<MainSetting>
    {

        private bool _isShowNotify;
        [DefaultValue(true)]
        [Display(Name = "在任务通知区域显示图标")]
        public bool IsShowNotify
        {
            get { return _isShowNotify; }
            set
            {
                _isShowNotify = value;
                RaisePropertyChanged();
            }
        }

        private bool _isShowThemeSet;
        [DefaultValue(true)]
        [Display(Name = "在主窗口显示主题设计")]
        public bool IsShowThemeSet
        {
            get { return _isShowThemeSet; }
            set
            {
                _isShowThemeSet = value;
                RaisePropertyChanged();
            }
        }

        private MainCloseMode _mainCloseMode;
        [DefaultValue(MainCloseMode.Exit)]
        [Display(Name = "关闭主面板时：")]
        public MainCloseMode MainCloseMode
        {
            get { return _mainCloseMode; }
            set
            {
                _mainCloseMode = value;
                RaisePropertyChanged();
            }
        }


    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum MainCloseMode
    {
        [Display(Name = "隐藏到任务栏通知区域，不退出程序")]
        Hide = 0,
        [Display(Name = "退出程序")]
        Exit
    }
}
