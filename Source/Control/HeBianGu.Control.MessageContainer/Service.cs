// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Control.MessageContainer
{
    public class Service : IService
    {

    }

    public interface IService
    {

    }

    [SettingConfig(Name = "参数设置", Group = "基本设置")]
    public class Setting : LazySettingInstance<Setting>
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
