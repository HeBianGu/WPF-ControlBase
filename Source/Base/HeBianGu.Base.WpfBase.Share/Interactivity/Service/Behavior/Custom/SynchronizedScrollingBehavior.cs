// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
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
}
