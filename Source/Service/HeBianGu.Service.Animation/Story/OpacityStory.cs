// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class OpacityStory : EngineStoryBase
    {
        public double From { get; set; } = 1;

        public double To { get; set; } = 0;

        public OpacityStory()
        {
            this.AutoReverse = true;
        }

        protected override StoryboardEngineBase CreateEngine(UIElement element)
        {
           return element.BeginAnimationOpacity(this.From, this.To, this.Duration, null, l =>
           {
               //l.Storyboard.SlipBehavior = SlipBehavior.Slip;
               l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;
               l.Storyboard.AutoReverse = this.AutoReverse;
               l.Easing = this.Easing;
           });
        }
    }
}
