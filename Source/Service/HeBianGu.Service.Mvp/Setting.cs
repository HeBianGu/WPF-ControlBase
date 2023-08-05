// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System
{
    [Displayer(Name = "MVP配置", GroupName = "开发者模式")]
    public class MvpSetting : LazySettingInstance<MvpSetting>
    {
        private bool _useIsEnabled;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "没有注册Presenter时设置不可用")]
        public bool UseIsEnabled
        {
            get { return _useIsEnabled; }
            set
            {
                _useIsEnabled = value;
                RaisePropertyChanged();
            }
        }


        private bool _useVisiblity;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "没有注册Presenter时设置隐藏")]
        public bool UseVisiblity
        {
            get { return _useVisiblity; }
            set
            {
                _useVisiblity = value;
                RaisePropertyChanged();
            }
        }

    }
}
