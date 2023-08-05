// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    [Displayer(Name = "雷达图", Icon = "\xe7e2")]
    public class RadarPresenter : Chart2DPresenterBase
    {
        public RadarPresenter()
        {
            this.Height = 500;
            var data = Enumerable.Range(0, 6).Select(x => x + random.NextDouble() * 10);
            this.RefreshData(data);
        }
        public RadarPresenter(IEnumerable<double> data) : this()
        {
            this.RefreshData(data);
        }

        public void RefreshData(IEnumerable<double> data)
        {
            this.LoadyAxis(data);
            this.LoadxAxis(data);
            this.Data = new DoubleCollection(data);
        }

        protected virtual void LoadyAxis(IEnumerable<double> data, double max = 360.0, double min = 0.0)
        {
            _yAxis.Clear();
            double span = 360.0 / data.Count();
            for (int i = 0; i < data.Count(); i++)
            {
                _yAxis.Add(span * i);
            }
        }

        protected virtual void LoadxAxis(IEnumerable<double> data, double max = double.PositiveInfinity, double min = 0.0, int count = 5)
        {
            var ds = this.Load(data, max, min, 5);
            this.xAxis = new DoubleCollection(ds);
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

        private ObservableCollection<Color> _foreground = new ObservableCollection<Color>();
        [Display(Name = "显示颜色", GroupName = "常用,数据")]
        [TypeConverter(typeof(BrushArrayTypeConverter))]
        public ObservableCollection<Color> Foreground
        {
            get { return _foreground; }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }
    }


}
