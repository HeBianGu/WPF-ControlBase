// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public class ShapeStyle : NotifyPropertyChangedBase
    {
        public ShapeStyle()
        {
            this.Fill = Application.Current.FindResource(BrushKeys.BackgroundDefault) as Brush;
            this.Stroke = Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush;
        }
        private Brush _fill;
        [Display(Name = "填充颜色", GroupName = "基础信息", Order = 0)]
        public Brush Fill
        {
            get { return _fill; }
            set
            {
                _fill = value;
                RaisePropertyChanged();
            }
        }

        private Brush _stroke;
        [Display(Name = "线条颜色", GroupName = "基础信息", Order = 0)]
        public Brush Stroke
        {
            get { return _stroke; }
            set
            {
                _stroke = value;
                RaisePropertyChanged();
            }
        }

        private double _strokeThickness = 1;
        [Display(Name = "线条宽度", GroupName = "基础信息", Order = 0)]
        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                RaisePropertyChanged();
            }
        }

        private DoubleCollection _strokeDashArray = new DoubleCollection();
        [Display(Name = "线条间隙", GroupName = "基础信息", Order = 0)]
        public DoubleCollection StrokeDashArray
        {
            get { return _strokeDashArray; }
            set
            {
                _strokeDashArray = value;
                RaisePropertyChanged();
            }
        }


        private PenLineCap _strokeDashCap = PenLineCap.Flat;
        [Display(Name = "端点显示", GroupName = "基础信息", Order = 0)]
        public PenLineCap StrokeDashCap
        {
            get { return _strokeDashCap; }
            set
            {
                _strokeDashCap = value;
                RaisePropertyChanged();
            }
        }

        private Stretch _stretch = Stretch.None;
        [Display(Name = "拉伸方式", GroupName = "基础信息", Order = 0)]
        public Stretch Stretch
        {
            get { return _stretch; }
            set
            {
                _stretch = value;
                RaisePropertyChanged();
            }
        }


        private double _strokeDashOffset = 0.0;
        [Display(Name = "间隙偏移", GroupName = "基础信息", Order = 0)]
        public double StrokeDashOffset
        {
            get { return _strokeDashOffset; }
            set
            {
                _strokeDashOffset = value;
                RaisePropertyChanged();
            }
        }


        private PenLineCap _strokeEndLineCap = PenLineCap.Flat;
        [Display(Name = "终点显示", GroupName = "基础信息", Order = 0)]
        public PenLineCap StrokeEndLineCap
        {
            get { return _strokeEndLineCap; }
            set
            {
                _strokeEndLineCap = value;
                RaisePropertyChanged();
            }
        }


        private PenLineJoin _strokeLineJoin = PenLineJoin.Miter;
        [Display(Name = "线条连接", GroupName = "基础信息", Order = 0)]
        public PenLineJoin StrokeLineJoin
        {
            get { return _strokeLineJoin; }
            set
            {
                _strokeLineJoin = value;
                RaisePropertyChanged();
            }
        }


        private double _strokeMiterLimit = 0.0;
        [Display(Name = "尖角限制", GroupName = "基础信息", Order = 0)]
        public double StrokeMiterLimit
        {
            get { return _strokeMiterLimit; }
            set
            {
                _strokeMiterLimit = value;
                RaisePropertyChanged();
            }
        }


        private PenLineCap _strokeStartLineCap = PenLineCap.Flat;
        [Display(Name = "起点显示", GroupName = "基础信息", Order = 0)]
        public PenLineCap StrokeStartLineCap
        {
            get { return _strokeStartLineCap; }
            set
            {
                _strokeStartLineCap = value;
                RaisePropertyChanged();
            }
        }
    }

    public class TextElementStyle : NotifyPropertyChangedBase
    {
        private double _fontSize = (double)Application.Current.FindResource(FontSizeKeys.Default);
        [Display(Name = "字体大小", GroupName = "基础信息", Order = 0)]
        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private FontFamily _fontFamily = Application.Current.FindResource(SystemKeys.FontFamily) as FontFamily;
        [Display(Name = "字体类型", GroupName = "基础信息", Order = 0)]
        public FontFamily FontFamily
        {
            get { return _fontFamily; }
            set
            {
                _fontFamily = value;
                RaisePropertyChanged();
            }
        }


        private FontStyle _fontStyle = FontStyles.Normal;
        [Display(Name = "文本样式", GroupName = "基础信息", Order = 0)]
        public FontStyle FontStyle
        {
            get { return _fontStyle; }
            set
            {
                _fontStyle = value;
                RaisePropertyChanged();
            }
        }

        private FontWeight _fontWeight = FontWeights.Normal;
        [Display(Name = "文本粗细", GroupName = "基础信息", Order = 0)]
        public FontWeight FontWeight
        {
            get { return _fontWeight; }
            set
            {
                _fontWeight = value;
                RaisePropertyChanged();
            }
        }

        private FontStretch _fontStretch = FontStretches.Normal;
        [Display(Name = "文本拉伸", GroupName = "基础信息", Order = 0)]
        public FontStretch FontStretch
        {
            get { return _fontStretch; }
            set
            {
                _fontStretch = value;
                RaisePropertyChanged();
            }
        }
    }

    public class ControlStyle : TextElementStyle
    {
        private double _width = double.NaN;
        [Display(Name = "宽度", GroupName = "基础信息", Order = 0)]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }


        private double _height = double.NaN;
        [Display(Name = "高度", GroupName = "基础信息", Order = 0)]
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged();
            }
        }


        private Thickness _margin;
        [Display(Name = "外部边距", GroupName = "基础信息", Order = 0)]
        public Thickness Margin
        {
            get { return _margin; }
            set
            {
                _margin = value;
                RaisePropertyChanged();
            }
        }


        private Thickness _padding;
        [Display(Name = "内部边距", GroupName = "基础信息", Order = 0)]
        public Thickness Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                RaisePropertyChanged();
            }
        }


        private HorizontalAlignment _horizontalAlignment;
        [Display(Name = "水平对齐", GroupName = "基础信息", Order = 0)]
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _horizontalAlignment; }
            set
            {
                _horizontalAlignment = value;
                RaisePropertyChanged();
            }
        }


        private HorizontalAlignment _horizontalContentAlignment;
        [Display(Name = "水平内容对齐", GroupName = "基础信息", Order = 0)]
        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return _horizontalContentAlignment; }
            set
            {
                _horizontalContentAlignment = value;
                RaisePropertyChanged();
            }
        }



        private VerticalAlignment _verticalAlignment;
        [Display(Name = "垂直对齐", GroupName = "基础信息", Order = 0)]
        public VerticalAlignment VerticalAlignment
        {
            get { return _verticalAlignment; }
            set
            {
                _verticalAlignment = value;
                RaisePropertyChanged();
            }
        }


        private VerticalAlignment _verticalContentAlignment;
        [Display(Name = "垂直内容对齐", GroupName = "基础信息", Order = 0)]
        public VerticalAlignment VerticalContentAlignment
        {
            get { return _verticalContentAlignment; }
            set
            {
                _verticalContentAlignment = value;
                RaisePropertyChanged();
            }
        }


        private Brush _foreground = Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush;
        [Display(Name = "文本颜色", GroupName = "基础信息", Order = 0)]
        public Brush Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }

        private Brush _background = Application.Current.FindResource(BrushKeys.BackgroundDefault) as Brush;
        [Display(Name = "背景颜色", GroupName = "基础信息", Order = 0)]
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                RaisePropertyChanged();
            }
        }


        private Brush _borderBrush = Application.Current.FindResource(BrushKeys.BorderBrushDefault) as Brush;
        [Display(Name = "边框颜色", GroupName = "基础信息", Order = 0)]
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                RaisePropertyChanged();
            }
        }

        private double _borderThickness = 1.00;
        [Display(Name = "边框宽度", GroupName = "基础信息", Order = 0)]
        public double BorderThickness
        {
            get { return _borderThickness; }
            set
            {
                _borderThickness = value;
                RaisePropertyChanged();
            }
        }
    }
}
