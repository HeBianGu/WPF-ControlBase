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
    /// <summary> 网格 </summary>
    public class GridLayer : XyLayer
    {
        public Style HorizontalLineStyle
        {
            get { return (Style)GetValue(HorizontalLineStyleProperty); }
            set { SetValue(HorizontalLineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalLineStyleProperty =
            DependencyProperty.Register("HorizontalLineStyle", typeof(Style), typeof(GridLayer), new PropertyMetadata(default(Style), (d, e) =>
             {
                 GridLayer control = d as GridLayer;

                 if (control == null) return;

                 Style config = e.NewValue as Style; 

             }));



        public Style VerticalLineStyle
        {
            get { return (Style)GetValue(VerticalLineStyleProperty); }
            set { SetValue(VerticalLineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VerticalLineStyleProperty =
            DependencyProperty.Register("VerticalLineStyle", typeof(Style), typeof(GridLayer), new PropertyMetadata(default(Style), (d, e) =>
             {
                 GridLayer control = d as GridLayer;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));
     
        void DrawBorder(Canvas canvas)
        {
            //  Do ：绘制边框
            Line xleft = new Line();
            xleft.X1 = 0;
            xleft.X2 = 0;
            xleft.Y1 = 0;
            xleft.Y2 = this.ActualHeight;
            xleft.Style = this.LineStyle;
            Canvas.SetLeft(xleft, 0);

            Line xright = new Line();
            xright.X1 = 0;
            xright.X2 = 0;
            xright.Y1 = 0;
            xright.Y2 = this.ActualHeight;
            xleft.Style = this.LineStyle;
            Canvas.SetRight(xright, 0);

            Line yleft = new Line();
            yleft.X1 = 0;
            yleft.X2 = this.ActualWidth;
            yleft.Y1 = 0;
            yleft.Y2 = 0;
            xleft.Style = this.LineStyle;
            Canvas.SetTop(yleft, 0);

            Line yright = new Line();
            yright.X1 = 0;
            yright.X2 = this.ActualWidth;
            yright.Y1 = 0;
            yright.Y2 = 0;
            xleft.Style = this.LineStyle;
            Canvas.SetBottom(yright, 0);


            this.Children.Add(xleft);
            this.Children.Add(xright);
            this.Children.Add(yleft);
            this.Children.Add(yright);
        }

        void DrawCross(Canvas canvas)
        {
            foreach (var item in this.yAxis)
            {
                Line l = new Line();
                l.X1 = 0;
                l.Y1 = 0;
                l.Y2 = 0;
                l.Height = 1;
                l.Style = this.VerticalLineStyle;
                l.X2 = this.ActualWidth;

                //   l.MouseEnter +=(m, k) =>
                //{
                //    l.Stroke = Brushes.Red;

                //};


                Canvas.SetTop(l, this.GetY(item, this.ActualHeight));

                this.Children.Add(l);
            }

            foreach (var item in this.xAxis)
            {
                Line l = new Line();
                l.X1 = 0;
                l.Y1 = 0;
                l.Y2 = this.ActualHeight;
                l.Style = this.HorizontalLineStyle;
                l.X2 = 0;
                l.Width = 1;

                Canvas.SetLeft(l, this.GetX(item, this.ActualWidth));
                this.Children.Add(l);
            }
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.DrawBorder(this);

            this.DrawCross(this);
        }

        //protected override void InitX()
        //{
        //    if (this.xAxis.Count > 0)
        //    {
        //        this.minX = this.xAxis.Min();
        //        this.maxX = this.xAxis.Max();
        //    }
        //}

        //protected override void InitY()
        //{
        //    if (this.yAxis.Count > 0)
        //    {
        //        this.minY = this.yAxis.Min();
        //        this.maxY = this.yAxis.Max();
        //    }
        //}
    }
}
