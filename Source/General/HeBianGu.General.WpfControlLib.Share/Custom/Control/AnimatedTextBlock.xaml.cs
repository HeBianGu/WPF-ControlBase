using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    ///     Provides a control for displaying small amounts of flow content and animating its appearance.
    /// </summary>
    public class AnimatedTextBlock : TextBlock
    {
        #region Fields

        private enum State
        {
            Idle,
            CreatingRuns,
            Animating,
        }

        private static readonly PropertyPath _opacityPropertyPath = new PropertyPath("(0).(1)",
            TextElement.ForegroundProperty, Brush.OpacityProperty);

        private readonly Random _random = new Random();
        private readonly Storyboard _storyboard = new Storyboard();

        private State _state;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes the <see cref="AnimatedTextBlock" /> class.
        /// </summary>
        static AnimatedTextBlock()
        {
            TextProperty.OverrideMetadata(typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(CreateRunsOnPropertyChanged));
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        ///     Gets or sets the length of the segment.
        /// </summary>
        /// <value>The length of the segment.</value>
        public int SegmentLength
        {
            get { return (int)GetValue(SegmentLengthProperty); }
            set { SetValue(SegmentLengthProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="SegmentLength" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SegmentLengthProperty =
            DependencyProperty.Register("SegmentLength", typeof(int), typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(3, CreateRunsOnPropertyChanged));

        /// <summary>
        ///     Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public AnimatedTextMode Mode
        {
            get { return (AnimatedTextMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Mode" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(AnimatedTextMode), typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(AnimatedTextMode.RevealAndHide, ApplyAnimationsOnPropertyChanged));

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public AnimatedTextOrder Order
        {
            get { return (AnimatedTextOrder)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Order" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(AnimatedTextOrder), typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(AnimatedTextOrder.Random, ApplyAnimationsOnPropertyChanged));

        /// <summary>
        ///     Gets or sets the segment animation duration.
        /// </summary>
        /// <value>The segment animation duration.</value>
        public Duration Duration
        {
            get { return (Duration)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Duration" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            Timeline.DurationProperty.AddOwner(typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(500)),
                    ApplyAnimationsOnPropertyChanged));

        /// <summary>
        ///     Gets or sets the delay between segment animations.
        /// </summary>
        /// <value>The delay between segment animations.</value>
        public TimeSpan Delay
        {
            get { return (TimeSpan)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Delay" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DelayProperty =
            DependencyProperty.Register("Delay", typeof(TimeSpan), typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(TimeSpan.FromMilliseconds(125), ApplyAnimationsOnPropertyChanged));

        /// <summary>
        ///     Gets or sets the repeat behavior.
        /// </summary>
        /// <value>The repeat behavior.</value>
        public RepeatBehavior RepeatBehavior
        {
            get { return (RepeatBehavior)GetValue(RepeatBehaviorProperty); }
            set { SetValue(RepeatBehaviorProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="RepeatBehavior" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RepeatBehaviorProperty =
            Timeline.RepeatBehaviorProperty.AddOwner(typeof(AnimatedTextBlock),
                new FrameworkPropertyMetadata(ApplyAnimationsOnPropertyChanged));

        #endregion

        #region Methods

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.FrameworkElement.Initialized" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs" /> that contains the event data.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ApplyAnimations();
        }

        private void CreateRuns()
        {
            if (_state == State.Idle)
            {
                _state = State.CreatingRuns;

                string text = Text;

                Inlines.Clear();

                int segmentLength = SegmentLength;

                for (int i = 0; i < text.Length; i += segmentLength)
                {
                    string segment = i + segmentLength >= text.Length
                        ? text.Substring(i)
                        : text.Substring(i, segmentLength);
                    Inlines.Add(segment);
                }

                _state = State.Idle;

                if (IsInitialized)
                {
                    ApplyAnimations();
                }
            }
        }

        private void ApplyAnimations()
        {
            if (_state == State.Idle)
            {
                _state = State.Animating;

                _storyboard.Remove(this);
                _storyboard.Children.Clear();

                IEnumerable<Run> runs = Inlines.OfType<Run>();
                if (Order == AnimatedTextOrder.Random)
                {
                    runs = runs.OrderBy(r => _random.NextDouble());
                }
                else if (Order == AnimatedTextOrder.Backward)
                {
                    runs = runs.Reverse();
                }

                int timeIndex = 0;
                AnimatedTextMode mode = Mode;
                double initialOpacity = (mode == AnimatedTextMode.None || mode == AnimatedTextMode.Hide) ? 1 : 0;

                // ReSharper disable PossibleMultipleEnumeration
                if (mode != AnimatedTextMode.None)
                {
                    _storyboard.RepeatBehavior = RepeatBehavior;
                    Duration duration = Duration;
                    long delay = Delay.Ticks;

                    foreach (Run run in runs)
                    {
                        SetRunOpacity(run, initialOpacity);

                        var animation = new DoubleAnimation(initialOpacity, 1 - initialOpacity, duration)
                        {
                            BeginTime = new TimeSpan(delay * timeIndex++)
                        };
                        if (mode == AnimatedTextMode.Spotlight)
                        {
                            animation.AutoReverse = true;
                        }
                        Storyboard.SetTarget(animation, run);
                        Storyboard.SetTargetProperty(animation, _opacityPropertyPath);
                        _storyboard.Children.Add(animation);
                    }

                    if (mode == AnimatedTextMode.RevealAndHide)
                    {
                        foreach (Run run in runs)
                        {
                            var animation = new DoubleAnimation(1, 0, duration)
                            {
                                BeginTime = new TimeSpan(delay * timeIndex++)
                            };
                            Storyboard.SetTarget(animation, run);
                            Storyboard.SetTargetProperty(animation, _opacityPropertyPath);
                            _storyboard.Children.Add(animation);
                        }
                    }

                    _storyboard.Begin(this, true);
                }
                else
                {
                    foreach (Run run in runs)
                    {
                        SetRunOpacity(run, initialOpacity);
                    }
                }
                // ReSharper restore PossibleMultipleEnumeration

                _state = State.Idle;
            }
        }

        private static void CreateRunsOnPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedTextBlock)o).CreateRuns();
        }

        private static void ApplyAnimationsOnPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((AnimatedTextBlock)o).ApplyAnimations();
        }

        private static void SetRunOpacity(Run run, double opacity)
        {
            if (run.Tag == null)
            {
                run.Foreground = run.Foreground.Clone();
                run.Tag = true;
            }
            run.Foreground.BeginAnimation(Brush.OpacityProperty, null);
            run.Foreground.Opacity = opacity;
        }

        #endregion
    }

    /// <summary>
    ///     Specifies the type of the animation in the <see cref="AnimatedTextBlock" /> control.
    /// </summary>
    public enum AnimatedTextMode
    {
        /// <summary>
        ///     No animation.
        /// </summary>
        None,

        /// <summary>
        ///     Reveal the text.
        /// </summary>
        Reveal,

        /// <summary>
        ///     Hide the text.
        /// </summary>
        Hide,

        /// <summary>
        ///     Reveal the text and then hide it.
        /// </summary>
        RevealAndHide,

        /// <summary>
        ///     Highlight a segment of the text and move on.
        /// </summary>
        Spotlight,
    }

    /// <summary>
    ///     Specifies the order of the animation in the <see cref="AnimatedTextBlock" /> control.
    /// </summary>
    public enum AnimatedTextOrder
    {
        /// <summary>
        ///     Forward
        /// </summary>
        Forward,

        /// <summary>
        ///     Backward
        /// </summary>
        Backward,

        /// <summary>
        ///     Random
        /// </summary>
        Random,
    }
}
