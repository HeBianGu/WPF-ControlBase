// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    [Displayer(Name = "对话框", GroupName = SystemSetting.GroupControl)]
    public class DialogSetting : LazySettingInstance<DialogSetting>
    {
        private Thickness _DialogMargin;
        [Display(Name = "消息框对话框边距")]
        [DefaultValue(typeof(Thickness), "20,20,20,20")]
        public Thickness DialogMargin
        {
            get { return _DialogMargin; }
            set
            {
                _DialogMargin = value;
                RaisePropertyChanged();
            }
        }
    }
}
