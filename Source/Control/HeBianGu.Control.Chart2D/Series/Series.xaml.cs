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
    public class Series : DataLayerGroup
    {
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



    /// <summary> 柱状图 </summary>
    public class BarSeriesLayer : Layer
    {

    }


}
