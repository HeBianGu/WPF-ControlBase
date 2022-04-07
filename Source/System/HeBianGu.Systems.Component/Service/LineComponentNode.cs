// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HeBianGu.Systems.Component
{
    public class LineComponentNode : ComponentNode
    {
        public LineComponentNode()
        {
            for (double i = 0; i < 5; i++)
            {
                this.xData.Add(i);
                this.xAxis.Add(i);

                double y = this.Random.NextDouble() * 100;
                this.yData.Add(y);
                this.yAxis.Add(y);

            }
        }
        private ObservableCollection<double> _yData = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public ObservableCollection<double> yData
        {
            get { return _yData; }
            set
            {
                _yData = value;
                RaisePropertyChanged("yData");
            }
        }


        private ObservableCollection<double> _xData = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public ObservableCollection<double> xData
        {
            get { return _xData; }
            set
            {
                _xData = value;
                RaisePropertyChanged("xData");
            }
        }

        private ObservableCollection<double> _yAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public ObservableCollection<double> yAxis
        {
            get { return _yAxis; }
            set
            {
                _yAxis = value;
                RaisePropertyChanged("yAxis");
            }
        }

        private ObservableCollection<double> _xAxis = new ObservableCollection<double>();
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public ObservableCollection<double> xAxis
        {
            get { return _xAxis; }
            set
            {
                _xAxis = value;
                RaisePropertyChanged("xAxis");
            }
        }


        private bool _drawOnce;
        /// <summary> 说明  </summary>
        [Browsable(false)]
        public bool DrawOnce
        {
            get { return _drawOnce; }
            set
            {
                _drawOnce = value;
                RaisePropertyChanged("DrawOnce");
            }
        }

        public override void Clear()
        {
            this.xData.Clear();

            this.yData.Clear();

            base.Clear();
        }
    }
}
