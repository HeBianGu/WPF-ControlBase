// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    /// Represents a behavior that allows moving items in an <see cref="ItemsControl" /> by dragging them.
    /// <remarks>For this behavior to work, the <see cref="P:ItemsControl.ItemsPanel" /> must not be a <see cref="VirtualizingPanel" />,
    /// and the <see cref="P:ItemsControl.ItemsSource" /> must be an <see cref="ObservableCollection{T}" />.</remarks>
    /// </summary>
    public class ListItemMoveBehavior : IDisposable
    {
        #region Fields

        private static readonly DependencyPropertyDescriptor ItemsSourcePropertyDescriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(ItemsControl));
        private static readonly DependencyPropertyDescriptor ItemsPanelPropertyDescriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsPanelProperty, typeof(ItemsControl));

        private readonly ItemsControl _itemsControl;

        private FrameworkElement _element;
        private Panel _itemsPanel;
        private ScrollViewer _scrollViewer;
        private IList _collection;
        private MethodInfo _moveMethod;
        private bool _isEnabled;
        private Point _positionDelta;
        private PreviewAdorner _previewAdorner;
        private DateTime _lastMove;
        private int _startIndex;
        private int _lastIndex;

        #endregion

        #region Initialization

        private ListItemMoveBehavior(ItemsControl itemsControl)
        {
            _itemsControl = itemsControl;
            ItemsSourcePropertyDescriptor.AddValueChanged(_itemsControl, OnItemsSourceChanged);
            ItemsPanelPropertyDescriptor.AddValueChanged(_itemsControl, OnItemsPanelChanged);
            InitializeCollection();
            _itemsControl.PreviewMouseDown += ElementOnPreviewMouseDown;
        }

        private void OnItemsPanelChanged(object sender, EventArgs e)
        {
            _itemsPanel = null;
        }

        private ScrollViewer ScrollViewer
        {
            get
            {
                if (_scrollViewer == null)
                {
                    _scrollViewer = _itemsControl.FindVisualDescendantByType<ScrollViewer>();
                }
                return _scrollViewer;
            }
        }

        private bool IsEnabled
        {
            get { return _isEnabled && ItemsPanel != null && !(ItemsPanel is VirtualizingPanel); }
        }

        private Panel ItemsPanel
        {
            get
            {
                if (_itemsPanel == null)
                {
                    _itemsPanel = _itemsControl.FindVisualDescendant(t =>
                    {
                        Panel p = t as Panel;
                        return p != null && p.IsItemsHost;
                    }) as Panel;
                }
                return _itemsPanel;
            }
        }

        private void OnItemsSourceChanged(object sender, EventArgs e)
        {
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            _collection = _itemsControl.ItemsSource as IList;
            if (_collection == null)
            {
                ICollectionView collectionView = _collection as ICollectionView;
                if (collectionView != null)
                {
                    _collection = collectionView.SourceCollection as IList;
                }
            }
            if (_collection != null && IsObservableCollection(_collection))
            {
                _moveMethod = _collection.GetType().GetMethod("Move", new[] { typeof(int), typeof(int) });
                _isEnabled = true;
            }
            else
            {
                _isEnabled = true;
            }
        }

        /// <summary>
        /// Performs event clean-up.
        /// </summary>
        public void Dispose()
        {
            ItemsSourcePropertyDescriptor.RemoveValueChanged(_itemsControl, OnItemsSourceChanged);
            ItemsPanelPropertyDescriptor.RemoveValueChanged(_itemsControl, OnItemsPanelChanged);
            _element.PreviewMouseDown -= ElementOnPreviewMouseDown;
        }

        #endregion

        #region Mouse Events

        private void ElementOnPreviewMouseDown(object sender, MouseEventArgs e)
        {
            if (!IsEnabled) return;
            _element = null;
            UIElement element = e.OriginalSource as UIElement;
            if (element != null)
            {
                if (!GetIsElementDraggable(element)) return;
                _element = _itemsControl.ContainerFromElement(element) as FrameworkElement;
            }
            if (_element == null) return;

            bool isCaptured = _element.CaptureMouse();
            if (isCaptured)
            {
                e.Handled = true;

                _element.MouseMove += ElementOnMouseMove;
                _element.MouseUp += ElementOnMouseUp;
                _element.LostMouseCapture += ElementOnLostMouseCapture;

                object sourceItem = _element.DataContext;
                _startIndex = _collection.IndexOf(sourceItem);
                _lastIndex = _startIndex;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(_itemsControl);
                if (adornerLayer != null)
                {
                    _element.SetCurrentValue(UIElement.OpacityProperty, 0.0);
                    FrameworkElement firstChild = (FrameworkElement)VisualTreeHelper.GetChild(_element, 0);
                    double top = e.GetPosition(_itemsControl).Y;
                    _positionDelta = e.GetPosition(_element);
                    _previewAdorner = new PreviewAdorner(_itemsControl, firstChild)
                    {
                        Top = top - _positionDelta.Y
                    };
                    adornerLayer.Add(_previewAdorner);
                }
            }
        }

        private void ElementOnMouseMove(object sender, MouseEventArgs e)
        {
            DateTime now = DateTime.Now;
            object sourceItem = _element.DataContext;
            int sourceIndex = _collection.IndexOf(sourceItem);
            if (sourceIndex < 0) return;

            Point position = e.GetPosition(_itemsControl);

            if (_previewAdorner != null)
            {
                _previewAdorner.Top = position.Y - _positionDelta.Y;
                Debug.WriteLine(_previewAdorner.Top);
            }

            if (now - _lastMove > GetAnimationDuration(_itemsControl))
            {
                FrameworkElement hitTestElement = _itemsControl.InputHitTest(new Point(_positionDelta.X, position.Y)) as FrameworkElement;
                if (hitTestElement != null)
                {
                    FrameworkElement element = _itemsControl.ContainerFromElement(hitTestElement) as FrameworkElement;
                    if (element != null)
                    {
                        object item = element.DataContext;
                        _lastIndex = _collection.IndexOf(item);
                        if (sourceIndex != _lastIndex)
                        {
                            _moveMethod.Invoke(_collection, new object[] { sourceIndex, _lastIndex });
                            _lastMove = now;
                        }
                    }
                }

                Scroll(position);
            }
        }

        private void ElementOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _element.ReleaseMouseCapture();
        }

        private void ElementOnLostMouseCapture(object sender, MouseEventArgs mouseEventArgs)
        {
            _element.MouseMove -= ElementOnMouseMove;
            _element.MouseUp -= ElementOnMouseUp;
            _element.LostMouseCapture -= ElementOnLostMouseCapture;

            if (_previewAdorner != null)
            {
                Point relativePoint = _element.TranslatePoint(new Point(), _itemsControl);

                DoubleAnimation topAnimation = new DoubleAnimation { To = relativePoint.Y, Duration = GetAnimationDuration(_itemsControl), EasingFunction = new PowerEase() };
                topAnimation.Completed +=
                    (o, args) =>
                    {
                        AdornerLayer.GetAdornerLayer(_itemsControl).Remove(_previewAdorner);
                        _previewAdorner = null;
                        _element.InvalidateProperty(UIElement.OpacityProperty);
                        _element = null;
                    };
                _previewAdorner.BeginAnimation(PreviewAdorner.TopProperty, topAnimation);
            }

            if (_startIndex != _lastIndex)
            {
                _itemsControl.RaiseEvent(new RoutedPropertyChangedEventArgs<int>(_startIndex, _lastIndex, ReorderCompletedEvent));
            }
        }

        #endregion

        #region Helper Methods

        private static bool IsObservableCollection(object collection)
        {
            Type type = collection.GetType();
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObservableCollection<>);
        }

        private void Scroll(Point position)
        {
            if (ScrollViewer != null)
            {
                double scrollMargin = Math.Min(ScrollViewer.FontSize * 2.0, ScrollViewer.ActualHeight / 2.0);
                if ((position.X >= (ScrollViewer.ActualWidth - scrollMargin)) &&
                    (ScrollViewer.HorizontalOffset < (ScrollViewer.ExtentWidth - ScrollViewer.ViewportWidth)))
                {
                    ScrollViewer.LineRight();
                }
                else if ((position.X < scrollMargin) && (ScrollViewer.HorizontalOffset > 0.0))
                {
                    ScrollViewer.LineLeft();
                }
                else if ((position.Y >= (ScrollViewer.ActualHeight - scrollMargin)) &&
                         (ScrollViewer.VerticalOffset < (ScrollViewer.ExtentHeight - ScrollViewer.ViewportHeight)))
                {
                    ScrollViewer.LineDown();
                }
                else if ((position.Y < scrollMargin) && (ScrollViewer.VerticalOffset > 0.0))
                {
                    ScrollViewer.LineUp();
                }
            }
        }

        #endregion

        #region Attached Properties

        /// <summary>
        /// Gets a value indicating whether the beavior is enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsEnabled(ItemsControl obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// Sets a value indicating whether the beavior is enabled.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c>, enables the behavior.</param>
        public static void SetIsEnabled(ItemsControl obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// Identifies the <c>IsEnabled</c> attached dependency property.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ListItemMoveBehavior),
                                                new FrameworkPropertyMetadata(OnIsEnabledChanged));

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ItemsControl element = (ItemsControl)d;
                ListItemMoveBehavior behavior = new ListItemMoveBehavior(element);
                SetBehavior(element, behavior);
            }
            else
            {
                GetBehavior(d).Dispose();
                d.ClearValue(BehaviorProperty);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the element can be used for dragging.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsElementDraggable(UIElement obj)
        {
            return (bool)obj.GetValue(IsElementDraggableProperty);
        }

        /// <summary>
        /// Sets a value indicating whether the element can be used for dragging.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c>, enables the behavior.</param>
        public static void SetIsElementDraggable(UIElement obj, bool value)
        {
            obj.SetValue(IsElementDraggableProperty, value);
        }

        /// <summary>
        /// Identifies the <c>IsElementDraggable</c> attached dependency property.
        /// </summary>
        public static readonly DependencyProperty IsElementDraggableProperty =
            DependencyProperty.RegisterAttached("IsElementDraggable", typeof(bool), typeof(ListItemMoveBehavior),
                                                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        private static ListItemMoveBehavior GetBehavior(DependencyObject obj)
        {
            return (ListItemMoveBehavior)obj.GetValue(BehaviorProperty);
        }

        private static void SetBehavior(DependencyObject obj, ListItemMoveBehavior value)
        {
            obj.SetValue(BehaviorProperty, value);
        }

        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached("Behavior", typeof(ListItemMoveBehavior), typeof(ListItemMoveBehavior),
                                                new FrameworkPropertyMetadata());

        /// <summary>
        /// Identifies the <c>AnimationDuration</c> attached dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.RegisterAttached(
            "AnimationDuration", typeof(TimeSpan), typeof(ListItemMoveBehavior), new FrameworkPropertyMetadata(TimeSpan.FromSeconds(0.1)));

        /// <summary>
        /// Gets the duration of the animation.
        /// </summary>
        /// <param name="itemsControl">The items control.</param>
        /// <returns></returns>
        public static TimeSpan GetAnimationDuration(ItemsControl itemsControl)
        {
            return (TimeSpan)itemsControl.GetValue(AnimationDurationProperty);
        }

        /// <summary>
        /// Sets the duration of the animation.
        /// </summary>
        /// <param name="itemsControl">The items control.</param>
        /// <param name="value">The value.</param>
        public static void SetAnimationDuration(ItemsControl itemsControl, TimeSpan value)
        {
            itemsControl.SetValue(AnimationDurationProperty, value);
        }

        #endregion

        #region Attached Events

        /// <summary>
        /// Identifies the <c>ReorderCompleted</c> attached event.
        /// </summary>
        public static readonly RoutedEvent ReorderCompletedEvent = EventManager.RegisterRoutedEvent("ReorderCompleted", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>), typeof(ListItemMoveBehavior));

        /// <summary>
        /// Adds the reorder completed handler.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void AddReorderCompletedHandler(FrameworkElement element,
                                                      RoutedPropertyChangedEventHandler<int> handler)
        {
            element.AddHandler(ReorderCompletedEvent, handler);
        }

        /// <summary>
        /// Removes the reorder completed handler.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void RemoveReorderCompletedHandler(FrameworkElement element,
                                                         RoutedPropertyChangedEventHandler<int> handler)
        {
            element.RemoveHandler(ReorderCompletedEvent, handler);
        }

        #endregion

        #region Preview Adorner

        private class PreviewAdorner : Adorner
        {
            private readonly UIElement _itemElement;
            private readonly Rectangle _rectangle;

            public PreviewAdorner(UIElement adornedElement, UIElement itemElement)
                : base(adornedElement)
            {
                IsHitTestVisible = true;
                Opacity = 0.75;
                _itemElement = itemElement;
                _rectangle = new Rectangle
                {
                    Fill = new VisualBrush(itemElement),
                    Effect = new DropShadowEffect { ShadowDepth = 0, BlurRadius = 20 }
                };

                AddVisualChild(_rectangle);
                AddLogicalChild(_rectangle);
            }

            public static readonly DependencyProperty TopProperty =
                DependencyProperty.Register("Top", typeof(double), typeof(PreviewAdorner), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsArrange));

            public double Top
            {
                // ReSharper disable once MemberCanBePrivate.Local
                get { return (double)GetValue(TopProperty); }
                set { SetValue(TopProperty, value); }
            }

            protected override IEnumerator LogicalChildren
            {
                get { yield return _rectangle; }
            }

            protected override Visual GetVisualChild(int index)
            {
                if (index == 0)
                {
                    return _rectangle;
                }
                throw new ArgumentOutOfRangeException("index");
            }

            protected override int VisualChildrenCount
            {
                get { return 1; }
            }

            protected override Size MeasureOverride(Size constraint)
            {
                _rectangle.Measure(_itemElement.RenderSize);
                return constraint;
            }

            protected override Size ArrangeOverride(Size finalSize)
            {
                _rectangle.Arrange(new Rect(new Point(0,
                                                      Math.Max(0, Math.Min(
                                                                   AdornedElement.RenderSize.Height -
                                                                   _itemElement.RenderSize.Height, Top))),
                                            _itemElement.RenderSize));
                return finalSize;
            }
        }

        #endregion
    }
}
