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
    /// <summary> 数据列表 </summary>
    public class Series : DataLayer
    {
        public Style MarkStyle
        {
            get { return (Style)GetValue(MarkStyleProperty); }
            set { SetValue(MarkStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkStyleProperty =
            DependencyProperty.Register("MarkStyle", typeof(Style), typeof(Series), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Series control = d as Series;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

        public ObservableCollection<double> xAxis
        {
            get { return (ObservableCollection<double>)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(ObservableCollection<double>), typeof(Series), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
            {
                Series control = d as Series;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

            }));


        public ObservableCollection<double> yAxis
        {
            get { return (ObservableCollection<double>)GetValue(yAxisProperty); }
            set { SetValue(yAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisProperty =
            DependencyProperty.Register("yAxis", typeof(ObservableCollection<double>), typeof(Series), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
             {
                 Series control = d as Series;

                 if (control == null) return;

                 ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

             }));



        protected override void InitX()
        {
            if (this.xAxis.Count > 0)
            {
                this.minX = this.xAxis.Min();

                this.maxX = this.xAxis.Max();
            }
        }

        protected override void InitY()
        {
            if (this.yAxis.Count > 0)
            {
                this.minY = this.yAxis.Min();

                this.maxY = this.yAxis.Max();
            }
        }
       
    }



    /// <summary> 柱状图 </summary>
    public class BarSeries: Series
    {

    }


}
