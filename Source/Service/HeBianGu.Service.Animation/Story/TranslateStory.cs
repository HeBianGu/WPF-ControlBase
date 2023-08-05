// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public class TranslateStory : EnginesStoryBase
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public TranslateStory()
        {
            this.AutoReverse = false;
        }

        public override void Start(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.Start(element);
        }

        protected override List<StoryboardEngineBase> CreateEngines(UIElement element)
        {
            List<StoryboardEngineBase> result = new List<StoryboardEngineBase>();
            {
                var engine = element.BeginAnimationX(this.StartPoint.X, this.EndPoint.X, this.Duration, null, l =>
                {
                    l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;

                    l.Storyboard.AutoReverse = this.AutoReverse;

                    l.Easing = this.Easing;
                });
                result.Add(engine);
            }

            {
                var engine = element.BeginAnimationY(this.EndPoint.Y, this.EndPoint.Y, this.Duration, null, l =>
                {
                    l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;

                    l.Storyboard.AutoReverse = this.AutoReverse;

                    l.Easing = this.Easing;
                });
                result.Add(engine);
            }

            return result;
        }
    }


    public class ScaleStory : EnginesStoryBase
    {
        public double StartX { get; set; } = 0.5;

        public double EndX { get; set; } = 1.5;

        public double StartY { get; set; } = 0.5;

        public double EndY { get; set; } = 1.5;

        public Point RenderTransformOrigin { get; set; } = new Point(0.5, 0.5);

        public override void Start(UIElement element)
        {
            if (!element.CheckDefaultTransformGroup()) return;
            element.RenderTransformOrigin = this.RenderTransformOrigin;
            base.Start(element);
        }

        protected override List<StoryboardEngineBase> CreateEngines(UIElement element)
        {
            List<StoryboardEngineBase> result = new List<StoryboardEngineBase>();

            {
                var engine = element.BeginAnimationScaleX(this.StartX, this.EndX, this.Duration, null, l =>
                {
                    l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;
                    l.Storyboard.AutoReverse = this.AutoReverse;
                    l.Easing = this.Easing;
                });

                result.Add(engine);
            }


            {
                var engine = element.BeginAnimationScaleY(this.StartY, this.EndY, this.Duration, null, l =>
                {
                    l.Storyboard.RepeatBehavior = RepeatBehavior.Forever;
                    l.Storyboard.AutoReverse = this.AutoReverse;
                    l.Easing = this.Easing;
                });

                result.Add(engine);
            }

            return result;
        }

    }
}
