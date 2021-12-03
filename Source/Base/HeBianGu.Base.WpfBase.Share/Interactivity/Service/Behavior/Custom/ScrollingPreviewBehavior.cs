using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives; 
using System.Windows.Media;
using System.Windows.Threading; 

namespace HeBianGu.Base.WpfBase
{
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
            get { return (DataTemplate) GetValue(VerticalScrollingPreviewTemplateProperty); }
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
            get { return (DataTemplate) GetValue(HorizontalScrollingPreviewTemplateProperty); }
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

        void ScrollBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar)sender;
            if (scrollBar.Track != null)
            {
                scrollBar.SizeChanged -= ScrollBar_SizeChanged;
                AttachToScrollBar(scrollBar);
            }
        }

        void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollBar scrollBar = (ScrollBar) sender;

            if (_previewToolTip != null)
            {
                // The ScrollBar's value isn't updated quite yet, so wait until Input priority
                AssociatedObject.Dispatcher.BeginInvoke((Action)(() =>
                     {
                         ScrollingPreviewData data = (ScrollingPreviewData) _previewToolTip?.Content;
                         data?.UpdateScrollingValues(scrollBar);
                         data?.UpdateItem(AssociatedObject as ItemsControl, scrollBar.Orientation == Orientation.Vertical);
                     }
                    ), DispatcherPriority.Input);
            }
        }

        void ThumbDragStarted(object sender, DragStartedEventArgs e)
        {
            Thumb thumb = (Thumb) sender;
            ScrollBar scrollBar = thumb.GetParent<ScrollBar>();
            bool isVertical = scrollBar.Orientation == Orientation.Vertical;

            // Update the content in the ToolTip
            ScrollingPreviewData data = new ScrollingPreviewData();
            data.UpdateScrollingValues(scrollBar);
            data.UpdateItem(AssociatedObject as ItemsControl, isVertical);

            _previewToolTip = new ToolTip
              {
                  PlacementTarget = (UIElement) sender,
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

        void ThumbDragDelta(object sender, DragDeltaEventArgs e)
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

        void ThumbDragCompleted(object sender, DragCompletedEventArgs e)
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
        class ScrollingPreviewData : INotifyPropertyChanged
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
}