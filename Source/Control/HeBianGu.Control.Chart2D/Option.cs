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
    public class LayerCanvas : Canvas, IDraw
    {
        public LayerCanvas()
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
            this.InitX();

            this.InitY();

            canvas.Children.Clear();
        }



        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(LayerCanvas), new PropertyMetadata(Brushes.Black, (d, e) =>
             {
                 LayerCanvas control = d as LayerCanvas;

                 if (control == null) return;

                 Brush config = e.NewValue as Brush;

             }));

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

        }
        protected virtual void InitX()
        {

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
            DependencyProperty.Register("LineStyle", typeof(Style), typeof(LayerCanvas), new PropertyMetadata(default(Style), (d, e) =>
            {
                Grid control = d as Grid;

                if (control == null) return;

                Style config = e.NewValue as Style;

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

    public class DataLayer : LayerCanvas
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
