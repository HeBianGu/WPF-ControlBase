// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 网格 </summary>
    public class Grid : XyLayer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Grid), "S.Grid.Default");
        public static ComponentResourceKey CrossKey => new ComponentResourceKey(typeof(Grid), "S.Grid.Cross");
        public static ComponentResourceKey VerticalKey => new ComponentResourceKey(typeof(Grid), "S.Grid.Vertical");

        static Grid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Grid), new FrameworkPropertyMetadata(typeof(Grid)));
        }
        public Style HorizontalLineStyle
        {
            get { return (Style)GetValue(HorizontalLineStyleProperty); }
            set { SetValue(HorizontalLineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HorizontalLineStyleProperty =
            DependencyProperty.Register("HorizontalLineStyle", typeof(Style), typeof(Grid), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Grid control = d as Grid;

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
            DependencyProperty.Register("VerticalLineStyle", typeof(Style), typeof(Grid), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Grid control = d as Grid;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

             }));

        private void DrawBorder(Canvas canvas)
        {
            //  Do ：绘制边框
            System.Windows.Shapes.Line xleft = new System.Windows.Shapes.Line();
            xleft.X1 = 0;
            xleft.X2 = 0;
            xleft.Y1 = 0;
            xleft.Y2 = this.ActualHeight;
            xleft.Style = this.LineStyle;
            Canvas.SetLeft(xleft, 0);

            System.Windows.Shapes.Line xright = new System.Windows.Shapes.Line();
            xright.X1 = 0;
            xright.X2 = 0;
            xright.Y1 = 0;
            xright.Y2 = this.ActualHeight;
            xleft.Style = this.LineStyle;
            Canvas.SetRight(xright, 0);

            System.Windows.Shapes.Line yleft = new System.Windows.Shapes.Line();
            yleft.X1 = 0;
            yleft.X2 = this.ActualWidth;
            yleft.Y1 = 0;
            yleft.Y2 = 0;
            xleft.Style = this.LineStyle;
            Canvas.SetTop(yleft, 0);

            System.Windows.Shapes.Line yright = new System.Windows.Shapes.Line();
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

        private void DrawCross(Canvas canvas)
        {
            for (int i = 0; i < this.yAxis.Count; i++)
            {
                //if (i == 0) continue;

                double item = this.yAxis[i];

                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
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

            for (int i = 0; i < this.xAxis.Count; i++)
            {
                if (i == 0) continue;

                double item = this.xAxis[i];

                System.Windows.Shapes.Line l = new System.Windows.Shapes.Line();
                l.X1 = 0;
                l.Y1 = 0;
                l.Y2 = this.ActualHeight;
                l.Style = this.HorizontalLineStyle;
                l.X2 = 0;
                l.Width = 1;

                //Canvas.SetLeft(l, this.GetX(item, this.ActualWidth));

                l.Loaded += (o, e) =>
                {
                    if (this.xAxis.Count == 1)
                    {
                        Canvas.SetLeft(l, this.ActualWidth / 2);
                    }
                    else
                    {
                        Canvas.SetLeft(l, this.GetX(item));
                    }
                };
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
