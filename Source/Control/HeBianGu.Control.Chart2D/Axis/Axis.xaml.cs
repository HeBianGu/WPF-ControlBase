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
    public class Axis : XyLayer
    {

        public int LineLenght
        {
            get { return (int)GetValue(LineLenghtProperty); }
            set { SetValue(LineLenghtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineLenghtProperty =
            DependencyProperty.Register("LineLenght", typeof(int), typeof(Axis), new PropertyMetadata(5, (d, e) =>
             {
                 Axis control = d as Axis;

                 if (control == null) return;

                 //int config = e.NewValue as int;

             }));

        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.Register("LabelStyle", typeof(Style), typeof(Axis), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Axis control = d as Axis;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

    }

    public class xAxis : Axis
    {
        public bool AlignmentCenter
        {
            get { return (bool)GetValue(AlignmentCenterProperty); }
            set { SetValue(AlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignmentCenterProperty =
            DependencyProperty.Register("AlignmentCenter", typeof(bool), typeof(xAxis), new PropertyMetadata(default(bool), (d, e) =>
             {
                 xAxis control = d as xAxis;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

                 control.Draw(control);

             }));


        public bool AlignAlignmentCenter
        {
            get { return (bool)GetValue(AlignAlignmentCenterProperty); }
            set { SetValue(AlignAlignmentCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlignAlignmentCenterProperty =
            DependencyProperty.Register("AlignAlignmentCenter", typeof(bool), typeof(xAxis), new PropertyMetadata(default(bool), (d, e) =>
             {
                 xAxis control = d as xAxis;

                 if (control == null) return;

                 control.Draw(control);

             }));


        protected override void InitX()
        {
            base.InitX();

            if (this.AlignmentCenter)
            {
                double span = (this.maxX - this.minX) / this.xAxis.Count;

                this.maxX = this.maxX + span / 2;

                this.minX = this.minX - span / 2;
            }

        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            double span = this.AlignmentCenter ? this.AlignAlignmentCenter ? (this.maxX - this.minX) / (this.xAxis.Count) : 0 : 0;

            //  Do ：绘制坐标
            foreach (var item in this.xAxis)
            {
                //  Do ：刻度线
                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
                l.X1 = 0;
                l.X2 = 0;
                l.Y1 = 0;
                l.Y2 = this.LineLenght;
                l.Style = this.LineStyle;
                Canvas.SetLeft(l, this.GetX(item + span / 2, this.ActualWidth));
                canvas.Children.Add(l);

                //  Do ：底线
                System.Windows.Shapes.Line yright = new System.Windows.Shapes.Line();
                yright.X1 = 0;
                yright.X2 = this.ActualWidth;
                yright.Y1 = 0;
                yright.Y2 = 0;
                yright.Style = this.LineStyle;
                Canvas.SetBottom(yright, 0);
                canvas.Children.Add(yright);

                //  Do ：显示文本
                Label t = new Label();

                t.Content = this.Display.Count > this.xAxis.IndexOf(item) ? this.Display[this.xAxis.IndexOf(item)] : item.ToString();
                t.Style = this.LabelStyle;

                t.Loaded += (o, e) =>
                {
                    Canvas.SetLeft(t, this.GetX(item, this.ActualWidth) - t.ActualWidth / 2);
                    Canvas.SetTop(t, this.LineLenght);
                };
                canvas.Children.Add(t);
            }
        }

        //protected override void InitX()
        //{
        //    if (this.Data.Count > 0)
        //    {
        //        this.minX = this.Data.Min();

        //        this.maxX = this.Data.Max();
        //    }
        //}
    }

    public class yAxis : Axis
    {
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            //Y坐标
            foreach (var item in this.yAxis)
            {
                //  Do ：刻度
                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
                l.X1 = 0;
                l.X2 = this.LineLenght;
                l.Y1 = 0;
                l.Y2 = 0;
                l.Style = this.LineStyle;

                Canvas.SetTop(l, this.GetY(item, this.ActualHeight));
                Canvas.SetRight(l, 0);
                canvas.Children.Add(l);

                //  Do ：底线
                System.Windows.Shapes.Line xleft = new System.Windows.Shapes.Line();
                xleft.X1 = 0;
                xleft.X2 = 0;
                xleft.Y1 = 0;
                xleft.Y2 = this.ActualHeight;
                xleft.Style = this.LineStyle;
                Canvas.SetLeft(xleft, 0);

                canvas.Children.Add(xleft);

                // Todo ：绘制文本 
                Label t = new Label();
                t.Content = item;
                t.Style = this.LabelStyle;

                t.Loaded += (o, e) =>
                {
                    Canvas.SetTop(t, this.GetY(item, this.ActualHeight) - t.ActualHeight / 2);
                    Canvas.SetRight(t, this.LineLenght);
                };

                canvas.Children.Add(t);
            }
        }

        //protected override void InitY()
        //{
        //    if (this.Data.Count > 0)
        //    {
        //        this.minY = this.Data.Min();

        //        this.maxY = this.Data.Max();
        //    }
        //}
    }
}
