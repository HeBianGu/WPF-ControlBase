using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 主题设置页面 </summary>
    public partial class ThemeSetControl : UserControl
    {
        public ThemeSetControl()
        {
            InitializeComponent();

            this.DataContext = new SettingsAppearanceViewModel();
        }
    }

    /// <summary> 主题设置模型 </summary>
    public class SettingsAppearanceViewModel : NotifyPropertyChanged
    {
        private const string FontSmall = "Small";
        private const string FontLarge = "Large";

        private const string PaletteMetro = "Custom";
        private const string PaletteWP = "Window";

        //  Message：主题颜色
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
            Color.FromRgb(0, 0, 0),   // black
        };

        private string selectedPalette = PaletteWP;

        private Color selectedAccentColor;

        private ObservableCollection<ColorLink> themes = new ObservableCollection<ColorLink>();

        private Link selectedTheme;

        private string selectedFontSize;

        public SettingsAppearanceViewModel()
        {
            //  Message：主题
            this.themes.Add(new ColorLink { DisplayName = "Light", Source = ThemeService.LightThemeSource, Color = Brushes.White,Text=Brushes.Black });
            this.themes.Add(new ColorLink { DisplayName = "Dark", Source = ThemeService.DarkThemeSource, Color = Brushes.Black, Text = Brushes.White });
            this.themes.Add(new ColorLink { DisplayName = "Gray", Source = ThemeService.GrayThemeSource, Color = Brushes.Gray, Text = Brushes.White });
            this.themes.Add(new ColorLink { DisplayName = "Accent", Source = ThemeService.AccentThemeSource, Color = Brushes.White, Text = Brushes.Black });

            this.SelectedFontSize = ThemeService.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;

            SyncThemeAndColor();

            ThemeService.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;

        }

        private void SyncThemeAndColor()
        {
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(ThemeService.Current.ThemeSource));

            this.SelectedAccentColor = ThemeService.Current.AccentColor;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }


        public ObservableCollection<ColorLink> Themes
        {
            get { return this.themes; }
        }

        public string[] FontSizes
        {
            get { return new string[] { FontSmall, FontLarge }; }
        }

        public string[] Palettes
        {
            get { return new string[] { PaletteMetro, PaletteWP }; }
        }

        public Color[] AccentColors
        {
            get { return this.selectedPalette == PaletteMetro ? this.metroAccentColors : this.wpAccentColors; }
        }

        public string SelectedPalette
        {
            get { return this.selectedPalette; }
            set
            {
                if (this.selectedPalette != value)
                {
                    this.selectedPalette = value;
                    RaisePropertyChanged("AccentColors");

                    this.SelectedAccentColor = this.AccentColors.FirstOrDefault();
                }
            }
        }

        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;

                    RaisePropertyChanged("SelectedTheme");

                    // and update the actual theme
                    ThemeService.Current.ThemeSource = value.Source;
                }
            }
        }

        public string SelectedFontSize
        {
            get { return this.selectedFontSize; }
            set
            {
                if (this.selectedFontSize != value)
                {
                    this.selectedFontSize = value;
                    RaisePropertyChanged("SelectedFontSize");

                    ThemeService.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
                }
            }
        }

        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;

                    RaisePropertyChanged("SelectedAccentColor");

                    ThemeService.Current.AccentColor = value;
                }
            }
        }



        private bool _isFollowSystemSystem;
        /// <summary> 说明  </summary>
        public bool IsFollowSystemSystem
        {
            get { return _isFollowSystemSystem; }
            set
            {
                _isFollowSystemSystem = value;

                RaisePropertyChanged("IsFollowSystemSystem");

                if (value == true)
                {
                    //  Message：备份一下之前的主题
                    this.selectedAccentColor = ThemeService.Current.AccentColor;

                    ThemeService.Current.AccentColor = SystemColors.DesktopColor;
                }
                else
                {
                    ThemeService.Current.AccentColor = SelectedAccentColor;
                }
            }
        }

    }

   public class ColorLink : Link
    {
        public Brush Color { get; set; }
        public Brush Text { get; set; }
    }

}
