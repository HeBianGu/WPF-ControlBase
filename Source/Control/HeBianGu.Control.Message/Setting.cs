// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.General.WpfControlLib;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace HeBianGu.Control.Message
{
    [Displayer(Name = "消息设置", GroupName = SystemSetting.GroupMessage)]
    public class MessageSetting : RefreshDefaultSettingInstance<MessageSetting>
    {
        public override void LoadDefault()
        {
            base.LoadDefault();

            {
                Style style = Application.Current.TryFindResource(ProgressBarKeys.Dynamic) as Style;
                StyleHost host = new StyleHost();
                host.Style = style;
                this.ProgressbarStyleHost = host;
            }

            //{
            //    Style style = Application.Current.TryFindResource(ModernProgressRing.OverridesDefaultStyleProperty) as Style;
            //    StyleHost host = new StyleHost();
            //    host.Style = style;
            //    this.WaittingStyleHost = host;
            //}

            //{
            //    Style style = Application.Current.TryFindResource(Snackbar.DataContextProperty) as Style;
            //    StyleHost host = new StyleHost();
            //    host.Style = style;
            //    this.SnackbarStyleHost = host;
            //}
        }
        private StyleHost _waittingStyleHost;
        /// <summary> 说明  </summary>
        [Display(Name = "等待样式")]
        [StyleSettingFilter(Filter = nameof(ModernProgressRing))]
        [PropertyItemType(Type = typeof(ViewStylesPropertyItem))]
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
        [PropertyItemType(Type = typeof(SnackbarStylesPropertyItem))]
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
        [PropertyItemType(Type = typeof(KeyStylesPropertyItem))]
        public StyleHost ProgressbarStyleHost
        {
            get { return _progressbarStyleHost; }
            set
            {
                _progressbarStyleHost = value;
                RaisePropertyChanged();
            }
        }

        private Thickness _objectConentDialgMargin;
        [Display(Name = "消息框边距")]
        [DefaultValue(typeof(Thickness), "5,5,5,5")]
        public Thickness ObjectConentDialgMargin
        {
            get { return _objectConentDialgMargin; }
            set
            {
                _objectConentDialgMargin = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "对话消息", GroupName = SystemSetting.GroupMessage)]
    public class ConentDialogSetting : RefreshDefaultSettingInstance<ConentDialogSetting>
    {
        private Thickness _margin;
        [Display(Name = "消息框边距")]
        [DefaultValue(typeof(Thickness), "20,20,20,20")]
        public Thickness Margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
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
