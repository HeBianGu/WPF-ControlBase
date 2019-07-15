using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> DoubleAnimation动画引擎 </summary>
    public class DoubleStoryboardEngine : StoryboardEngineBase<double>
    {
        /// <summary> 构造方法 </summary>
        /// <param name="from"> 起始值</param>
        /// <param name="to"> 结束值  </param>
        /// <param name="second"> 间隔时间秒 </param>
        /// <param name="property"> 修改属性名称 </param>
        /// 
        public static DoubleStoryboardEngine Create(double from, double to, int second, string property)
        {
            return new DoubleStoryboardEngine(from, to, second, property);
        }
        /// <summary> 构造函数 </summary>
        /// <param name="from"> 起始值</param>
        /// <param name="to"> 结束值  </param>
        /// <param name="second"> 间隔时间秒 </param>
        /// <param name="property"> 修改属性名称 </param>
        public DoubleStoryboardEngine(double from, double to, int second, string property) : base(from, to, second, property)
        {

        }

        public override StoryboardEngineBase Start(UIElement element)
        {
            //  Do：时间线
            DoubleAnimation animation = new DoubleAnimation(1, 0, this.Duration);

            if (this.Easing != null)
                animation.EasingFunction = this.Easing;

            //if (this.RepeatBehavior != default(RepeatBehavior))
            //    animation.RepeatBehavior = (RepeatBehavior);

            //  Do：属性动画
            storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, this.PropertyPath);

            if (CompletedEvent != null)
                storyboard.Completed += CompletedEvent;
            storyboard.Begin();

            return this;
        }

        public override StoryboardEngineBase Stop()
        {
            this.storyboard.Stop();

            return this;
        }
    }
}
