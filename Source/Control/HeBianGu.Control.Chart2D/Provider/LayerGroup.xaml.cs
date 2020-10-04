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
    /// <summary> 容器 </summary>
    public class LayerGroup : ItemsControl
    {


    }

    /// <summary> 绘图结构关系 </summary>
    public class ViewLayerGroup : LayerGroup
    {
        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> xAxis
        {
            get { return (ObservableCollection<double>)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(ObservableCollection<double>), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                Chart control = d as Chart;

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
            DependencyProperty.Register("yAxis", typeof(ObservableCollection<double>), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

            }));

    }


    public abstract class DataLayerGroup : ViewLayerGroup
    {
        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> Data
        {
            get { return (ObservableCollection<double>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double>), typeof(DataLayerGroup), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
            {
                DataLayerGroup control = d as DataLayerGroup;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

                //control.Draw(control);

            }));

    }

}
