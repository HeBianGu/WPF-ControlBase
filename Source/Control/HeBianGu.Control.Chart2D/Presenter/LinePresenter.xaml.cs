// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    [Displayer(Name = "折线图", Icon = "\xe7e5")]
    public class LinePresenter : Chart2DPresenterBase
    {
        public LinePresenter() : this(Enumerable.Range(0, 10).Select(x => random.NextDouble() * 10))
        {

        }
        public LinePresenter(IEnumerable<double> data)
        {
            this.RefreshData(data);
            this.Height = 300.0;
        }

        public void RefreshData(IEnumerable<double> data)
        {
            this.LoadyAxis(data);
            this.LoadxAxis(data);
            this.Data = new DoubleCollection(data);
        }

        public virtual void LoadyAxis(IEnumerable<double> data, double max = double.PositiveInfinity, double min = 0.0, int count = 5)
        {
            var ds = this.Load(data, max, min, 5);
            this.yAxis = new DoubleCollection(ds);
        }

        public virtual void LoadxAxis(IEnumerable<double> data, double max = double.PositiveInfinity, double min = 0.0, int count = 5)
        {
            this.xAxis = new DoubleCollection(Enumerable.Range(0, data.Count()).Select(x => (double)x));
        }

        private DoubleCollection _xAxis = new DoubleCollection();
        [Display(Name = "X轴", GroupName = "常用,数据")]
        public DoubleCollection xAxis
        {
            get { return _xAxis; }
            set
            {
                _xAxis = value;
                RaisePropertyChanged();
            }
        }

        private DoubleCollection _yAxis = new DoubleCollection();
        [Display(Name = "Y轴", GroupName = "常用,数据")]
        public DoubleCollection yAxis
        {
            get { return _yAxis; }
            set
            {
                _yAxis = value;
                RaisePropertyChanged();
            }
        }

        //private ObservableCollection<Color> _foreground = new ObservableCollection<Color>();
        //[Display(Name = "显示颜色", GroupName = "常用,数据")]
        //[TypeConverter(typeof(BrushArrayTypeConverter))]
        //public ObservableCollection<Color> Foreground
        //{
        //    get { return _foreground; }
        //    set
        //    {
        //        _foreground = value;
        //        RaisePropertyChanged();
        //    }
        //}

        private bool _useAverageMarkLine = false;
        [Display(Name = "均值标记线", GroupName = "常用,样式")]
        public bool UseAverageMarkLine
        {
            get { return _useAverageMarkLine; }
            set
            {
                _useAverageMarkLine = value;
                RaisePropertyChanged();
            }
        }

        private bool _useMinMarkLine = false;
        [Display(Name = "下限标记线", GroupName = "常用,样式")]
        public bool UseMinMarkLine
        {
            get { return _useMinMarkLine; }
            set
            {
                _useMinMarkLine = value;
                RaisePropertyChanged();
            }
        }

        private bool _useMaxMarkLine = false;
        [Display(Name = "上限标记线", GroupName = "常用,样式")]
        public bool UseMaxMarkLine
        {
            get { return _useMaxMarkLine; }
            set
            {
                _useMaxMarkLine = value;
                RaisePropertyChanged();
            }
        }

        private bool _useMinMarkPositon = false;
        [Display(Name = "最小值标记点", GroupName = "常用,样式")]
        public bool UseMinMarkPositon
        {
            get { return _useMinMarkPositon; }
            set
            {
                _useMinMarkPositon = value;
                RaisePropertyChanged();
            }
        }

        private bool _useMaxMarkPositon = false;
        [Display(Name = "最大值标记点", GroupName = "常用,样式")]
        public bool UseMaxMarkPositon
        {
            get { return _useMaxMarkPositon; }
            set
            {
                _useMaxMarkPositon = value;
                RaisePropertyChanged();
            }
        }
        private bool _useLastMarkPositon = false;
        [Display(Name = "显示最新值标记点", GroupName = "常用,样式")]
        public bool UseLastMarkPositon
        {
            get { return _useLastMarkPositon; }
            set
            {
                _useLastMarkPositon = value;
                RaisePropertyChanged();
            }
        }

        private double _minValue = double.NaN;
        [Display(Name = "下限", GroupName = "常用,样式")]
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                RaisePropertyChanged();
            }
        }

        private double _maxValue = double.NaN;
        [Display(Name = "上限", GroupName = "常用,样式")]
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                RaisePropertyChanged();
            }
        }
    }

    public class DynimacLinePresenter : LinePresenter
    {
        public DynimacLinePresenter()
        {
            this.UseyAxis = false;
            this.UseyAxis = false;
            this.UseLastMarkPositon = true;
        }
    }
}
