using HeBianGu.Base.WpfBase;
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

                    var layers = this.GetChildren<LayerBase>();


                    foreach (var item in layers)
                    {
                        item.TryDraw();
                    }

                    //List<LayerBase> layers;

                    //layers = this.Dispatcher?.Invoke(() => this.GetChildren<LayerBase>())?.ToList();

                    //var split = this.Dispatcher?.Invoke(() => this.SplitMilliSecond);

                    //Task.Run(() =>
                    //{
                    //    foreach (var item in layers)
                    //    {
                    //        this.Dispatcher?.Invoke(() =>
                    //        item.TryDraw()
                    //        );

                    //        Thread.Sleep((int)500.0);

                    //    }
                    //});

                });

                this.CommandBindings.Add(binding);
            }
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

    //public class StaticCurveChartScene : Chart
    //{

    //}

}
