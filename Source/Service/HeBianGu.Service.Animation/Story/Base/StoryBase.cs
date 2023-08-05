// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public abstract class StoryBase : IStory
    {
        public double Duration { get; set; } = 1000;

        public EasingFunctionBase Easing { get; set; }

        public bool AutoReverse { get; set; }

        public abstract void Stop();

        public abstract void Start(UIElement element);
    }

    public abstract class EngineStoryBase : StoryBase
    {
        public StoryboardEngineBase Engine { get; set; }

        public override void Stop()
        {
            if (this.Engine == null) return;

            this.Engine.Stop();
        }

        public override void Start(UIElement element)
        {
            if (this.Engine == null)
            {
                this.Engine = this.CreateEngine(element);
            }
            else
            {
                this.Engine.Start(element);
            }
        }

        protected abstract StoryboardEngineBase CreateEngine(UIElement element);
    }

    public abstract class EnginesStoryBase : StoryBase
    {
        public List<StoryboardEngineBase> Engines { get; set; }

        public override void Stop()
        {
            if (this.Engines == null) return;

            foreach (var item in this.Engines)
            {
                item.Stop();
            }
        }

        public override void Start(UIElement element)
        {
            if (this.Engines == null)
            {
                this.Engines = this.CreateEngines(element);
            }
            else
            {
                foreach (var item in this.Engines)
                {
                    item.Start(element);
                }
            } 
        }
        protected abstract List<StoryboardEngineBase> CreateEngines(UIElement element);
    }
}
