using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 主题颜色管理器 </summary>
    public class ThemeService : NotifyPropertyChanged
    {
        /// <summary> 深颜色主题 </summary>

        public static readonly Uri DarkThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Theme/Color/DarkThemeResource.xaml", UriKind.Relative);

        /// <summary> 浅颜色主题 </summary>
        public static readonly Uri LightThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Theme/Color/LightThemeResource.xaml", UriKind.Relative);

        /// <summary> 主颜色Key </summary>
        public const string KeyAccentColor = "AccentColor";

        /// <summary> 主颜色字段 </summary>
        public const string KeyAccent = "Accent";

        /// <summary> 默认大小 </summary>
        public const string KeyDefaultFontSize = "S.FontSize.Default";

        /// <summary> 固定字体 </summary>
        public const string KeyFixedFontSize = "S.FontSize.Fixed";

        public static ThemeService Current = new ThemeService();

        private ThemeService()
        {
            //DarkThemeCommand = new RelayCommand(o => ThemeSource = DarkThemeSource, o => !DarkThemeSource.Equals(ThemeSource));
            //LightThemeCommand = new RelayCommand(o => ThemeSource = LightThemeSource, o => !LightThemeSource.Equals(ThemeSource));
            //SetThemeCommand = new RelayCommand(o =>
            //{
            //    var uri = NavigationHelper.ToUri(o);
            //    if (uri != null)
            //    {
            //        ThemeSource = uri;
            //    }
            //}, o => o is Uri || o is string);
            //LargeFontSizeCommand = new RelayCommand(o => FontSize = FontSize.Large);
            //SmallFontSizeCommand = new RelayCommand(o => FontSize = FontSize.Small);
            //AccentColorCommand = new RelayCommand(o =>
            //{
            //    if (o is Color)
            //    {
            //        AccentColor = (Color)o;
            //    }
            //    else
            //    {
            //        // parse color from string
            //        var str = o as string;
            //        if (str != null)
            //        {
            //            AccentColor = (Color)ColorConverter.ConvertFromString(str);
            //        }
            //    }
            //}, o => o is Color || o is string);
        }


        private void Storyboard_CurrentGlobalSpeedInvalidated(object sender, EventArgs e)
        {

            Debug.WriteLine("说明");

        }


        /// <summary> 获取第一个带有WindowBackground的资源字典作为主题 </summary>
        private ResourceDictionary GetThemeDictionary()
        {
            return (from dict in Application.Current.Resources.MergedDictionaries
                    where dict.Contains("S.Brush.Accent")
                    select dict).FirstOrDefault();
        }

        /// <summary> 获取主题地址 </summary>
        private Uri GetThemeSource()
        {
            var dict = GetThemeDictionary();

            if (dict != null)
            {
                return dict.Source;
            }

            //  Message：没有获取到 可以设置WindowBackground的字典
            return null;
        }

        /// <summary> 设置主题 </summary>
        private void SetThemeSource(Uri source, bool useThemeAccentColor)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var oldThemeDict = GetThemeDictionary();

            var dictionaries = Application.Current.Resources.MergedDictionaries;

            var themeDict = new ResourceDictionary { Source = source };

            var accentColor = themeDict[KeyAccentColor] as System.Windows.Media.Color?;

            if (accentColor.HasValue)
            {
                themeDict.Remove(KeyAccentColor);

                if (useThemeAccentColor)
                {
                    ApplyAccentColor(accentColor.Value);
                }
            }

            dictionaries.Add(themeDict);

            if (oldThemeDict != null)
            {
                dictionaries.Remove(oldThemeDict);
            }

            RaisePropertyChanged("ThemeSource");
        }

        private void ApplyAccentColor(System.Windows.Media.Color accentColor)
        {
            Application.Current.Resources[KeyAccentColor] = accentColor;

            Application.Current.Resources[KeyAccent] = new SolidColorBrush(accentColor);
        }

        private FontSize GetFontSize()
        {
            var defaultFontSize = Application.Current.Resources[KeyDefaultFontSize] as double?;

            if (defaultFontSize.HasValue)
            {
                return defaultFontSize.Value == 12D ? FontSize.Small : FontSize.Large;
            }

            return FontSize.Large;
        }

        private void SetFontSize(FontSize fontSize)
        {
            if (GetFontSize() == fontSize)
            {
                return;
            }

            Application.Current.Resources[KeyDefaultFontSize] = fontSize == FontSize.Small ? 12D : 13D;

            Application.Current.Resources[KeyFixedFontSize] = fontSize == FontSize.Small ? 10.667D : 13.333D;

            RaisePropertyChanged("FontSize");
        }

        private System.Windows.Media.Color GetAccentColor()
        {
            var accentColor = Application.Current.Resources[KeyAccentColor] as System.Windows.Media.Color?;

            if (accentColor.HasValue)
            {
                return accentColor.Value;
            }

            return System.Windows.Media.Color.FromArgb(0xff, 0x1b, 0xa1, 0xe2);
        }

        private void SetAccentColor(System.Windows.Media.Color value)
        {
            ApplyAccentColor(value);

            var themeSource = GetThemeSource();

            if (themeSource != null)
            {
                SetThemeSource(themeSource, false);
            }

            RaisePropertyChanged("AccentColor");
        }


        /// <summary>
        /// The command that sets the dark theme.
        /// </summary>
        public ICommand DarkThemeCommand { get; private set; }
        /// <summary>
        /// The command that sets the light color theme.
        /// </summary>
        public ICommand LightThemeCommand { get; private set; }
        /// <summary>
        /// The command that sets a custom theme.
        /// </summary>
        public ICommand SetThemeCommand { get; private set; }

        /// <summary>
        /// The command that sets the large font size.
        /// </summary>
        public ICommand LargeFontSizeCommand { get; private set; }

        /// <summary>
        /// The command that sets the small font size.
        /// </summary>
        public ICommand SmallFontSizeCommand { get; private set; }

        /// <summary>
        /// The command that sets the accent color.
        /// </summary>
        public ICommand AccentColorCommand { get; private set; }

        /// <summary> 主题地址 </summary>
        public Uri ThemeSource
        {
            get { return GetThemeSource(); }
            set { SetThemeSource(value, true); }
        }

        /// <summary> 字体大小 </summary>
        public FontSize FontSize
        {
            get { return GetFontSize(); }
            set { SetFontSize(value); }
        }

        /// <summary> 主色调 </summary>
        public System.Windows.Media.Color AccentColor
        {
            get { return GetAccentColor(); }
            set { SetAccentColor(value); }
        }


        /// <summary> 描述资源的信息 </summary>
        public Dictionary<string, string> KeyToMarkDictionary
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("S.Brush.Accent", "<!--主色调-->");
                dic.Add("S.Brush.Accent.MouseOver", "<!--半透明的主色调 一般用于MouseOver-->");
                dic.Add("S.Brush.WindowBackground", "<!--磨砂窗体背景色调-->");
                dic.Add("S.Brush.Accent.Opacity.5", "<!--背景透明窗体色调-->");
                dic.Add("S.Brush.LightGray.Notice", "< !--用于内容域、面板底色-- > ");
                dic.Add("S.Brush.LightGray.NoticeOpacity.5", "<!--用于透明背景内容域、面板底色-->");
                dic.Add("S.Brush.Gray.Notice", "<!--用于边框、分割线-->");
                dic.Add(".Brush.Gray.Opacity.5", "<!--用于边框、分割线-->");
                dic.Add("S.Brush.Orange.Notice", "<!--黄色  用于提示文字和图标-->");
                dic.Add("S.Brush.Black.Notice", "<!--黑色 用于提示文字和图标-->");
                dic.Add("S.Brush.Red.Notice", "<!--红色 用于警告文字和图标-->");
                dic.Add("S.Brush.Green.Notice", "<!--绿色 用于提示文字和图标-->");
                dic.Add("S.Brush.White.Notice", "<!--面板、标题、输入框底色-->");
                dic.Add("S.Brush.TextForeground.Title", "<!--重要文字信息及标题-->");
                dic.Add("S.Brush.TextForeground.Default", "<!--用于普通文字、内容信息-->");
                dic.Add("S.Brush.TextForeground.Assist", "<!--用于标注、辅助文字-->");
                dic.Add("S.Brush.TextForeground.Link", "<!--用于链接文字文字-->");
                dic.Add("S.Brush.TextBackground.Default", "<!--用于面板底色内容域底色-->");
                dic.Add("S.Brush.TextBorderBrush.Default", "<!--用于边框、分割线-->");
                return dic;
            }
        }


        //  Message：主题颜色
        private System.Windows.Media.Color[] metroAccentColors = new System.Windows.Media.Color[]{
            System.Windows.Media.Color.FromRgb(0x33, 0x99, 0xff),   // blue
            System.Windows.Media.Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            System.Windows.Media.Color.FromRgb(0x33, 0x99, 0x33),   // green
            System.Windows.Media.Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
            System.Windows.Media.Color.FromRgb(0xf0, 0x96, 0x09),   // orange
            System.Windows.Media.Color.FromRgb(0xff, 0x45, 0x00),   // orange red
            System.Windows.Media.Color.FromRgb(0xe5, 0x14, 0x00),   // red
            System.Windows.Media.Color.FromRgb(0xff, 0x00, 0x97),   // magenta
            System.Windows.Media.Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        };

        //  Message：主题颜色
        private System.Windows.Media.Color[] wpAccentColors = new System.Windows.Media.Color[]{
            System.Windows.Media.Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            System.Windows.Media.Color.FromRgb(0x60, 0xa9, 0x17),   // green
            System.Windows.Media.Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            System.Windows.Media.Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            System.Windows.Media.Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            System.Windows.Media.Color.FromRgb(0x00, 0x50, 0xef),   // cobalt
            System.Windows.Media.Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            System.Windows.Media.Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            System.Windows.Media.Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            System.Windows.Media.Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            System.Windows.Media.Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            System.Windows.Media.Color.FromRgb(0xe5, 0x14, 0x00),   // red
            System.Windows.Media.Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            System.Windows.Media.Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            System.Windows.Media.Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            System.Windows.Media.Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            System.Windows.Media.Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            System.Windows.Media.Color.FromRgb(0x64, 0x76, 0x87),   // steel
            System.Windows.Media.Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            System.Windows.Media.Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };


        Timer _timer = new Timer();

        Random _random = new Random();
        public void StartAnimationTheme(int timespan = 5000, int type = 0)
        {
            _timer.Interval = timespan;

            _timer.Elapsed += (l, k) =>
              {
                  Action action = () =>
                 {
                     if (type == 0)
                     {
                         this.AccentColor = wpAccentColors[_random.Next(wpAccentColors.Length)];

                     }
                     else
                     {
                         this.AccentColor = metroAccentColors[_random.Next(metroAccentColors.Length)];
                     }
                 };

                  Application.Current?.Dispatcher.Invoke(action);

              };

            _timer.Start();

        }

        public void StopAnimationTheme()
        {
            _timer.Stop();
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

}
