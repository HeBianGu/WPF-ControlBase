// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary> 过度幻灯片 </summary>
    public class TransitionerSlide : TransitioningContentBase
    {
        static TransitionerSlide()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TransitionerSlide), new FrameworkPropertyMetadata(typeof(TransitionerSlide)));
        }

        public static readonly DependencyProperty TransitionOriginProperty = DependencyProperty.Register(
            "TransitionOrigin", typeof(Point), typeof(Transitioner), new PropertyMetadata(new Point(0.5, 0.5)));

        /// <summary> 过度动画参考点 </summary>
        public Point TransitionOrigin
        {
            get => (Point)GetValue(TransitionOriginProperty);
            set => SetValue(TransitionOriginProperty, value);
        }

        public static RoutedEvent InTransitionFinished =
            EventManager.RegisterRoutedEvent("InTransitionFinished", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(TransitionerSlide));

        protected void OnInTransitionFinished(RoutedEventArgs e)
        {
            RaiseEvent(e);
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(TransitionerSlideState), typeof(TransitionerSlide), new PropertyMetadata(default(TransitionerSlideState), new PropertyChangedCallback(StatePropertyChangedCallback)));

        private static void StatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TransitionerSlide)d).AnimateToState();
        }

        /// <summary> 过度标识 </summary>
        public TransitionerSlideState State
        {
            get => (TransitionerSlideState)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }

        public static readonly DependencyProperty ForwardWipeProperty = DependencyProperty.Register(
            "ForwardWipe", typeof(ITransitionWipe), typeof(TransitionerSlide), new PropertyMetadata(new CircleWipe()));

        /// <summary> 上一页过度效果 </summary>
        public ITransitionWipe ForwardWipe
        {
            get => (ITransitionWipe)GetValue(ForwardWipeProperty);
            set => SetValue(ForwardWipeProperty, value);
        }

        public static readonly DependencyProperty BackwardWipeProperty = DependencyProperty.Register(
            "BackwardWipe", typeof(ITransitionWipe), typeof(TransitionerSlide), new PropertyMetadata(new SlideOutWipe()));

        /// <summary> 下一页的过度效果 </summary>
        public ITransitionWipe BackwardWipe
        {
            get => (ITransitionWipe)GetValue(BackwardWipeProperty);
            set => SetValue(BackwardWipeProperty, value);
        }

        private void AnimateToState()
        {
            if (State != TransitionerSlideState.Current) return;

            RunOpeningEffects();
        }
    }
}
