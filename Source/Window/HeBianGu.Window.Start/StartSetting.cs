// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System
{
    [SettingConfig(Name = "启动页面", Group = "基本设置")]
    public class StartSetting : LazySettingInstance<StartSetting>
    {
        private string _ImagePath;

        [DefaultValue(@"/HeBianGu.General.WpfControlLib;component/Resources/logo.png")]
        [Display(Name = "图片路径")]
        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                _ImagePath = value;
                RaisePropertyChanged();
            }
        }

        private string _title;
        [Required]
        [Display(Name = "产品标题")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }


        private string _cpyright;
        [Required]
        [Display(Name = "版权")]
        public string Copyright
        {
            get { return _cpyright; }
            set
            {
                _cpyright = value;
                RaisePropertyChanged();
            }
        }

        private int _titleFontSize = 80;
        [DefaultValue(true)]
        [Display(Name = "产品标题字体大小")]
        [Range(10, 100)]
        public int TitleFontSize
        {
            get { return _titleFontSize; }
            set
            {
                _titleFontSize = value;
                RaisePropertyChanged();
            }
        }


        private WindowType _type;
        [DefaultValue(WindowType.Message)]
        [Display(Name = "启动窗口类型")]
        public WindowType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged();
            }
        }

        private int _sleepmlliseconds = 1000;
        [DefaultValue(true)]
        [Display(Name = "启动页面延时")]
        [Range(0, 10 * 1000)]
        public int SleepMilliseconds
        {
            get { return _sleepmlliseconds; }
            set
            {
                _sleepmlliseconds = value;
                RaisePropertyChanged();
            }
        }
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum WindowType
    {
        [Display(Name = "图片样式")]
        Logo = 0,
        [Display(Name = "消息样式")]
        Message
    }
}
