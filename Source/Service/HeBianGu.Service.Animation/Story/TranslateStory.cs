// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class TranslateStory : StoryBase
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public double Duration { get; set; } = 1000;

        private StoryboardEngineBase _engine;

        public TranslateStory()
        {
            this.AutoReverse = false;
        }
        public override void Start(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            if (_engine == null)
            {
                _engine = element.BeginAnimationX(this.StartPoint.X, this.EndPoint.X, this.Duration, null, l =>
                {
                    l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;

                    l.Storyboard.AutoReverse = this.AutoReverse;

                    l.Easing = this.Easing;
                });
                _engine = element.BeginAnimationY(this.EndPoint.Y, this.EndPoint.Y, this.Duration, null, l =>
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
