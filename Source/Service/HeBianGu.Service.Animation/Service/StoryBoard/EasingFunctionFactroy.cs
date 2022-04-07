// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 说明：https://docs.microsoft.com/zh-cn/dotnet/framework/wpf/graphics-multimedia/easing-functions </summary>
    public static class EasingFunctionFactroy
    {
        /// <summary> PowerEase：创建加速和/或减速使用的公式的动画f(t) = tp其中 p 等于Power属性。 </summary>
        public static PowerEase PowerEase { get; set; } = new PowerEase();
        /// <summary> BackEase：略微收回动画的动作，然后再开始进行动画处理指示的路径中。 </summary>
        public static BackEase BackEase { get; set; } = new BackEase();
        /// <summary> ElasticEase：创建类似于弹簧来回直到静止的动画 </summary>
        public static ElasticEase ElasticEase { get; set; } = new ElasticEase();
        /// <summary> BounceEase：创建弹跳效果。 </summary>
        public static BounceEase BounceEase { get; set; } = new BounceEase();
        /// <summary> CircleEase：创建加速和/或减速使用循环函数的动画。 </summary>
        public static CircleEase CircleEase { get; set; } = new CircleEase();

        /// <summary> QuadraticEase：创建加速和/或减速使用的公式的动画f(t) = t2。 </summary>
        public static QuadraticEase QuadraticEase { get; set; } = new QuadraticEase();

        /// <summary> CubicEase：创建加速和/或减速使用的公式的动画f(t) = t3。 </summary>
        public static CubicEase CubicEase { get; set; } = new CubicEase();
        /// <summary> QuarticEase：创建加速和/或减速使用的公式的动画f(t) = t4。 </summary>
        public static QuarticEase QuarticEase { get; set; } = new QuarticEase();
        /// <summary> QuinticEase：创建加速和/或减速使用的公式的动画f(t) = t5。 </summary>
        public static QuinticEase QuinticEase { get; set; } = new QuinticEase();

        /// <summary> ExponentialEase：创建加速和/或减速使用指数公式的动画。 </summary>
        public static ExponentialEase ExponentialEase { get; set; } = new ExponentialEase();

        /// <summary> SineEase：创建加速和/或减速使用正弦公式的动画。 </summary>
        public static SineEase SineEase { get; set; } = new SineEase();

    }

    /// <summary> 指定次幂 </summary>
    public class PowerEasingFunction : EasingFunctionBase
    {
        public PowerEasingFunction()
            : base()
        {

        }

        public int Pow { get; set; } = 7;

        protected override double EaseInCore(double normalizedTime)
        {
            return Math.Pow(normalizedTime, Pow);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new PowerEasingFunction();
        }
    }

    /// <summary> 贝塞尔曲线 </summary>
    public class CubicBezierEase : IEasingFunction
    {
        private Point _p1;
        private Point _p2;
        private Point _c1;
        private Point _c2;

        public CubicBezierEase(Point p1, Point c1, Point c2, Point p2)
        {
            _p1 = p1;
            _c1 = c1;
            _c2 = c2;
            _p2 = p2;
        }

        public double Ease(double normalizedTime)
        {
            double t = normalizedTime;

            double invt = 1.0 - normalizedTime;

            Point result = new Point();

            result = result.Add(_p1.Multiply(invt * invt * invt));
            result = result.Add(_c1.Multiply(3 * invt * invt * t));
            result = result.Add(_c2.Multiply(3 * invt * t * t));
            result = result.Add(_p2.Multiply(t * t * t));

            return result.Y;
        }
    }

    public static class PointEx
    {
        public static Point Add(this Point point, Point value)
        {
            return new Point(point.X + value.X, point.Y + value.Y);
        }

        public static Point Multiply(this Point point, double value)
        {
            return new Point(point.X * value, point.Y * value);
        }
    }
}
