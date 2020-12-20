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
        public virtual void Draw()
        {
            var layers = this.GetChildren<LayerBase>();

            if (layers == null) return;

            foreach (var layer in layers)
            {
                layer.TryDraw();
            }
        }

        /// <summary> 不应用冻结 强制刷新 </summary>
        public virtual void ForceDraw()
        {
            var layers = this.GetChildren<LayerBase>();

            if (layers == null) return;

            //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
            // {
            //     foreach (var layer in layers)
            //     {
            //         try
            //         {
            //             layer.Draw(layer);
            //         }
            //         catch (Exception ex)
            //         {
            //             Debug.WriteLine(ex);
            //         }
            //     }
            // }));

            foreach (var layer in layers)
            {
                try
                {
                    layer.Draw(layer);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }


            //foreach (var layer in layers)
            //{
            //   await Task.Run(() =>
            //    {
            //        try
            //        {
            //            layer.Draw(layer);
            //        }
            //        catch (Exception)
            //        {

            //        }
            //    });
            //}

        }

        public string DisplayName
        {
            get { return (string)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayNameProperty =
            DependencyProperty.Register("DisplayName", typeof(string), typeof(LayerGroup), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                LayerGroup control = d as LayerGroup;

                if (control == null) return;

                string config = e.NewValue as string;

            }));


        //public bool GotoRefresh
        //{
        //    get { return (bool)GetValue(GotoRefreshProperty); }
        //    set { SetValue(GotoRefreshProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty GotoRefreshProperty =
        //    DependencyProperty.Register("GotoRefresh", typeof(bool), typeof(LayerGroup), new PropertyMetadata(default(bool), (d, e) =>
        //     {
        //         LayerGroup control = d as LayerGroup;

        //         if (control == null) return;

        //         bool config = (bool)e.NewValue;

        //         if(config)
        //         {
        //             control.ForceDraw();

        //             control.GotoRefresh = false;
        //         }

        //     }));


    }

    /// <summary> 绘图结构关系 </summary>
    public class ViewLayerGroup : LayerGroup
    {
        /// <summary> x轴坐标数据集合 </summary>
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

                //control.Draw();

            }));


        /// <summary> y轴坐标数据集合 </summary>
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

        /// <summary> x轴数据集合 </summary>
        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> xDatas
        {
            get { return (ObservableCollection<double>)GetValue(xDatasProperty); }
            set { SetValue(xDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xDatasProperty =
            DependencyProperty.Register("xDatas", typeof(ObservableCollection<double>), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

                //control.Draw();

            }));


        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> yDatas
        {
            get { return (ObservableCollection<double>)GetValue(yDatasProperty); }
            set { SetValue(yDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yDatasProperty =
            DependencyProperty.Register("yDatas", typeof(ObservableCollection<double>), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

            }));

    }


    public abstract class DataLayerGroup : ViewLayerGroup
    {
        //[TypeConverter(typeof(DataTypeConverter))]
        //public ObservableCollection<double> Data
        //{
        //    get { return (ObservableCollection<double>)GetValue(DataProperty); }
        //    set { SetValue(DataProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DataProperty =
        //    DependencyProperty.Register("Data", typeof(ObservableCollection<double>), typeof(DataLayerGroup), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
        //    {
        //        DataLayerGroup control = d as DataLayerGroup;

        //        if (control == null) return;

        //        ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

        //        //control.Draw(control);

        //    }));

        [TypeConverter(typeof(DataArrayTypeConverter))]
        public ObservableCollection<double[]> DataMap
        {
            get { return (ObservableCollection<double[]>)GetValue(DataMapProperty); }
            set { SetValue(DataMapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataMapProperty =
            DependencyProperty.Register("DataMap", typeof(ObservableCollection<double[]>), typeof(DataLayerGroup), new PropertyMetadata(default(ObservableCollection<double[]>), (d, e) =>
            {
                DataLayerGroup control = d as DataLayerGroup;

                if (control == null) return;

                ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

            }));

    }

}
