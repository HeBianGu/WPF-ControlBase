using HeBianGu.Service.Mvc;
using System;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace HeBianGu.App.Chart
{
    [ViewModel("Loyout")]
    internal class LoyoutViewModel : MvcViewModelBase
    {

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

        private DoubleCollection _xGridAxis = new DoubleCollection();
        /// <summary> 说明  </summary>
        public DoubleCollection xGridAxis
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
                double log = Math.Log10(i);
                //this.Datas.Add(Convert.ToDouble(i) / (count / 10.0));
                //this.xDatas.Add(log);

                int c = (int)(log);

                if (log % 1 == 0)
                {
                    xAxis.Add(log);
                }

                if (i % Math.Pow(10, c) == 0)
                {
                    xGridAxis.Add(log);
                }

            }

            double p = Math.Log10(count);

            double v = p % 1 == 0 ? p : p + 1;

            xAxis.Add((int)v);

            xGridAxis.Add((int)v);
        }


        protected override void Loaded(string args)
        {

        }


    }
}
