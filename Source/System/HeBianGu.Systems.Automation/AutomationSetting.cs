// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System
{
    [Displayer(Name = "数据列表", GroupName = "系统配置")]
    public class AutomationSetting : LazySettingInstance<AutomationSetting>
    {
        private bool _useProperty = true;
        /// <summary> 说明  </summary>
        [Display(Name = "启用属性列表")]
        public bool UseProperty
        {
            get { return _useProperty; }
            set
            {
                _useProperty = value;
                RaisePropertyChanged();
            }
        }


        private bool _useLocator = true;
        [DefaultValue(true)]
        [Display(Name = "启用定位器")]
        public bool UseLocator
        {
            get { return _useLocator; }
            set
            {
                _useLocator = value;
                RaisePropertyChanged();
            }
        }


        private bool _useToolBox = true;
        [DefaultValue(true)]
        [Display(Name = "启用工具箱")]
        public bool UseToolBox
        {
            get { return _useToolBox; }
            set
            {
                _useToolBox = value;
                RaisePropertyChanged();
            }
        }
    }

    //[TypeConverter(typeof(DisplayEnumConverter))]
    //public enum InteropMode
    //{
    //    [Display(Name = "注入仓储")]
    //    IRepository = 0,
    //    [Display(Name = "自定义行为")]
    //    CustomAction,
    //    [Display(Name = "注入互操作")]
    //    IInterop
    //}

}
