using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase.Color
{
    /// <summary> 主题颜色管理器 </summary>
    public class ThemeService : NotifyPropertyChanged
    {
        /// <summary> 深颜色主题 </summary>

        public static readonly Uri DarkThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Color/Theme/DarkThemeResource.xaml", UriKind.Relative);

        /// <summary> 浅颜色主题 </summary>
        public static readonly Uri LightThemeSource = new Uri("/HeBianGu.Base.WpfBase;component/Color/Theme/LightThemeResource.xaml", UriKind.Relative);

        /// <summary> 主颜色Key </summary>
        public const string KeyAccentColor = "AccentColor";

        /// <summary> 主颜色字段 </summary>
        public const string KeyAccent = "Accent";

        /// <summary> 默认大小 </summary>
        public const string KeyDefaultFontSize = "DefaultFontSize";

        /// <summary> 固定字体 </summary>
        public const string KeyFixedFontSize = "FixedFontSize";

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
                    where dict.Contains("S_AccentBrush")
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
                dic.Add("S_AccentBrush", "<!--主色调-->");
                dic.Add("S_AccentBrush_MouseOver", "<!--半透明的主色调 一般用于MouseOver-->");
                dic.Add("S_WindowOpacityAccent", "<!--磨砂窗体背景色调-->");
                dic.Add("S_OpacityGray", "<!--背景透明窗体色调-->");
                dic.Add("S_GrayLight", "< !--用于内容域、面板底色-- > ");
                dic.Add("S_GrayLightOpacity", "<!--用于透明背景内容域、面板底色-->");
                dic.Add("S_GrayNotice", "<!--用于边框、分割线-->");
                dic.Add("S_GrayNoticeOpacity", "<!--用于边框、分割线-->");
                dic.Add("S_OrangeNotice", "<!--黄色  用于提示文字和图标-->");
                dic.Add("S_BlackNotice", "<!--黑色 用于提示文字和图标-->");
                dic.Add("S_RedNotice", "<!--红色 用于警告文字和图标-->");
                dic.Add("S_GreenNotice", "<!--绿色 用于提示文字和图标-->");
                dic.Add("S_WhiteNotice", "<!--面板、标题、输入框底色-->");
                dic.Add("TitleTextForeground", "<!--重要文字信息及标题-->");
                dic.Add("NormalTextForeground", "<!--用于普通文字、内容信息-->");
                dic.Add("AssistTextForeground", "<!--用于标注、辅助文字-->");
                dic.Add("LinkTextForeground", "<!--用于链接文字文字-->");
                dic.Add("TextBackground", "<!--用于面板底色内容域底色-->");
                dic.Add("TextBorderBrush", "<!--用于边框、分割线-->");
                return dic;
            }
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
