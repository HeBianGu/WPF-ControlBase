// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class AngleStory : EngineStoryBase
    {
        public double From { get; set; } = 0;

        public double To { get; set; } = 360;

        public AngleStory()
        {
            this.AutoReverse = false;
        }

        protected override StoryboardEngineBase CreateEngine(UIElement element)
        {
            return element.BeginAnimationAngle(this.From, this.To, this.Duration, null, l =>
            {
                l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;
                l.Storyboard.AutoReverse = this.AutoReverse;
                l.Easing = this.Easing;
            });
        }

    }
}
