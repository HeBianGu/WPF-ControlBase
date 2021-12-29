using System.Windows;
using System.Windows.Controls; 

namespace HeBianGu.Base.WpfBase
{
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
            get { return (double) GetValue(VerticalOffsetProperty);  }    
            set { SetValue(VerticalOffsetProperty, value);}
        }

        /// <summary>
        /// Vertical height of the scroll viewer
        /// </summary>
        public static readonly DependencyProperty ViewportHeightProperty = DependencyProperty.Register("ViewportHeight", typeof (double), typeof (ViewportSynchronizerBehavior),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// The vertical height of the scrollviewer
        /// </summary>
        public double ViewportHeight
        {
            get { return (double) GetValue(ViewportHeightProperty); }
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
                loadAction = (s,e) => { AssociatedObject.Loaded -= loadAction; FindAndAttachToScrollViewer(); };
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
        void AssociatedObjectSizeChanged(object sender, SizeChangedEventArgs e)
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
        void ScrollViewerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ViewportHeight = _scrollViewer.ViewportHeight;
            this.ViewportWidth = _scrollViewer.ViewportWidth;
        }

        /// <summary>
        /// This method is called when the scroll viewer is scrolled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
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
            var vsb = (ViewportSynchronizerBehavior) dpo;
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
            var vsb = (ViewportSynchronizerBehavior)dpo;
            if (vsb._scrollViewer != null)
                vsb._scrollViewer.ScrollToHorizontalOffset((double)e.NewValue);
        }
    }
}
