// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 说明</summary>
    public class ChartData : NotifyPropertyChangedBase
    {
        private ObservableCollection<string> _xDisplay = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> xDisplay
        {
            get { return _xDisplay; }
            set
            {
                _xDisplay = value;
                RaisePropertyChanged();
            }
        }


        private ObservableCollection<string> _yDisplay = new ObservableCollection<string>();
        /// <summary> 说明  </summary>
        public ObservableCollection<string> yDisplay
        {
            get { return _yDisplay; }
            set
            {
                _yDisplay = value;
                RaisePropertyChanged();
            }
        }

        private DoubleCollection _xAxis = new DoubleCollection();
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public DoubleCollection yAxis
        {
            get { return _yAxis; }
            set
            {
                _yAxis = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ISeriesData> _seriesDatas = new ObservableCollection<ISeriesData>();
        public ObservableCollection<ISeriesData> SeriesDatas
        {
            get { return _seriesDatas; }
            set
            {
                _seriesDatas = value;
                RaisePropertyChanged();
            }
        }

        public void Build(IEnumerable<Tuple<double, double>> collection)
        {
            this.xAxis = new DoubleCollection(collection.Select(x => x.Item1));
            //PointData seriesData = new PointData();
            //seriesData.Build(collection);
            //this.SeriesDatas.Add(seriesData);
            if (collection.Count() > 0)
                this.BuildyAxis(collection.Min(x => x.Item2), collection.Max(x => x.Item2));
        }

        public void Build(IEnumerable<Tuple<DateTime, double>> collection, string format = "yyyy-MM-dd")
        {
            this.xAxis = new DoubleCollection(collection.Select(x => (double)x.Item1.Ticks));
            this.xDisplay = new ObservableCollection<string>(collection.Select(x => x.Item1.ToString(format)));
            //PointData seriesData = new PointData();
            //seriesData.Build(collection);
            //this.SeriesDatas.Add(seriesData);
            if (collection.Count() > 0)
                this.BuildyAxis(collection.Min(x => x.Item2), collection.Max(x => x.Item2));
        }

        public void Build(IEnumerable<Tuple<string, double>> collection)
        {
            this.xAxis = new DoubleCollection(Enumerable.Range(0, collection.Count()).Select(x => (double)x));
            this.xDisplay = new ObservableCollection<string>(collection.Select(x => x.Item1));
            //PointData seriesData = new PointData();
            //seriesData.Build(collection);
            //this.SeriesDatas.Add(seriesData);
            //this.yAxis.Add(collection.Min(x => x.Item2) - 1);
            //this.yAxis.Add(collection.Max(x => x.Item2) + 1);

            if (collection.Count() > 0)
                this.BuildyAxis(collection.Min(x => x.Item2), collection.Max(x => x.Item2));
        }


        public void BuildyAxis(double min, double max, int count = 5)
        {
            this.yAxis.Clear();
            if (min >= max) return;
            double span = ((max - min) * 1.0 / (count - 1));
            double current = min;
            while (true)
            {
                if (current >= max)
                {
                    this.yAxis.Add(current);
                    break;
                }

                this.yAxis.Add(current);
                current += span;
            }
        }
    }

    public interface ISeriesData
    {

    }


    public class PointData : NotifyPropertyChangedBase, ISeriesData
    {
        private DoubleCollection _yDatas = new DoubleCollection();
        public DoubleCollection yDatas
        {
            get { return _yDatas; }
            set
            {
                _yDatas = value;
                RaisePropertyChanged();
            }
        }

        private DoubleCollection _xDatas = new DoubleCollection();
        public DoubleCollection xDatas
        {
            get { return _xDatas; }
            set
            {
                _xDatas = value;
                RaisePropertyChanged();
            }
        }

        void Clear()
        {
            this.xDatas.Clear();
            this.yDatas.Clear();
        }


        public void Build(IEnumerable<Tuple<double, double>> collection)
        {
            this.Clear();
            foreach (var item in collection)
            {
                this.xDatas.Add(item.Item1);
                this.yDatas.Add(item.Item2);
            }
        }

        public void Build(IEnumerable<Tuple<DateTime, double>> collection)
        {
            this.Clear();
            foreach (var item in collection)
            {
                this.xDatas.Add(item.Item1.Ticks);
                this.yDatas.Add(item.Item2);
            }
        }

        public void Build(IEnumerable<Tuple<string, double>> collection)
        {
            this.Clear();
            int index = 0;
            foreach (var item in collection)
            {
                this.xDatas.Add(index);
                this.yDatas.Add(item.Item2);
                index++;
            }
        }
    }

    public class DoubleData : NotifyPropertyChangedBase, ISeriesData
    {
        private DoubleCollection _data = new DoubleCollection();
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
        public ObservableCollection<string> xDisplay
        {
            get { return _xDisplay; }
            set
            {
                _xDisplay = value;
                RaisePropertyChanged();
            }
        }

        public void Clear()
        {
            this.Data.Clear();
            this.xDisplay.Clear();
        }


        public void Build(IEnumerable<Tuple<double, string>> collection)
        {
            this.Clear();
            this.Data = new DoubleCollection(collection.Select(x => x.Item1));
            this.xDisplay = new ObservableCollection<string>(collection.Select(x => x.Item2));
        }
    }
}
