using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.App.Screen
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<double> _animationBarSource = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> AnimationBarSource
        {
            get { return _animationBarSource; }
            set
            {
                _animationBarSource = value;
                RaisePropertyChanged("AnimationBarSource");
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

        private ObservableCollection<double> _waveData2 = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> WaveData2
        {
            get { return _waveData2; }
            set
            {
                _waveData2 = value;
                RaisePropertyChanged("WaveData2");
            }
        }

        private ObservableCollection<double> _waveyAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> WaveyAxis
        {
            get { return _waveyAxis; }
            set
            {
                _waveyAxis = value;
                RaisePropertyChanged("WaveyAxis");
            }
        }

        private ObservableCollection<Student> _students = new ObservableCollection<Student>();
        /// <summary> 说明  </summary>
        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                RaisePropertyChanged("Students");
            }
        }

        Random random = new Random();

        protected override void Init()
        {

            for (int i = 0; i < 50; i++)
            {
                this.Students.Add(Student.Random());
            }

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

                    this.AnimationBarSource = source?.ToObservable();

                    if (index > 500) index = 0;

                    index++;

                    this.Year = DateTime.Now.AddYears(-500).AddYears(index).ToString("yyyy");

                    Thread.Sleep(100);

                    //this.GotoRefresh = true;
                }
            });


            List<Func<double, double>> waves = new List<Func<double, double>>();

            waves.Add(l => 1 * Math.Sin(l));
            waves.Add(l => 2 * Math.Sin(l / 2));
            waves.Add(l => 3 * Math.Sin(l * 2));
            waves.Add(l => 4 * Math.Sin(l / 3));
            waves.Add(l => 5 * Math.Sin(l * 3));
            waves.Add(l => 1 * Math.Sin(l / 4));
            waves.Add(l => 2 * Math.Sin(l * 4));
            waves.Add(l => 3 * Math.Sin(l / 5));
            waves.Add(l => 4 * Math.Sin(l * 5));
            waves.Add(l => 5 * Math.Sin(l / 6));
            waves.Add(l => 1 * Math.Sin(l * 6));
            waves.Add(l => 2 * Math.Sin(l / 7));
            waves.Add(l => 3 * Math.Sin(l * 7));
            waves.Add(l => 4 * Math.Sin(l / 8));
            waves.Add(l => 5 * Math.Sin(l * 8));

            waves.Add(l => 1 * Math.Cos(l));
            waves.Add(l => 2 * Math.Cos(l / 2));
            waves.Add(l => 3 * Math.Cos(l * 2));
            waves.Add(l => 4 * Math.Cos(l / 3));
            waves.Add(l => 5 * Math.Cos(l * 3));
            waves.Add(l => 1 * Math.Cos(l / 4));
            waves.Add(l => 2 * Math.Cos(l * 4));
            waves.Add(l => 3 * Math.Cos(l / 5));
            waves.Add(l => 4 * Math.Cos(l * 5));
            waves.Add(l => 5 * Math.Cos(l / 6));
            waves.Add(l => 1 * Math.Cos(l * 6));
            waves.Add(l => 2 * Math.Cos(l / 7));
            waves.Add(l => 3 * Math.Cos(l * 7));
            waves.Add(l => 4 * Math.Cos(l / 8));
            waves.Add(l => 5 * Math.Cos(l * 8));

            double fz = 150;

            double center = 360;

            double weight = 90;

            double offsite = 90;


            Task.Run(() =>
            {
                double param = -5;

                while (true)
                {
                    //List<double> data = new List<double>();

                    //List<double> data1 = new List<double>();

                    List<double> data2 = new List<double>();

                    List<double> axis = new List<double>();

                    for (double i = 0; i <= 360 * 2; i = i + 1)
                    {
                        int count = random.Next(3, waves.Count);

                        var find = Enumerable.Range(0, count).Select(l => waves[l])?.ToList();

                        Func<double, double> func = l =>
                        {
                            double result = 0;

                            find.ForEach(k => result = result + k.Invoke(l));

                            return result;
                        };

                        axis.Add(i);

                        double t = i / 360 * Math.PI;

                        double value = func(t);

                        //data.Add(func(t));

                        //data1.Add(Math.Sin((i * param) / 360 * Math.PI) * 50);

                        double wave = i > center - weight && i < center + weight ? Math.Sin(((i + offsite) * 2) / 360 * Math.PI) * fz : 0;

                        data2.Add(wave + value);
                    }

                    this.WaveyAxis = axis.ToObservable();

                    //this.WaveData = data.ToObservable();

                    //this.WaveData1 = data1.ToObservable();

                    this.WaveData2 = data2.ToObservable(); 


                    param = param + 0.01;

                    if (param > 5) param = -5;

                    Thread.Sleep(100);

                }
            });
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {

        }
    }
}
