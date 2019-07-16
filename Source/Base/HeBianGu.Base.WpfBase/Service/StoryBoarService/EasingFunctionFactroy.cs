using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}
