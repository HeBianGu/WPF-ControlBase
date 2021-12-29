using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using JJ.Kby.App.Demo1.View.Dialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace JJ.Kby.App.Demo1
{
    class ShellViewModel : NotifyPropertyChanged
    {
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

        private ObservableCollection<double> _waveData = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> WaveData
        {
            get { return _waveData; }
            set
            {
                _waveData = value;
                RaisePropertyChanged("WaveData");
            }
        }



        private ObservableCollection<TestViewModel> _logMessages = new ObservableCollection<TestViewModel>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TestViewModel> LogMessages
        {
            get { return _logMessages; }
            set
            {
                _logMessages = value;
                RaisePropertyChanged("LogMessages");
            }
        }



        Random random = new Random();

        protected override void Init()
        {
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

                    for (double i = 0; i <= 360 * 0.2; i = i + 1)
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

                        data.Add(func(t));

                        data1.Add(Math.Sin((i * param) / 360 * Math.PI) * 50);

                        double wave = i > center - weight && i < center + weight ? Math.Sin(((i + offsite) * 2) / 360 * Math.PI) * fz : 0;

                        data2.Add(wave + value);
                    }

                    this.WaveyAxis = axis.ToObservable();

                    this.WaveData = data.ToObservable(); 

                    param = param + 0.01;

                    if (param > 5) param = -5;

                    Thread.Sleep(100);

                }
            });

            Task.Run(()=> 
            {
                while(true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        this.LogMessages.Insert(0, new TestViewModel() { Value = "加载成功", Value2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Value3 = "刷新成功，总计360条数据" });

                        if (this.LogMessages.Count > 100)
                        {
                            this.LogMessages.RemoveAt(99);
                        }

                    });

                    Thread.Sleep(5000);
                }
            });
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            if (command == "Button.Click.ShowWave")
            {
                WaveDialog dialog = new WaveDialog();
                dialog.DataContext = this;
                MessageService.ShowLayer(dialog);
            }
            else if (command == "Button.Click.ShowLog")
            {
                LogDialog dialog = new LogDialog();
                dialog.DataContext = this;
                MessageService.ShowLayer(dialog);
            }

        }
    }
}
