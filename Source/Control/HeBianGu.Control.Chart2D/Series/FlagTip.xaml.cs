// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HeBianGu.Control.Chart2D
{
    public class FlagTip : Layer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(FlagTip), "S.FlagTip.Default");

        static FlagTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlagTip), new FrameworkPropertyMetadata(typeof(FlagTip)));
        }


        public FlagTip()
        {
            this.MouseMove += FlagTip_MouseMove;

            this.MouseEnter += (l, k) => this.RefreshVisibility();

            this.MouseLeave += (l, k) => this.RefreshVisibility();

        }

        private void RefreshVisibility()
        {
            if (this.X == null || this.Y == null) return;

            this.X.Visibility = this.IsMouseOver && (this.FlagTipType == FlagTipType.Cross || this.FlagTipType == FlagTipType.CrossStep || this.FlagTipType == FlagTipType.OnlyX || this.FlagTipType == FlagTipType.StepX) ? Visibility.Visible : Visibility.Collapsed;
            this.Y.Visibility = this.IsMouseOver && (this.FlagTipType == FlagTipType.Cross || this.FlagTipType == FlagTipType.CrossStep || this.FlagTipType == FlagTipType.OnlyY || this.FlagTipType == FlagTipType.StepY) ? Visibility.Visible : Visibility.Collapsed;

            this.LabelX.Visibility = this.IsMouseOver ? Visibility.Visible : Visibility.Collapsed;
            this.LabelY.Visibility = this.IsMouseOver ? Visibility.Visible : Visibility.Collapsed;

            this.Marker.Visibility = this.IsMouseOver ? Visibility.Visible : Visibility.Collapsed;

        }

        private void FlagTip_MouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(this);

            if (this.xAxis.Count == 0) return;
            if (this.Y == null) return;
            if (this.X == null) return;

            double xValue = (this.maxX - this.minX) * (position.X / this.ActualWidth) + this.minX;

            double yValue = (this.maxY - this.minY) * (position.Y / this.ActualHeight) + this.minY;



            if (this.FlagTipType == FlagTipType.CrossStep || this.FlagTipType == FlagTipType.StepY || this.FlagTipType == FlagTipType.StepX)
            {
                double mx = this.xAxis.Min(l => Math.Abs(l - xValue));
                double xFind = this.xAxis.FirstOrDefault(l => Math.Abs(l - xValue) == mx);

                Canvas.SetLeft(this.Y, this.GetX(xFind));

                Canvas.SetLeft(this.Marker, this.GetX(xFind));

                //double my = this.xAxis.Min(l => Math.Abs(l - yValue));
                //var yFind = this.xAxis.FirstOrDefault(l => Math.Abs(l - yValue) == my);
                //Canvas.SetBottom(this.X, this.GetY(yFind));

                int index = this.xAxis.IndexOf(xFind);

                if (this.Data.Count > index)
                {
                    double data = this.Data[index];
                    Canvas.SetTop(this.X, this.GetY(data));

                    this.LabelY.Content = $"{xFind}";
                    this.LabelX.Content = $"{data}";

                    Canvas.SetLeft(this.LabelX, -this.LabelX.ActualWidth);
                    Canvas.SetTop(this.LabelX, this.GetY(data));


                    Canvas.SetTop(this.Marker, this.GetY(data));
                }
                else
                {



                    double my = this.yAxis.Min(l => Math.Abs(l - yValue));
                    double yFind = this.yAxis.FirstOrDefault(l => Math.Abs(l - yValue) == my);

                    Canvas.SetBottom(this.X, this.GetY(yFind));
                    this.LabelY.Content = $"{xFind}";
                    this.LabelX.Content = $"{my}";

                    Canvas.SetLeft(this.LabelX, -this.LabelX.ActualWidth);
                    Canvas.SetTop(this.LabelX, position.Y);

                    Canvas.SetBottom(this.Marker, this.GetY(yFind));
                }
            }
            else
            {
                Canvas.SetLeft(this.Y, position.X);
                Canvas.SetTop(this.X, position.Y);
                this.LabelY.Content = $"{Math.Round(xValue, 2)}";
                this.LabelX.Content = $"{Math.Round(yValue, 2)}";

                Canvas.SetLeft(this.LabelX, -this.LabelX.ActualWidth);
                Canvas.SetTop(this.LabelX, position.Y);

                Canvas.SetLeft(this.Marker, position.X);
                Canvas.SetTop(this.Marker, position.Y);

            }


            Canvas.SetLeft(this.LabelY, position.X);
            Canvas.SetBottom(this.LabelY, -this.LabelX.ActualHeight);


        }

        public Path X
        {
            get { return (Path)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(Path), typeof(FlagTip), new PropertyMetadata(default(Path), (d, e) =>
            {
                FlagTip control = d as FlagTip;

                if (control == null) return;

                Path config = e.NewValue as Path;

            }));

        public Path Y
        {
            get { return (Path)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(Path), typeof(FlagTip), new PropertyMetadata(default(Path), (d, e) =>
            {
                FlagTip control = d as FlagTip;

                if (control == null) return;

                Path config = e.NewValue as Path;

            }));


        public Label LabelX
        {
            get { return (Label)GetValue(LabelXProperty); }
            set { SetValue(LabelXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelXProperty =
            DependencyProperty.Register("LabelX", typeof(Label), typeof(FlagTip), new PropertyMetadata(default(Label), (d, e) =>
            {
                FlagTip control = d as FlagTip;

                if (control == null) return;

                Label config = e.NewValue as Label;

            }));


        public Label LabelY
        {
            get { return (Label)GetValue(LabelYProperty); }
            set { SetValue(LabelYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelYProperty =
            DependencyProperty.Register("LabelY", typeof(Label), typeof(FlagTip), new PropertyMetadata(default(Label), (d, e) =>
            {
                FlagTip control = d as FlagTip;

                if (control == null) return;

                Label config = e.NewValue as Label;

            }));


        public Shape Marker
        {
            get { return (Shape)GetValue(MarkerProperty); }
            set { SetValue(MarkerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkerProperty =
            DependencyProperty.Register("Marker", typeof(Shape), typeof(FlagTip), new PropertyMetadata(default(Shape), (d, e) =>
             {
                 FlagTip control = d as FlagTip;

                 if (control == null) return;

                 Shape config = e.NewValue as Shape;

             }));



        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            this.X = new Path();

            this.X.Style = this.PathStyle;

            {
                PolyLineSegment pls = new PolyLineSegment();

                pls.Points.Add(new Point(this.GetX(this.minX), this.GetY(this.maxY)));
                pls.Points.Add(new Point(this.GetX(this.maxX), this.GetY(this.maxY)));

                PathFigure pf = new PathFigure();
                pf.StartPoint = pls.Points.FirstOrDefault();
                pf.Segments.Add(pls);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                pg.Figures.Add(pf);

                this.X.Data = pg;

                this.Children.Add(this.X);
            }

            this.Y = new Path();

            this.Y.Style = this.PathStyle;

            {
                PolyLineSegment pls = new PolyLineSegment();
                pls.Points.Add(new Point(this.GetX(this.minX), this.GetY(this.minY)));
                pls.Points.Add(new Point(this.GetX(this.minX), this.GetY(this.maxY)));

                PathFigure pf = new PathFigure();
                pf.StartPoint = pls.Points.FirstOrDefault();
                pf.Segments.Add(pls);

                PathGeometry pg = new PathGeometry(new List<PathFigure>() { pf });

                pg.Figures.Add(pf);

                this.Y.Data = pg;

                this.Children.Add(this.Y);
            }

            //  Do ：显示文本
            this.LabelX = new Label();
            this.LabelX.Style = this.LabelStyle;

            this.LabelY = new Label();
            this.LabelY.Style = this.LabelStyle;

            canvas.Children.Add(this.LabelX);
            canvas.Children.Add(this.LabelY);

            //  Do ：中心点
            this.Marker = new EllipseMarker();
            this.Marker.Style = this.MarkStyle;
            canvas.Children.Add(this.Marker);

        }


        public FlagTipType FlagTipType
        {
            get { return (FlagTipType)GetValue(FlagTipTypeProperty); }
            set { SetValue(FlagTipTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlagTipTypeProperty =
            DependencyProperty.Register("FlagTipType", typeof(FlagTipType), typeof(FlagTip), new PropertyMetadata(default(FlagTipType), (d, e) =>
            {
                FlagTip control = d as FlagTip;

                if (control == null) return;

                //FlagTipType config = e.NewValue as FlagTipType;

            }));
    }

    public enum FlagTipType
    {
        Cross, OnlyX, OnlyY, StepX, StepY, CrossStep
    }


}
