using System.Windows;
using System.Windows.Controls.Primitives; 

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
        static void OnValueChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ((DeferredScrollBehavior)dpo).OnValueChanged((double) e.OldValue, (double) e.NewValue);
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
        void OnScroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollEventType != ScrollEventType.ThumbTrack)
            {
                Value = e.NewValue;
            }
        }
    }
}
