// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.Service.TypeConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace HeBianGu.Control.ThemeSet
{


    /// <summary> 主题颜色管理器 </summary>
    public class ThemeViewModel : NotifyPropertyChanged
    {
        private ThemeViewModel()
        {
            _timer.Elapsed += (l, k) =>
            {
                Application.Current?.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                 {
                     this.AccentColor = this.SelectColorSource.Colors[_random.Next(this.SelectColorSource.Colors.Count)];
                 }));
            };


            #region - 颜色 -

            this.ColorSource.Add(new AccentColorSource() { DispalyName = "适中", Colors = ColorSourceFactory.Create(0.6).ToObservable() });
            this.ColorSource.Add(new AccentColorSource() { DispalyName = "浅色", Colors = ColorSourceFactory.Create(0.8).ToObservable() });
            this.ColorSource.Add(new AccentColorSource() { DispalyName = "深色", Colors = ColorSourceFactory.Create(0.3).ToObservable() });
            this.ColorSource.Add(new AccentColorSource() { DispalyName = "高亮", Colors = ColorSourceFactory.Create(1.0).ToObservable() });
            this.SelectColorSource = this.ColorSource?.FirstOrDefault();

            #endregion

            #region - 主题 - 

            var values = Enum.GetValues(typeof(ThemeType));

            foreach (var value in values)
            {
                string name = Enum.GetName(typeof(ThemeType), value);
                if (name == null)
                    continue;
                var field = typeof(ThemeType).GetField(name);
                var display = field.GetCustomAttribute<DisplayAttribute>();
                ColorLink colorLink = new ColorLink();
                colorLink.ThemeType = (ThemeType)value;
                colorLink.DisplayName = display.Name;
                colorLink.Source = new Uri($"/HeBianGu.Base.WpfBase;component/Themes/Color/{display.ShortName}.xaml", UriKind.Relative);
                this.ColorLinks.Add(colorLink);
            }

            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Light, DisplayName = "浅色调", Source = ThemeViewModel.LightThemeSource, });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Dark, DisplayName = "深色调", Source = ThemeViewModel.DarkThemeSource });

            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.DarkGray, DisplayName = "深(灰)色调", Source = ThemeViewModel.DarkGrayThemeSource });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.DarkBlue, DisplayName = "深(蓝)色调", Source = ThemeViewModel.DarkBlueThemeSource });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.DarkPurple, DisplayName = "深(紫)色调", Source = ThemeViewModel.DarkPurpleThemeSource });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.DarkBlack, DisplayName = "深(黑)色调", Source = ThemeViewModel.DarkBlackThemeSource });

            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.LightGray, DisplayName = "浅(灰)色调", Source = ThemeViewModel.LightGrayThemeSource });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.LightBlue, DisplayName = "浅(蓝)色调", Source = ThemeViewModel.LightBlueThemeSource });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.LightPurple, DisplayName = "浅(紫)色调", Source = ThemeViewModel.LightPurpleThemeSource });

            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.TranparentLight, DisplayName = "浅(透明)色调", Source = ThemeViewModel.TranparentLightThemeSource, });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.TranparentDark, DisplayName = "深(透明)色调", Source = ThemeViewModel.TranparentDarkThemeSource, });
            //this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Accent, DisplayName = "主色调", Source = ThemeViewModel.AccentThemeSource);

            this.SelectedColorLink = this.ColorLinks?.FirstOrDefault();
            #endregion

        }

        #region - 依赖属性 -

        private AccentColorSource _selectColorSource;
        /// <summary> 选中的颜色集合  </summary>
        public AccentColorSource SelectColorSource
        {
            get { return _selectColorSource; }
            set
            {
                _selectColorSource = value;
                RaisePropertyChanged("SelectColorSource");
            }
        }

        private ObservableCollection<ColorLink> _colorLink = new ObservableCollection<ColorLink>();
        /// <summary> 颜色集合  </summary>
        public ObservableCollection<ColorLink> ColorLinks
        {
            get { return _colorLink; }
            set
            {
                _colorLink = value;
                RaisePropertyChanged("ColorLinks");
            }
        }

        private ColorLink _selectedColorLink;
        /// <summary> 说明  </summary>
        public ColorLink SelectedColorLink
        {
            get { return _selectedColorLink; }
            set
            {
                _selectedColorLink = value;
                RaisePropertyChanged("SelectedColorLink");
            }
        }

        private double _defaultFontSize = 15;
        /// <summary> 说明  </summary>
        public double DefaultFontSize
        {
            get { return _defaultFontSize; }
            set
            {
                _defaultFontSize = value;
                RaisePropertyChanged();
            }
        }


        public double CurrentFontSize
        {
            get
            {
                return (double)Application.Current.TryFindResource(FontSizeKeys.Default);
            }
            set
            {
                Application.Current.Resources[FontSizeKeys.Default] = value;
                RaisePropertyChanged();
            }
        }

        //public double SmallFontSize
        //{
        //    get
        //    {
        //        return (double)Application.Current.TryFindResource(FontSizeKeys.Small);
        //    }
        //    set
        //    {
        //        Application.Current.Resources[FontSizeKeys.Small] = value;

        //        RaisePropertyChanged("SmallFontSize");
        //    }
        //} 

        //public double LargeFontSize
        //{
        //    get
        //    {
        //        return (double)Application.Current.TryFindResource(FontSizeKeys.Large);
        //    }
        //    set
        //    {
        //        Application.Current.Resources[FontSizeKeys.Large] = value;

        //        RaisePropertyChanged("LargeFontSize");
        //    }
        //}

        FontSize _fontSize = FontSize.Normal;
        /// <summary> 字体大小 </summary>
        public FontSize FontSize
        {
            get
            {
                return _fontSize;
                //double? defaultFontSize = Application.Current.TryFindResource(FontSizeKeys.Default) as double?;
                //if (defaultFontSize.HasValue)
                //{
                //    return defaultFontSize.Value == SmallFontSize ? FontSize.Small : FontSize.Large;
                //}
                //return FontSize.Small;
            }
            set
            {
                //Application.Current.Resources[FontSizeKeys.Default] = value == FontSize.Small ? this.SmallFontSize : this.LargeFontSize;
                //Application.Current.Resources[FontSizeKeys.Fixed] = value == FontSize.Small ? this.SmallFontSize - 1.5 : this.LargeFontSize - 1.5;
                _fontSize = value;
                this.CurrentFontSize = this.DefaultFontSize * ((int)this.FontSize / 10.0);
                RaisePropertyChanged();
            }
        }

        private AccentBrushType _accentBrushType = AccentBrushType.LinearGradientBrush;
        /// <summary> 主题颜色刷类型  </summary>
        public AccentBrushType AccentBrushType
        {
            get { return _accentBrushType; }
            set
            {
                _accentBrushType = value;
                RaisePropertyChanged("AccentBrushType");
                this.RefreshAccent();
            }
        }

        /// <summary> 主色调 </summary>
        public Color AccentColor
        {
            get
            {
                Color? accentColor = Application.Current.TryFindResource(SystemKeys.ColorAccent) as Color?;
                if (accentColor.HasValue)
                {
                    return accentColor.Value;
                }

                return Color.FromArgb(0xff, 0x1b, 0xa1, 0xe2);
            }
            set
            {
                //Application.Current.Resources["AccentColor"] = value;
                //Application.Current.Resources["Accent"] = new SolidColorBrush(value);
                Application.Current.Resources[SystemKeys.ColorAccent] = value;
                RaisePropertyChanged("AccentColor");
                this.RefreshAccent();
            }
        }

        private void RefreshAccent()
        {
            Brush find = new SolidColorBrush(this.AccentColor);

            if (this.AccentBrushType == AccentBrushType.LinearGradientBrush)
            {
                LinearGradientBrush radial = new LinearGradientBrush();
                radial.StartPoint = new Point(-3, 0);
                radial.EndPoint = new Point(1, 0);
                radial.GradientStops.Add(new GradientStop(Colors.White, 0.0));
                radial.GradientStops.Add(new GradientStop(this.AccentColor, 1.0));

                find = radial;
            }
            else if (this.AccentBrushType == AccentBrushType.LinearGradientBrushReverse)
            {
                LinearGradientBrush radial = new LinearGradientBrush();
                radial.StartPoint = new Point(0, 0);
                radial.EndPoint = new Point(4, 0);
                radial.GradientStops.Add(new GradientStop(Colors.White, 1.0));
                radial.GradientStops.Add(new GradientStop(this.AccentColor, 0.0));

                find = radial;
            }
            else if (this.AccentBrushType == AccentBrushType.RadialGradientBrush)
            {
                RadialGradientBrush radial = new RadialGradientBrush();
                radial.Center = new Point(0.5, 0.5);
                radial.GradientStops.Add(new GradientStop(Colors.White, -3.0));
                radial.GradientStops.Add(new GradientStop(this.AccentColor, 1.0));

                find = radial;
            }
            else
            {
                RadialGradientBrush radial = new RadialGradientBrush();
                radial.Center = new Point(0.5, 0.5);
                radial.GradientStops.Add(new GradientStop(Colors.White, 8.0));
                radial.GradientStops.Add(new GradientStop(this.AccentColor, 0.0));
            }

            //Application.Current.Resources["S.Brush.Accent"] = find;

            Application.Current.Resources[BrushKeys.Accent] = find;

            IEnumerable<PropertyInfo> ps = typeof(BrushKeys).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(l => l.GetCustomAttribute<AccentKeyAttribute>() != null);

            foreach (PropertyInfo p in ps)
            {
                object value = p.GetValue(null);

                object findSolid = Application.Current.Resources[value];

                if (findSolid is SolidColorBrush solid)
                {
                    Application.Current.Resources[value] = new SolidColorBrush() { Color = this.AccentColor, Opacity = solid.Opacity };
                }
            }

        }

        public double ItemHeight
        {
            set
            {
                Application.Current.Resources[SystemKeys.ItemHeight] = value;

                Application.Current.Resources[CornerRadiusKeys.Circle] = new CornerRadius(value / 2);
                Application.Current.Resources[CornerRadiusKeys.CircleLeft] = new CornerRadius(value / 2, 0, 0, value / 2);
                Application.Current.Resources[CornerRadiusKeys.CircleRight] = new CornerRadius(0, value / 2, value / 2, 0);
                Application.Current.Resources[CornerRadiusKeys.CircleTop] = new CornerRadius(value / 2, value / 2, 0, 0);
                Application.Current.Resources[CornerRadiusKeys.CircleBottom] = new CornerRadius(0, 0, value / 2, value / 2);


                RaisePropertyChanged("ItemHeight");
            }
            get
            {
                return (double)Application.Current.TryFindResource(SystemKeys.ItemHeight);
            }
        }

        public double ItemWidth
        {
            set
            {
                Application.Current.Resources[SystemKeys.ItemWidth] = value;
                RaisePropertyChanged("ItemWidth");
            }
            get
            {
                return (double)Application.Current.TryFindResource(SystemKeys.ItemWidth);
            }
        }

        public double ItemCornerRadius
        {
            set
            {
                Application.Current.Resources[CornerRadiusKeys.Value] = value;

                Application.Current.Resources[CornerRadiusKeys.CornerRadius] = new CornerRadius(value, value, value, value);

                Application.Current.Resources[CornerRadiusKeys.Left] = new CornerRadius(value, 0, 0, value);
                Application.Current.Resources[CornerRadiusKeys.Right] = new CornerRadius(0, value, value, 0);
                Application.Current.Resources[CornerRadiusKeys.Top] = new CornerRadius(value, value, 0, 0);
                Application.Current.Resources[CornerRadiusKeys.Bottom] = new CornerRadius(0, 0, value, value);

                Application.Current.Resources[CornerRadiusKeys.LeftTop] = new CornerRadius(value, 0, 0, 0);
                Application.Current.Resources[CornerRadiusKeys.RightTop] = new CornerRadius(0, value, 0, 0);
                Application.Current.Resources[CornerRadiusKeys.LeftBottom] = new CornerRadius(0, 0, value, 0);
                Application.Current.Resources[CornerRadiusKeys.RightBottom] = new CornerRadius(0, 0, 0, value);

                RaisePropertyChanged("ItemCornerRadius");
            }
            get
            {
                return (double)Application.Current.TryFindResource(CornerRadiusKeys.Value);
            }
        }

        public double RowHeight
        {
            set
            {
                Application.Current.Resources[SystemKeys.RowHeight] = value;
                RaisePropertyChanged("RowHeight");
            }
            get
            {
                return (double)Application.Current.TryFindResource(SystemKeys.RowHeight);
            }
        }


        private bool _isFollowSystemSysteme;

        public bool IsFollowSystemSystem
        {
            get { return _isFollowSystemSysteme; }
            set
            {
                _isFollowSystemSysteme = value;

                if (value == true)
                {
                    ThemeViewModel.Current.AccentColor = SystemColors.DesktopColor;
                }
                else
                {
                    ThemeViewModel.Current.AccentColor = this.SelectColorSource.Colors.FirstOrDefault();
                }

                RaisePropertyChanged("IsFollowSystemSystem");
            }
        }

        private Language _language;
        /// <summary> 设置语言 </summary>
        public Language Language
        {
            get
            {
                //XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

                //if (provider.Source == LanguageChineseSource)
                //{
                //    return Language.Chinese;
                //}
                //else if (provider.Source == LanguageEnglishSource)
                //{
                //    return Language.English;
                //}

                //return Language.Chinese;

                return _language;
            }
            set
            {
                //XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

                //if (value == Language.Chinese)
                //{
                //    provider.Source = LanguageChineseSource;
                //}
                //else if (value == Language.English)
                //{
                //    provider.Source = LanguageEnglishSource;
                //}
            }
        }

        public StyleType StyleType
        {
            get
            {
                return StyleSetting.Instance.StyleType;
            }
            set
            {
                StyleSetting.Instance.StyleType = value;
                RaisePropertyChanged("StyleType");
            }
        }

        private ObservableCollection<AccentColorSource> _colorSource = new ObservableCollection<AccentColorSource>();
        /// <summary> 颜色选择类型  </summary>
        public ObservableCollection<AccentColorSource> ColorSource
        {
            get { return _colorSource; }
            set
            {
                _colorSource = value;
                RaisePropertyChanged("ColorSource");
            }
        }

        public ThemeType ThemeType
        {
            get
            {
                return this.SelectedColorLink?.ThemeType ?? ThemeType.Light;
            }
            set
            {

                ColorLink find = this.ColorLinks.FirstOrDefault(l => l.ThemeType == value);
                ResourceDictionary old = Application.Current.Resources.MergedDictionaries.FirstOrDefault(l => l.Source == this.SelectedColorLink.Source);
                Collection<ResourceDictionary> dictionaries = Application.Current.Resources.MergedDictionaries;
                ResourceDictionary add = new ResourceDictionary { Source = find.Source }; ;
                dictionaries.Add(add);
                if (old != null)
                {
                    dictionaries.Remove(old);
                }

                this.SelectedColorLink = find;
                RaisePropertyChanged("ThemeType");
            }
        }


        private bool _isUseAnimal;

        public bool IsUseAnimal
        {

            get
            {
                return _isUseAnimal;
            }
            set
            {
                if (_isUseAnimal == value) return;
                _isUseAnimal = value;
                if (value)
                {
                    this.StartAnimationTheme(AnimalSpeed);
                }
                else
                {
                    this.StopAnimationTheme();
                }
            }
        }


        public int AnimalSpeed
        {
            get
            {
                return (int)_timer.Interval;
            }
            set
            {
                _timer.Interval = value;
            }
        }


        public int AccentColorSelectType
        {
            get
            {
                return this.ColorSource.IndexOf(this.SelectColorSource);
            }
            set
            {
                this.SelectColorSource = this.ColorSource[value];
            }
        }


        public SolidColorBrush DialogCoverBrush
        {
            set
            {
                Application.Current.Resources[BrushKeys.DialogCover] = value;
            }
            get
            {
                return (SolidColorBrush)Application.Current.TryFindResource(BrushKeys.DialogCover);
            }
        }

        #endregion
        //public static readonly Uri DarkThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Dark.xaml", UriKind.Relative);
        //public static readonly Uri LightThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Light.xaml", UriKind.Relative);
        //public static readonly Uri TranparentDarkThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/TranparentDark.xaml", UriKind.Relative);
        //public static readonly Uri TranparentLightThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/TranparentLight.xaml", UriKind.Relative);
        //public static readonly Uri DarkGrayThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Drak.Gray.xaml", UriKind.Relative);
        //public static readonly Uri AccentThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Accent.xaml", UriKind.Relative);
        //public static readonly Uri DarkBlueThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Dark.Blue.xaml", UriKind.Relative);
        //public static readonly Uri DarkPurpleThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Dark.Purple.xaml", UriKind.Relative);
        //public static readonly Uri DarkBlackThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Dark.Black.xaml", UriKind.Relative);

        //public static readonly Uri LightGrayThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Light.Gray.xaml", UriKind.Relative);
        //public static readonly Uri LightBlueThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Light.Blue.xaml", UriKind.Relative);
        //public static readonly Uri LightPurpleThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Themes/Color/Light.Purple.xaml", UriKind.Relative);

        public static ThemeViewModel Current = new ThemeViewModel();
        private static readonly Uri LanguageChineseSource = new Uri("/HeBianGu.Base.WpfBase;component/Resources/zh-cn.xml", UriKind.RelativeOrAbsolute);
        private static readonly Uri LanguageEnglishSource = new Uri("/HeBianGu.Base.WpfBase;component/Resources/en-us.xml", UriKind.RelativeOrAbsolute);

        //private ResourceDictionary GetThemeDictionary()
        //{
        //    var uri = this.SelectedColorLink.Source;

        //    var find = Application.Current.Resources.MergedDictionaries.FirstOrDefault(l => l.Source == uri);

        //    if (find == null)
        //    {
        //        resource.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        //    }
        //}

        #region - 设置随机播放颜色 -

        private Timer _timer = new Timer(1000);
        private Random _random = new Random();

        private void StartAnimationTheme(int timespan = 5000)
        {
            _timer.Interval = timespan;

            _timer.Start();

        }

        private void StopAnimationTheme()
        {
            _timer.Stop();
        }

        #endregion

        public ThemeConfig SaveTo()
        {
            ThemeConfig themeLocalize = new ThemeConfig();
            PropertyInfo[] ps = this.GetType().GetProperties();
            foreach (PropertyInfo p in ps)
            {
                PropertyInfo find = typeof(ThemeConfig).GetProperty(p.Name);
                if (find == null)
                    continue;
                if (!find.CanWrite)
                    continue;
                find.SetValue(themeLocalize, p.GetValue(this));
            }
            return themeLocalize;
        }

        public void LoadFrom(ThemeConfig config)
        {
            this.AccentColor = config.AccentColor == default(Color) ? this.AccentColor : config.AccentColor;
            //this.SmallFontSize = config.SmallFontSize == default(double) ? this.SmallFontSize : config.SmallFontSize;
            //this.LargeFontSize = config.LargeFontSize == default(double) ? this.LargeFontSize : config.LargeFontSize;

            if (config.DefaultFontSize > 0)
                this.DefaultFontSize = config.DefaultFontSize;
            this.FontSize = config.FontSize;
            this.CurrentFontSize = this.DefaultFontSize * ((int)this.FontSize / 10.0);
            this.ItemHeight = config.ItemHeight == default(double) ? this.ItemHeight : config.ItemHeight;
            this.ItemWidth = config.ItemWidth == default(double) ? this.ItemWidth : config.ItemWidth;
            this.ItemCornerRadius = config.ItemCornerRadius == default(double) ? this.ItemCornerRadius : config.ItemCornerRadius;
            this.RowHeight = config.RowHeight == default(double) ? this.RowHeight : config.RowHeight;
            this.Language = config.Language;
            this.ThemeType = config.ThemeType;
            this.AnimalSpeed = config.AnimalSpeed == 0 ? 1000 : config.AnimalSpeed;
            this.AccentColorSelectType = config.AccentColorSelectType;
            this.IsUseAnimal = config.IsUseAnimal;
            this.Version = config.Version;
            this.AccentBrushType = config.AccentBrushType;

            //this.ForegroundColor = config.ForegroundColor == Colors.White ? this.ForegroundColor : config.ForegroundColor;


            //var ps = config.GetType().GetProperties();

            //foreach (var p in ps)
            //{
            //    var find = this.GetType().GetProperty(p.Name);

            //    if (find == null) continue;

            //    if (!find.CanWrite) continue;

            //    find.SetValue(this, p.GetValue(config));
            //}
        }

        public int Version { get; set; }
    }

    public class ColorLink : NotifyPropertyChangedBase
    {
        public ThemeType ThemeType { get; set; }
        //public Brush Color { get; set; }
        //public Brush Text { get; set; }

        private Uri source;
        /// <summary> 连接地址 </summary>
        public Uri Source
        {
            get { return this.source; }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                    RaisePropertyChanged("Source");
                }
            }
        }

        private string displayName;
        /// <summary> 显示名称 </summary>
        public string DisplayName
        {
            get { return this.displayName; }
            set
            {
                if (this.displayName != value)
                {
                    this.displayName = value;
                    RaisePropertyChanged("DisplayName");
                }
            }
        }

        private string _logo;
        /// <summary> 说明  </summary>
        public string Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                RaisePropertyChanged("Logo");
            }
        }
    }


    public class AccentColorSource : NotifyPropertyChanged
    {
        #region - 属性 - 

        private string _displayName;
        /// <summary> 显示名称  </summary>
        public string DispalyName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged("DispalyName");
            }
        }


        private ObservableCollection<Color> _colors = new ObservableCollection<Color>();
        /// <summary> 颜色列表  </summary>
        public ObservableCollection<Color> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                RaisePropertyChanged("Colors");
            }
        }


        #endregion
    }

    public static class ColorSourceFactory
    {
        public static List<Color> Create(double b = 0.5)
        {
            List<Color> result = new List<Color>();

            for (int i = 0; i < 100; i++)
            {
                if (i % 3 == 0)
                {
                    result.Add(new HsbaColor(3.6 * i, 1.0, b, 1.0).Color);
                }
            }

            return result;
        }
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum FontSize
    {
        /// <summary>
        /// Small fonts.
        /// </summary>
        [Display(Name = "较小")]
        MoreSmall = 6,
        /// <summary>
        /// Small fonts.
        /// </summary>
        [Display(Name = "小")]
        Small = 8,
        /// <summary>
        /// Small fonts.
        /// </summary>
        [Display(Name = "适中")]
        Normal = 10,
        /// <summary>
        /// Large fonts.
        /// </summary>
        [Display(Name = "大")]
        Large = 12,
        /// <summary>
        /// Large fonts.
        /// </summary>
        [Display(Name = "超大")]
        MoreLarge = 15

    }

    public enum Language
    {
        /// <summary>
        /// Large fonts.
        /// </summary>
        Chinese,
        /// <summary>
        /// Small fonts.
        /// </summary>
        English
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum ThemeType
    {
        [Display(Name = "浅主题", ShortName = "Light")]
        Light,
        [Display(Name = "深主题", ShortName = "Dark")]
        Dark,
        [Display(Name = "深(蓝)主题", ShortName = "Dark.Blue")]
        DarkBlue,
        [Display(Name = "深(紫)主题", ShortName = "Dark.Purple")]
        DarkPurple,
        [Display(Name = "深(黑)主题", ShortName = "Dark.Black")]
        DarkBlack,
        [Display(Name = "深(灰)色调", ShortName = "Dark.Gray")]
        DarkGray,
        [Display(Name = "浅(蓝)色调", ShortName = "Light.Blue")]
        LightBlue,
        [Display(Name = "浅(紫)色调", ShortName = "Light.Purple")]
        LightPurple,
        [Display(Name = "浅(灰)色调", ShortName = "Light.Gray")]
        LightGray,
        [Display(Name = "浅(透明)主题", ShortName = "Light.Tranparent")]
        TranparentLight,
        [Display(Name = "深(透明)主题", ShortName = "Dark.Tranparent")]
        TranparentDark,
        [Display(Name = "主色调", ShortName = "Accent")]
        Accent
    }

    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum AccentBrushType
    {
        [Display(Name = "纯色")]
        SolidColorBrush,
        [Display(Name = "线性渐变(左右)")]
        LinearGradientBrush,
        [Display(Name = "线性渐变(右左)")]
        LinearGradientBrushReverse,
        [Display(Name = "径向渐变(外内)")]
        RadialGradientBrush,
        [Display(Name = "径向渐变(内外)")]
        RadialGradientBrushReverse
    }
}
