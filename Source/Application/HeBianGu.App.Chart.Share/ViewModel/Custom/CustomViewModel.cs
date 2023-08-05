using HeBianGu.Base.WpfBase;
using HeBianGu.Service.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace HeBianGu.App.Chart
{
    [ViewModel("Custom")]
    internal class CustomViewModel : MvcViewModelBase
    {
        private DoubleCollection _waveyAxis = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection WaveyAxis
        {
            get { return _waveyAxis; }
            set
            {
                _waveyAxis = value;
                RaisePropertyChanged("WaveyAxis");
            }
        }

        private DoubleCollection _waveData = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection WaveData
        {
            get { return _waveData; }
            set
            {
                _waveData = value;
                RaisePropertyChanged("WaveData");
            }
        }

        private DoubleCollection _waveData1 = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection WaveData1
        {
            get { return _waveData1; }
            set
            {
                _waveData1 = value;
                RaisePropertyChanged("WaveData1");
            }
        }

        private DoubleCollection _waveData2 = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection WaveData2
        {
            get { return _waveData2; }
            set
            {
                _waveData2 = value;
                RaisePropertyChanged("WaveData2");
            }
        }


        private DoubleCollection _animationBarSource = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection AnimationBarSource
        {
            get { return _animationBarSource; }
            set
            {
                _animationBarSource = value;
                RaisePropertyChanged("AnimationBarSource");
            }
        }



        private DoubleCollection _datas = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection Datas
        {
            get { return _datas; }
            set
            {
                _datas = value;
                RaisePropertyChanged("Datas");
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
                RaisePropertyChanged("xAxis");
            }
        }

        private string _year;
        /// <summary> 说明  </summary>
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged("Year");
            }
        }


        //private bool _gotoRefresh;
        ///// <summary> 说明  </summary>
        //public bool GotoRefresh
        //{
        //    get { return _gotoRefresh; }
        //    set
        //    {
        //        _gotoRefresh = value;
        //        RaisePropertyChanged("GotoRefresh");
        //    }
        //}


        private Random random = new Random();

        protected override void Init()
        {
            List<Func<double, double>> waves = new List<Func<double, double>>
            {
                l => 1 * Math.Sin(l),
                l => 2 * Math.Sin(l / 2),
                l => 3 * Math.Sin(l * 2),
                l => 4 * Math.Sin(l / 3),
                l => 5 * Math.Sin(l * 3),
                l => 1 * Math.Sin(l / 4),
                l => 2 * Math.Sin(l * 4),
                l => 3 * Math.Sin(l / 5),
                l => 4 * Math.Sin(l * 5),
                l => 5 * Math.Sin(l / 6),
                l => 1 * Math.Sin(l * 6),
                l => 2 * Math.Sin(l / 7),
                l => 3 * Math.Sin(l * 7),
                l => 4 * Math.Sin(l / 8),
                l => 5 * Math.Sin(l * 8),

                l => 1 * Math.Cos(l),
                l => 2 * Math.Cos(l / 2),
                l => 3 * Math.Cos(l * 2),
                l => 4 * Math.Cos(l / 3),
                l => 5 * Math.Cos(l * 3),
                l => 1 * Math.Cos(l / 4),
                l => 2 * Math.Cos(l * 4),
                l => 3 * Math.Cos(l / 5),
                l => 4 * Math.Cos(l * 5),
                l => 5 * Math.Cos(l / 6),
                l => 1 * Math.Cos(l * 6),
                l => 2 * Math.Cos(l / 7),
                l => 3 * Math.Cos(l * 7),
                l => 4 * Math.Cos(l / 8),
                l => 5 * Math.Cos(l * 8)
            };

            double fz = 150;

            double center = 1800;

            double weight = 90;

            double offsite = 90;


            Task.Run(() =>
            {
                double param = -5;

                while (true)
                {
                    List<double> data = new List<double>();

                    List<double> data1 = new List<double>();

                    List<double> data2 = new List<double>();

                    List<double> axis = new List<double>();

                    for (double i = 0; i <= 360 * 10; i = i + 1)
                    {
                        int count = random.Next(3, waves.Count);

                        List<Func<double, double>> find = Enumerable.Range(0, count).Select(l => waves[l])?.ToList();

                        Func<double, double> func = l =>
                        {
                            double result = 0;

                            find.ForEach(k => result = result + k.Invoke(l));

                            return result;
                        };

                        axis.Add(i);

                        double t = i / 360 * Math.PI;

                        double value = func(t);

                        data.Add(func(t));

                        data1.Add(Math.Sin((i * param) / 360 * Math.PI) * 50);

                        double wave = i > center - weight && i < center + weight ? Math.Sin(((i + offsite) * 2) / 360 * Math.PI) * fz : 0;

                        data2.Add(wave + value);
                    }

                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        WaveyAxis = new DoubleCollection(axis);
                        WaveData = new DoubleCollection(data);
                        WaveData1 = new DoubleCollection(data1);
                        WaveData2 = new DoubleCollection(data2);
                    });

                    param = param + 0.01;

                    if (param > 5) param = -5;

                    Thread.Sleep(100);

                    //System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    // {
                    //     this.GotoRefresh = true;
                    // });

                }
            });


            List<double> source = new List<double>();

            int index = 0;

            Task.Run(() =>
            {
                while (true)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (source.Count == 12)
                        {
                            source[i] = source[i] + random.NextDouble() - 0.5;

                            if (source[i] > 10)
                            {
                                source[i] = 10;
                            }

                            if (source[i] < 0)
                            {
                                source[i] = 0;
                            }
                        }
                        else
                        {
                            source.Add(random.Next(1, 10));
                        }
                    }



                    System.Windows.Application.Current?.Dispatcher.Invoke(() =>
                       {
                           AnimationBarSource = new DoubleCollection(source);
                       });


                    if (index > 500) index = 0;

                    index++;

                    Year = DateTime.Now.AddYears(-500).AddYears(index).ToString("yyyy");

                    Thread.Sleep(100);

                    //this.GotoRefresh = true;
                }
            });
            {
                for (double i = 0; i <= 100 * 100; i = i + 1)
                {
                    int count = random.Next(3, waves.Count);

                    List<Func<double, double>> find = Enumerable.Range(0, count).Select(l => waves[l])?.ToList();

                    Func<double, double> func = l =>
                    {
                        double result = 0;

                        find.ForEach(k => result = result + k.Invoke(l));

                        return result;
                    };

                    double t = i / 360 * Math.PI;

                    double value = func(t);

                    Datas.Add(func(t));
                    xAxis.Add(i);
                }
            }

        }

        protected override void Loaded(string args)
        {

        }



    }
}
