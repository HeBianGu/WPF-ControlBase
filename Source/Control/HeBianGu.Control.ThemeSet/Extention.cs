// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Control.ThemeSet;
using HeBianGu.Service.Mvp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace System
{
    public static class Extention
    {
        /// <summary>
        /// 用本地保存的配置初始化主题，如果本地没有保存业务则使用默认主题
        /// </summary>
        /// <param name="builder"> 主程序构建对象 </param>
        /// <param name="useDefaultTheme">  默认主题 </param>
        /// <returns></returns>
        public static IApplicationBuilder UseLocalTheme(this IApplicationBuilder builder, Action<ThemeViewModel> useDefaultTheme, int version = 0)
        {
            ThemeConfig local = ThemeConfig.Instance;

            if ((!local.IsInit()) && local.Version == version)
            {
                local.Load();

                ////  Do：设置默认主题
                //builder.UseTheme(l =>
                //{
                //    ThemeViewModel.Current.LoadFrom(local); 
                //    ThemeViewModel.Current.Version = version;
                //});

                ThemeViewModel.Current.LoadFrom(local);
                ThemeViewModel.Current.Version = version;

                return builder;
            }

            useDefaultTheme?.Invoke(ThemeViewModel.Current);

            return builder;

        }

        [Obsolete("AddThemeRightViewPresenter")]
        public static void AddTheme(this IServiceCollection service, Action<IThemeViewPresenterOption> option = null)
        {
            service.AddSingleton<IThemeSaveService, ThemeSaveService>();
        }

        public static void AddThemeViewPresenter(this IServiceCollection service, Action<IThemeViewPresenterOption> option = null)
        {
            service.AddSingleton<IThemeViewPresenter, ThemeViewPresenter>();
            option?.Invoke(ThemeViewPresenter.Instance);
            SystemSetting.Instance?.Add(ThemeViewPresenter.Instance);
        }

        public static void AddThemeRightViewPresenter(this IServiceCollection service, Action<IThemeRightViewPresenterOption> option = null)
        {
            service.AddSingleton<IThemeRightViewPresenter, ThemeRightViewPresenter>();
            option?.Invoke(ThemeRightViewPresenter.Instance);
            SystemSetting.Instance?.Add(ThemeRightViewPresenter.Instance);

            service.AddSingleton<IThemeRightToolViewPresenter, ThemeRightToolViewPresenter>();
            //option?.Invoke(ThemeRightToolViewPresenter.Instance);
            //SystemSetting.Instance?.Add(ThemeRightToolViewPresenter.Instance);
        }

        //public static void AddThemeRightToolViewPresenter(this IServiceCollection service, Action<IThemeRightToolViewPresenterOption> option = null)
        //{
        //    service.AddSingleton<IThemeRightToolViewPresenter, ThemeRightToolViewPresenter>();
        //    option?.Invoke(ThemeRightToolViewPresenter.Instance);
        //    SystemSetting.Instance?.Add(ThemeRightToolViewPresenter.Instance);
        //}

        ///// <summary>
        ///// 设置主题
        ///// </summary>
        ///// <param name="builder"> 主程序构建对象 </param>
        ///// <param name="theme"> 主题 </param>
        ///// <returns></returns>
        //public static IApplicationBuilder UseTheme(this IApplicationBuilder builder, Action<ThemeSetting> setting)
        //{
        //    setting?.Invoke(ThemeSetting.Instance);

        //    SystemSetting.Instance?.Add(ThemeSetting.Instance);

        //    return builder;
        //}
    }

    //[Display(Name = "主题", GroupName = SystemSetting.GroupSystem)]
    //public class ThemeSetting : LazySettingInstance<ThemeSetting>
    //{
    //    private bool _useFontSize = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用字体大小配置")]
    //    public bool UseFontSize
    //    {
    //        get { return _useFontSize; }
    //        set
    //        {
    //            _useFontSize = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private bool _useBrushType = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用颜色类型配置")]
    //    public bool UseBrushType
    //    {
    //        get { return _useBrushType; }
    //        set
    //        {
    //            _useBrushType = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private bool _useFollowSystem = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用跟随系统配置")]
    //    public bool UseFollowSystem
    //    {
    //        get { return _useFollowSystem; }
    //        set
    //        {
    //            _useFollowSystem = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private bool _useDynamic = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用动态主题配置")]
    //    public bool UseDynamic
    //    {
    //        get { return _useDynamic; }
    //        set
    //        {
    //            _useDynamic = value;
    //            RaisePropertyChanged();
    //        }
    //    }


    //    private bool _useRowHeight = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用行高度配置")]
    //    public bool UseRowHeight
    //    {
    //        get { return _useRowHeight; }
    //        set
    //        {
    //            _useRowHeight = value;
    //            RaisePropertyChanged();
    //        }
    //    }

    //    private bool _useItemHeight = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用项高度配置")]
    //    public bool UseItemHeight
    //    {
    //        get { return _useItemHeight; }
    //        set
    //        {
    //            _useItemHeight = value;
    //            RaisePropertyChanged();
    //        }
    //    }



    //    private bool _useCorner = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用项圆角配置")]
    //    public bool UseCorner
    //    {
    //        get { return _useCorner; }
    //        set
    //        {
    //            _useCorner = value;
    //            RaisePropertyChanged();
    //        }
    //    }


    //    private bool _useCustomBrush = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用自定义色配置")]
    //    public bool UseCustomBrush
    //    {
    //        get { return _useCustomBrush; }
    //        set
    //        {
    //            _useCustomBrush = value;
    //            RaisePropertyChanged();
    //        }
    //    }


    //    private bool _useThemeType = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用应用主题配置")]
    //    public bool UseThemeType
    //    {
    //        get { return _useThemeType; }
    //        set
    //        {
    //            _useThemeType = value;
    //            RaisePropertyChanged();
    //        }
    //    }


    //    private bool _useSceneType = true;
    //    /// <summary> 说明  </summary>
    //    [DefaultValue(true)]
    //    [Display(Name = "启用应用场景配置")]
    //    public bool UseSceneType
    //    {
    //        get { return _useSceneType; }
    //        set
    //        {
    //            _useSceneType = value;
    //            RaisePropertyChanged();
    //        }
    //    }
    //}
}
