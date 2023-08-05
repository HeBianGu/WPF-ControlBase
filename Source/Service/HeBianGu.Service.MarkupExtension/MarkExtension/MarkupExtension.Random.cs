// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media;

namespace HeBianGu.Service.MarkupExtension
{
    /// <summary> 生成随机数据 </summary>
    public abstract class RandomExtension<T> : System.Windows.Markup.MarkupExtension
    {
        public T From { get; set; }

        public T To { get; set; }

        public static Random random = new Random();
    }

    public class IntRandowmExtension : RandomExtension<int>
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return random.Next(this.From, this.To);
        }
    }

    public class DoubleRandowmExtension : RandomExtension<double>
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return random.NextDouble() * (this.To - this.From) + this.From;
        }
    } 

    public class PointExtension : System.Windows.Markup.MarkupExtension
    {
        public double X { get; set; }

        public double Y { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new Point(X, Y);
        }
    }


    public class PointRandomExtension : RandomExtension<Point>
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            double x = random.NextDouble() * (this.To.X - this.From.X) + this.From.X;

            double y = random.NextDouble() * (this.To.Y - this.From.Y) + this.From.Y;

            return new Point(x, y);
        }
    }


    public class RectMarkupExtension : System.Windows.Markup.MarkupExtension
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new Rect(X, Y, Width, Height);
        }
    }

    public class RectRandomExtension : RandomExtension<Rect>
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            double x = random.NextDouble() * (this.To.X - this.From.X) + this.From.X;

            double y = random.NextDouble() * (this.To.Y - this.From.Y) + this.From.Y;

            double w = random.NextDouble() * (this.To.Width - this.From.Width) + this.From.Width;

            double h = random.NextDouble() * (this.To.Height - this.From.Height) + this.From.Height;

            return new Rect(x, y, w, h);
        }
    }
}
