// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> 当进度条滚动时 返回当前值 </summary>
    public class DeferredScrollBehavior : Behavior<ScrollBar>
    {
        #region Value property
        /// <summary>
        /// The Value property represents the current position of the ScrollBar.  Changes made to this 
        /// value will be propogated to the thumb value.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(DeferredScrollBehavior), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        /// <summary>
        /// The current position of the ScrollBar.
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// This method is called when the value is changed.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnValueChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ((DeferredScrollBehavior)dpo).OnValueChanged((double)e.OldValue, (double)e.NewValue);
        }
        #endregion

        /// <summary>
        /// This method adjusts the thumb position to the currently tracked value position.
        /// </summary>
        /// <param name="oldValue">Current value</param>
        /// <param name="newValue">New value</param>
        private void OnValueChanged(double oldValue, double newValue)
        {
            AssociatedObject.Value = newValue;
        }

        /// <summary>
        /// This attaches the behavior to the ScrollBar.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Scroll += OnScroll;
        }

        /// <summary>
        /// This detaches the behavior from the ScrollBar.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Scroll -= OnScroll;
        }

        /// <summary>
        /// This is called when ScrollBar changes the thumb position.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnScroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollEventType != ScrollEventType.ThumbTrack)
            {
                Value = e.NewValue;
            }
        }
    }


    public class MouseGestureScrollViewerBehavior : Behavior<ScrollViewer>
    {
        #region Private Data
        private bool _isCaptured;
        private Point _startingPoint, _endingPoint;
        private double _horizontalOffset, _verticalOffset, _inertiaX, _inertiaY;
        private DateTime _startDrag;
        private DispatcherTimer _timer;
        private const double EPSILON = 0.001;
        #endregion

        /// <summary>
        /// Backing storage for EnableInertia property
        /// </summary>
        public static readonly DependencyProperty EnableInertiaProperty =
            DependencyProperty.Register("EnableInertia", typeof(bool), typeof(MouseGestureScrollViewerBehavior), new PropertyMetadata(true));

        /// <summary>
        /// 启用惯性
        /// </summary>
        public bool EnableInertia
        {
            get { return (bool)GetValue(EnableInertiaProperty); }
            set { SetValue(EnableInertiaProperty, value); }
        }

        /// <summary>
        /// Backing storage for EnablePageSwipe property
        /// </summary>
        public static readonly DependencyProperty EnablePageSwipeProperty =
            DependencyProperty.Register("EnablePageSwipe", typeof(bool), typeof(MouseGestureScrollViewerBehavior), new PropertyMetadata(default(bool)));

        /// <summary>
        /// True to not perform pixel movement, but instead move enter page at a time. 按页移动
        /// </summary>
        public bool EnablePageSwipe
        {
            get { return (bool)GetValue(EnablePageSwipeProperty); }
            set { SetValue(EnablePageSwipeProperty, value); }
        }

        public MouseButton ChangeButton { get; set; } = MouseButton.Middle;

        //public MouseButton ChangeButton
        //{
        //    get { return (MouseButton)GetValue(ChangeButtonProperty); }
        //    set { SetValue(ChangeButtonProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ChangeButtonProperty =
        //    DependencyProperty.Register("ChangeButton", typeof(MouseButton), typeof(MouseGestureScrollViewerBehavior), new FrameworkPropertyMetadata(MouseButton.Middle, (d, e) =>
        //     {
        //         MouseGestureScrollViewerBehavior control = d as MouseGestureScrollViewerBehavior;

        //         if (control == null) return;

        //         if (e.OldValue is MouseButton o)
        //         {

        //         }

        //         if (e.NewValue is MouseButton n)
        //         {

        //         }

        //     }));



        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            //AssociatedObject.MouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            //AssociatedObject.MouseMove += OnPreviewMouseMove;
            //AssociatedObject.MouseLeftButtonUp += OnPreviewMouseLeftButtonUp;

            AssociatedObject.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseLeftButtonDown), true);
            AssociatedObject.AddHandler(UIElement.MouseMoveEvent, new MouseEventHandler(OnPreviewMouseMove), true);
            AssociatedObject.AddHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnPreviewMouseLeftButtonUp), true);

            AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
            AssociatedObject.SourceUpdated += AssociatedObject_SourceUpdated;

        }

        private void AssociatedObject_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
           
        }

        private void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            //AssociatedObject.MouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            //AssociatedObject.MouseMove -= OnPreviewMouseMove;
            //AssociatedObject.MouseLeftButtonUp -= OnPreviewMouseLeftButtonUp;

            AssociatedObject.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseLeftButtonDown));
            AssociatedObject.RemoveHandler(UIElement.MouseMoveEvent, new MouseEventHandler(OnPreviewMouseMove));
            AssociatedObject.RemoveHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(OnPreviewMouseLeftButtonUp));

            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
        }

        /// <summary>
        /// This is invoked when the mouse button is clicked on the ScrollViewer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != this.ChangeButton) return;

            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }

            _verticalOffset = AssociatedObject.VerticalOffset;
            _horizontalOffset = AssociatedObject.HorizontalOffset;
            _startingPoint = e.GetPosition(AssociatedObject);
            _isCaptured = true;
            _startDrag = DateTime.Now;

            //AssociatedObject.CaptureMouse();
        }

        /// <summary>
        /// This is called when the mouse is moved.  It scrolls the contents based on mouse movement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isCaptured) return;

            if (this.ChangeButton == MouseButton.Left && e.LeftButton != MouseButtonState.Pressed) return;

            if (this.ChangeButton == MouseButton.Right && e.RightButton != MouseButtonState.Pressed) return;

            if (this.ChangeButton == MouseButton.Middle && e.MiddleButton != MouseButtonState.Pressed) return;


            Point point = e.GetPosition(AssociatedObject);

            double dx = point.X - _startingPoint.X;
            if (AssociatedObject.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled)
                AssociatedObject.ScrollToHorizontalOffset(_horizontalOffset - dx);

            if (!EnablePageSwipe)
            {
                double dy = point.Y - _startingPoint.Y;
                if (AssociatedObject.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                    AssociatedObject.ScrollToVerticalOffset(_verticalOffset - dy);
            }

            _endingPoint = point;
        }

        /// <summary>
        /// This is called when the mouse is released.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != this.ChangeButton) return;

            AssociatedObject.ReleaseMouseCapture();

            _isCaptured = false;

            if (EnablePageSwipe)
            {
                if (AssociatedObject.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                {
                    if (_endingPoint.Y > _startingPoint.Y)
                    {
                        AssociatedObject.PageUp();
                    }
                    else if (_endingPoint.Y < _startingPoint.Y)
                    {
                        AssociatedObject.PageDown();
                    }
                }

            }
            else if (EnableInertia)
            {
                if ((DateTime.Now - _startDrag).TotalMilliseconds < 200)
                {
                    _inertiaY = _inertiaX = 0;
                    if (AssociatedObject.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled)
                    {
                        _inertiaY = _endingPoint.Y - _startingPoint.Y;
                    }
                    if (AssociatedObject.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled)
                    {
                        _inertiaX = _endingPoint.X - _startingPoint.X;
                    }

                    bool doFling = (Math.Abs(_inertiaX) > 10 || Math.Abs(_inertiaY) > 10);
                    if (doFling)
                    {
                        _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(50),
                            DispatcherPriority.Normal, OnFlingScroll, AssociatedObject.Dispatcher);
                    }
                }
            }
        }

        /// <summary>
        /// This is called on the timer when you "fling" the screen in a direction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFlingScroll(object sender, EventArgs e)
        {
            if (Math.Abs(_inertiaY) > 0)
            {
                AssociatedObject.ScrollToVerticalOffset(AssociatedObject.VerticalOffset - _inertiaY);
                if (_inertiaY > 0)
                {
                    _inertiaY -= _inertiaY / 2;
                    if (_inertiaY < 0)
                        _inertiaY = 0;
                }
                else
                {
                    _inertiaY += Math.Abs(_inertiaY) / 2;
                    if (_inertiaY > 0)
                        _inertiaY = 0;
                }
            }

            if (Math.Abs(_inertiaX) > 0)
            {
                AssociatedObject.ScrollToHorizontalOffset(AssociatedObject.HorizontalOffset - _inertiaX);
                if (_inertiaX > 0)
                {
                    _inertiaX -= _inertiaX / 2;
                    if (_inertiaX < 0)
                        _inertiaX = 0;
                }
                else
                {
                    _inertiaX += Math.Abs(_inertiaX) / 2;
                    if (_inertiaX > 0)
                        _inertiaX = 0;
                }
            }

            if (Math.Abs(_inertiaX) < EPSILON && Math.Abs(_inertiaY) < EPSILON)
            {
                if (_timer != null)
                {
                    _timer.Stop();
                    _timer = null;
                }
            }
        }
    }

    public class RightMouseGestureScrollViewerBehavior : MouseGestureScrollViewerBehavior
    {
        public RightMouseGestureScrollViewerBehavior()
        {
            this.ChangeButton = MouseButton.Right;
        }
    }

    /// <summary>
    /// Displays a ToolTip next to the ScrollBar thumb while it is being dragged.  
    /// The original idea and code was taken from an MSDN sample - see http://code.msdn.microsoft.com/getwpfcode/Release/ProjectReleases.aspx?ReleaseId=1445
    /// for the original source code and project.
    /// </summary>
    /// 
    /// <summary> 当滚动条滚动时 显示当前刻度等信息 </summary>
    public class ScrollbarPreviewBehavior : Behavior<ScrollBar>
    {

        #region IsEnabledProperty
        /// <summary>
        /// This property allows the behavior to be used as a traditional
        /// attached property behavior.
        /// </summary>
        public static DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ScrollbarPreviewBehavior),
                new FrameworkPropertyMetadata(false, OnIsEnabledChanged));

        /// <summary>
        /// Returns whether NumericTextBoxBehavior is enabled via attached property
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns>True/FalseB</returns>
        public static bool GetIsEnabled(ScrollBar textBox)
        {
            return (bool)textBox.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Adds NumericTextBoxBehavior to TextBox
        /// </summary>
        /// <param name="textBox">TextBox to apply</param>
        /// <param name="value">True/False</param>
        public static void SetIsEnabled(ScrollBar textBox, bool value)
        {
            textBox.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ScrollBar tb = dpo as ScrollBar;
            if (tb != null)
            {
                BehaviorCollection behColl = Interaction.GetBehaviors(tb);
                ScrollbarPreviewBehavior existingBehavior = behColl.FirstOrDefault(b => b.GetType() == typeof(ScrollbarPreviewBehavior)) as ScrollbarPreviewBehavior;
                if ((bool)e.NewValue == false && existingBehavior != null)
                {
                    behColl.Remove(existingBehavior);
                }
                else if ((bool)e.NewValue == true && existingBehavior == null)
                {
                    behColl.Add(new ScrollbarPreviewBehavior());
                }
            }
        }
        #endregion

        #region ScrollingPreviewTemplate
        /// <summary>
        /// Specifies a ContentTemplate for a ToolTip that will appear next to the ScrollBar while dragging the thumb.
        /// </summary>
        public static readonly DependencyProperty ScrollingPreviewTemplateProperty = DependencyProperty.Register("ScrollingPreviewTemplate", typeof(DataTemplate), typeof(ScrollbarPreviewBehavior), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets/Sets the vertical scrolling preview template
        /// </summary>
        public DataTemplate ScrollingPreviewTemplate
        {
            get { return (DataTemplate)GetValue(ScrollingPreviewTemplateProperty); }
            set { SetValue(ScrollingPreviewTemplateProperty, value); }
        }
        #endregion

        /// <summary>
        /// Holds the ToolTip when it is being used.
        /// </summary>
        private ToolTip _previewToolTip;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            if (ScrollingPreviewTemplate != null)
            {
                Dispatcher.BeginInvoke(new Action(AttachToScrollBar),
                    DispatcherPriority.Loaded);
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.SizeChanged -= ScrollBar_SizeChanged;
            Track track = AssociatedObject.Track ?? AssociatedObject.GetTemplateByName<Track>("PART_Track");
            if (track != null)
            {
                Thumb thumb = track.Thumb;
                if (thumb != null)
                {
                    thumb.DragStarted -= ThumbDragStarted;
                    thumb.DragDelta -= ThumbDragDelta;
                    thumb.DragCompleted -= ThumbDragCompleted;
                    AssociatedObject.Scroll -= ScrollBar_Scroll;
                }
            }
        }

        private void AttachToScrollBar()
        {
            Track track = AssociatedObject.Track ?? AssociatedObject.GetTemplateByName<Track>("PART_Track");
            if (track == null)
            {
                if (AssociatedObject.Visibility != Visibility.Visible)
                {
                    AssociatedObject.SizeChanged += ScrollBar_SizeChanged;
                }
            }
            else // if (track != null)
            {
                Thumb thumb = track.Thumb;
                if (thumb != null)
                {
                    thumb.DragStarted += ThumbDragStarted;
                    thumb.DragDelta += ThumbDragDelta;
                    thumb.DragCompleted += ThumbDragCompleted;
                    AssociatedObject.Scroll += ScrollBar_Scroll;
                }
            }
        }

        private void ScrollBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            if (scrollBar.Track != null)
            {
                scrollBar.SizeChanged -= ScrollBar_SizeChanged;
                AttachToScrollBar();
            }
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ToolTip toolTip = _previewToolTip;
            if (toolTip != null && toolTip.Content != null)
            {
                // The ScrollBar's value isn't updated quite yet, so wait until Input priority
                AssociatedObject.Dispatcher.BeginInvoke((Action)(() => ((ScrollingPreviewData)toolTip.Content).UpdateScrollingValues(AssociatedObject)), DispatcherPriority.Input);
            }
        }

        private void ThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            _previewToolTip = new ToolTip();
            ScrollingPreviewData data = new ScrollingPreviewData();
            _previewToolTip.Content = data;

            // Update the content in the ToolTip
            data.UpdateScrollingValues(AssociatedObject);

            // Set the Placement and the PlacementTarget
            _previewToolTip.PlacementTarget = (UIElement)sender;
            _previewToolTip.Placement = AssociatedObject.Orientation == Orientation.Vertical ? PlacementMode.Left : PlacementMode.Top;

            _previewToolTip.VerticalOffset = 0.0;
            _previewToolTip.HorizontalOffset = 0.0;

            _previewToolTip.ContentTemplate = ScrollingPreviewTemplate;
            _previewToolTip.IsOpen = true;
        }

        private void ThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            // Check that we're within the range of the ScrollBar
            if ((_previewToolTip != null) &&
                (AssociatedObject.Value > AssociatedObject.Minimum) &&
                (AssociatedObject.Value < AssociatedObject.Maximum))
            {
                // This is a little trick to cause the ToolTip to update its position next to the Thumb
                if (AssociatedObject.Orientation == Orientation.Vertical)
                    _previewToolTip.VerticalOffset = _previewToolTip.VerticalOffset == 0.0 ? 0.001 : 0.0;
                else
                    _previewToolTip.HorizontalOffset = _previewToolTip.HorizontalOffset == 0.0 ? 0.001 : 0.0;
            }
        }

        private void ThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (_previewToolTip != null)
            {
                _previewToolTip.IsOpen = false;
                _previewToolTip = null;
            }
        }

        /// <summary>
        ///     Provides data that should be useful to templates displaying
        ///     a preview.
        /// </summary>
        internal class ScrollingPreviewData : INotifyPropertyChanged
        {
            private double _offset, _viewport, _extent, _value;

            /// <summary>
            /// The Scrollbar's current value
            /// </summary>
            public double Value
            {
                get { return _value; }
                set { _value = value; OnPropertyChanged("Value"); }
            }

            /// <summary>
            ///     The ScrollBar's offset.
            /// </summary>
            public double Offset
            {
                get
                {
                    return _offset;
                }
                internal set
                {
                    _offset = value;
                    OnPropertyChanged("Offset");
                }
            }

            /// <summary>
            ///     The size of the current viewport.
            /// </summary>
            public double Viewport
            {
                get
                {
                    return _viewport;
                }
                internal set
                {
                    _viewport = value;
                    OnPropertyChanged("Viewport");
                }
            }

            /// <summary>
            ///     The entire scrollable range.
            /// </summary>
            public double Extent
            {
                get
                {
                    return _extent;
                }
                internal set
                {
                    _extent = value;
                    OnPropertyChanged("Extent");
                }
            }

            /// <summary>
            ///     Updates Offset, Viewport, and Extent.
            /// </summary>
            internal void UpdateScrollingValues(ScrollBar scrollBar)
            {
                Offset = scrollBar.Value;
                Viewport = scrollBar.ViewportSize;
                Value = scrollBar.Value;
                Extent = scrollBar.Maximum - scrollBar.Minimum;
            }

            #region INotifyPropertyChanged Members

            /// <summary>
            ///     Notifies listeners of changes to properties on this object.
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            ///     Raises the PropertyChanged event.
            /// </summary>
            /// <param name="name">The name of the property.</param>
            protected void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            #endregion
        }
    }

    /// <summary>
    /// Displays a ToolTip next to the ScrollBar thumb while it is being dragged.  
    /// This code was taken from an MSDN sample - 
    /// see http://code.msdn.microsoft.com/getwpfcode/Release/ProjectReleases.aspx?ReleaseId=1445
    /// for the original source code and project.
    /// </summary>
    /// <summary> 同ScrollbarPreviewBehavior 只是这个会自动查找子元素中的ScrollBar </summary>
    public class ScrollingPreviewBehavior : Behavior<Control>
    {
        #region VerticalScrollingPreviewTemplate
        /// <summary>
        /// Allows for specifying a ContentTemplate for a ToolTip that will appear next to the 
        /// vertical ScrollBar while dragging the thumb.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollingPreviewTemplateProperty = DependencyProperty.Register("VerticalScrollingPreviewTemplate", typeof(DataTemplate), typeof(ScrollingPreviewBehavior), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Retrieves the vertical scrolling preview template
        /// </summary>
        public DataTemplate VerticalScrollingPreviewTemplate
        {
            get { return (DataTemplate)GetValue(VerticalScrollingPreviewTemplateProperty); }
            set { SetValue(VerticalScrollingPreviewTemplateProperty, value); }
        }
        #endregion

        #region HorizontalScrollingPreviewTemplate

        /// <summary>
        ///     Allows for specifying a ContentTemplate for a ToolTip that will appear next to the 
        ///     horizontal ScrollBar while dragging the thumb.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollingPreviewTemplateProperty = DependencyProperty.Register("HorizontalScrollingPreviewTemplate", typeof(DataTemplate), typeof(ScrollingPreviewBehavior), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets the horizontal scrolling preview template
        /// </summary>
        public DataTemplate HorizontalScrollingPreviewTemplate
        {
            get { return (DataTemplate)GetValue(HorizontalScrollingPreviewTemplateProperty); }
            set { SetValue(HorizontalScrollingPreviewTemplateProperty, value); }
        }
        #endregion

        private ToolTip _previewToolTip;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            Dispatcher.BeginInvoke(new Action(DoAttach), DispatcherPriority.Loaded);
        }

        private void DoAttach()
        {
            ScrollViewer scrollViewer = AssociatedObject.GetElement<ScrollViewer>();
            if (scrollViewer != null)
            {
                if (VerticalScrollingPreviewTemplate != null)
                {
                    ScrollBar scrollBar = scrollViewer.GetTemplateByName<ScrollBar>("PART_VerticalScrollBar");
                    if (scrollBar != null)
                        AttachToScrollBar(scrollBar);
                }

                if (HorizontalScrollingPreviewTemplate != null)
                {
                    ScrollBar scrollBar = scrollViewer.GetTemplateByName<ScrollBar>("PART_HorizontalScrollBar");
                    if (scrollBar != null)
                        AttachToScrollBar(scrollBar);
                }
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            ScrollViewer scrollViewer = AssociatedObject.GetElement<ScrollViewer>();
            if (scrollViewer != null)
            {
                ScrollBar scrollBar = scrollViewer.GetTemplateByName<ScrollBar>("PART_VerticalScrollBar");
                if (scrollBar != null)
                    DetachFromScrollBar(scrollBar);
                scrollBar = scrollViewer.GetTemplateByName<ScrollBar>("PART_HorizontalScrollBar");
                if (scrollBar != null)
                    DetachFromScrollBar(scrollBar);
            }
        }

        private void DetachFromScrollBar(ScrollBar scrollBar)
        {
            scrollBar.SizeChanged -= ScrollBar_SizeChanged;

            Track track = scrollBar.Track ?? scrollBar.GetTemplateByName<Track>("PART_Track");
            if (track != null)
            {
                Thumb thumb = track.Thumb;
                if (thumb != null)
                {
                    thumb.DragStarted -= ThumbDragStarted;
                    thumb.DragDelta -= ThumbDragDelta;
                    thumb.DragCompleted -= ThumbDragCompleted;
                    scrollBar.Scroll -= ScrollBar_Scroll;
                }
            }
        }

        private void AttachToScrollBar(ScrollBar scrollBar)
        {
            Track track = scrollBar.Track ?? scrollBar.GetTemplateByName<Track>("PART_Track");
            if (track == null)
            {
                if (scrollBar.Visibility != Visibility.Visible)
                {
                    scrollBar.SizeChanged += ScrollBar_SizeChanged;
                }
            }
            else // if (track != null)
            {
                Thumb thumb = track.Thumb;
                if (thumb != null)
                {
                    thumb.DragStarted += ThumbDragStarted;
                    thumb.DragDelta += ThumbDragDelta;
                    thumb.DragCompleted += ThumbDragCompleted;
                    scrollBar.Scroll += ScrollBar_Scroll;
                }
            }
        }

        private void ScrollBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            if (scrollBar.Track != null)
            {
                scrollBar.SizeChanged -= ScrollBar_SizeChanged;
                AttachToScrollBar(scrollBar);
            }
        }

        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;

            if (_previewToolTip != null)
            {
                // The ScrollBar's value isn't updated quite yet, so wait until Input priority
                AssociatedObject.Dispatcher.BeginInvoke((Action)(() =>
                {
                    ScrollingPreviewData data = (ScrollingPreviewData)_previewToolTip?.Content;
                    data?.UpdateScrollingValues(scrollBar);
                    data?.UpdateItem(AssociatedObject as ItemsControl, scrollBar.Orientation == Orientation.Vertical);
                }
                    ), DispatcherPriority.Input);
            }
        }

        private void ThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            ScrollBar scrollBar = thumb.GetParent<ScrollBar>();
            bool isVertical = scrollBar.Orientation == Orientation.Vertical;

            // Update the content in the ToolTip
            ScrollingPreviewData data = new ScrollingPreviewData();
            data.UpdateScrollingValues(scrollBar);
            data.UpdateItem(AssociatedObject as ItemsControl, isVertical);

            _previewToolTip = new ToolTip
            {
                PlacementTarget = (UIElement)sender,
                Placement = isVertical ? PlacementMode.Left : PlacementMode.Top,
                VerticalOffset = 0.0,
                HorizontalOffset = 0.0,
                ContentTemplate = (isVertical)
                                        ? VerticalScrollingPreviewTemplate
                                        : HorizontalScrollingPreviewTemplate,
                Content = data,
                IsOpen = true
            };
        }

        private void ThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = (Thumb)sender;
            ScrollBar scrollBar = thumb.GetParent<ScrollBar>();

            // Check that we're within the range of the ScrollBar
            if ((_previewToolTip != null) &&
                (scrollBar.Value > scrollBar.Minimum) &&
                (scrollBar.Value < scrollBar.Maximum))
            {
                // This is a little trick to cause the ToolTip to update its position next to the Thumb
                if (scrollBar.Orientation == Orientation.Vertical)
                    _previewToolTip.VerticalOffset = _previewToolTip.VerticalOffset == 0.0 ? 0.001 : 0.0;
                else
                    _previewToolTip.HorizontalOffset = _previewToolTip.HorizontalOffset == 0.0 ? 0.001 : 0.0;
            }
        }

        private void ThumbDragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (_previewToolTip != null)
            {
                _previewToolTip.IsOpen = false;
                _previewToolTip = null;
            }
        }

        /// <summary>
        ///     Provides data that should be useful to templates displaying
        ///     a preview.
        /// </summary>
        private class ScrollingPreviewData : INotifyPropertyChanged
        {
            private double _offset, _viewport, _extent, _value;
            private object _firstItem, _lastItem;

            /// <summary>
            /// The Scrollbar's current value
            /// </summary>
            public double Value
            {
                get { return _value; }
                set { _value = value; OnPropertyChanged("Value"); }
            }

            /// <summary>
            ///     The ScrollBar's offset.
            /// </summary>
            public double Offset
            {
                get
                {
                    return _offset;
                }
                internal set
                {
                    _offset = value;
                    OnPropertyChanged("Offset");
                }
            }

            /// <summary>
            ///     The size of the current viewport.
            /// </summary>
            public double Viewport
            {
                get
                {
                    return _viewport;
                }
                internal set
                {
                    _viewport = value;
                    OnPropertyChanged("Viewport");
                }
            }

            /// <summary>
            ///     The entire scrollable range.
            /// </summary>
            public double Extent
            {
                get
                {
                    return _extent;
                }
                internal set
                {
                    _extent = value;
                    OnPropertyChanged("Extent");
                }
            }

            /// <summary>
            ///     The first visible item in the viewport.
            /// </summary>
            public object FirstItem
            {
                get
                {
                    return _firstItem;
                }
                private set
                {
                    _firstItem = value;
                    OnPropertyChanged("FirstItem");
                }
            }

            /// <summary>
            ///     The last visible item in the viewport.
            /// </summary>
            public object LastItem
            {
                get
                {
                    return _lastItem;
                }
                private set
                {
                    _lastItem = value;
                    OnPropertyChanged("LastItem");
                }
            }

            /// <summary>
            ///     Updates Offset, Viewport, and Extent.
            /// </summary>
            internal void UpdateScrollingValues(ScrollBar scrollBar)
            {
                Offset = scrollBar.Value;
                Viewport = scrollBar.ViewportSize;
                Value = scrollBar.Value;
                Extent = scrollBar.Maximum - scrollBar.Minimum;
            }

            /// <summary>
            ///     Updates FirstItem and LastItem based on the
            ///     Offset and Viewport properties.
            /// </summary>
            /// <param name="itemsControl">The ItemsControl that contains the data items.</param>
            /// <param name="vertical">True for vertical scrollbar</param>
            internal void UpdateItem(ItemsControl itemsControl, bool vertical)
            {
                if (itemsControl != null)
                {
                    int numItems = itemsControl.Items.Count;
                    if (numItems > 0)
                    {
                        if (VirtualizingStackPanel.GetIsVirtualizing(itemsControl))
                        {
                            // Items scrolling (value == index)
                            int firstIndex = (int)_offset;
                            int lastIndex = (int)_offset + (int)_viewport - 1;

                            if ((firstIndex >= 0) && (firstIndex < numItems))
                            {
                                FirstItem = itemsControl.Items[firstIndex];
                            }
                            else
                            {
                                FirstItem = null;
                            }

                            if ((lastIndex >= 0) && (lastIndex < numItems))
                            {
                                LastItem = itemsControl.Items[lastIndex];
                            }
                            else
                            {
                                LastItem = null;
                            }
                        }
                        else
                        {
                            // Pixel scrolling (no virtualization)

                            // This will do a linear search through all of the items.
                            // It will assume that the first item encountered that is within view is
                            // the first visible item and the last item encountered that is
                            // within view is the last visible item.
                            // Improvements could be made to this algorithm depending on the
                            // number of items in the collection and the their order relative
                            // to each other on-screen.

                            ScrollContentPresenter scp = null;
                            bool foundFirstItem = false;
                            int bestLastItemIndex = -1;
                            object firstVisibleItem = null;
                            object lastVisibleItem = null;

                            for (int i = 0; i < numItems; i++)
                            {
                                UIElement child = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as UIElement;
                                if (child != null)
                                {
                                    if (scp == null)
                                    {
                                        scp = child.GetParent<ScrollContentPresenter>();
                                        if (scp == null)
                                        {
                                            // Not in a ScrollViewer that we understand
                                            return;
                                        }
                                    }

                                    // Transform the origin of the child element to see if it is within view
                                    GeneralTransform t = child.TransformToAncestor(scp);
                                    Point p = t.Transform(foundFirstItem ? new Point(child.RenderSize.Width, child.RenderSize.Height) : new Point());

                                    if (!foundFirstItem && ((vertical ? p.Y : p.X) >= 0.0))
                                    {
                                        // Found the first visible item
                                        firstVisibleItem = itemsControl.Items[i];
                                        bestLastItemIndex = i;
                                        foundFirstItem = true;
                                    }
                                    else if (foundFirstItem && ((vertical ? p.Y : p.X) < scp.ActualHeight))
                                    {
                                        // Found a candidate for the last visible item
                                        bestLastItemIndex = i;
                                    }
                                }
                            }

                            if (bestLastItemIndex >= 0)
                            {
                                lastVisibleItem = itemsControl.Items[bestLastItemIndex];
                            }

                            // Update the item properties
                            FirstItem = firstVisibleItem;
                            LastItem = lastVisibleItem;
                        }
                    }
                }

            }

            #region INotifyPropertyChanged Members

            /// <summary>
            ///     Notifies listeners of changes to properties on this object.
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            ///     Raises the PropertyChanged event.
            /// </summary>
            /// <param name="name">The name of the property.</param>
            private void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                }
            }

            #endregion
        }
    }

    /// <summary> ScrollViewer带有鼠标拖动和触摸拖动效果 </summary>
    public class ScrollViewMouseDragBehavior : Behavior<ScrollViewer>
    {
        private ScrollViewer scrollViewer;

        protected override void OnAttached()
        {

            scrollViewer = AssociatedObject;

            if (scrollViewer == null) return;


            scrollViewer.PreviewMouseDown += AssociatedObject_PreviewMouseDown;

            scrollViewer.PreviewMouseMove += AssociatedObject_PreviewMouseMove;

            scrollViewer.TouchDown += AssociatedObject_TouchDown;

            scrollViewer.PreviewTouchMove += AssociatedObject_PreviewTouchMove;
        }

        protected override void OnDetaching()
        {
            if (scrollViewer == null) return;

            scrollViewer.PreviewMouseDown -= AssociatedObject_PreviewMouseDown;

            scrollViewer.PreviewMouseMove -= AssociatedObject_PreviewMouseMove;


            scrollViewer.TouchDown -= AssociatedObject_TouchDown;

            scrollViewer.PreviewTouchMove -= AssociatedObject_PreviewTouchMove;
        }



        /// <summary> 是否到达顶部 </summary>
        public bool IsTopped
        {
            get { return (bool)GetValue(IsToppedProperty); }
            set { SetValue(IsToppedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsToppedProperty =
            DependencyProperty.Register("IsTopped", typeof(bool), typeof(ScrollViewMouseDragBehavior), new PropertyMetadata(default(bool), (d, e) =>
            {
                ScrollViewMouseDragBehavior control = d as ScrollViewMouseDragBehavior;

                if (control == null) return;

                //bool config = e.NewValue as bool;

            }));

        /// <summary> 是否到达底部 </summary>
        public bool IsBottomed
        {
            get { return (bool)GetValue(IsBottomedProperty); }
            set { SetValue(IsBottomedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsBottomedProperty =
            DependencyProperty.Register("IsBottomed", typeof(bool), typeof(ScrollViewMouseDragBehavior), new PropertyMetadata(default(bool), (d, e) =>
            {
                ScrollViewMouseDragBehavior control = d as ScrollViewMouseDragBehavior;

                if (control == null) return;

                //bool config = e.NewValue as bool;

            }));

        private void AssociatedObject_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) return;

            this.ScrollMove(e.Source);
        }

        private void AssociatedObject_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.GetLast(e.Source);
        }


        private void AssociatedObject_PreviewTouchMove(object sender, TouchEventArgs e)
        {
            this.ScrollMove(e.Source);
        }

        private void AssociatedObject_TouchDown(object sender, TouchEventArgs e)
        {
            this.GetLast(e.Source);
        }

        private void GetLast(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            last = temp;
        }

        private Point last;

        private void ScrollMove(object source)
        {
            Point pp = Mouse.GetPosition(source as FrameworkElement);

            Point temp = (source as FrameworkElement).PointToScreen(pp);

            double y = temp.Y - last.Y;

            this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.VerticalOffset - y);

            last = temp;

            this.CheckPosition();


        }

        private void CheckPosition()
        {
            if (this.scrollViewer.VerticalOffset == 0)
            {
                this.IsTopped = true;
            }

            else if (IsVerticalScrollBarAtBottom(this.scrollViewer))
            {
                this.IsBottomed = true;
            }
            else
            {
                this.IsTopped = false;
                this.IsBottomed = false;
            }
        }

        public bool IsVerticalScrollBarAtBottom(ScrollViewer s)
        {
            bool isAtButtom = false;

            double dVer = s.VerticalOffset;

            double dViewport = s.ViewportHeight;

            double dExtent = s.ExtentHeight;

            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }
            return isAtButtom;
        }
    }

    /// <summary>
    /// This behavior tracks two ScrollViewer based controls and keeps their 
    /// Horizontal and Vertical positions synchronized.
    /// </summary>
    /// <summary> 设置两个滚动条滚动同步行为 </summary>
    public class SynchronizedScrollingBehavior : Behavior<UIElement>
    {
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(OnSourceScroll));
            base.OnAttached();
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(OnSourceScroll));
            base.OnDetaching();
        }

        /// <summary>
        /// This dependency property holds the horizontal adjustment factor when two ScrollViewer instances do not have the
        /// same size or elements.
        /// </summary>
        public static readonly DependencyProperty HorizontalAdjustmentProperty =
            DependencyProperty.Register("HorizontalAdjustment", typeof(double),
                typeof(SynchronizedScrollingBehavior), new UIPropertyMetadata(0.0));

        /// <summary>
        /// This dependency property holds the vertical adjustment factor when two ScrollViewer instances do not have the
        /// same size or elements.
        /// </summary>
        public static readonly DependencyProperty VerticalAdjustmentProperty =
            DependencyProperty.RegisterAttached("VerticalAdjustment", typeof(double),
                typeof(SynchronizedScrollingBehavior), new UIPropertyMetadata(0.0));

        /// <summary>
        /// This holds the target to synchronize to.
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.RegisterAttached("Target", typeof(UIElement),
                typeof(SynchronizedScrollingBehavior), new UIPropertyMetadata(null, OnTargetChanged));

        /// <summary>
        /// The horizontal adjustment to apply between source and target.
        /// </summary>
        public double HorizontalAdjustment
        {
            get { return (double)base.GetValue(HorizontalAdjustmentProperty); }
            set { base.SetValue(HorizontalAdjustmentProperty, value); }
        }

        /// <summary>
        /// The vertical adjustment to apply between source and target.
        /// </summary>
        public double VerticalAdjustment
        {
            get { return (double)base.GetValue(VerticalAdjustmentProperty); }
            set { base.SetValue(VerticalAdjustmentProperty, value); }
        }

        /// <summary>
        /// The target ScrollViewer to synchronize to
        /// </summary>
        public UIElement Target
        {
            get { return (UIElement)base.GetValue(TargetProperty); }
            set { base.SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// This method is called when the target property is changed.
        /// </summary>
        /// <param name="dpo">Dependency object</param>
        /// <param name="e">EventArgs</param>
        private static void OnTargetChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            SynchronizedScrollingBehavior sb = (SynchronizedScrollingBehavior)dpo;

            UIElement target = e.OldValue as UIElement;
            if (target != null)
                target.RemoveHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(sb.OnTargetScroll));

            target = e.NewValue as UIElement;
            if (target != null)
                target.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(sb.OnTargetScroll));
        }

        /// <summary>
        /// This handles the synchronization when the source list is scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSourceScroll(object sender, ScrollChangedEventArgs e)
        {
            if (Target != null)
            {
                ScrollViewer sv = Target as ScrollViewer ?? Target.GetChild<ScrollViewer>();
                if (sv != null)
                    AdjustScrollPosition(sv, e, -1 * HorizontalAdjustment, -1 * VerticalAdjustment);
            }
        }

        /// <summary>
        /// This handles the synchronization when the target list is scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTargetScroll(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = AssociatedObject as ScrollViewer ?? AssociatedObject.GetChild<ScrollViewer>();
            if (sv != null)
                AdjustScrollPosition(sv, e, HorizontalAdjustment, VerticalAdjustment);
        }

        /// <summary>
        /// This is the command scroll adjustment code which synchronizes two ScrollViewer instances.
        /// </summary>
        /// <param name="sv">ScrollViewer to adjust</param>
        /// <param name="e">Change in the source</param>
        /// <param name="hadjust">Horizontal adjustment</param>
        /// <param name="vadjust">Vertical adjustment</param>
        private static void AdjustScrollPosition(ScrollViewer sv, ScrollChangedEventArgs e, double hadjust, double vadjust)
        {
            if (sv.HorizontalScrollBarVisibility != ScrollBarVisibility.Disabled && (e.HorizontalChange != 0 || e.ExtentWidthChange != 0))
            {
                if (e.HorizontalOffset == 0)
                    sv.ScrollToLeftEnd();
                else if (e.HorizontalOffset >= e.ExtentWidth - 5)
                    sv.ScrollToRightEnd();
                else if (e.ExtentWidth + hadjust == sv.ExtentWidth)
                    sv.ScrollToHorizontalOffset(e.HorizontalOffset + hadjust);
                else
                    sv.ScrollToHorizontalOffset((sv.ExtentWidth * (e.HorizontalOffset / e.ExtentWidth)) + hadjust);
            }

            if (sv.VerticalScrollBarVisibility != ScrollBarVisibility.Disabled && (e.VerticalChange != 0 || e.ExtentHeightChange != 0))
            {
                if (e.VerticalOffset == 0)
                    sv.ScrollToTop();
                else if (e.VerticalOffset >= e.ExtentHeight - 5)
                    sv.ScrollToBottom();
                else if (e.ExtentHeight + vadjust == sv.ExtentHeight)
                    sv.ScrollToVerticalOffset(e.VerticalOffset + vadjust);
                else
                    sv.ScrollToVerticalOffset((sv.ExtentHeight * (e.VerticalOffset / e.ExtentHeight)) + vadjust);
            }
        }
    }

    /// <summary>
    /// This behavior allows a ViewModel to monitor and control the scrolling position of any ScrollViewer or 
    /// control with a ScrollViewer in the template.  It can also be used to synchronize two scrolling items
    /// against a single property in a ViewModel.
    /// </summary>
    /// <summary> 支持滚动条滚动位置属性可以绑定的行为，原生控件偏移量不支持绑定 </summary>
    public class ViewportSynchronizerBehavior : Behavior<Control>
    {
        private ScrollViewer _scrollViewer;

        /// <summary>
        /// Vertical offset of the scroll viewer
        /// </summary>
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.Register("VerticalOffset", typeof(double),
                typeof(ViewportSynchronizerBehavior), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVerticalOffsetChanged));

        /// <summary>
        /// The vertical offset
        /// </summary>
        public double VerticalOffset
        {
            get { return (double)GetValue(VerticalOffsetProperty); }
            set { SetValue(VerticalOffsetProperty, value); }
        }

        /// <summary>
        /// Vertical height of the scroll viewer
        /// </summary>
        public static readonly DependencyProperty ViewportHeightProperty = DependencyProperty.Register("ViewportHeight", typeof(double), typeof(ViewportSynchronizerBehavior),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// The vertical height of the scrollviewer
        /// </summary>
        public double ViewportHeight
        {
            get { return (double)GetValue(ViewportHeightProperty); }
            set { SetValue(ViewportHeightProperty, value); }
        }

        /// <summary>
        /// Horizontal width of the scroll viewer
        /// </summary>
        public static readonly DependencyProperty ViewportWidthProperty = DependencyProperty.Register("ViewportWidth", typeof(double), typeof(ViewportSynchronizerBehavior),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// The horizontal width of the scrollviewer
        /// </summary>
        public double ViewportWidth
        {
            get { return (double)GetValue(ViewportWidthProperty); }
            set { SetValue(ViewportWidthProperty, value); }
        }

        /// <summary>
        /// Horizontal offset of the scroll viewer
        /// </summary>
        public static readonly DependencyProperty HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double),
                typeof(ViewportSynchronizerBehavior), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHorizontalOffsetChanged));

        /// <summary>
        /// The horizontal offset
        /// </summary>
        public double HorizontalOffset
        {
            get { return (double)GetValue(HorizontalOffsetProperty); }
            set { SetValue(HorizontalOffsetProperty, value); }
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (!AssociatedObject.IsLoaded)
            {
                RoutedEventHandler loadAction = null;
                loadAction = (s, e) => { AssociatedObject.Loaded -= loadAction; FindAndAttachToScrollViewer(); };
                AssociatedObject.Loaded += loadAction;
            }
            else FindAndAttachToScrollViewer();
        }

        /// <summary>
        /// Attaches to the scrollviewer.
        /// </summary>
        private bool FindAndAttachToScrollViewer()
        {
            // Find the first scroll viewer in the visual tree.
            _scrollViewer = AssociatedObject as ScrollViewer ?? AssociatedObject.GetChild<ScrollViewer>();
            if (_scrollViewer != null)
            {
                this.ViewportHeight = _scrollViewer.ViewportHeight;
                this.ViewportWidth = _scrollViewer.ViewportWidth;

                // Position the scrollbar based on the current values.
                _scrollViewer.ScrollToVerticalOffset(VerticalOffset);
                _scrollViewer.ScrollToHorizontalOffset(HorizontalOffset);

                _scrollViewer.ScrollChanged += ScrollViewerScrollChanged;
                _scrollViewer.SizeChanged += ScrollViewerSizeChanged;
                return true;
            }

            // Possibly hasn't been instantiated yet?
            if (AssociatedObject.Visibility == Visibility.Collapsed)
            {
                AssociatedObject.SizeChanged += AssociatedObjectSizeChanged;
            }

            return false;
        }

        /// <summary>
        /// This is called when the associated object's size changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize != Size.Empty)
            {
                if (FindAndAttachToScrollViewer())
                    AssociatedObject.SizeChanged -= AssociatedObjectSizeChanged;
            }
        }

        /// <summary>
        /// Called when the scrollviewer changes the size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ViewportHeight = _scrollViewer.ViewportHeight;
            this.ViewportWidth = _scrollViewer.ViewportWidth;
        }

        /// <summary>
        /// This method is called when the scroll viewer is scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalChange != 0)
                HorizontalOffset = e.HorizontalOffset;
            if (e.VerticalChange != 0)
                VerticalOffset = e.VerticalOffset;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollChanged -= ScrollViewerScrollChanged;
                _scrollViewer.SizeChanged += ScrollViewerSizeChanged;
            }
            base.OnDetaching();
        }

        /// <summary>
        /// This method is called when the VerticalOffset property is changed.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnVerticalOffsetChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ViewportSynchronizerBehavior vsb = (ViewportSynchronizerBehavior)dpo;
            if (vsb._scrollViewer != null)
                vsb._scrollViewer.ScrollToVerticalOffset((double)e.NewValue);
        }

        /// <summary>
        /// This method is called when HorizontalOffset property is changed.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnHorizontalOffsetChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ViewportSynchronizerBehavior vsb = (ViewportSynchronizerBehavior)dpo;
            if (vsb._scrollViewer != null)
                vsb._scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
        }
    }
}
