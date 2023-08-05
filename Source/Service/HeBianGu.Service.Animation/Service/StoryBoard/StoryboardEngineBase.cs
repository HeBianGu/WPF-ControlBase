// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 动画引擎基类 </summary>
    public abstract class StoryboardEngineBase : IDisposable
    {
        public Storyboard Storyboard { get; set; } = StoryboardFactory.Create();

        public EventHandler CompletedEvent { get; set; }

        public EasingFunctionBase Easing { get; set; } = EasingFunctionFactroy.PowerEase;

        public PropertyPath PropertyPath { get; set; }

        public Duration Duration { get; set; }

        public void Dispose()
        {
            Storyboard.Completed -= CompletedEvent;
        }

        public abstract StoryboardEngineBase Start(UIElement element, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null);

        //public abstract StoryboardEngineBase Start(Animatable element, Action<Animatable> Completed = null, Action<Storyboard Timeline.DesiredFrameRate="{x:Static h:StoryboardSetting.DesiredFrameRate}"> init = null);

        public abstract StoryboardEngineBase Stop();

        public StoryboardEngineBase(double second, string property)
        {
            this.PropertyPath = new PropertyPath(property);
            this.Duration = new Duration(TimeSpan.FromSeconds(second));
        }

    }

    /// <summary> 动画泛型引擎基类 </summary>
    public abstract class StoryboardEngineBase<T> : StoryboardEngineBase
    {
        public StoryboardEngineBase(T from, T to, double second, string property) : base(second, property)
        {
            this.FromValue = from;
            this.ToValue = to;
        }

        public T FromValue { get; set; }

        public T ToValue { get; set; }

        //public RepeatBehavior RepeatBehavior { get; set; };

    }




}
