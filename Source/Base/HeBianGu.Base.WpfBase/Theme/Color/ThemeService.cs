using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml;

namespace HeBianGu.Base.WpfBase
{

    public class ThemeLocalizeConfig : IThemeService
    {
        public Color AccentColor { get; set; }

        //public Color ForegroundColor { get; set; }

        public FontSize FontSize { get; set; }

        public double ItemCornerRadius { get; set; }

        public double ItemHeight { get; set; }

        public double ItemWidth { get; set; }

        public Language Language { get; set; }

        public double LargeFontSize { get; set; }

        public double RowHeight { get; set; }

        public double SmallFontSize { get; set; }

        public ThemeType ThemeType { get; set; }

        public int AnimalSpeed { get; set; }

        public int AccentColorSelectType { get; set; }

        public bool IsUseAnimal { get; set; }

        public int Version { get; set; }

        public AccentBrushType AccentBrushType { get; set; }
    }

    public interface IThemeService
    {
        Color AccentColor { get; set; }

        //Color ForegroundColor { get; set; }

        FontSize FontSize { get; set; }

        double ItemCornerRadius { get; set; }

        double ItemHeight { get; set; }

        double ItemWidth { get; set; }

        Language Language { get; set; }

        double LargeFontSize { get; set; }

        double RowHeight { get; set; }

        double SmallFontSize { get; set; }

        ThemeType ThemeType { get; set; }

        int AnimalSpeed { get; set; }

        int AccentColorSelectType { get; set; }

        bool IsUseAnimal { get; set; }

        int Version { get; set; }

        AccentBrushType AccentBrushType { get; set; }
    }

    /// <summary> 主题颜色管理器 </summary>
    public class ThemeService : NotifyPropertyChanged
    {
        private ThemeService()
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

            this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Light, DisplayName = "浅色调", Source = ThemeService.LightThemeSource, Color = Brushes.White, Text = Brushes.Black });
            this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Dark, DisplayName = "深色调", Source = ThemeService.DarkThemeSource, Color = Brushes.Black, Text = Brushes.White });
            this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Gray, DisplayName = "灰色调", Source = ThemeService.GrayThemeSource, Color = Brushes.Gray, Text = Brushes.White });
            this.ColorLinks.Add(new ColorLink { ThemeType = ThemeType.Accent, DisplayName = "主色调", Source = ThemeService.AccentThemeSource, Color = Brushes.White, Text = Brushes.Black });

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

        public double SmallFontSize
        {
            get
            {
                return (double)Application.Current.Resources["S.FontSize.Small"];
            }
            set
            {
                Application.Current.Resources["S.FontSize.Small"] = value;

                RaisePropertyChanged("SmallFontSize");
            }
        }



        public double LargeFontSize
        {
            get
            {
                return (double)Application.Current.Resources["S.FontSize.Large"];
            }
            set
            {
                Application.Current.Resources["S.FontSize.Large"] = value;

                RaisePropertyChanged("LargeFontSize");
            }
        }

        /// <summary> 字体大小 </summary>
        public FontSize FontSize
        {
            get
            {
                var defaultFontSize = Application.Current.Resources["S.FontSize.Default"] as double?;

                if (defaultFontSize.HasValue)
                {
                    return defaultFontSize.Value == SmallFontSize ? FontSize.Small : FontSize.Large;
                }

                return FontSize.Small;
            }
            set
            {
                Application.Current.Resources["S.FontSize.Default"] = value == FontSize.Small ? this.SmallFontSize : this.LargeFontSize;
                Application.Current.Resources["S.FontSize.Fixed"] = value == FontSize.Small ? this.SmallFontSize - 1.5 : this.LargeFontSize - 1.5;

                RaisePropertyChanged("FontSize");
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

                this.AccentColor = this.AccentColor;

                RaisePropertyChanged("AccentBrushType");
            }
        }

        /// <summary> 主色调 </summary>
        public Color AccentColor
        {
            get
            {
                var accentColor = Application.Current.Resources["AccentColor"] as Color?;

                if (accentColor.HasValue)
                {
                    return accentColor.Value;
                }

                return Color.FromArgb(0xff, 0x1b, 0xa1, 0xe2);
            }
            set
            {
                Application.Current.Resources["AccentColor"] = value;

                Application.Current.Resources["Accent"] = new SolidColorBrush(value);

                if (this.AccentBrushType == AccentBrushType.SolidColorBrush)
                {
                    Application.Current.Resources["S.Brush.Accent"] = new SolidColorBrush(value);
                }
                else if (this.AccentBrushType == AccentBrushType.LinearGradientBrush)
                {
                    LinearGradientBrush radial = new LinearGradientBrush();
                    radial.StartPoint = new Point(-3, 0);
                    radial.EndPoint = new Point(1, 0);
                    radial.GradientStops.Add(new GradientStop(Colors.White, 0.0));
                    radial.GradientStops.Add(new GradientStop(value, 1.0));

                    Application.Current.Resources["S.Brush.Accent"] = radial;
                }
                else if (this.AccentBrushType == AccentBrushType.LinearGradientBrushReverse)
                {
                    LinearGradientBrush radial = new LinearGradientBrush();
                    radial.StartPoint = new Point(0, 0);
                    radial.EndPoint = new Point(4, 0);
                    radial.GradientStops.Add(new GradientStop(Colors.White, 1.0));
                    radial.GradientStops.Add(new GradientStop(value, 0.0));

                    Application.Current.Resources["S.Brush.Accent"] = radial;
                }
                else if (this.AccentBrushType == AccentBrushType.RadialGradientBrush)
                {
                    RadialGradientBrush radial = new RadialGradientBrush();
                    radial.Center = new Point(0.5, 0.5);
                    radial.GradientStops.Add(new GradientStop(Colors.White, -3.0));
                    radial.GradientStops.Add(new GradientStop(value, 1.0));

                    Application.Current.Resources["S.Brush.Accent"] = radial;
                }
                else
                {
                    RadialGradientBrush radial = new RadialGradientBrush();
                    radial.Center = new Point(0.5, 0.5); 
                    radial.GradientStops.Add(new GradientStop(Colors.White, 8.0));
                    radial.GradientStops.Add(new GradientStop(value, 0.0));

                    Application.Current.Resources["S.Brush.Accent"] = radial;
                }


                List<string> findAll = new List<string>();

                var oldThemeDict = GetThemeDictionary();

                foreach (DictionaryEntry item in oldThemeDict)
                {
                    var ss = item.Key;

                    if (item.Key.ToString().StartsWith("S.Brush.Accent"))
                    {
                        findAll.Add(item.Key.ToString());
                    }
                }

                foreach (var item in findAll)
                {

                    SolidColorBrush brush = Application.Current.Resources[item] as SolidColorBrush;

                    SolidColorBrush result = new SolidColorBrush(value);

                    if (brush != null)
                        result.Opacity = brush.Opacity;

                    Application.Current.Resources[item] = result;
                }

                RaisePropertyChanged("AccentColor");
            }
        }

        /// <summary> 项的高度 </summary>
        public double ItemHeight
        {
            set
            {
                Application.Current.Resources["S.Window.Item.Height"] = value;

                Application.Current.Resources["S.Window.Item.CornerRadius.Circle"] = new CornerRadius(value / 2);

                RaisePropertyChanged("ItemHeight");
            }
            get
            {
                return (double)Application.Current.Resources["S.Window.Item.Height"];
            }
        }

        /// <summary> 项的宽度 </summary>
        public double ItemWidth
        {
            set
            {
                Application.Current.Resources["S.Window.Item.Width"] = value;
                RaisePropertyChanged("ItemWidth");
            }
            get
            {
                return (double)Application.Current.Resources["S.Window.Item.Width"];
            }
        }

        /// <summary> 项的边角 </summary>
        public double ItemCornerRadius
        {
            set
            {
                Application.Current.Resources["S.Window.Item.CornerRadius.Value"] = value;

                Application.Current.Resources["S.Window.Item.CornerRadius"] = new CornerRadius(value, value, value, value);

                Application.Current.Resources["S.Window.Item.CornerRadius.Left"] = new CornerRadius(value, 0, 0, value);
                Application.Current.Resources["S.Window.Item.CornerRadius.Right"] = new CornerRadius(0, value, value, 0);
                Application.Current.Resources["S.Window.Item.CornerRadius.Top"] = new CornerRadius(value, value, 0, 0);
                Application.Current.Resources["S.Window.Item.CornerRadius.Bottom"] = new CornerRadius(0, 0, value, value);

                Application.Current.Resources["S.Window.Item.CornerRadius.LeftTop"] = new CornerRadius(value, 0, 0, 0);
                Application.Current.Resources["S.Window.Item.CornerRadius.RightTop"] = new CornerRadius(0, value, 0, 0);
                Application.Current.Resources["S.Window.Item.CornerRadius.LeftBottom"] = new CornerRadius(0, 0, value, 0);
                Application.Current.Resources["S.Window.Item.CornerRadius.RightBottom"] = new CornerRadius(0, 0, 0, value);

                RaisePropertyChanged("ItemCornerRadius");
            }
            get
            {
                return (double)Application.Current.Resources["S.Window.Item.CornerRadius.Value"];
            }
        }

        /// <summary> 行的高度 </summary>
        public double RowHeight
        {
            set
            {
                Application.Current.Resources["S.Window.Row.Height"] = value;

                RaisePropertyChanged("RowHeight");
            }
            get
            {
                return (double)Application.Current.Resources["S.Window.Row.Height"];
            }
        }


        private bool _isFollowSystemSysteme;
        /// <summary> 是否跟随系统  </summary>
        public bool IsFollowSystemSystem
        {
            get { return _isFollowSystemSysteme; }
            set
            {
                _isFollowSystemSysteme = value;


                if (value == true)
                {
                    ThemeService.Current.AccentColor = SystemColors.DesktopColor;
                }
                else
                {
                    ThemeService.Current.AccentColor = this.SelectColorSource.Colors.FirstOrDefault();
                }

                RaisePropertyChanged("IsFollowSystemSystem");
            }
        }

        Language _language;
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
                XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

                if (value == Language.Chinese)
                {
                    provider.Source = LanguageChineseSource;
                }
                else if (value == Language.English)
                {
                    provider.Source = LanguageEnglishSource;
                }
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

        /// <summary> 主题类型 </summary>
        public ThemeType ThemeType
        {
            get
            {
                var uri = this.GetThemeDictionary()?.Source;

                var find = this.ColorLinks.FirstOrDefault(l => l.Source == uri);

                return find == null ? ThemeType.Light : find.ThemeType;
            }
            set
            {

                var find = this.ColorLinks.FirstOrDefault(l => l.ThemeType == value);

                var oldThemeDict = GetThemeDictionary();

                var dictionaries = Application.Current.Resources.MergedDictionaries;

                var themeDict = new ResourceDictionary { Source = find.Source };

                dictionaries.Add(themeDict);

                if (oldThemeDict != null)
                {
                    dictionaries.Remove(oldThemeDict);
                }

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


        /// <summary> 消息弹窗背景色 </summary>
        public SolidColorBrush DialogCoverBrush
        {
            set
            {
                Application.Current.Resources["S.Brush.Dialog.Cover"] = value;
            }
            get
            {
                return (SolidColorBrush)Application.Current.Resources["S.Brush.Dialog.Cover"];
            }
        }

        #endregion
        /// <summary> 深颜色主题 </summary> 
        public static readonly Uri DarkThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Theme/Color/DarkThemeResource.xaml", UriKind.Relative);

        /// <summary> 浅颜色主题 </summary>
        public static readonly Uri LightThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Theme/Color/LightThemeResource.xaml", UriKind.Relative);

        /// <summary> 灰色主题 </summary>
        public static readonly Uri GrayThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Theme/Color/GrayThemeResource.xaml", UriKind.Relative);

        /// <summary> Accent主题 </summary>
        public static readonly Uri AccentThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Theme/Color/AccentThemeResource.xaml", UriKind.Relative);

        public static ThemeService Current = new ThemeService();

        static readonly Uri LanguageChineseSource = new Uri("/HeBianGu.Base.WpfBase;component/Resources/zh-cn.xml", UriKind.RelativeOrAbsolute);
        static readonly Uri LanguageEnglishSource = new Uri("/HeBianGu.Base.WpfBase;component/Resources/en-us.xml", UriKind.RelativeOrAbsolute);

        /// <summary> 获取第一个带有WindowBackground的资源字典作为主题 </summary>
        private ResourceDictionary GetThemeDictionary()
        {
            var result = (from dict in Application.Current.Resources.MergedDictionaries
                          where dict.Contains("S.DropShadowEffect.Window")
                          select dict).FirstOrDefault();
            return result;



        }

        #region - 设置随机播放颜色 -

        Timer _timer = new Timer(1000);

        Random _random = new Random();

        void StartAnimationTheme(int timespan = 5000)
        {
            _timer.Interval = timespan;

            _timer.Start();

        }

        void StopAnimationTheme()
        {
            _timer.Stop();
        } 

        #endregion




        public ThemeLocalizeConfig SaveTo()
        {
            ThemeLocalizeConfig themeLocalize = new ThemeLocalizeConfig();

            //themeLocalize.AccentColor = this.AccentColor;
            //themeLocalize.SmallFontSize = this.SmallFontSize;
            //themeLocalize.LargeFontSize = this.LargeFontSize;
            //themeLocalize.FontSize = this.FontSize;
            //themeLocalize.ItemHeight = this.ItemHeight;
            //themeLocalize.ItemWidth = this.ItemWidth;
            //themeLocalize.ItemCornerRadius = this.ItemCornerRadius;
            //themeLocalize.RowHeight = this.RowHeight;
            //themeLocalize.Language = this.Language;
            //themeLocalize.ThemeType = this.ThemeType;

            //themeLocalize.AnimalSpeed = this.AnimalSpeed;
            //themeLocalize.AccentColorSelectType = this.AccentColorSelectType;
            //themeLocalize.IsUseAnimal = this.IsUseAnimal;
            //themeLocalize.Version = this.Version;

            var ps = this.GetType().GetProperties();

            foreach (var p in ps)
            {
                var find = typeof(ThemeLocalizeConfig).GetProperty(p.Name);

                if (find == null) continue;

                if (!find.CanWrite) continue;

                find.SetValue(themeLocalize, p.GetValue(this));
            }

            return themeLocalize;
        }

        public void LoadFrom(ThemeLocalizeConfig config)
        {
            this.AccentColor = config.AccentColor == default(Color) ? this.AccentColor : config.AccentColor;
            this.SmallFontSize = config.SmallFontSize == default(double) ? this.SmallFontSize : config.SmallFontSize;
            this.LargeFontSize = config.LargeFontSize == default(double) ? this.LargeFontSize : config.LargeFontSize;
            this.FontSize = config.FontSize;
            this.ItemHeight = config.ItemHeight == default(double) ? this.ItemHeight : config.ItemHeight;
            this.ItemWidth = config.ItemWidth == default(double) ? this.ItemWidth : config.ItemWidth;
            this.ItemCornerRadius = config.ItemCornerRadius == default(double) ? this.ItemCornerRadius : config.ItemCornerRadius;
            this.RowHeight = config.RowHeight == default(double) ? this.RowHeight : config.RowHeight;
            this.Language = config.Language;
            this.ThemeType = config.ThemeType;
            this.AnimalSpeed = config.AnimalSpeed;
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

    public class ColorLink : Link
    {
        public ThemeType ThemeType { get; set; }
        public Brush Color { get; set; }
        public Brush Text { get; set; }
    }

    public class LanguageService
    {
        public static LanguageService Instance = new LanguageService();

        public string GetConfigByKey(string key)
        {
            XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

            if (provider.Document == null) return null;

            XmlElement root = provider.Document.DocumentElement as XmlElement;

            return root.SelectSingleNode(key)?.InnerText;

        }

        public string GetMessageByCode(string code)
        {
            XmlDataProvider provider = System.Windows.Application.Current.FindResource("S.XmlDataProvider.Language") as XmlDataProvider;

            if (provider.Document == null) return null;

            XmlElement root = provider.Document.DocumentElement as XmlElement;

            var elements = root?.GetElementsByTagName("Message");

            foreach (XmlNode item in elements)
            {
                foreach (XmlAttribute attribute in item.Attributes)
                {
                    if (attribute.Name == "Code")
                    {
                        if (attribute.Value == code)
                        {
                            return item.InnerText;
                        }
                    }
                }
            }

            return null;
        }
    }


    /// <summary> 说明</summary>
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

    /// <summary> 颜色生成 </summary>
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

    [TypeConverter(typeof(DisplayConverter))]
    public enum FontSize
    {
        /// <summary>
        /// Small fonts.
        /// </summary>
        [Display(Name = "小")]
        Small,

        /// <summary>
        /// Large fonts.
        /// </summary>
        [Display(Name = "大")]
        Large

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

    [TypeConverter(typeof(DisplayConverter))]
    public enum ThemeType
    {
        [Display(Name = "浅主题")]
        Light,
        [Display(Name = "深主题")]
        Dark,
        [Display(Name = "灰主题")]
        Gray,
        [Display(Name = "主色调")]
        Accent
    }

    [TypeConverter(typeof(DisplayConverter))]
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
