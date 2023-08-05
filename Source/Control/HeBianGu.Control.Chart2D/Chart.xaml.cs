// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    public class Chart : ViewLayerGroup
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Default");
        public static ComponentResourceKey CoordKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Coord");
        public static ComponentResourceKey CoordScreenKey => new ComponentResourceKey(typeof(Chart), "S.Chart.CoordScreen");

        public static ComponentResourceKey CoordWithOutToolKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Coord.WithOutTool");
        public static ComponentResourceKey CoordVerticalKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Coord.Vertical");
        public static ComponentResourceKey CoordSingleKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Coord.Single");
        public static ComponentResourceKey yBarKey => new ComponentResourceKey(typeof(Chart), "S.Chart.yBar.Default");
        public static ComponentResourceKey yStackBarKey => new ComponentResourceKey(typeof(Chart), "S.Chart.yStackBar.Default");


        public static ComponentResourceKey yAnimationBarKey => new ComponentResourceKey(typeof(Chart), "S.Chart.yAnimationBar.Default");
        public static ComponentResourceKey CoordAlignmentCenterKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Coord.AlignmentCenter");
        public static ComponentResourceKey BarKey => new ComponentResourceKey(typeof(Chart), "S.Chart.Bar.Default");
        public static ComponentResourceKey StackBarKey => new ComponentResourceKey(typeof(Chart), "S.Chart.StackBar.Default");

        static Chart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Chart), new FrameworkPropertyMetadata(typeof(Chart)));
        }

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



        public Func<double, string> xConvert
        {
            get { return (Func<double, string>)GetValue(xConvertProperty); }
            set { SetValue(xConvertProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xConvertProperty =
            DependencyProperty.Register("xConvert", typeof(Func<double, string>), typeof(ViewLayerGroup), new PropertyMetadata(default(Func<double, string>), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                Func<double, string> config = e.NewValue as Func<double, string>;

            }));


        public Func<double, string> yConvert
        {
            get { return (Func<double, string>)GetValue(yConvertProperty); }
            set { SetValue(yConvertProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yConvertProperty =
            DependencyProperty.Register("yConvert", typeof(Func<double, string>), typeof(ViewLayerGroup), new PropertyMetadata(default(Func<double, string>), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                Func<double, string> config = e.NewValue as Func<double, string>;

            }));


        //public Visibility LegendVisibility
        //{
        //    get { return (Visibility)GetValue(LegendVisibilityProperty); }
        //    set { SetValue(LegendVisibilityProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty LegendVisibilityProperty =
        //    DependencyProperty.Register("LegendVisibility", typeof(Visibility), typeof(Chart), new FrameworkPropertyMetadata(Visibility.Visible, (d, e) =>
        //     {
        //         Chart control = d as Chart;

        //         if (control == null) return;

        //         if (e.OldValue is Visibility o)
        //         {

        //         }

        //         if (e.NewValue is Visibility n)
        //         {

        //         }

        //     }));


        public bool UseRefreshButton
        {
            get { return (bool)GetValue(UseRefreshButtonProperty); }
            set { SetValue(UseRefreshButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseRefreshButtonProperty =
            DependencyProperty.Register("UseRefreshButton", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public bool UseLegend
        {
            get { return (bool)GetValue(UseLegendProperty); }
            set { SetValue(UseLegendProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseLegendProperty =
            DependencyProperty.Register("UseLegend", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
             {
                 Chart control = d as Chart;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }

                 if (e.NewValue is bool n)
                 {

                 }

             }));


        public bool UsexAxis
        {
            get { return (bool)GetValue(UsexAxisProperty); }
            set { SetValue(UsexAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsexAxisProperty =
            DependencyProperty.Register("UsexAxis", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
            }));


        public bool UseyAxis
        {
            get { return (bool)GetValue(UseyAxisProperty); }
            set { SetValue(UseyAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseyAxisProperty =
            DependencyProperty.Register("UseyAxis", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseGrid
        {
            get { return (bool)GetValue(UseGridProperty); }
            set { SetValue(UseGridProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseGridProperty =
            DependencyProperty.Register("UseGrid", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseMarkLine
        {
            get { return (bool)GetValue(UseMarkLineProperty); }
            set { SetValue(UseMarkLineProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseMarkLineProperty =
            DependencyProperty.Register("UseMarkLine", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseMarkPosition
        {
            get { return (bool)GetValue(UseMarkPositionProperty); }
            set { SetValue(UseMarkPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseMarkPositionProperty =
            DependencyProperty.Register("UseMarkPosition", typeof(bool), typeof(Chart), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


    }

    public class PolarChart : Chart
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(PolarChart), "S.PolarChart.Default");
        public static ComponentResourceKey AngleKey => new ComponentResourceKey(typeof(PolarChart), "S.PolarChart.Angle");

        static PolarChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PolarChart), new FrameworkPropertyMetadata(typeof(PolarChart)));
        }

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
        static ChartMap()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartMap), new FrameworkPropertyMetadata(typeof(ChartMap)));
        }

        private ThumbToolBar toolBar;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.toolBar = Template.FindName("PART_TOOLBAR", this) as ThumbToolBar;

            this.toolBar.ValueChanged += (l, k) =>
            {
                //  Do ：工具栏操作刷新
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    this.BeginRefresh();
                }));

                //this.BeginRefresh();
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
                {
                    //  Do ：尺寸改变刷新
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                    {
                        this.RefreshByData();
                    }));
                }
                //this.RefreshByData();
            };

            this.Loaded += (l, k) =>
            {
                //  Do ：加载时刷新
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                {
                    this.RefreshByData();
                }));
                //this.RefreshByData();
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


        public int MinCount
        {
            get { return (int)GetValue(MinCountProperty); }
            set { SetValue(MinCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinCountProperty =
            DependencyProperty.Register("MinCount", typeof(int), typeof(ChartMap), new PropertyMetadata(10, (d, e) =>
             {
                 ChartMap control = d as ChartMap;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));

        private void Register(Chart chart)
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

        private void UnRegister(Chart chart)
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

            Point position = Mouse.GetPosition(this.Chart);

            double left = position.X / this.Chart.ActualWidth;

            double right = (this.Chart.ActualWidth - position.X) / this.Chart.ActualWidth;

            double lp = this.toolBar.LeftPercent + 0.0001 * e.Delta * left;

            double rp = this.toolBar.RightPercent - 0.0001 * e.Delta * right;

            if (lp < 0) lp = 0;

            if (rp > 1) rp = 1;

            if (lp > rp) return;

            double value = Math.Abs(rp - lp);

            if (this.xDatas.Count * value < this.MinCount) return;

            //if (Math.Abs(rp - lp) < 0.005) return;

            this.toolBar.LeftPercent = lp;
            this.toolBar.RightPercent = rp;

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            {
                this.BeginRefresh();
            }));

            //this.BeginRefresh();
        }

        private void BeginRefresh()
        {
            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            //{
            //    this.RefreshChart();
            //}));

            this.RefreshChart();
        }

        private void RefreshChart()
        {
            if (this.xDatas.Count == 0) return;

            double left = this.toolBar.LeftPercent;
            double right = this.toolBar.RightPercent;

            double xmin = this.xDatas.Min();
            double xmax = this.xDatas.Max();

            double min = (xmax - xmin) * left + xmin;
            double max = (xmax - xmin) * right + xmin;

            //  Do ：更新数据 
            DoubleCollection xa = new DoubleCollection();
            DoubleCollection dd = new DoubleCollection();

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
