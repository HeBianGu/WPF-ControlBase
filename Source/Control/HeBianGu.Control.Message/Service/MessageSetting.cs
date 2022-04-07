// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace HeBianGu.Control.Message
{
    [SettingConfig(Name = "消息设置", Group = "基本设置")]
    public class MessageSetting : RefreshDefaultSettingInstance<MessageSetting>
    {
        private StyleHost _waittingStyleHost;
        /// <summary> 说明  </summary>
        [Display(Name = "等待样式")]
        [StyleSettingFilter(Filter = nameof(ModernProgressRing))]
        [Property(Type = typeof(ViewStylesPropertyItem))]
        public StyleHost WaittingStyleHost
        {
            get { return _waittingStyleHost; }
            set
            {
                _waittingStyleHost = value;
                RaisePropertyChanged();
            }
        }

        private StyleHost _snackbarStyleHost;
        /// <summary> 说明  </summary>
        [Display(Name = "提示样式")]
        [StyleSettingFilter(Filter = nameof(Snackbar))]
        [Property(Type = typeof(SnackbarStylesPropertyItem))]
        public StyleHost SnackbarStyleHost
        {
            get { return _snackbarStyleHost; }
            set
            {
                _snackbarStyleHost = value;
                RaisePropertyChanged();
            }
        }

        private StyleHost _progressbarStyleHost;
        /// <summary> 说明  </summary>
        [Display(Name = "进度样式")]
        [KeyStyleSettingFilter(KeyType = typeof(ProgressBarKeys))]
        [Property(Type = typeof(KeyStylesPropertyItem))]
        public StyleHost ProgressbarStyleHost
        {
            get { return _progressbarStyleHost; }
            set
            {
                _progressbarStyleHost = value;
                RaisePropertyChanged();
            }
        }
    }

    //public class WattingStylesPropertyItem : StylesPropertyItem
    //{
    //    public WattingStylesPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    //    {

    //    }
    //}

    public class SnackbarStylesPropertyItem : StylesPropertyItem
    {
        public SnackbarStylesPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }
    }
}
