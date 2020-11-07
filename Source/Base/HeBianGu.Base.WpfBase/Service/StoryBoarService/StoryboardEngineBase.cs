using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 动画引擎基类 </summary>
    public abstract class StoryboardEngineBase : IDisposable
    {
        protected Storyboard storyboard = new Storyboard();

        public EventHandler CompletedEvent { get; set; }

        public EasingFunctionBase Easing { get; set; } = EasingFunctionFactroy.PowerEase;

        public PropertyPath PropertyPath { get; set; }

        public Duration Duration { get; set; }

        public void Dispose()
        {
            storyboard.Completed -= CompletedEvent;
        }

        public abstract StoryboardEngineBase Start(UIElement element, Action<UIElement> Completed = null, Action<Storyboard> init = null);

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
