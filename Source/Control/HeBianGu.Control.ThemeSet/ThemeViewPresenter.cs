// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Control.ThemeSet
{
    public interface IThemeViewPresenterOption : IMvpSettingOption
    {
        bool UseBrushType { get; set; }
        bool UseCorner { get; set; }
        bool UseCustomBrush { get; set; }
        bool UseDynamic { get; set; }
        bool UseFollowSystem { get; set; }
        bool UseFontSize { get; set; }
        bool UseItemHeight { get; set; }
        bool UseRowHeight { get; set; }
        bool UseSceneType { get; set; }
        bool UseThemeType { get; set; }
    }

    public interface IThemeViewPresenter : IInvokePresenter
    {

    }

    [Displayer(Name = "主题设置", GroupName = SystemSetting.GroupSystem, Icon = IconAll.Theme, Description = "点击设置软件主题")]
    public class ThemeViewPresenter : ServiceMvpSettingBase<ThemeViewPresenter, IThemeViewPresenter>, IThemeViewPresenter, IThemeViewPresenterOption
    {
        private bool _useFontSize = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用字体大小配置")]
        public bool UseFontSize
        {
            get { return _useFontSize; }
            set
            {
                _useFontSize = value;
                RaisePropertyChanged();
            }
        }

        private bool _useBrushType = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用颜色类型配置")]
        public bool UseBrushType
        {
            get { return _useBrushType; }
            set
            {
                _useBrushType = value;
                RaisePropertyChanged();
            }
        }

        private bool _useFollowSystem = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用跟随系统配置")]
        public bool UseFollowSystem
        {
            get { return _useFollowSystem; }
            set
            {
                _useFollowSystem = value;
                RaisePropertyChanged();
            }
        }

        private bool _useDynamic = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用动态主题配置")]
        public bool UseDynamic
        {
            get { return _useDynamic; }
            set
            {
                _useDynamic = value;
                RaisePropertyChanged();
            }
        }


        private bool _useRowHeight = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用行高度配置")]
        public bool UseRowHeight
        {
            get { return _useRowHeight; }
            set
            {
                _useRowHeight = value;
                RaisePropertyChanged();
            }
        }

        private bool _useItemHeight = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用项高度配置")]
        public bool UseItemHeight
        {
            get { return _useItemHeight; }
            set
            {
                _useItemHeight = value;
                RaisePropertyChanged();
            }
        }



        private bool _useCorner = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用项圆角配置")]
        public bool UseCorner
        {
            get { return _useCorner; }
            set
            {
                _useCorner = value;
                RaisePropertyChanged();
            }
        }

        private bool _useCustomBrush = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用自定义色配置")]
        public bool UseCustomBrush
        {
            get { return _useCustomBrush; }
            set
            {
                _useCustomBrush = value;
                RaisePropertyChanged();
            }
        }

        private bool _useThemeType = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用应用主题配置")]
        public bool UseThemeType
        {
            get { return _useThemeType; }
            set
            {
                _useThemeType = value;
                RaisePropertyChanged();
            }
        }

        private bool _useThemeStyle = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用应用主题风格")]
        public bool UseThemeStyle
        {
            get { return _useThemeStyle; }
            set
            {
                _useThemeStyle = value;
                RaisePropertyChanged();
            }
        }

        private bool _useSceneType = true;
        /// <summary> 说明  </summary>
        [DefaultValue(true)]
        [Display(Name = "启用应用场景配置")]
        public bool UseSceneType
        {
            get { return _useSceneType; }
            set
            {
                _useSceneType = value;
                RaisePropertyChanged();
            }
        }
    }
}
