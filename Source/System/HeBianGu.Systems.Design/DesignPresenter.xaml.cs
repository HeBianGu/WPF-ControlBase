using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.Adorner;
using HeBianGu.General.WpfControlLib;
using HeBianGu.Service.TypeConverter;

namespace HeBianGu.Systems.Design
{
    public interface IDesignPresenter
    {

    }

    public class CommandsDesignPresenterBase : DesignPresenterBase, IDesignPresenter
    {
        [Displayer(Name = "恢复默认", Icon = Icons.Clock)]
        public RelayCommand DefaultCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {

            }
        });

        [Displayer(Name = "保存模板", Icon = Icons.Clock)]
        public RelayCommand SaveTempateCommand => new RelayCommand((s, e) =>
        {
            if (e is string project)
            {

            }
        });
    }

    public interface ITextData
    {
        string Text { get; set; }
    }

    [Displayer(Name = "文本", Icon = "\xe649")]
    public class TextBlockPresenter : CommandsDesignPresenterBase, ITextData
    {
        public TextBlockPresenter()
        {
            this.Text = this.Name;
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.FontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
            this.FontFamily = Application.Current.FindResource(SystemKeys.FontFamily) as FontFamily;
            this.FontStyle = FontStyles.Normal;
            this.FontWeight = FontWeights.Normal;
            this.FontStretch = FontStretches.Normal;
            this.Foreground = Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush;
            //this.Height = (double)Application.Current.FindResource(SystemKeys.ItemHeight);
            this.Foreground = Brushes.Black;
            this.Height = DesignSetting.Instance.RowHeight;
            this.VerticalContentAlignment = VerticalAlignment.Center;
        }
        private string _text;
        [Display(Name = "文本", GroupName = "常用,数据")]
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged();
            }
        }

        private double _fontSize;
        [Display(Name = "字号", GroupName = "常用,样式")]
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private FontFamily _fontFamily;
        [Display(Name = "字体", GroupName = "样式")]
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set
            {
                _fontFamily = value;
                RaisePropertyChanged();
            }
        }


        private FontStyle _fontStyle;
        [Display(Name = "字体样式", GroupName = "样式")]
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set
            {
                _fontStyle = value;
                RaisePropertyChanged();
            }
        }

        private FontWeight _fontWeight;
        [Display(Name = "字体加粗", GroupName = "样式")]
        public FontWeight FontWeight
        {
            get { return _fontWeight; }
            set
            {
                _fontWeight = value;
                RaisePropertyChanged();
            }
        }

        private FontStretch _fontStretch;
        [Display(Name = "字体展开", GroupName = "样式")]
        public FontStretch FontStretch
        {
            get { return _fontStretch; }
            set
            {
                _fontStretch = value;
                RaisePropertyChanged();
            }
        }

        private Brush _foreground;
        [DefaultValue(null)]
        [Display(Name = "文本颜色", GroupName = "常用,样式")]
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "标题", Icon = "\xe71f")]
    public class TitlePresenter : TextBlockPresenter
    {
        public TitlePresenter()
        {
            this.Title = "我是标题：";
            this.Text = "我是数据";

        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.TitleFontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
            this.TitleFontWeight = FontWeights.Normal;
            //this.TitleForeground = Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush;
            this.TitleForeground = Brushes.Black;

        }

        private string _title;
        [Display(Name = "文本颜色", GroupName = "常用,数据")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private double _titleFontSize;
        [Display(Name = "抬头字号", GroupName = "常用,样式")]
        public double TitleFontSize
        {
            get { return _titleFontSize; }
            set
            {
                _titleFontSize = value;
                RaisePropertyChanged();
            }
        }

        private Brush _titleForeground;
        [DefaultValue(null)]
        [Display(Name = "抬头颜色", GroupName = "常用,样式")]
        public Brush TitleForeground
        {
            get { return _titleForeground; }
            set
            {
                _titleForeground = value;
                RaisePropertyChanged();
            }
        }

        private FontWeight _titleFontWeight;
        [Display(Name = "抬头加粗", GroupName = "常用,样式")]
        public FontWeight TitleFontWeight
        {
            get { return _titleFontWeight; }
            set
            {
                _titleFontWeight = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "当前日期", Icon = Icons.Clock)]
    public class DateTimeNowPresenter : TitlePresenter
    {
        public DateTimeNowPresenter()
        {
            this.Title = "当前日期：";
            this.Text = DateTime.Now.ToString(this.Format);
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
        }

        private string _format = "yyyy-MM-dd HH:mm:ss";
        [Display(Name = "日期格式", GroupName = "常用,样式")]
        public string Format
        {
            get { return _format; }
            set
            {
                _format = value;
                RaisePropertyChanged();
            }
        }

    }

    [Displayer(Name = "当前用户", Icon = "\xe84b")]
    public class UserPresenter : TitlePresenter
    {
        public UserPresenter()
        {
            this.Title = "当前用户：";
            this.Text = Loginner.Instance?.User?.Name;
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
        }
    }

    [Displayer(Name = "卡片", Icon = "\xe71e")]
    public class CardPresenter : TitlePresenter
    {
        public CardPresenter()
        {
            this.Title = "总计：";
            this.Text = "80.34%";
            this.Height = 80.0;
        }
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.FromColor = Colors.Orange;
            this.ToColor = Colors.OrangeRed;
            this.DropShadowEffectOpacity = 0.5;
            this.Height = 80;
            this.Width = 200;
            this.Padding = new Thickness(10);
            this.Margin = new Thickness(10);
            this.CornerRadius = new CornerRadius(5);
            this.Foreground = Brushes.White;
            this.TitleForeground = Brushes.White;
            this.FontSize = 25;
            this.TitleFontSize = 15;
            this.HorizontalContentAlignment = HorizontalAlignment.Center;
            this.VerticalContentAlignment = VerticalAlignment.Center;
            this.Orientation = Orientation.Horizontal;
        }
        private Color _fromColor;
        [Display(Name = "起始颜色", GroupName = "常用,样式")]
        public Color FromColor
        {
            get { return _fromColor; }
            set
            {
                _fromColor = value;
                RaisePropertyChanged();
            }
        }

        private Color _toColor;
        [Display(Name = "终止颜色", GroupName = "常用,样式")]
        public Color ToColor
        {
            get { return _toColor; }
            set
            {
                _toColor = value;
                RaisePropertyChanged();
            }
        }

        private double _dropShadowEffectOpacity;
        [Display(Name = "阴影透明度", GroupName = "常用,样式")]
        public double DropShadowEffectOpacity
        {
            get { return _dropShadowEffectOpacity; }
            set
            {
                _dropShadowEffectOpacity = value;
                RaisePropertyChanged();
            }
        }


        private CornerRadius _CornerRadius;
        [Display(Name = "圆角", GroupName = "常用,样式")]
        public CornerRadius CornerRadius
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                RaisePropertyChanged();
            }
        }

        private Orientation _orientation;
        [Display(Name = "对齐方式", GroupName = "常用,样式")]
        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                _orientation = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "图片", Icon = "\xe606")]
    public class ImagePresenter : CommandsDesignPresenterBase
    {
        public ImagePresenter()
        {
            this.ImageSource = Application.Current.FindResource(ImageSourceKeys.Logo) as ImageSource;
            this.Width = 200;
            this.Height = 80;
        }
        private ImageSource _imageSource;
        [Display(Name = "图像资源", GroupName = "常用,数据")]
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChanged();
            }
        }

        private Stretch _stretch = Stretch.Uniform;
        [Display(Name = "拉伸方式", GroupName = "常用,布局")]
        public Stretch Stretch
        {
            get { return _stretch; }
            set
            {
                _stretch = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "Uniform", Icon = "\xe734")]
    public class UniformGridPresenter : PanelPresenterBase
    {
        private int _columns;
        [Display(Name = "列数", GroupName = "常用,布局")]
        public int Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;
                RaisePropertyChanged();
            }
        }

        private int _rows;
        [Display(Name = "行数", GroupName = "常用,布局")]
        public int Rows
        {
            get { return _rows; }
            set
            {
                _rows = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "属性列表", Icon = "\xe740")]
    public class PropertyGridDesignPresenter : CommandsDesignPresenterBase
    {
        private object _data;
        [Display(Name = "数据源", GroupName = "常用,数据")]
        public object Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }
    }

    [Displayer(Name = "GridArea", Icon = "\xe921")]
    public class GridAreaPresenter : PanelPresenterBase
    {

    }

    [Displayer(Name = "DataGrid", Icon = "\xe890")]
    public class DataGridDesignPresenter : DataGridPresenterBase, IDesignPresenter
    {
        public DataGridDesignPresenter()
        {
            this.ColumnSpan = 12;
        }
    }
}
