// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    [DefaultProperty("Items")]
    [ContentProperty("Items")]
    [TemplatePart(Name = ElementPanelPage, Type = typeof(Panel))]
    [TemplatePart(Name = ElementItemsControl, Type = typeof(ItemsPresenter))]
    public class CarouselControl : ListBox
    {
        #region Constants

        private const string ElementPanelPage = "PART_PanelPage";
        private const string ElementItemsControl = "PART_ItemsControl";

        #endregion Constants

        #region Data

        private Panel _panelPage;

        private bool _appliedTemplate;

        private ItemsPresenter _itemsControl;

        private int _pageIndex = 0;

        private Button _selectedButton;

        private DispatcherTimer _updateTimer;

        private readonly List<double> _widthList = new List<double>();

        #endregion Data

        public override void OnApplyTemplate()
        {
            _appliedTemplate = false;

            _panelPage?.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ButtonPages_OnClick));

            base.OnApplyTemplate();

            _itemsControl = GetTemplateChild(ElementItemsControl) as ItemsPresenter;
            _panelPage = GetTemplateChild(ElementPanelPage) as Panel;

            if (!CheckNull()) return;

            _panelPage.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ButtonPages_OnClick));
            _appliedTemplate = true;
            Update();
        }

        private void Update()
        {
            TimerSwitch(AutoRun);
            UpdatePageButtons(_pageIndex);
        }

        private bool CheckNull() => !(_itemsControl == null || _panelPage == null);

        public static readonly DependencyProperty AutoRunProperty = DependencyProperty.Register(
            "AutoRun", typeof(bool), typeof(CarouselControl), new PropertyMetadata(false, (o, args) =>
            {
                CarouselControl ctl = (CarouselControl)o;
                ctl.TimerSwitch((bool)args.NewValue);
            }));

        public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register(
            "Interval", typeof(TimeSpan), typeof(CarouselControl), new PropertyMetadata(TimeSpan.FromSeconds(2)));

        public static readonly DependencyProperty ExtendWidthProperty = DependencyProperty.Register(
            "ExtendWidth", typeof(double), typeof(CarouselControl), new PropertyMetadata(0.0));

        public double ExtendWidth
        {
            get => (double)GetValue(ExtendWidthProperty);
            set => SetValue(ExtendWidthProperty, value);
        }

        public static readonly DependencyProperty IsCenterProperty = DependencyProperty.Register(
            "IsCenter", typeof(bool), typeof(CarouselControl), new PropertyMetadata(false));

        public bool IsCenter
        {
            get => (bool)GetValue(IsCenterProperty);
            set => SetValue(IsCenterProperty, value);
        }

        public CarouselControl()
        {
            CommandBindings.Add(new CommandBinding(Commander.Prev, ButtonPrev_OnClick));
            CommandBindings.Add(new CommandBinding(Commander.Next, ButtonNext_OnClick));
            CommandBindings.Add(new CommandBinding(Commander.Selected, ButtonPages_OnClick));

            Loaded += (s, e) => UpdatePageButtons();
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            UpdatePageButtons();
        }

        /// <summary>
        ///     是否自动跳转
        /// </summary>
        public bool AutoRun
        {
            get => (bool)GetValue(AutoRunProperty);
            set => SetValue(AutoRunProperty, value);
        }

        /// <summary>
        ///     跳转时间间隔
        /// </summary>
        public TimeSpan Interval
        {
            get => (TimeSpan)GetValue(IntervalProperty);
            set => SetValue(IntervalProperty, value);
        }

        /// <summary>
        ///     页码
        /// </summary>
        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                if (Items.Count == 0) return;
                if (_pageIndex == value) return;
                if (value < 0)
                    _pageIndex = Items.Count - 1;
                else if (value >= Items.Count)
                    _pageIndex = 0;
                else
                    _pageIndex = value;
                UpdatePageButtons(_pageIndex);
            }
        }

        /// <summary>
        ///     计时器开关
        /// </summary>
        private void TimerSwitch(bool run)
        {
            if (!_appliedTemplate) return;

            if (_updateTimer != null)
            {
                _updateTimer.Tick -= UpdateTimer_Tick;
                _updateTimer.Stop();
                _updateTimer = null;
            }

            if (!run) return;
            _updateTimer = new DispatcherTimer
            {
                Interval = Interval
            };
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (IsMouseOver) return;
            PageIndex++;
        }

        /// <summary>
        ///     更新页按钮
        /// </summary>
        public void UpdatePageButtons(int index = -1)
        {
            if (!CheckNull()) return;
            if (!_appliedTemplate) return;

            int count = Items.Count;
            _widthList.Clear();
            _widthList.Add(0);
            double width = .0;
            foreach (FrameworkElement item in Items)
            {
                item.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                width += item.DesiredSize.Width;
                _widthList.Add(width);
            }

            _itemsControl.Width = _widthList.Last() + ExtendWidth;
            _panelPage.Children.Clear();
            for (int i = 0; i < count; i++)
            {
                _panelPage.Children.Add(CreatePateButton());
            }

            if (index == -1)
            {
                if (count > 0)
                {
                    UIElement button = _panelPage.Children[0];
                    button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, button));
                }
            }
            else if (index >= 0 && index < count)
            {
                UIElement button = _panelPage.Children[index];
                button.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent, button));
                UpdateItemsPosition();
            }
        }

        /// <summary>
        ///     更新项的位置
        /// </summary>
        private void UpdateItemsPosition()
        {
            if (!CheckNull()) return;
            if (!_appliedTemplate) return;
            if (Items.Count == 0) return;
            if (!IsCenter)
            {
                _itemsControl.BeginAnimation(MarginProperty,
                    AnimationHelpers.CreateAnimation(new Thickness(-_widthList[PageIndex], 0, 0, 0)));
            }
            else
            {
                FrameworkElement ctl = (FrameworkElement)Items[PageIndex];
                double ctlWidth = ctl.DesiredSize.Width;
                _itemsControl.BeginAnimation(MarginProperty,
                    AnimationHelpers.CreateAnimation(
                        new Thickness(-_widthList[PageIndex] + (ActualWidth - ctlWidth) / 2, 0, 0, 0)));
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            UpdateItemsPosition();
        }

        private Button CreatePateButton()
        {
            try
            {
                Button button = new Button
                {

                    Style = this.TryFindResource("S.CarourselControl.SmapleButton") as Style,
                    Content = new Border
                    {
                        Width = 10,
                        Height = 10,
                        CornerRadius = new CornerRadius(5),
                        Background = this.TryFindResource(BrushKeys.BackgroundDefault) as Brush,
                        Margin = new Thickness(5, 0, 5, 0),
                        BorderThickness = new Thickness(1)
                    }

                };
                return button;
            }
            catch
            {
                return null;
            }

        }

        private void ButtonPages_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;

            if (_selectedButton != null && _selectedButton.Content is Border borderOri)
            {
                borderOri.Background = this.TryFindResource(BrushKeys.Dark4) as Brush;
            }

            _selectedButton = e.OriginalSource as Button;

            if (_selectedButton != null && _selectedButton.Content is Border border)
            {
                border.Background = this.TryFindResource(BrushKeys.Accent) as Brush;
            }

            int index = _panelPage.Children.IndexOf(_selectedButton);

            if (index != -1)
                PageIndex = index;
        }

        private void ButtonPrev_OnClick(object sender, RoutedEventArgs e) => PageIndex--;

        private void ButtonNext_OnClick(object sender, RoutedEventArgs e) => PageIndex++;
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
            foreach (AnimationTimeline anim in storyboard.Children.OfType<AnimationTimeline>())
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
            TAnimation timeline = new TAnimation();

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
            AnimationCompletedHandler completionHandler = new AnimationCompletedHandler(timeline, handler);
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
            FrameworkElement element = (FrameworkElement)o;

            int percent = (int)e.NewValue;

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
            FrameworkElement fe = (FrameworkElement)sender;
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
            FrameworkElement element = (FrameworkElement)o;

            int percent = (int)e.NewValue;

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
            FrameworkElement fe = (FrameworkElement)sender;
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
            int current = (int)value;

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
                throw new ArgumentException("property");
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


