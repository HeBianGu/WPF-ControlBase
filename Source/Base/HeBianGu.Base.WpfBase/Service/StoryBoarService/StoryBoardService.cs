using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public class StoryBoardService
    {

        /// <summary>  喷泉效果  </summary>
        /// <param name="cav">画布</param>
        /// <param name="uclist">展示集合</param>
        /// <param name="pL">喷出点左</param>
        /// <param name="pT">喷出点上</param>
        /// <param name="Mul">放大倍数</param>
        /// <param name="middle_value">放大时间点</param>
        /// <param name="end_value">还原时间点</param>
        public static void FountainAnimation(List<DependencyObject> uclist, double pL = 0, double pT = 0, double Mul = 10, double middle_value = 0.5, double end_value = 1)
        {
            if (uclist.Count <= 0)
            {
                return;
            }
            Storyboard storyboard = new Storyboard();

            double Init = 0;
            double Org = 1;
            double first_value = 0;
            Random r2 = new Random();

            for (int i = 0; i < uclist.Count; i++)
            {
                double first = i * 0.05 + first_value;
                double middle = i * 0.05 + middle_value;
                double end = i * 0.05 + end_value;

                var c = uclist[i];

                EasingDoubleKeyFrame edf0 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));//所有元素起点都是0
                EasingDoubleKeyFrame edf1 = new EasingDoubleKeyFrame(Init, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(first)));
                EasingDoubleKeyFrame edf2 = new EasingDoubleKeyFrame(Mul, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
                EasingDoubleKeyFrame edf3 = new EasingDoubleKeyFrame(Org, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));

                DoubleAnimationUsingKeyFrames daukf1 = new DoubleAnimationUsingKeyFrames();
                daukf1.KeyFrames.Add(edf0);
                daukf1.KeyFrames.Add(edf1);
                daukf1.KeyFrames.Add(edf2);
                daukf1.KeyFrames.Add(edf3);
                storyboard.Children.Add(daukf1);
                Storyboard.SetTarget(daukf1, c);
                Storyboard.SetTargetProperty(daukf1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

                DoubleAnimationUsingKeyFrames daukf2 = new DoubleAnimationUsingKeyFrames();
                daukf2.KeyFrames.Add(edf0);
                daukf2.KeyFrames.Add(edf1);
                daukf2.KeyFrames.Add(edf2);
                daukf2.KeyFrames.Add(edf3);
                storyboard.Children.Add(daukf2);
                Storyboard.SetTarget(daukf2, c);
                Storyboard.SetTargetProperty(daukf2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));

                DoubleAnimationUsingKeyFrames daukf3 = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame edf31 = new EasingDoubleKeyFrame(r2.Next(-1000, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
                //EasingDoubleKeyFrame edf31 = new EasingDoubleKeyFrame(r.Next(200, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
                EasingDoubleKeyFrame edf32 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
                daukf3.KeyFrames.Add(edf31);
                daukf3.KeyFrames.Add(edf32);
                storyboard.Children.Add(daukf3);
                Storyboard.SetTarget(daukf3, c);
                Storyboard.SetTargetProperty(daukf3, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)"));

                DoubleAnimationUsingKeyFrames daukf4 = new DoubleAnimationUsingKeyFrames();
                EasingDoubleKeyFrame edf41 = new EasingDoubleKeyFrame(r2.Next(-1000, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)));
                //EasingDoubleKeyFrame edf41 = new EasingDoubleKeyFrame(r2.Next(200, 1000), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(middle)));
                EasingDoubleKeyFrame edf42 = new EasingDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(end)));
                daukf4.KeyFrames.Add(edf41);
                daukf4.KeyFrames.Add(edf42);
                storyboard.Children.Add(daukf4);
                Storyboard.SetTarget(daukf4, c);
                Storyboard.SetTargetProperty(daukf4, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            }
            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();
        }
    }

    public static class AnimatorService
    {
        #region Public Methods

        public static void AnimatePropertyTo(DependencyObject target, DependencyProperty property, double targetValue, int duration)
        {
            AnimatePropertyTo(target, property, targetValue, duration, new DoubleAnimation(), null, 0);
        }

        public static void AnimatePropertyTo(DependencyObject target, DependencyProperty property, double targetValue, int duration, DoubleAnimation animation)
        {
            AnimatePropertyTo(target, property, targetValue, duration, animation, null, 0);
        }

        public static void AnimatePropertyTo(DependencyObject target, DependencyProperty property, double targetValue, int duration, EventHandler animationDone)
        {
            AnimatePropertyTo(target, property, targetValue, duration, new DoubleAnimation(), animationDone, 0);
        }

        public static void AnimatePropertyTo(DependencyObject target, DependencyProperty property, double targetValue, int duration, DoubleAnimation animation, EventHandler animationDone)
        {
            AnimatePropertyTo(target, property, targetValue, duration, animation, animationDone, 0);
        }

        public static void AnimatePropertyTo(DependencyObject target, DependencyProperty property, double targetValue, int duration, DoubleAnimation animation, EventHandler animationDone, int startTime)
        {
            AnimatePropertyFromTo(target, property, null, targetValue, duration, animation, animationDone, startTime);
        }

        public static void AnimatePropertyFromTo(DependencyObject target, DependencyProperty property, double? fromValue, double? targetValue, int duration)
        {
            AnimatePropertyFromTo(target, property, fromValue, targetValue, duration, new DoubleAnimation(), null, 0);
        }

        public static void AnimatePropertyFromTo(DependencyObject target, DependencyProperty property, double? fromValue, double? targetValue, int duration, EventHandler animationDone)
        {
            AnimatePropertyFromTo(target, property, fromValue, targetValue, duration, new DoubleAnimation(), animationDone, 0);
        }

        public static void AnimatePropertyFromTo(DependencyObject target, DependencyProperty property, double? fromValue, double? targetValue, int duration, DoubleAnimation animation, EventHandler animationDone, int startTime)
        {
            if (property.PropertyType != typeof(double))
            {
                throw new ArgumentException( "property");
            }
            if (!(target is IAnimatable))
            {
                throw new ArgumentException("target");
            }
            animation.From = fromValue;
            animation.To = targetValue;
            animation.BeginTime = TimeSpan.FromMilliseconds(startTime);
            animation.Duration = TimeSpan.FromMilliseconds(duration);
            if (animationDone != null)
            {
                animation.AttachCompletedEventHandler(animationDone);
            }
            AnimateProperty(target, property, animation, animationDone);
        }

        #endregion

        #region Public Properties

        public static bool IsAnimationEnabled
        {
            get
            {
                if (Policy == AnimationsPolicy.Detect)
                {
                    return (RenderCapability.Tier >= 0x20000);
                }
                return (Policy > AnimationsPolicy.Disable);
            }
        }

        public static AnimationsPolicy Policy { get; set; }

        #endregion

        #region AnimationsPolicy enumeration

        public enum AnimationsPolicy
        {
            Detect,
            Disable,
            Enable,
            Debug
        }

        #endregion

        #region Private Methods

        private static void AnimateProperty(DependencyObject target, DependencyProperty property, DoubleAnimation animation, EventHandler animationDone)
        {
            if (IsAnimationEnabled)
            {
                ((IAnimatable)target).BeginAnimation(property, animation);
            }
            else
            {
                ((IAnimatable)target).BeginAnimation(property, null);
                if (animation.To != null)
                {
                    target.SetValue(property, animation.To);
                }
                else
                {
                    target.ClearValue(property);
                }

                if (animationDone != null)
                {
                    animationDone(target, EventArgs.Empty);
                }
            }
        }

        #endregion
    }

    /// <summary>
    ///     Encapsulates methods and properties for handling animations.
    /// </summary>
    public static class AnimationHelpers
    {
        #region Methods

        /// <summary>
        ///     Switches between the To and From properties of the each AnimationTimeline in the Storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard.</param>
        public static void ReverseStoryboard(this Storyboard storyboard)
        {
            foreach (var anim in storyboard.Children.OfType<AnimationTimeline>())
            {
                DependencyProperty from = anim.GetDependencyProperty("From");
                DependencyProperty to = anim.GetDependencyProperty("To");

                object fromValue = anim.GetValue(from);
                anim.SetValue(from, anim.GetValue(to));
                anim.SetValue(to, fromValue);
            }
        }

        /// <summary>
        ///     Returns a cloned Storyboard where the To and From properties of the AnimationTimeline have been switched.
        /// </summary>
        /// <param name="storyboard">The storyboard.</param>
        /// <returns></returns>
        public static Storyboard GetReversedStoryboard(this Storyboard storyboard)
        {
            Storyboard cloned = storyboard.Clone();

            ReverseStoryboard(cloned);

            return cloned;
        }

        /// <summary>
        ///     Creates and adds an AnimationTimeline to a Storyboard.
        /// </summary>
        /// <typeparam name="TAnimation">The type of the animation.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="storyboard">The storyboard.</param>
        /// <param name="path">The path.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="duration">The duration.</param>
        /// <returns></returns>
        public static TAnimation AddLinearAnimation<TAnimation, T>(this Storyboard storyboard, PropertyPath path,
            T? from, T? to, Duration duration)
            where TAnimation : AnimationTimeline, new()
            where T : struct
        {
            var timeline = new TAnimation();

            DependencyProperty fromProp = timeline.GetDependencyProperty("From");
            DependencyProperty toProp = timeline.GetDependencyProperty("To");

            timeline.Duration = duration;

            timeline.SetValue(fromProp, from);
            timeline.SetValue(toProp, to);

            Storyboard.SetTargetProperty(timeline, path);

            storyboard.Children.Add(timeline);

            return timeline;
        }

        /// <summary>
        ///     Adds the animation to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard.</param>
        /// <param name="timeline">The timeline.</param>
        /// <param name="target">The target.</param>
        /// <param name="property">The property.</param>
        public static void AddAnimation(this Storyboard storyboard, Timeline timeline, DependencyObject target,
            DependencyProperty property)
        {
            Storyboard.SetTarget(timeline, target);
            Storyboard.SetTargetProperty(timeline, new PropertyPath(property));
            storyboard.Children.Add(timeline);
        }

        /// <summary>
        ///     Adds the animation to the storyboard.
        /// </summary>
        /// <param name="storyboard">The storyboard.</param>
        /// <param name="timeline">The timeline.</param>
        /// <param name="targetName">Name of the target.</param>
        /// <param name="property">The property.</param>
        public static void AddAnimation(this Storyboard storyboard, Timeline timeline, string targetName,
            DependencyProperty property)
        {
            Storyboard.SetTargetName(timeline, targetName);
            Storyboard.SetTargetProperty(timeline, new PropertyPath(property));
            storyboard.Children.Add(timeline);
        }

        /// <summary>
        /// Attaches the specified event handler to the <see cref="E:Timeline.Completed"/> event.
        /// <remarks>
        /// Also ensures that the reference is released upon completion.
        /// </remarks>
        /// </summary>
        /// <param name="timeline">The timeline.</param>
        /// <param name="handler">The handler.</param>
        public static void AttachCompletedEventHandler(this Timeline timeline, EventHandler handler)
        {
            var completionHandler = new AnimationCompletedHandler(timeline, handler);
            timeline.Completed += completionHandler.OnTimelineCompleted;
        }

        /// <summary>
        /// Provides a closure so that allows the <see cref="E:Timeline.Completed"/> event to be disconnected.
        /// </summary>
        public class AnimationCompletedHandler
        {
            private readonly Timeline _timeline;
            private readonly EventHandler _handler;

            internal AnimationCompletedHandler(Timeline timeline, EventHandler handler)
            {
                _timeline = timeline;
                _handler = handler;
            }

            internal void OnTimelineCompleted(object sender, EventArgs e)
            {
                _timeline.Completed -= OnTimelineCompleted;
                _handler(_timeline, EventArgs.Empty);
            }
        }

        #endregion

        #region Attached Properties

        #region ActualWidth (private)

        private static double? GetActualWidth(FrameworkElement obj)
        {
            return (double?)obj.GetValue(ActualWidthProperty);
        }

        private static void SetActualWidth(FrameworkElement obj, double? value)
        {
            obj.SetValue(ActualWidthPropertyKey, value);
        }

        private static void ClearActualWidth(FrameworkElement obj)
        {
            obj.ClearValue(ActualWidthPropertyKey);
        }

        private static readonly DependencyPropertyKey ActualWidthPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ActualWidth", typeof(double?), typeof(AnimationHelpers),
                new FrameworkPropertyMetadata());

        private static readonly DependencyProperty ActualWidthProperty = ActualWidthPropertyKey.DependencyProperty;

        #endregion

        #region WidthPercentage

        /// <summary>
        ///     Gets the width percentage.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static int GetWidthPercentage(FrameworkElement obj)
        {
            return (int)obj.GetValue(WidthPercentageProperty);
        }

        /// <summary>
        ///     Sets the width percentage.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetWidthPercentage(FrameworkElement obj, int value)
        {
            obj.SetValue(WidthPercentageProperty, value);
        }

        /// <summary>
        ///     Identifies the <c>WidthPercentage</c> attached property.
        /// </summary>
        public static readonly DependencyProperty WidthPercentageProperty =
            DependencyProperty.RegisterAttached("WidthPercentage", typeof(int), typeof(AnimationHelpers),
                new FrameworkPropertyMetadata(100, OnWidthPercentageChanged, CoercePercentage));

        private static void OnWidthPercentageChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)o;

            var percent = (int)e.NewValue;

            if (percent == 100)
            {
                element.ClearValue(FrameworkElement.WidthProperty);
                ClearActualWidth(element);
            }
            else
            {
                double? actualWidth = GetActualWidth(element);
                if (actualWidth == null)
                {
                    if (element.IsArrangeValid)
                    {
                        actualWidth = element.ActualWidth;
                        SetActualWidth(element, actualWidth);
                    }
                    else
                    {
                        element.Loaded += DeferActualWidth;
                    }
                }

                if (actualWidth != null)
                {
                    SetWidth(element, percent, actualWidth.Value);
                }
            }
        }

        private static void DeferActualWidth(object sender, RoutedEventArgs e)
        {
            var fe = (FrameworkElement)sender;
            fe.Loaded -= DeferActualWidth;

            SetActualWidth(fe, fe.ActualWidth);
            SetWidth(fe, GetWidthPercentage(fe), fe.ActualWidth);
        }

        private static void SetWidth(FrameworkElement element, int percent, double actualWidth)
        {
            element.Width = (percent / 100D) * actualWidth;
        }

        #endregion

        #region ActualHeight (private)

        private static double? GetActualHeight(FrameworkElement obj)
        {
            return (double?)obj.GetValue(ActualHeightProperty);
        }

        private static void SetActualHeight(FrameworkElement obj, double? value)
        {
            obj.SetValue(ActualHeightPropertyKey, value);
        }

        private static void ClearActualHeight(FrameworkElement obj)
        {
            obj.ClearValue(ActualHeightPropertyKey);
        }

        private static readonly DependencyPropertyKey ActualHeightPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly("ActualHeight", typeof(double?), typeof(AnimationHelpers),
                new FrameworkPropertyMetadata());

        private static readonly DependencyProperty ActualHeightProperty = ActualHeightPropertyKey.DependencyProperty;

        #endregion

        #region HeightPercentage

        /// <summary>
        ///     Gets the height percentage.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static int GetHeightPercentage(FrameworkElement obj)
        {
            return (int)obj.GetValue(HeightPercentageProperty);
        }

        /// <summary>
        ///     Sets the height percentage.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="value">The value.</param>
        public static void SetHeightPercentage(FrameworkElement obj, int value)
        {
            obj.SetValue(HeightPercentageProperty, value);
        }

        /// <summary>
        ///     Identifies the <c>HeightPercentage</c> attached property.
        /// </summary>
        public static readonly DependencyProperty HeightPercentageProperty =
            DependencyProperty.RegisterAttached("HeightPercentage", typeof(int), typeof(AnimationHelpers),
                new FrameworkPropertyMetadata(100, OnHeightPercentageChanged, CoercePercentage));

        private static void OnHeightPercentageChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)o;

            var percent = (int)e.NewValue;

            if (percent == 100)
            {
                element.ClearValue(FrameworkElement.HeightProperty);
                ClearActualHeight(element);
            }
            else
            {
                double? actualHeight = GetActualHeight(element);
                if (actualHeight == null)
                {
                    if (element.IsArrangeValid)
                    {
                        actualHeight = element.ActualHeight;
                        SetActualHeight(element, actualHeight);
                    }
                    else
                    {
                        element.Loaded += DeferActualHeight;
                    }
                }

                if (actualHeight != null)
                {
                    SetHeight(element, percent, actualHeight.Value);
                }
            }
        }

        private static void DeferActualHeight(object sender, RoutedEventArgs e)
        {
            var fe = (FrameworkElement)sender;
            fe.Loaded -= DeferActualHeight;

            SetActualHeight(fe, fe.ActualHeight);
            SetHeight(fe, GetHeightPercentage(fe), fe.ActualHeight);
        }

        private static void SetHeight(FrameworkElement element, int percent, double actualHeight)
        {
            element.Height = (percent / 100D) * actualHeight;
        }

        #endregion

        #region Private Methods

        private static object CoercePercentage(DependencyObject o, object value)
        {
            var current = (int)value;

            if (current < 0)
            {
                current = 0;
            }

            return current;
        }

        #endregion

        #endregion


        /// <summary>
        ///     创建一个Thickness动画
        /// </summary>
        /// <param name="thickness"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static ThicknessAnimation CreateAnimation(Thickness thickness = default(Thickness), double milliseconds = 200)
        {
            return new ThicknessAnimation(thickness, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
        }

        /// <summary>
        ///     创建一个Double动画
        /// </summary>
        /// <param name="toValue"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DoubleAnimation CreateAnimation(double toValue, double milliseconds = 200)
        {
            return new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
        }
    }

    /// <summary>
    ///     Encapsulates methods for dealing with dependency objects and properties.
    /// </summary>
    public static class DependencyHelpers
    {
        /// <summary>
        ///     Gets the dependency property according to its name.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static DependencyProperty GetDependencyProperty(Type type, string propertyName)
        {
            DependencyProperty prop = null;

            if (type != null)
            {
                FieldInfo fieldInfo = type.GetField(propertyName + "Property",
                    BindingFlags.Static | BindingFlags.Public);

                if (fieldInfo != null)
                {
                    prop = fieldInfo.GetValue(null) as DependencyProperty;
                }
            }

            return prop;
        }

        /// <summary>
        ///     Retrieves a <see cref="DependencyProperty" /> using reflection.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static DependencyProperty GetDependencyProperty(this DependencyObject o, string propertyName)
        {
            DependencyProperty prop = null;

            if (o != null)
            {
                prop = GetDependencyProperty(o.GetType(), propertyName);
            }

            return prop;
        }

 
    }
}
