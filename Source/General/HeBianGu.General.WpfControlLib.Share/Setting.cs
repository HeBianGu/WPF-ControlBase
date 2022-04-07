// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
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

        private double _minWidth = 100;
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


        //private double? _mouseOverOpacity = 0.2;
        ///// <summary> 说明  </summary>
        //public double? MouseOverOpactiy
        //{
        //    get { return _mouseOverOpacity; }
        //    set
        //    {
        //        _mouseOverOpacity = value;
        //        RaisePropertyChanged();
        //    }
        //}

    }

    [SettingConfig(Name = "样式设置", Group = "基本设置")]
    public class ScrollViewerSetting : LazySettingInstance<ScrollViewerSetting>
    {
        private double _ScrollBarWidth = 10;
        /// <summary> 说明  </summary>
        public double ScrollBarWidth
        {
            get { return _ScrollBarWidth; }
            set
            {
                _ScrollBarWidth = value;
                RaisePropertyChanged();
            }
        }

        private double _ScrollBarHeight = 10;
        /// <summary> 说明  </summary>
        public double ScrollBarHeight
        {
            get { return _ScrollBarHeight; }
            set
            {
                _ScrollBarHeight = value;
                RaisePropertyChanged();
            }
        }
    }
}
