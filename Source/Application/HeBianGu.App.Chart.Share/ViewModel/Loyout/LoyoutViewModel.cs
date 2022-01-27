using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;

namespace HeBianGu.App.Chart
{
    [ViewModel("Loyout")]
    internal class LoyoutViewModel : MvcViewModelBase
    {

        private ObservableCollection<double> _xAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> xAxis
        {
            get { return _xAxis; }
            set
            {
                _xAxis = value;
                RaisePropertyChanged("xAxis");
            }
        }

        private ObservableCollection<double> _xGridAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        public ObservableCollection<double> xGridAxis
        {
            get { return _xGridAxis; }
            set
            {
                _xGridAxis = value;
                RaisePropertyChanged("xGridAxis");
            }
        }

        protected override void Init()
        {
            double count = 100.0 * 1000.0;

            for (int i = 1; i < count; i++)
            {
                var log = Math.Log10(i);
                //this.Datas.Add(Convert.ToDouble(i) / (count / 10.0));
                //this.xDatas.Add(log);

                int c = (int)(log);

                if (log % 1 == 0)
                {
                    this.xAxis.Add(log);
                }

                if (i % Math.Pow(10, c) == 0)
                {
                    this.xGridAxis.Add(log);
                }

            }

            double p = Math.Log10(count);

            double v = p % 1 == 0 ? p : p + 1;

            this.xAxis.Add((int)v);

            this.xGridAxis.Add((int)v);
        }


        protected override void Loaded(string args)
        {

        }


        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Sumit")
            {

            }

            //  Do：等待消息
            else if (command == "Cancel")
            {

            }
        }

    }
}
