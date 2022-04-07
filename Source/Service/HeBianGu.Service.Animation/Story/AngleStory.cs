// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class AngleStory : StoryBase
    {
        public double From { get; set; } = 0;

        public double To { get; set; } = 360;

        public double Duration { get; set; } = 1000;

        private StoryboardEngineBase _engine;

        public AngleStory()
        {
            this.AutoReverse = false;
        }
        public override void Start(UIElement element)
        {
            if (_engine == null)
            {
                _engine = element.BeginAnimationAngle(this.From, this.To, this.Duration, null, l =>
                {
                    l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;
                    l.Storyboard.AutoReverse = this.AutoReverse;
                    l.Easing = this.Easing;
                });
            }
            else
            {
                _engine.Start(element);
            }


        }

        public override void Stop()
        {
            if (_engine == null) return;

            _engine.Stop();
        }
    }
}
