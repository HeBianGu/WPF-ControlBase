// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace HeBianGu.Control.Chart2D
{
    public class Chart2DPresenterBase : DesignPresenterBase
    {
        public Chart2DPresenterBase()
        {
            this.Height = 200.0;
            this.RowSpan = 20;
            this.ColumnSpan = 20;
        }

        //public override void LoadDefault()
        //{
        //    base.LoadDefault();
        //    this.Foreground = Application.Current.FindResource(BrushKeys.ForegroundDefault) as Brush;
        //}

        protected static Random random = new Random();

        private bool _drawOnce;
        [Browsable(false)]
        [XmlIgnore]
        public bool drawOnce
        {
            get { return _drawOnce; }
            set
            {
                _drawOnce = value;
                RaisePropertyChanged();
            }
        }


        private DoubleCollection _data = new DoubleCollection();
        [Display(Name = "数据值", GroupName = "常用,数据")]
        public DoubleCollection Data
        {
            get { return _data; }
            set
            {
                _data = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> _xDisplay = new ObservableCollection<string>();
        [Display(Name = "显示名称", GroupName = "常用,数据")]
        [TypeConverter(typeof(DisplayTypeConverter))]
        public ObservableCollection<string> xDisplay
        {
            get { return _xDisplay; }
            set
            {
                _xDisplay = value;
                RaisePropertyChanged();
            }
        }

        private bool _usexAxis = true;
        [Display(Name = "显示x坐标", GroupName = "常用,样式")]
        public bool UsexAxis
        {
            get { return _usexAxis; }
            set
            {
                _usexAxis = value;
                RaisePropertyChanged();
            }
        }


        private bool _useyAxis = true;
        [Display(Name = "显示y坐标", GroupName = "常用,样式")]
        public bool UseyAxis
        {
            get { return _useyAxis; }
            set
            {
                _useyAxis = value;
                RaisePropertyChanged();
            }
        }


        private bool _useGrid = true;
        [Display(Name = "显示网格", GroupName = "常用,样式")]
        public bool UseGrid
        {
            get { return _useGrid; }
            set
            {
                _useGrid = value;
                RaisePropertyChanged();
            }
        }

        //private Brush _foreground;
        //[Display(Name = "线条颜色", GroupName = "常用,样式")]
        //public Brush Foreground
        //{
        //    get { return _foreground; }
        //    set
        //    {
        //        _foreground = value;
        //        RaisePropertyChanged();
        //    }
        //}


        protected IEnumerable<double> Load(IEnumerable<double> data, double max = double.PositiveInfinity, double min = 0.0, int count = 5)
        {
            max = max == double.PositiveInfinity ? data.Max() : max;
            min = min == double.PositiveInfinity ? data.Min() : min;
            double vSpan = (max - min) / count;
            double current = min;
            while (max - current > -0.0001)
            {
                yield return current;
                var ssss = max - current;
                if (ssss <= 0)
                    break;
                current += vSpan;
            }
        }
    }


}
