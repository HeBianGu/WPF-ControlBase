// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HeBianGu.Control.MessageContainer
{
    [Displayer(Name = "蒙版设置", GroupName = SystemSetting.GroupMessage)]
    public class MessageContainerSetting : LazySettingInstance<MessageContainerSetting>
    {
        private int _hideDuration;
        [DefaultValue(5000)]
        [Display(Name = "自动隐藏间隔(s)")]
        public int HideDuration
        {
            get { return _hideDuration; }
            set
            {
                _hideDuration = value;
                RaisePropertyChanged();
            }
        }
    }
}
