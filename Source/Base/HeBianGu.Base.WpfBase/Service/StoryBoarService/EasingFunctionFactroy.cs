using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{

    public static class EasingFunctionFactroy
    {
        /// <summary> 说明 </summary>
        public static PowerEase PowerEase { get; set; } = new PowerEase();
        /// <summary> 说明 </summary>
        public static BackEase BackEase { get; set; } = new BackEase();
        /// <summary> 说明 </summary>
        public static ElasticEase ElasticEase { get; set; } = new ElasticEase();
        /// <summary> 说明 </summary>
        public static BounceEase BounceEase { get; set; } = new BounceEase();
        /// <summary> 说明 </summary>
        public static CircleEase CircleEase { get; set; } = new CircleEase();
        /// <summary> 说明 </summary>
        public static QuadraticEase QuadraticEase { get; set; } = new QuadraticEase();
        /// <summary> 说明 </summary>
        public static QuarticEase QuarticEase { get; set; } = new QuarticEase();
        /// <summary> 说明 </summary>
        public static QuinticEase QuinticEase { get; set; } = new QuinticEase();

        /// <summary> 说明 </summary>
        public static ExponentialEase ExponentialEase { get; set; } = new ExponentialEase();



    }
}
