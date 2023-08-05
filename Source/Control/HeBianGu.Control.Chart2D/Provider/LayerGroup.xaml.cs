// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Chart2D
{
    public class LayerGroup : ItemsControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LayerGroup), "S.LayerGroup.Default");

        static LayerGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayerGroup), new FrameworkPropertyMetadata(typeof(LayerGroup)));
        }

        public virtual void Draw()
        {
            System.Collections.Generic.IEnumerable<LayerBase> layers = this.GetChildren<LayerBase>();

            if (layers == null) return;

            foreach (LayerBase layer in layers)
            {
                layer.TryDraw();
            }
        }

        /// <summary> 不应用冻结 强制刷新 </summary>
        public virtual void ForceDraw()
        {
            System.Collections.Generic.IEnumerable<LayerBase> layers = this.GetChildren<LayerBase>();

            if (layers == null) 
                return;

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

            foreach (LayerBase layer in layers)
            {
                //try
                //{
                layer.ForceDraw();
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(ex);
                //}
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

    public class ViewLayerGroup : LayerGroup
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(ViewLayerGroup), "S.ViewLayerGroup.Default");

        static ViewLayerGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewLayerGroup), new FrameworkPropertyMetadata(typeof(ViewLayerGroup)));
        }

        /// <summary>
        /// true:需要调用DrawOne去刷新，用于显示实时数据修改完数据后手动调用刷新，false：属性更改实时刷新，手动编辑数据刷新
        /// </summary>
        public bool UseDrawOnce
        {
            get { return (bool)GetValue(UseDrawOnceProperty); }
            set { SetValue(UseDrawOnceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseDrawOnceProperty =
            DependencyProperty.Register("UseDrawOnce", typeof(bool), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

        [TypeConverter(typeof(DoubleCollectionConverter))]
        public DoubleCollection xAxis
        {
            get { return (DoubleCollection)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(DoubleCollection), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new DoubleCollection(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

                control.Draw();

            }));


        [TypeConverter(typeof(DoubleCollectionConverter))]
        public DoubleCollection yAxis
        {
            get { return (DoubleCollection)GetValue(yAxisProperty); }
            set { SetValue(yAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisProperty =
            DependencyProperty.Register("yAxis", typeof(DoubleCollection), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new DoubleCollection(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

                control.Draw();

            }));

        /// <summary> x轴数据集合 </summary>
        //[TypeConverter(typeof(DataTypeConverter))]
        [TypeConverter(typeof(DoubleCollectionConverter))]
        public DoubleCollection xDatas
        {
            get { return (DoubleCollection)GetValue(xDatasProperty); }
            set { SetValue(xDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xDatasProperty =
            DependencyProperty.Register("xDatas", typeof(DoubleCollection), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new DoubleCollection(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                Chart control = d as Chart;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

                //control.Draw();

                control.Draw();
            }));


        //[TypeConverter(typeof(DataTypeConverter))]
        [TypeConverter(typeof(DoubleCollectionConverter))]
        public DoubleCollection yDatas
        {
            get { return (DoubleCollection)GetValue(yDatasProperty); }
            set { SetValue(yDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yDatasProperty =
            DependencyProperty.Register("yDatas", typeof(DoubleCollection), typeof(ViewLayerGroup), new FrameworkPropertyMetadata(new DoubleCollection(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

                control.Draw();

            }));


        /// <summary> y轴坐标点个数 </summary>

        public int yAxisCount
        {
            get { return (int)GetValue(yAxisCountProperty); }
            set { SetValue(yAxisCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisCountProperty =
            DependencyProperty.Register("yAxisCount", typeof(int), typeof(ViewLayerGroup), new PropertyMetadata(5, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                //int config = e.NewValue as int;

            }));


        /// <summary> x轴坐标点个数 </summary>
        public int xAxisCount
        {
            get { return (int)GetValue(xAxisCountProperty); }
            set { SetValue(xAxisCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisCountProperty =
            DependencyProperty.Register("xAxisCount", typeof(int), typeof(ViewLayerGroup), new PropertyMetadata(10, (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                //int config = e.NewValue as int;

            }));


        /// <summary> y坐标轴根据数据自动计算 </summary>
        public bool yAxisAuto
        {
            get { return (bool)GetValue(yAxisAutoProperty); }
            set { SetValue(yAxisAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisAutoProperty =
            DependencyProperty.Register("yAxisAuto", typeof(bool), typeof(ViewLayerGroup), new PropertyMetadata(false, (d, e) =>
             {
                 ViewLayerGroup control = d as ViewLayerGroup;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        /// <summary> x轴坐标根据数据自动计算 </summary>
        public bool xAxisAuto
        {
            get { return (bool)GetValue(xAxisAutoProperty); }
            set { SetValue(xAxisAutoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisAutoProperty =
            DependencyProperty.Register("xAxisAuto", typeof(bool), typeof(ViewLayerGroup), new PropertyMetadata(false, (d, e) =>
             {
                 ViewLayerGroup control = d as ViewLayerGroup;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));




        public void RefreshByData()
        {
            if (this.xDatas.Count == 0 || this.yDatas.Count == 0) 
                return;

            //  Do ：设置y轴
            if (this.yAxisAuto)
            {
                double minY = this.yDatas.Count == 0 ? 0 : this.yDatas.Min();

                double maxY = this.yDatas.Count == 0 ? 0 : this.yDatas.Max();

                double step = (maxY - minY) / this.yAxisCount;

                minY = Convert.ToDouble((minY - 2 * step));
                maxY = Convert.ToDouble((maxY + 2 * step));

                step = (maxY - minY) / this.yAxisCount;

                DoubleCollection ya = new DoubleCollection();

                if (minY == maxY)
                {
                    if (this.yAxisCount % 2 == 0)
                    {
                        for (int i = 0; i < this.yAxisCount / 2; i++)
                        {
                            ya.Add(minY - 1 * i - 0.5);
                            ya.Add(minY + 1 * i + 0.5);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < (this.yAxisCount - 1) / 2; i++)
                        {
                            ya.Add(minY - 1 * i - 1);
                            ya.Add(minY + 1 * i + 1);
                        }

                        ya.Add(minY);
                    }
                }
                else
                {
                    for (int i = 0; i < this.yAxisCount + 1; i++)
                    {
                        ya.Add(minY + i * step);
                    }
                }

                this.yAxis = ya;
            }

            //  Do ：设置x轴
            if (this.xAxisAuto)
            {
                if (this.xDatas.Count > this.xAxisCount)
                {
                    DoubleCollection cx = new DoubleCollection();

                    double minX = this.xDatas.Min();

                    double maxX = this.xDatas.Max();

                    double stepx = (maxX - minX) / this.xAxisCount;

                    for (int i = 0; i < this.xAxisCount + 1; i++)
                    {
                        cx.Add(minX + i * stepx);
                    }

                    this.xAxis = cx;
                }
                else
                {
                    this.xAxis = this.xDatas;
                }
            }

            this.ForceDraw();
        }

        public bool DrawOnce
        {
            get { return (bool)GetValue(DrawOnceProperty); }
            set { SetValue(DrawOnceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawOnceProperty =
            DependencyProperty.Register("DrawOnce", typeof(bool), typeof(ViewLayerGroup), new PropertyMetadata(default(bool), (d, e) =>
            {
                ViewLayerGroup control = d as ViewLayerGroup;

                if (control == null) return;

                bool config = (bool)e.NewValue;

                if (config)
                {
                    control.RefreshByData();
                    control.DrawOnce = false;
                }
            }));
    }

    public abstract class DataLayerGroup : ViewLayerGroup
    {

        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DataLayerGroup), "S.DataLayerGroup.Default");

        static DataLayerGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataLayerGroup), new FrameworkPropertyMetadata(typeof(DataLayerGroup)));
        }

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
