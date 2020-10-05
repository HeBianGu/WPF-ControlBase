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
    public interface IDraw
    {
        void Draw(Canvas canvas);

        void Clear();
    }

    public class LayerBase : Canvas, IDraw
    {
        public LayerBase()
        {
            this.SizeChanged += (l, k) =>
            {
                this.Draw(this);

            };

            this.Loaded += (l, k) =>
            {
                this.Draw(this);

            };
        }


        public virtual void Draw(Canvas canvas)
        {
            this.Clear();
        }

        public virtual void Clear()
        {
            this.Children.Clear();
        }

        public virtual void TryDraw()
        {
            try
            {
                this.Draw(this);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                //Trace.Fail(ex.Message);
            }
        }
    }

    public class XyLayer : LayerBase
    {
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(XyLayer), new PropertyMetadata(Brushes.Black, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

                control.TryDraw();

            }));

        public override void Draw(Canvas canvas)
        {
            this.InitX();

            this.InitY();

            base.Draw(canvas);
        }

        protected double minX;
        protected double maxX;

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetX(double value, double width)
        {
            if (this.maxX == this.minX) return 0;

            var bottom = ((value - this.minX) / (this.maxX - this.minX)) * width;

            return bottom;
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetX(double value)
        {
            return this.GetX(value,this.ActualWidth);
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetY(double value, double height)
        {
            if (this.maxY == this.minY) return 0;

            var bottom = height - ((value - this.minY) / (this.maxY - this.minY)) * height;

            return bottom;
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetY(double value)
        {
            return this.GetY(value,this.ActualHeight);
        }

        protected double minY;
        protected double maxY;

        protected virtual void InitY()
        {
            if (this.yAxis == null || this.yAxis.Count == 0) return;

            this.minY = this.yAxis.Min();
            this.maxY = this.yAxis.Max();


        }
        protected virtual void InitX()
        {
            if (this.xAxis == null || this.xAxis.Count == 0) return;

            this.minX = this.xAxis.Min();
            this.maxX = this.xAxis.Max();
        }



        public Style LineStyle
        {
            get { return (Style)GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(Style), typeof(XyLayer), new PropertyMetadata(default(Style), (d, e) =>
            {
                Grid control = d as Grid;

                if (control == null) return;

                Style config = e.NewValue as Style;

                control.TryDraw();

            }));

        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> xAxis
        {
            get { return (ObservableCollection<double>)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(ObservableCollection<double>), typeof(XyLayer), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

                control.TryDraw();

            }));

        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> yAxis
        {
            get { return (ObservableCollection<double>)GetValue(yAxisProperty); }
            set { SetValue(yAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisProperty =
            DependencyProperty.Register("yAxis", typeof(ObservableCollection<double>), typeof(XyLayer), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

                control.TryDraw();

            }));


        [TypeConverter(typeof(DisplayTypeConverter))]
        public ObservableCollection<string> xDisplay
        {
            get { return (ObservableCollection<string>)GetValue(xDisplayProperty); }
            set { SetValue(xDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xDisplayProperty =
            DependencyProperty.Register("xDisplay", typeof(ObservableCollection<string>), typeof(XyLayer), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

                control.TryDraw();

            }));

        [TypeConverter(typeof(DisplayTypeConverter))]

        public ObservableCollection<string> yDisplay
        {
            get { return (ObservableCollection<string>)GetValue(yDisplayProperty); }
            set { SetValue(yDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yDisplayProperty =
            DependencyProperty.Register("yDisplay", typeof(ObservableCollection<string>), typeof(XyLayer), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 XyLayer control = d as XyLayer;

                 if (control == null) return;

                 ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

                 control.TryDraw();

             }));


    }

    public class DataLayer : XyLayer
    {
        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> Data
        {
            get { return (ObservableCollection<double>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double>), typeof(DataLayer), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
            {
                DataLayer control = d as DataLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

                control.TryDraw();

            }));
    }

    /// <summary> 数据列表 </summary>
    public class Layer : DataLayer
    {
        public Style MarkStyle
        {
            get { return (Style)GetValue(MarkStyleProperty); }
            set { SetValue(MarkStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkStyleProperty =
            DependencyProperty.Register("MarkStyle", typeof(Style), typeof(Layer), new PropertyMetadata(default(Style), (d, e) =>
            {
                Layer control = d as Layer;

                if (control == null) return;

                Style config = e.NewValue as Style;

                control.TryDraw();

            }));


        public Style TextStyle
        {
            get { return (Style)GetValue(TextStyleProperty); }
            set { SetValue(TextStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextStyleProperty =
            DependencyProperty.Register("TextStyle", typeof(Style), typeof(Layer), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Layer control = d as Layer;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

                 control.TryDraw();

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

        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(Layer), new PropertyMetadata(default(Style), (d, e) =>
            {
                Layer control = d as Layer;

                if (control == null) return;

                Style config = e.NewValue as Style;

                control.TryDraw();

            }));

    }

}
