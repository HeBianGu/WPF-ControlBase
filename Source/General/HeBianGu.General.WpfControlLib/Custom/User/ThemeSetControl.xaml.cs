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

        SettingsAppearanceViewModel _context = new SettingsAppearanceViewModel();

        public ThemeSetControl()
        {
            InitializeComponent();

            this.DataContext = _context;

            this.Loaded += (l, k) =>
              {
                  _context.Load();
              };

        }
    }

    /// <summary> 主题设置模型 </summary>
    public class SettingsAppearanceViewModel : NotifyPropertyChanged
    {
        //private const string FontSmall = "Small";
        //private const string FontLarge = "Large";

        private const string PaletteMetro = "浅色";
        private const string PaletteWP = "深色";


        private int _selectIndex;
        /// <summary> 说明  </summary>
        public int SelectIndex
        {
            get { return _selectIndex; }
            set
            {
                _selectIndex = value;

                RaisePropertyChanged("SelectIndex");


                ThemeService.Current.FontSize = value == 1 ? FontSize.Large : FontSize.Small;
            }
        }


        private ObservableCollection<AccentColorSource> _colorSource = new ObservableCollection<AccentColorSource>();
        /// <summary> 说明  </summary>
        public ObservableCollection<AccentColorSource> ColorSource
        {
            get { return _colorSource; }
            set
            {
                _colorSource = value;
                RaisePropertyChanged("ColorSource");
            }
        }


        private AccentColorSource _selectColorSource;
        /// <summary> 说明  </summary>
        public AccentColorSource SelectColorSource
        {
            get { return _selectColorSource; }
            set
            {
                _selectColorSource = value;
                RaisePropertyChanged("SelectColorSource");
            }
        }


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
            
            (Color)ColorConverter.ConvertFromString("#FF8AA500"),
            (Color)ColorConverter.ConvertFromString("#FF4B9601"),
            (Color)ColorConverter.ConvertFromString("#FF009E00"),
            (Color)ColorConverter.ConvertFromString("#FF009392"),
            (Color)ColorConverter.ConvertFromString("#FF0292D8"),
            (Color)ColorConverter.ConvertFromString("#FF002F8E"),
            (Color)ColorConverter.ConvertFromString("#FF00205F"),
            (Color)ColorConverter.ConvertFromString("#FF3A008C"),
            (Color)ColorConverter.ConvertFromString("#FF7D00BC"),
            (Color)ColorConverter.ConvertFromString("#FF46006A"),
            (Color)ColorConverter.ConvertFromString("#FFD8009C"),
            (Color)ColorConverter.ConvertFromString("#FFC8006A"),
            (Color)ColorConverter.ConvertFromString("#FF8C004A"),
            (Color)ColorConverter.ConvertFromString("#FF85001E"),
            (Color)ColorConverter.ConvertFromString("#FFB10F00"),
            (Color)ColorConverter.ConvertFromString("#FFBF4F00"),
             (Color)ColorConverter.ConvertFromString("#FFCE8900"),
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
            this.themes.Add(new ColorLink { DisplayName = "浅色调", Source = ThemeService.LightThemeSource, Color = Brushes.White, Text = Brushes.Black });
            this.themes.Add(new ColorLink { DisplayName = "深色调", Source = ThemeService.DarkThemeSource, Color = Brushes.Black, Text = Brushes.White });
            this.themes.Add(new ColorLink { DisplayName = "灰色调", Source = ThemeService.GrayThemeSource, Color = Brushes.Gray, Text = Brushes.White });
            this.themes.Add(new ColorLink { DisplayName = "主色调", Source = ThemeService.AccentThemeSource, Color = Brushes.White, Text = Brushes.Black });

            //this.SelectedFontSize = ThemeService.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;

            this.ColorSource.Clear();

            var dark = new AccentColorSource() { DispalyName="适中",Colors=ColorSourceFactory.Create(0.6).ToObservable()};

            this.ColorSource.Add(dark);

            var light = new AccentColorSource() { DispalyName = "浅色", Colors = ColorSourceFactory.Create(0.8).ToObservable() };

            this.ColorSource.Add(light);

            var deep = new AccentColorSource() { DispalyName = "深色", Colors = ColorSourceFactory.Create(0.3).ToObservable() };

            this.ColorSource.Add(deep);

            var height = new AccentColorSource() { DispalyName = "高亮", Colors = ColorSourceFactory.Create(1.0).ToObservable() };

            this.ColorSource.Add(height);


            this.SelectColorSource = this.ColorSource.FirstOrDefault();

            SyncThemeAndColor();

            ThemeService.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;

        }

        public void Load()
        {

            this.SelectIndex = ThemeService.Current.FontSize == FontSize.Large ? 1 : 0;

            this.ItemHeight = (int)ThemeService.Current.ItemHeight;

            this.RowHeight = (int)ThemeService.Current.RowHeight;

            this.IsUseAnimal = ThemeService.Current.IsUseAnimal;

            this.AnimalSpeed = (int)(ThemeService.Current.AnimalSpeed / 1000);

            this.ItemCornerRadius = (int)ThemeService.Current.ItemCornerRadius;
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

        //public string[] FontSizes
        //{
        //    get { return new string[] { FontSmall, FontLarge }; }
        //}

        //public string[] Palettes
        //{
        //    get { return new string[] { PaletteMetro, PaletteWP }; }
        //}

        //public Color[] AccentColors
        //{
        //    get { return this.selectedPalette == PaletteMetro ? this.metroAccentColors : this.wpAccentColors; }
        //}

        //public string SelectedPalette
        //{
        //    get { return this.selectedPalette; }
        //    set
        //    {
        //        if (this.selectedPalette != value)
        //        {
        //            this.selectedPalette = value;
        //            RaisePropertyChanged("AccentColors");

        //            this.SelectedAccentColor = this.AccentColors.FirstOrDefault();
        //        }
        //    }
        //}

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

        private int _itemHeight;
        /// <summary> 说明  </summary>
        public int ItemHeight
        {
            get { return _itemHeight; }
            set
            {
                _itemHeight = value;

                RaisePropertyChanged("ItemHeight");

                ThemeService.Current.ItemHeight = value;
            }
        }

        private int _rowHeight;
        /// <summary> 说明  </summary>
        public int RowHeight
        {
            get { return _rowHeight; }
            set
            {
                _rowHeight = value;
                RaisePropertyChanged("RowHeight");

                ThemeService.Current.RowHeight = value;
            }
        }

        private int _animalSpeed;
        /// <summary> 说明  </summary>
        public int AnimalSpeed
        {
            get { return _animalSpeed; }
            set
            {
                _animalSpeed = value;

                RaisePropertyChanged("AnimalSpeed");

                ThemeService.Current.AnimalSpeed = value * 1000;
            }
        }

        private bool _isUseAnimal;
        /// <summary> 说明  </summary>
        public bool IsUseAnimal
        {
            get { return _isUseAnimal; }
            set
            {
                _isUseAnimal = value;
                RaisePropertyChanged("IsUseAnimal");

                ThemeService.Current.IsUseAnimal = value;
            }
        }

        private int _itemCornerRadius;
        /// <summary> 说明  </summary>
        public int ItemCornerRadius
        {
            get { return _itemCornerRadius; }
            set
            {
                _itemCornerRadius = value;

                RaisePropertyChanged("ItemCornerRadius");

                ThemeService.Current.ItemCornerRadius = value;
            }
        }


        protected override void RelayMethod(object obj)
        {
            string command = obj.ToString();

            //  Do：应用
            if (command == "Combobox.SelectChanged.Refresh")
            {
                this.SelectedAccentColor = this.SelectColorSource.Colors.FirstOrDefault();
            }
            //  Do：取消
            else if (command == "Cancel")
            {


            }
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


    public class ColorLink : Link
    {
        public Brush Color { get; set; }
        public Brush Text { get; set; }
    }

}
