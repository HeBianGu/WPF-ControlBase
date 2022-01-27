using HeBianGu.Base.WpfBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.General.WpfControlLib
{ 
    [SettingConfig(Name = "样式设置", Group = "基本设置")]
    public class ButtonSetting : LazySettingInstance<ButtonSetting>
    {
        private EffectType _effectType;
        [Display(Name = "Effect")]
        public EffectType EffectType
        {
            get { return _effectType; }
            set
            {
                _effectType = value;
                RaisePropertyChanged();
            }
        }

        private double _minWidth=100;
        /// <summary> 说明  </summary>
        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                RaisePropertyChanged();
            }
        }
    }
}
