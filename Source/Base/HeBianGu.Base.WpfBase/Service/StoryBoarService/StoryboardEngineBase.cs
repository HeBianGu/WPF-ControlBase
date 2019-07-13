using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
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

        public abstract StoryboardEngineBase Start(UIElement element);

        public abstract StoryboardEngineBase Stop();

        public StoryboardEngineBase(int second, string property)
        {
            this.PropertyPath = new PropertyPath(property);
            this.Duration = new Duration(TimeSpan.FromSeconds(second));
        }

    }

    public abstract class StoryboardEngineBase<T> : StoryboardEngineBase
    {
        public StoryboardEngineBase(T from, T to, int second, string property) : base(second, property)
        {
            this.FromValue = from;
            this.ToValue = to;
        }

        public T FromValue { get; set; }

        public T ToValue { get; set; }

        //public RepeatBehavior RepeatBehavior { get; set; };

    }

 


}
