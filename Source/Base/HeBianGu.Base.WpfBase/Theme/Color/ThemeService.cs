using System;
using System.Collections.Generic;
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
using System.Xml;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 主题颜色管理器 </summary>
    public class ThemeService : NotifyPropertyChanged
    {
        private ThemeService()
        {

        }

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

        public double SmallFontSize { get; set; } = 13D;

        public double LargeFontSize { get; set; } = 15D;


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

        /// <summary> 获取第一个带有WindowBackground的资源字典作为主题 </summary>
        private ResourceDictionary GetThemeDictionary()
        {
            var result = (from dict in Application.Current.Resources.MergedDictionaries
                          where dict.Contains("S.DropShadowEffect.Window")
                          select dict).FirstOrDefault();
            return result;
        } 

        /// <summary> 主题地址 </summary>
        public Uri ThemeSource
        {
            get
            {
                return this.GetThemeDictionary()?.Source; 
            }
            set
            {
                if (value == null) return;

                var oldThemeDict = GetThemeDictionary();

                var dictionaries = Application.Current.Resources.MergedDictionaries;

                var themeDict = new ResourceDictionary { Source = value };

                dictionaries.Add(themeDict);

                if (oldThemeDict != null)
                {
                    dictionaries.Remove(oldThemeDict);
                }

                RaisePropertyChanged("ThemeSource"); 
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
                if (this.FontSize == value) return;

                Application.Current.Resources["S.FontSize.Default"] = value == FontSize.Small ? SmallFontSize : LargeFontSize;

                Application.Current.Resources["S.FontSize.Fixed"] = value == FontSize.Small ? SmallFontSize - 1.5 : LargeFontSize - 1.5;

                RaisePropertyChanged("FontSize");
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

                Application.Current.Resources["S.Brush.Accent"] = new SolidColorBrush(value);

                RaisePropertyChanged("AccentColor");
            }
        }  

        private Color[] metroAccentColors = new Color[]{
            Color.FromRgb(0x33, 0x99, 0xff),   // blue
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x33, 0x99, 0x33),   // green
            Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };

        //  Message：主题颜色
        private Color[] wpAccentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

        #region - 设置随机播放颜色 -

        Timer _timer = new Timer();

        Random _random = new Random();

        public void StartAnimationTheme(int timespan = 5000, int type = 0)
        {
            _timer.Interval = timespan;

            _timer.Elapsed += (l, k) =>
            {
                Application.Current?.Dispatcher.Invoke(() =>
                {
                    this.AccentColor = type == 0 ? wpAccentColors[_random.Next(wpAccentColors.Length)] : metroAccentColors[_random.Next(metroAccentColors.Length)];
                });

            };

            _timer.Start();

        }

        public void StopAnimationTheme()
        {
            _timer.Stop();
        }

        #endregion  

        /// <summary> 项的高度 </summary>
        public double ItemHeight
        {
            set
            {
                Application.Current.Resources["S.Window.Item.Height"] = value;
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
            }
            get
            {
                return (double)Application.Current.Resources["S.Window.Item.Width"];
            }
        }

        /// <summary> 项的边角 </summary>
        public CornerRadius ItemCornerRadius
        {
            set
            {
                Application.Current.Resources["S.Window.Item.CornerRadius"] = value;
            }
            get
            {
                return (CornerRadius)Application.Current.Resources["S.Window.Item.CornerRadius"];
            }
        }
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

    public enum FontSize
    {
        /// <summary>
        /// Large fonts.
        /// </summary>
        Large,
        /// <summary>
        /// Small fonts.
        /// </summary>
        Small
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

}
