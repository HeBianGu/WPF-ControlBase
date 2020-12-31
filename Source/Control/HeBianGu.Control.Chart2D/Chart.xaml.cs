using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    public class Chart : ViewLayerGroup
    {
        public static RoutedCommand RefeshCommand = new RoutedCommand();

        public Chart()
        {
            {
                RoutedCommand command = new RoutedCommand();

                CommandBinding binding = new CommandBinding(RefeshCommand, (l, k) =>
                {
                    this.ForceDraw();

                });

                this.CommandBindings.Add(binding);
            }

            this.SizeChanged += (l, k) =>
              {
                  if (this.IsLoaded)
                      this.ForceDraw();
              };

            this.Loaded += (l, k) =>
              {
                  this.RefreshByData();
              };
        }

        public double SplitMilliSecond
        {
            get { return (double)GetValue(SplitMilliSecondProperty); }
            set { SetValue(SplitMilliSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitMilliSecondProperty =
            DependencyProperty.Register("SplitMilliSecond", typeof(double), typeof(Chart), new PropertyMetadata(500.0, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;
            }));

        [TypeConverter(typeof(DisplayTypeConverter))]
        public ObservableCollection<string> xDisplay
        {
            get { return (ObservableCollection<string>)GetValue(xDisplayProperty); }
            set { SetValue(xDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xDisplayProperty =
            DependencyProperty.Register("xDisplay", typeof(ObservableCollection<string>), typeof(Chart), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

            }));

        [TypeConverter(typeof(DisplayTypeConverter))]
        public ObservableCollection<string> yDisplay
        {
            get { return (ObservableCollection<string>)GetValue(yDisplayProperty); }
            set { SetValue(yDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yDisplayProperty =
            DependencyProperty.Register("yDisplay", typeof(ObservableCollection<string>), typeof(Chart), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

             }));


        public Style xAxisStyle
        {
            get { return (Style)GetValue(xAxisStyleProperty); }
            set { SetValue(xAxisStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisStyleProperty =
            DependencyProperty.Register("xAxisStyle", typeof(Style), typeof(Chart), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style yAxisStyle
        {
            get { return (Style)GetValue(yAxisStyleProperty); }
            set { SetValue(yAxisStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisStyleProperty =
            DependencyProperty.Register("yAxisStyle", typeof(Style), typeof(Chart), new PropertyMetadata(default(Style), (d, e) =>
           {
               Chart control = d as Chart;

               if (control == null) return;

               Style config = e.NewValue as Style;

           }));


        public Style GridStyle
        {
            get { return (Style)GetValue(GridStyleProperty); }
            set { SetValue(GridStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridStyleProperty =
            DependencyProperty.Register("GridStyle", typeof(Style), typeof(Chart), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style LegendStyle
        {
            get { return (Style)GetValue(LegendStyleProperty); }
            set { SetValue(LegendStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LegendStyleProperty =
            DependencyProperty.Register("LegendStyle", typeof(Style), typeof(Chart), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));



        public Style VisualMapStyle
        {
            get { return (Style)GetValue(VisualMapStyleProperty); }
            set { SetValue(VisualMapStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisualMapStyleProperty =
            DependencyProperty.Register("VisualMapStyle", typeof(Style), typeof(Chart), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

    }

    public class PolarChart : Chart
    {
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(PolarChart), new PropertyMetadata(default(double), (d, e) =>
             {
                 PolarChart control = d as PolarChart;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));
    }

    [TemplatePart(Name = "PART_TOOLBAR", Type = typeof(Thumb))]
    public class ChartMap : ViewLayerGroup
    {
        StoryBoardToolBar toolBar;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.toolBar = Template.FindName("PART_TOOLBAR", this) as StoryBoardToolBar;

            this.toolBar.ValueChanged += (l, k) =>
            {
                this.BeginRefresh();
            };

            if (this.Chart != null)
            {
                this.toolBar.LeftPercent = 0.0;
                this.toolBar.RightPercent = 1.0;

                this.BeginRefresh();
            }

            this.SizeChanged += (l, k) =>
            {
                if (this.IsLoaded)
                    this.RefreshByData();
            };

            this.Loaded += (l, k) =>
            {
                this.RefreshByData();
            };

        }

        public Chart Chart
        {
            get { return (Chart)GetValue(ChartProperty); }
            set { SetValue(ChartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartProperty =
            DependencyProperty.Register("Chart", typeof(Chart), typeof(ChartMap), new PropertyMetadata(default(Chart), (d, e) =>
             {
                 ChartMap control = d as ChartMap;

                 if (control == null) return;

                 control.UnRegister(e.OldValue as Chart);

                 control.Register(e.NewValue as Chart);
             }));

        void Register(Chart chart)
        {
            if (chart == null) return;

            if (this.toolBar != null)
            {
                this.toolBar.LeftPercent = 0.0;
                this.toolBar.RightPercent = 1.0;

                this.BeginRefresh();
            }

            chart.MouseWheel += Chart_MouseWheel;
            chart.MouseMove += Chart_MouseMove;
        }


        void UnRegister(Chart chart)
        {
            if (chart == null) return;

            chart.MouseWheel -= Chart_MouseWheel;
            chart.MouseMove -= Chart_MouseMove;
        }

        private void Chart_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Chart_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (this.Chart == null) return;

            var position = Mouse.GetPosition(this.Chart);

            double left = position.X / this.Chart.ActualWidth;

            double right = (this.Chart.ActualWidth - position.X) / this.Chart.ActualWidth;

            double lp = this.toolBar.LeftPercent + 0.0001 * e.Delta * left;

            double rp = this.toolBar.RightPercent - 0.0001 * e.Delta * right;

            if (lp < 0) lp = 0;

            if (rp > 1) rp = 1;

            if (lp > rp) return;

            var value = Math.Abs(rp - lp);

            if (this.xDatas.Count * value < 3) return;

            if (Math.Abs(rp - lp) < 0.005) return;

            this.toolBar.LeftPercent = lp;
            this.toolBar.RightPercent = rp;

            this.BeginRefresh();
        }

        void BeginRefresh()
        {
            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            //{
            //    this.RefreshChart();
            //}));

            this.RefreshChart();
        }

        void RefreshChart()
        {
            if (this.xDatas.Count == 0) return;

            double left = this.toolBar.LeftPercent;
            double right = this.toolBar.RightPercent;

            double xmin = this.xDatas.Min();
            double xmax = this.xDatas.Max();

            double min = (xmax - xmin) * left + xmin;
            double max = (xmax - xmin) * right + xmin;

            //  Do ：更新数据 
            ObservableCollection<double> xa = new ObservableCollection<double>();
            ObservableCollection<double> dd = new ObservableCollection<double>();

            for (int i = 0; i < this.xDatas.Count; i++)
            {
                double current = this.xDatas[i];

                if (current < min || current > max) continue;

                dd.Add(this.yDatas[i]);
                xa.Add(current);
            }

            this.Chart.xDatas = xa;

            this.Chart.yDatas = dd;

            this.Chart.RefreshByData();
        }
    }

}
