using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using HeBianGu.WPF.EChart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Applications.ControlBase.LinkWindow
{
    [ViewModel("Chart")]
    public class ChartViewModel : MvcViewModelBase
    {

        private List<ICurveEntitySource> _collection = new List<ICurveEntitySource>();
        /// <summary> 曲线图数据 </summary>
        public List<ICurveEntitySource> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        //void Init()
        //{
        //    RefreshCurveData();

        //    this.RefreshCardiogramCurve(); 
        //}

        private double _left = 0.1;
        /// <summary> 说明 </summary>
        public double Left
        {
            get { return _left; }
            set
            {
                _left = value;
                RaisePropertyChanged();
            }
        }

        private double _right = 0.7;
        /// <summary> 说明 </summary>
        public double Right
        {
            get { return _right; }
            set
            {
                _right = value;
                RaisePropertyChanged();
            }
        }

        public void RefreshCurveData()
        {

            List<ICurveEntitySource> collection = new List<ICurveEntitySource>();

            CurveEntitySource entity = new CurveEntitySource();
            entity.Text = "长度(km)";
            entity.Color = Brushes.Red;
            entity.Marker = new CirclePointMarker();

            entity.Marker.Fill = Brushes.Red;

            for (int i = 0; i < 20; i++)
            {
                PointC point = new PointC();
                point.X = i;
                point.Y = i * i;
                point.Text = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                entity.Source.Add(point);

                this.MinValue = this.MinValue > point.X ? point.X : this.MinValue;
                this.MaxValue = this.MaxValue < point.X ? point.X : this.MaxValue;

            }
            collection.Add(entity);

            entity = new CurveEntitySource();
            entity.Text = "重量(kg)";
            entity.Color = Brushes.Blue;
            entity.Marker = new T5PointMarker();

            entity.Marker.Fill = Brushes.Blue;

            for (int i = 0; i < 20; i++)
            {
                PointC point = new PointC();
                point.X = i;
                point.Y = (20 - i) * (20 - i);
                point.Text = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                entity.Source.Add(point);

                this.MinValue = this.MinValue > point.X ? point.X : this.MinValue;
                this.MaxValue = this.MaxValue < point.X ? point.X : this.MaxValue;

            }
            collection.Add(entity);

            this.Collection = collection;

        }

        private double _maxValue = double.MinValue;
        /// <summary> 说明 </summary>
        public double MaxValue
        {
            get { return _maxValue; }
            set
            {
                _maxValue = value;
                RaisePropertyChanged("MaxValue");
            }
        }

        private double _minValue = double.MaxValue;
        /// <summary> 说明 </summary>
        public double MinValue
        {
            get { return _minValue; }
            set
            {
                _minValue = value;
                RaisePropertyChanged("MinValue");
            }
        }

        private List<ICurveEntitySource> _cardiogramCollection = new List<ICurveEntitySource>();
        /// <summary> 心电图数据 </summary>
        public List<ICurveEntitySource> CardiogramCollection
        {
            get { return _cardiogramCollection; }
            set
            {
                _cardiogramCollection = value;
                RaisePropertyChanged("CardiogramCollection");
            }
        }


        public void RefreshCardiogramCurve()
        {
            this.StopFlag = false;

            string str = Properties.Resources.心电图;

            var collection = str.Split(',').ToList();

            List<string> cache = new List<string>();

            Action action = () =>
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    cache.Add(collection[i]);

                    if (this.StopFlag) break;

                    //if (i == collection.Count - 1)
                    //{
                    //    // Todo ：触发显示 
                    //    this.ShowLast(cache, 140, cache.Count);

                    //    break;
                    //}

                    if (i % 150 == 0)
                    {
                        Thread.Sleep(1000);
                    }

                    if (i % this._refreshCount == 0)
                    {

                        //while (cache.Count < this.ShowCount)
                        //{
                        //    cache.Insert(0, "2048");
                        //}

                        // Todo ：触发显示 
                        this.ShowLast(cache, 150, this.ShowCount);

                        Thread.Sleep(this.RefreshTime);
                    }


                }

                this.StopFlag = false;
            };

            Task task = new Task(action);
            task.Start();

        }


        private bool _stopFlag = false;
        /// <summary> 说明 </summary>
        public bool StopFlag
        {
            get { return _stopFlag; }
            set
            {
                _stopFlag = value;
                RaisePropertyChanged();
            }
        }

        /// <summary> 刷新显示最后的几条 </summary>
        public void ShowLast(List<string> collection, int xMargin = 150, int count = 600)
        {

            Func<int, double> convertFuncX = l =>
            {
                if (collection.Count < count)
                {
                    l = count - collection.Count + l;
                }

                return ((double)l / count) * xMargin + (150 - xMargin) / 2;
            };

            Func<double, double> convertFuncY = l =>
            {
                //return (50 * (l - 1848) + 0 * (2448 - l)) / (2448 - 1848);

                return ((l - 2048) / 150) * 10 + 50 / 2;
            };

            int total = collection.Count;

            int skip = total > count ? total - count : 0;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var cs = collection.Skip(skip).ToList();

                List<ICurveEntitySource> collections = new List<ICurveEntitySource>();

                CardiogramCurveEntitySource entity = new CardiogramCurveEntitySource();

                for (int i = 0; i < cs.Count; i++)
                {
                    PointC point = new PointC();

                    point.X = convertFuncX(i);

                    double d;
                    bool result = double.TryParse(cs[i], out d);
                    if (result)
                    {
                        point.Y = convertFuncY(d);
                        point.Text = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                        entity.Source.Add(point);
                    }
                }

                collections.Add(entity);

                this.CardiogramCollection = collections;
            });



        }

        private int _refreshTime = 500;
        /// <summary> 说明 </summary>
        public int RefreshTime
        {
            get { return _refreshTime; }
            set
            {
                _refreshTime = value;
                RaisePropertyChanged("RefreshTime");
            }
        }

        private int _refreshCount = 50;
        /// <summary> 说明 </summary>
        public int RefreshCount
        {
            get { return _refreshCount; }
            set
            {
                _refreshCount = value;
                RaisePropertyChanged("RefreshCount");
            }
        }

        private int _showCount = 750;
        /// <summary> 说明 </summary>
        public int ShowCount
        {
            get { return _showCount; }
            set
            {
                _showCount = value;
                RaisePropertyChanged("ShowCount");
            }
        }

        /// <summary> 命令通用方法 </summary>
        protected override  void RelayMethod(object obj)
        {
            string command = obj?.ToString();
            if (command == "RefreshCardiogramCurve")
            {
                this.RefreshCardiogramCurve();

            }
            else if (command == "StopCardiogramCurve")
            {
                this.StopFlag = true;
            }
            else if (command == "ValueChanged")
            {
                string str = string.Empty;

                var collection = str.Split(',').ToList();

                // Todo ：开始位置 
                int start = (int)((this.Left) * collection.Count);

                // Todo ：结束位置 
                int end = (int)((this.Right) * collection.Count);

                var items = collection.Skip(start).Take(end - start).ToList();

                this.ShowLast(items, 140, items.Count);
            }
            else if (command == "ClearCurve")
            {
                List<ICurveEntitySource> collection = new List<ICurveEntitySource>();
                this.MinValue = 0;
                this.MaxValue = 10;
                this.Collection = collection;
            }
            else if (command == "RefreshCurve")
            {
                this.RefreshCurveData();
            }
        }

        protected override void Loaded(string args)
        {
            base.Loaded("Curve");


            this.RefreshCurveData();
        }
    }
}
