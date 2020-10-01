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
    public class ChartScene : ViewScene
    {

        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> xAxis
        {
            get { return (ObservableCollection<double>)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(ObservableCollection<double>), typeof(ChartScene), new PropertyMetadata(default(ObservableCollection<double>), (d, e) =>
             {
                 ChartScene control = d as ChartScene;

                 if (control == null) return;

                 ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

             }));



        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> yAxis
        {
            get { return (ObservableCollection<double>)GetValue(yAxisProperty); }
            set { SetValue(yAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisProperty =
            DependencyProperty.Register("yAxis", typeof(ObservableCollection<double>), typeof(ChartScene), new PropertyMetadata(default(ObservableCollection<double>), (d, e) =>
             {
                 ChartScene control = d as ChartScene;

                 if (control == null) return;

                 ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

             }));

    }

    public class Chart : ChartScene
    {

    }

    public class StaticCurveChartScene : Chart
    {

    }

}
