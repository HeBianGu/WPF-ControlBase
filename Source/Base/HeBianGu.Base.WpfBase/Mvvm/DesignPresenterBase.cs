// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HeBianGu.Base.WpfBase
{
    public abstract class DesignPresenterBase : DisplayerViewModelBase, ICloneable
    {
        private bool _isSelected;
        [Browsable(false)]
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        private bool _isMouseOver;
        [Browsable(false)]
        [XmlIgnore]
        public bool IsMouseOver
        {
            get { return _isMouseOver; }
            set
            {
                _isMouseOver = value;
                RaisePropertyChanged();
            }
        }


        private double _height = double.NaN;
        [Display(Name = "高", GroupName = "常用,布局")]
        [TypeConverter(typeof(LengthConverter))]
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged();
            }
        }

        private double _width = double.NaN;
        [Display(Name = "宽", GroupName = "常用,布局")]
        [TypeConverter(typeof(LengthConverter))]
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }

        private double _minHeight = double.NaN;
        [Display(Name = "最小高度", GroupName = "布局")]
        [TypeConverter(typeof(LengthConverter))]
        public double MinHeight
        {
            get { return _minHeight; }
            set
            {
                _minHeight = value;
                RaisePropertyChanged();
            }
        }

        private double _minWidth = double.NaN;
        [Display(Name = "最小宽度", GroupName = "布局")]
        [TypeConverter(typeof(LengthConverter))]
        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                RaisePropertyChanged();
            }
        }

        private Thickness _margin = new Thickness();
        [Display(Name = "外部间距", GroupName = "布局")]
        /// <summary> 说明  </summary>
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
        [Display(Name = "内部间距", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public Thickness Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                RaisePropertyChanged();
            }
        }

        private HorizontalAlignment _horizontalAlignment = HorizontalAlignment.Stretch;
        [Display(Name = "水平对齐", GroupName = "布局")]
        /// <summary> 说明  </summary>
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
        [Display(Name = "水平内容对齐", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public HorizontalAlignment HorizontalContentAlignment
        {
            get { return _horizontalContentAlignment; }
            set
            {
                _horizontalContentAlignment = value;
                RaisePropertyChanged();
            }
        }

        private VerticalAlignment _verticalAlignment = VerticalAlignment.Stretch;
        [Display(Name = "垂直对齐", GroupName = "布局")]
        /// <summary> 说明  </summary>
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
        [Display(Name = "垂直内部对齐", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public VerticalAlignment VerticalContentAlignment
        {
            get { return _verticalContentAlignment; }
            set
            {
                _verticalContentAlignment = value;
                RaisePropertyChanged();
            }
        }


        private Brush _background;
        [Display(Name = "背景颜色", GroupName = "样式")]
        /// <summary> 说明  </summary>
        public Brush Background
        {
            get { return _background; }
            set
            {
                _background = value;
                RaisePropertyChanged();
            }
        }

        private Brush _borderBrush;
        [Display(Name = "边框颜色", GroupName = "样式")]
        /// <summary> 说明  </summary>
        public Brush BorderBrush
        {
            get { return _borderBrush; }
            set
            {
                _borderBrush = value;
                RaisePropertyChanged();
            }
        }

        private Thickness _borderThickness;
        [Display(Name = "边框粗细", GroupName = "样式")]
        /// <summary> 说明  </summary>
        public Thickness BorderThickness
        {
            get { return _borderThickness; }
            set
            {
                _borderThickness = value;
                RaisePropertyChanged();
            }
        }

        private double _opacity = 1;
        [Display(Name = "透明度", GroupName = "样式")]
        /// <summary> 说明  </summary>
        public double Opacity
        {
            get { return _opacity; }
            set
            {
                _opacity = value;
                RaisePropertyChanged();
            }
        }

        private bool _isVisible = true;
        [Display(Name = "可见", GroupName = "样式")]
        /// <summary> 说明  </summary>
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabled = true;
        [Display(Name = "可用", GroupName = "样式")]
        /// <summary> 说明  </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }


        private int _row;
        [Display(Name = "行", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                RaisePropertyChanged();
            }
        }


        private int _column;
        [Display(Name = "列", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public int Column
        {
            get { return _column; }
            set
            {
                _column = value;
                RaisePropertyChanged();
            }
        }


        private int _rowSpan;
        [Display(Name = "行跨距", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public int RowSpan
        {
            get { return _rowSpan; }
            set
            {
                _rowSpan = value;
                RaisePropertyChanged();
            }
        }

        private int _columnSpan;
        [Display(Name = "列跨距", GroupName = "布局")]
        /// <summary> 说明  </summary>
        public int ColumnSpan
        {
            get { return _columnSpan; }
            set
            {
                _columnSpan = value;
                RaisePropertyChanged();
            }
        }

        [Displayer(Name = "删除", Icon = Icons.Clock)]
        public RelayCommand DeleteCommand => new RelayCommand((s, e) =>
        {
            this.Delete(e);
        });

        protected virtual void Delete(object e)
        {
            if (e is ContentControl project)
            {
                var adorner = project.GetParent<Adorner>();
                var source = adorner.AdornedElement.GetParent<ItemsControl>();
                source.GetItemsSource<IList>().Remove(project.Content);
            }
        }

        public virtual object Clone()
        {
            //return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
            return this.CloneXml();
        }
    }

    public abstract class DataGridPresenterBase : DesignPresenterBase
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.GridHeaderBackground = Brushes.Black;
            this.GridHeaderForeground = Brushes.White;
            this.HorizontalGridLinesBrush = Brushes.LightGray;
            this.VerticalGridLinesBrush = Brushes.LightGray;
            this.GridForeground = Brushes.Black;
            this.GridBackground = Brushes.White;
            this.AlternatingRowBackground = Brushes.WhiteSmoke;
            this.ColumnHeaderHeight = 40;
            this.RowHeight = 35;
            this.GridBorderBrush = Brushes.LightGray;
            this.ColumnHorizontalContentAlignment = HorizontalAlignment.Left;
            this.CellHorizontalContentAlignment = HorizontalAlignment.Left;
        }

        private Brush _gridForeground;
        [Display(Name = "表格文本颜色", GroupName = "样式")]
        public Brush GridForeground
        {
            get { return _gridForeground; }
            set
            {
                _gridForeground = value;
                RaisePropertyChanged();
            }
        }


        private Brush _gridBackground;
        [Display(Name = "表格背景色", GroupName = "样式")]
        public Brush GridBackground
        {
            get { return _gridBackground; }
            set
            {
                _gridBackground = value;
                RaisePropertyChanged();
            }
        }


        private Brush _gridHeaderBackground;
        [Display(Name = "列头背景色", GroupName = "样式")]
        public Brush GridHeaderBackground
        {
            get { return _gridHeaderBackground; }
            set
            {
                _gridHeaderBackground = value;
                RaisePropertyChanged();
            }
        }


        private Brush _gridHeaderForeground;
        [Display(Name = "列头文本色", GroupName = "样式")]
        public Brush GridHeaderForeground
        {
            get { return _gridHeaderForeground; }
            set
            {
                _gridHeaderForeground = value;
                RaisePropertyChanged();
            }
        }

        private Brush _gridborderBrush;
        [Display(Name = "表格边框颜色", GroupName = "样式")]
        public Brush GridBorderBrush
        {
            get { return _gridborderBrush; }
            set
            {
                _gridborderBrush = value;
                RaisePropertyChanged();
            }
        }

        private Brush _alternatingRowBackground;
        [Display(Name = "表格换行色", GroupName = "样式")]
        public Brush AlternatingRowBackground
        {
            get { return _alternatingRowBackground; }
            set
            {
                _alternatingRowBackground = value;
                RaisePropertyChanged();
            }
        }


        private Brush _verticalGridLinesBrush;
        [Display(Name = "垂直分割线色", GroupName = "样式")]
        public Brush VerticalGridLinesBrush
        {
            get { return _verticalGridLinesBrush; }
            set
            {
                _verticalGridLinesBrush = value;
                RaisePropertyChanged();
            }
        }


        private Brush _horizontalGridLinesBrush;
        [Display(Name = "水平分割线色", GroupName = "样式")]
        public Brush HorizontalGridLinesBrush
        {
            get { return _horizontalGridLinesBrush; }
            set
            {
                _horizontalGridLinesBrush = value;
                RaisePropertyChanged();
            }
        }


        private HorizontalAlignment _columnHorizontalContentAlignment;
        [Display(Name = "列头水平停靠", GroupName = "常用,样式")]
        public HorizontalAlignment ColumnHorizontalContentAlignment
        {
            get { return _columnHorizontalContentAlignment; }
            set
            {
                _columnHorizontalContentAlignment = value;
                RaisePropertyChanged();
            }
        }

        private HorizontalAlignment _cellHorizontalContentAlignment;
        [Display(Name = "单元水平停靠", GroupName = "常用,样式")]
        public HorizontalAlignment CellHorizontalContentAlignment
        {
            get { return _cellHorizontalContentAlignment; }
            set
            {
                _cellHorizontalContentAlignment = value;
                RaisePropertyChanged();
            }
        }

        private int _columnHeaderHeight;
        [Display(Name = "表格列高", GroupName = "常用,样式")]
        public int ColumnHeaderHeight
        {
            get { return _columnHeaderHeight; }
            set
            {
                _columnHeaderHeight = value;
                RaisePropertyChanged();
            }
        }

        private int _rowHeight;
        [Display(Name = "表格行高", GroupName = "常用,样式")]
        public int RowHeight
        {
            get { return _rowHeight; }
            set
            {
                _rowHeight = value;
                RaisePropertyChanged();
            }
        }


        private IEnumerable _itemsSource;
        [XmlIgnore]
        [Browsable(false)]
        public IEnumerable ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ColumnPropertyInfo> _columnPropertyInfos = new ObservableCollection<ColumnPropertyInfo>();
        [XmlIgnore]
        [ReadOnly(true)]
        [Display(Name = "列头设置", GroupName = "常用,数据")]
        public ObservableCollection<ColumnPropertyInfo> ColumnPropertyInfos
        {
            get { return _columnPropertyInfos; }
            set
            {
                _columnPropertyInfos = value;
                RaisePropertyChanged();
            }
        }

        public void Refresh()
        {
            this.ColumnPropertyInfos = this.ColumnPropertyInfos.ToObservable();
        }
    }

    [Displayer(Name = "列头设置")]
    public class ColumnPropertyInfo : NotifyPropertyChangedBase
    {
        public ColumnPropertyInfo(PropertyInfo t)
        {
            var display = t.GetCustomAttribute<DisplayAttribute>();
            this.Header = display?.Name ?? t.Name;
            this.PropertyInfo = t;
        }
        [XmlIgnore]
        [Browsable(false)]
        public PropertyInfo PropertyInfo { get; }

        private string _header;
        [Display(Name = "列名")]
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChanged();
            }
        }

        //private double _width = double.NaN;
        //[Display(Name = "列宽")]
        //public double Width
        //{
        //    get { return _width; }
        //    set
        //    {
        //        _width = value;
        //        RaisePropertyChanged();
        //    }
        //}

        //private int _displayIndex;
        //[Display(Name = "排序")]
        //public int DisplayIndex
        //{
        //    get { return _displayIndex; }
        //    set
        //    {
        //        _displayIndex = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private bool _isVisible;
        [Display(Name = "显示")]
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                RaisePropertyChanged();
            }
        }

        public override string ToString()
        {
            string v = this.IsVisible ? "[显示]" : "[隐藏]";
            return $"列名：{v} {this.Header}";
        }
    }
}
