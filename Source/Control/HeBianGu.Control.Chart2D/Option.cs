using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{

    public interface IDraw
    {
        void Draw(Canvas canvas);
    }

    public class CanvasLayer : Canvas, IDraw
    {
        public CanvasLayer()
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
            canvas.Children.Clear();
        }
    }

    public class XyLayer : CanvasLayer
    {
        public XyLayer()
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
        public double GetX(double value, double width)
        {

            var bottom = ((value - this.minX) / (this.maxX - this.minX)) * width;

            return bottom;
        }

        protected double minY;
        protected double maxY;

        protected virtual void InitY()
        {
            if (this.xAxis == null || this.xAxis.Count == 0) return;

            this.minX = this.xAxis.Min();
            this.maxX = this.xAxis.Max();

        }
        protected virtual void InitX()
        {
            if (this.yAxis == null || this.yAxis.Count == 0) return;

            this.minY = this.yAxis.Min();
            this.maxY = this.yAxis.Max();
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public double GetY(double value, double height)
        {
            var bottom = height - ((value - this.minY) / (this.maxY - this.minY)) * height;

            return bottom;
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
                GridLayer control = d as GridLayer;

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
            DependencyProperty.Register("xAxis", typeof(ObservableCollection<double>), typeof(XyLayer), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
            {
                XyLayer control = d as XyLayer;

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
            DependencyProperty.Register("yAxis", typeof(ObservableCollection<double>), typeof(XyLayer), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

            }));

    }

    public class Option
    {
        /// <summary> 默认颜色列表 </summary>
        public Brush[] Color { get; set; }

        public Brush Background { get; set; }

    }

    /// <summary> 标题 </summary>
    public class Title : Label
    {

    }

    /// <summary> 图例 </summary>
    public class Legend : ListBox
    {

    }



    public class DataTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return value?.ToString().Split(',')?.Select(l => Convert.ToDouble(l))?.ToObservable();
        }
    }

    //public class xAxisTypeConverter : TypeConverter
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        return sourceType == typeof(string);
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {
    //        xAxis axis = new xAxis();

    //        axis.Data = value?.ToString().Split(',')?.Select(l => Convert.ToDouble(l))?.ToObservable();

    //        return axis;
    //    }
    //}

    //public class yAxisTypeConverter : TypeConverter
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        return sourceType == typeof(string);
    //    }

    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {
    //        yAxis axis = new yAxis();

    //        axis.Data = value?.ToString().Split(',')?.Select(l => Convert.ToDouble(l))?.ToObservable();

    //        return axis;
    //    }
    //}

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

                 //control.Draw(control);

             }));

    }







}
