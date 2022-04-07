// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HeBianGu.Control.Chart2D
{
    /// <summary> 曲线视图 </summary>
    public class MarkTip : MarkPosition
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MarkTip), "S.MarkTip.Default");

        static MarkTip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkTip), new FrameworkPropertyMetadata(typeof(MarkTip)));
        }

        public MarkTip()
        {
            this.MouseMove += MarkTip_MouseMove;
            this.MouseUp += MarkTip_MouseUp;

            //this.MouseLeave += MarkTip_MouseLeave;
        }

        //private void MarkTip_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    ElementDragStateBehavior.SetIsDragging(this.Marker,false);
        //}

        private void MarkTip_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ElementDragStateBehavior.SetIsDragging(this.Marker, false);
        }

        private void MarkTip_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Marker == null) return;

            bool b = ElementDragStateBehavior.GetIsDragging(this.Marker);

            if (e.LeftButton == MouseButtonState.Released) return;

            if (!b) return;

            Point position = e.GetPosition(this);

            if (this.xAxis.Count == 0) return;

            double xValue = (this.maxX - this.minX) * (position.X / this.ActualWidth) + this.minX;

            double yValue = (this.maxY - this.minY) * (position.Y / this.ActualHeight) + this.minY;


            if (this.MarkTipType == MarkTipType.Step)
            {
                double mx = this.xAxis.Min(l => Math.Abs(l - xValue));
                double xFind = this.xAxis.FirstOrDefault(l => Math.Abs(l - xValue) == mx);

                Canvas.SetLeft(this.Marker, this.GetX(xFind));

                int index = this.xAxis.IndexOf(xFind);

                if (this.Data.Count > index)
                {
                    double data = this.Data[index];

                    Canvas.SetTop(this.Marker, this.GetY(data));

                    this.Marker.Content = $"({xFind.ToString("G3")},{data.ToString("G3")})";
                }
                else
                {
                    double my = this.yAxis.Min(l => Math.Abs(l - yValue));
                    double yFind = this.yAxis.FirstOrDefault(l => Math.Abs(l - yValue) == my);

                    Canvas.SetBottom(this.Marker, this.GetY(yFind));

                    this.Marker.Content = $"({xFind.ToString("G3")},{yFind.ToString("G3")})";
                }
            }
            else
            {

                Canvas.SetLeft(this.Marker, position.X);
                Canvas.SetTop(this.Marker, position.Y);

                this.Marker.Content = $"({position.X.ToString("G3")},{position.Y.ToString("G3")})";
            }
        }

        public Label Marker { get; set; }

        public override void DrawMark()
        {
            //  Do ：显示文本
            Label t = new Label();

            t.Style = this.LabelStyle;

            //  Do ：增加拖拽行为
            BehaviorCollection bc = Interaction.GetBehaviors(t);

            bc.Add(new ElementDragStateBehavior());

            this.Marker = t;

            Point point = this.GetPoint();

            t.Content = $"({point.X.ToString("G3")},{point.Y.ToString("G3")})";

            t.Loaded += (o, e) =>
            {
                if (this.xAxis.Count == 1)
                {
                    Canvas.SetLeft(t, this.ActualWidth / 2);
                    Canvas.SetTop(t, this.ActualHeight / 2);
                }
                else
                {

                    Canvas.SetLeft(t, this.GetX(point.X));
                    Canvas.SetTop(t, this.GetY(point.Y));
                }
            };

            this.Children.Add(this.Marker);
        }

        public MarkTipType MarkTipType
        {
            get { return (MarkTipType)GetValue(MarkTipTypeProperty); }
            set { SetValue(MarkTipTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkTipTypeProperty =
            DependencyProperty.Register("MarkTipType", typeof(MarkTipType), typeof(FlagTip), new PropertyMetadata(default(MarkTipType), (d, e) =>
            {
                FlagTip control = d as FlagTip;

                if (control == null) return;

                //MarkTipType config = e.NewValue as MarkTipType;

            }));

        //public MarkValueType DefaultMarkValueType
        //{
        //    get { return (MarkValueType)GetValue(DefaultMarkValueTypeProperty); }
        //    set { SetValue(DefaultMarkValueTypeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DefaultMarkValueTypeProperty =
        //    DependencyProperty.Register("DefaultMarkValueType", typeof(MarkValueType), typeof(FlagTip), new PropertyMetadata(MarkValueType.Max, (d, e) =>
        //    {
        //        FlagTip control = d as FlagTip;

        //        if (control == null) return;

        //        //MarkValueType config = e.NewValue as MarkValueType;

        //    }));


        //Point GetPoint()
        //{
        //    if (this.DefaultMarkValueType == MarkValueType.Default) return new Point(0, 0);

        //    if (this.Data.Count == 0) return new Point(0, 0);

        //    double v = double.NaN;

        //    if (this.DefaultMarkValueType == MarkValueType.Max)
        //        v = this.Data.Max();

        //    if (this.DefaultMarkValueType == MarkValueType.Min)
        //        v = this.Data.Min();

        //    if (this.DefaultMarkValueType == MarkValueType.First)
        //        v = this.Data.FirstOrDefault();

        //    if (this.DefaultMarkValueType == MarkValueType.Last)
        //        v = this.Data.LastOrDefault();

        //    int index = this.Data.IndexOf(v);

        //    double x = this.xAxis[index];

        //    return new Point(x, v);
        //}
    }


    public enum MarkTipType
    {
        Step, MousePosition
    }
}
