// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public static DoubleStoryboardEngine Create(double from, double to, double second, string property)
        {
            return new DoubleStoryboardEngine(from, to, second, property);
        }

        public static void StartAll(UIElement element, params DoubleStoryboardEngine[] engines)
        {
            foreach (DoubleStoryboardEngine item in engines)
            {
                item.Start(element);
            }
        }

        /// <summary> 构造函数 </summary>
        /// <param name="from"> 起始值</param>
        /// <param name="to"> 结束值  </param>
        /// <param name="second"> 间隔时间秒 </param>
        /// <param name="property"> 修改属性名称 </param>
        public DoubleStoryboardEngine(double from, double to, double second, string property) : base(from, to, second, property)
        {

        }


        protected bool CanAnimation(UIElement element)
        {
            if (element is FrameworkElement framework)
            {
                return element.IsVisible && framework.IsLoaded;
            }
            return element.IsVisible;
        }

        public override StoryboardEngineBase Start(UIElement element, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            if (this.CanAnimation(element) == false)
            {
                Completed?.Invoke(element);
                return this;
            }
            DoubleAnimation animation = new DoubleAnimation(this.FromValue, this.ToValue, this.Duration);
            Storyboard.Children.Add(animation);
            Storyboard.SetTarget(animation, element);
            Storyboard.SetTargetProperty(animation, this.PropertyPath);
            if (CompletedEvent != null)
            {
                Storyboard.Completed += CompletedEvent;
            }
            if (Completed != null)
            {
                Storyboard.Completed += (l, k) =>
                {
                    Completed?.Invoke(element);
                };
            }
            Timeline.SetDesiredFrameRate(Storyboard, StoryboardSetting.DesiredFrameRate);
            init?.Invoke(this);
            animation.EasingFunction = this.Easing;
            Storyboard.Begin();
            return this;
        }
        public override StoryboardEngineBase Stop()
        {
            this.Storyboard.Stop();
            return this;
        }
    }
}
