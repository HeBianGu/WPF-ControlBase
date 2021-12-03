using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// A trigger that fires when scrolling to a specific element in a ScrollViewer.
    /// </summary>
    /// <summary> 当滚动到绑定的一个子元素时触发 </summary>
    [TypeConstraint(typeof(ScrollViewer))]
    public class ScrollingTrigger : TriggerBase<ScrollViewer>
    {
        /// <summary>
        /// TargetElementProperty
        /// </summary>
        public static readonly DependencyProperty TargetElementProperty =
            DependencyProperty.Register("TargetElement", typeof(FrameworkElement), typeof(ScrollingTrigger), new PropertyMetadata(null));

        /// <summary>
        /// The target element to scroll to
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return (FrameworkElement)GetValue(TargetElementProperty); }
            set { SetValue(TargetElementProperty, value); }
        }

        /// <summary>
        /// OnAttached
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            }

            AssociatedObject.ScrollChanged += AssociatedObject_ScrollChanged;
        }

        /// <summary>
        /// OnDetaching
        /// </summary>
        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.ScrollChanged -= AssociatedObject_ScrollChanged;
            }

            base.OnDetaching();
        }

        private void AssociatedObject_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // get the current vertical offset of the ScrollViewer
            var currentScrollPosition = AssociatedObject.VerticalOffset;
            var point = new Point(0, currentScrollPosition);

            var targetBound = TargetElement.TransformToVisual(AssociatedObject).TransformBounds(new Rect(0, currentScrollPosition,
                TargetElement.ActualWidth, TargetElement.ActualHeight));

            if (currentScrollPosition >= targetBound.Y && currentScrollPosition <= targetBound.Y + targetBound.Height)
            {
                InvokeActions(null);
            }

            //if (e.VerticalChange > 0)
            //{
            //    //  Do ：向下移动

            //    currentScrollPosition = currentScrollPosition + e.ViewportHeight;

            //    if (currentScrollPosition >= targetBound.Y && currentScrollPosition <= targetBound.Y + targetBound.Height)
            //    {
            //        InvokeActions(null);
            //    }
            //}
            //else
            //{
            //    //  Do ：向上移动
            //    if (currentScrollPosition <= targetBound.Y + targetBound.Height)
            //    {
            //        InvokeActions(null);
            //    }
            //}


            Debug.WriteLine(e.ViewportHeight);

        }
    }
}