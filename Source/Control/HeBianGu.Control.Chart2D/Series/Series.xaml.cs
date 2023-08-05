// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.Control.Chart2D
{
    public class Series : DataLayerGroup
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Series), "S.Series.Default");
        public static ComponentResourceKey LineKey => new ComponentResourceKey(typeof(Series), "S.Series.Line");
        public static ComponentResourceKey LinePolarKey => new ComponentResourceKey(typeof(Series), "S.Series.Line.Polar");
        public static ComponentResourceKey LineSmoothKey => new ComponentResourceKey(typeof(Series), "S.Series.Line.Smooth");
        public static ComponentResourceKey BarKey => new ComponentResourceKey(typeof(Series), "S.Series.Bar.Basic");

        public static ComponentResourceKey LineAreaKey => new ComponentResourceKey(typeof(Series), "S.Series.Line.Area");

        static Series()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Series), new FrameworkPropertyMetadata(typeof(Series)));
        }

        public Style LineStyle
        {
            get { return (Style)GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(Style), typeof(Series), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Series control = d as Series;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));


        public Style ScatterStyle
        {
            get { return (Style)GetValue(ScatterStyleProperty); }
            set { SetValue(ScatterStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScatterStyleProperty =
            DependencyProperty.Register("ScatterStyle", typeof(Style), typeof(Series), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Series control = d as Series;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));



        public Style BarStyle
        {
            get { return (Style)GetValue(BarStyleProperty); }
            set { SetValue(BarStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BarStyleProperty =
            DependencyProperty.Register("BarStyle", typeof(Style), typeof(Series), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Series control = d as Series;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));



    }

    public class BarSeriesLayer : Layer
    {
        static BarSeriesLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BarSeriesLayer), new FrameworkPropertyMetadata(typeof(BarSeriesLayer)));
        }
    }


}
