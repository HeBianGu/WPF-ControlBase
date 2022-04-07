// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media.Animation;

namespace HeBianGu.Service.Animation
{
    public abstract class StoryBase : IStory
    {
        public EasingFunctionBase Easing { get; set; }

        public bool AutoReverse { get; set; }

        public abstract void Start(UIElement element);

        public abstract void Stop();
    }
}
