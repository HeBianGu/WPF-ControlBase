using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.General.WpfControlLib
{
    [TemplatePart(Name = OverflowButtonKey, Type = typeof(ContextMenuToggleButton))]
    [TemplatePart(Name = HeaderPanelKey, Type = typeof(FTabPanel))]
    [TemplatePart(Name = OverflowScrollviewer, Type = typeof(FScrollViewer))]
    [TemplatePart(Name = ScrollButtonLeft, Type = typeof(System.Windows.Controls.Primitives.ButtonBase))]
    [TemplatePart(Name = ScrollButtonRight, Type = typeof(System.Windows.Controls.Primitives.ButtonBase))]
    [TemplatePart(Name = HeaderBorder, Type = typeof(Border))]
    public class FTabControl : System.Windows.Controls.TabControl
    {
        private const string OverflowButtonKey = "PART_OverflowButton";

        private const string HeaderPanelKey = "PART_HeaderPanel";

        private const string OverflowScrollviewer = "PART_OverflowScrollviewer";

        private const string ScrollButtonLeft = "PART_ScrollButtonLeft";

        private const string ScrollButtonRight = "PART_ScrollButtonRight";

        private const string HeaderBorder = "PART_HeaderBorder";

        private ContextMenuToggleButton _buttonOverflow;

        internal FTabPanel HeaderPanel { get; private set; }

        private FScrollViewer _scrollViewerOverflow;

        private System.Windows.Controls.Primitives.ButtonBase _buttonScrollLeft;

        private System.Windows.Controls.Primitives.ButtonBase _buttonScrollRight;

        private Border _headerBorder;

        /// <summary>
        ///     是否为内部操作
        /// </summary>
        internal bool IsInternalAction;

        /// <summary>
        ///     是否启用动画
        /// </summary>
        public static readonly DependencyProperty IsEnableAnimationProperty = DependencyProperty.Register(
            "IsEnableAnimation", typeof(bool), typeof(FTabControl), new PropertyMetadata(false));

        /// <summary>
        ///     是否启用动画
        /// </summary>
        public bool IsEnableAnimation
        {
            get => (bool)GetValue(IsEnableAnimationProperty);
            set => SetValue(IsEnableAnimationProperty, value);
        }

        /// <summary>
        ///     是否可以拖动
        /// </summary>
        public static readonly DependencyProperty IsDraggableProperty = DependencyProperty.Register(
            "IsDraggable", typeof(bool), typeof(FTabControl), new PropertyMetadata(false));

        /// <summary>
        ///     是否可以拖动
        /// </summary>
        public bool IsDraggable
        {
            get => (bool)GetValue(IsDraggableProperty);
            set => SetValue(IsDraggableProperty, value);
        }

        /// <summary>
        ///     是否显示关闭按钮
        /// </summary>
        public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.RegisterAttached(
            "ShowCloseButton", typeof(bool), typeof(FTabControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetShowCloseButton(DependencyObject element, bool value)
            => element.SetValue(ShowCloseButtonProperty, value);

        public static bool GetShowCloseButton(DependencyObject element)
            => (bool)element.GetValue(ShowCloseButtonProperty);

        /// <summary>
        ///     是否显示关闭按钮
        /// </summary>
        public bool ShowCloseButton
        {
            get => (bool)GetValue(ShowCloseButtonProperty);
            set => SetValue(ShowCloseButtonProperty, value);
        }

        /// <summary>
        ///     是否显示上下文菜单
        /// </summary>
        public static readonly DependencyProperty ShowContextMenuProperty = DependencyProperty.RegisterAttached(
            "ShowContextMenu", typeof(bool), typeof(FTabControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetShowContextMenu(DependencyObject element, bool value)
            => element.SetValue(ShowContextMenuProperty, value);

        public static bool GetShowContextMenu(DependencyObject element)
            => (bool)element.GetValue(ShowContextMenuProperty);

        /// <summary>
        ///     是否显示上下文菜单
        /// </summary>
        public bool ShowContextMenu
        {
            get => (bool)GetValue(ShowContextMenuProperty);
            set => SetValue(ShowContextMenuProperty, value);
        }

        /// <summary>
        ///     是否将标签填充
        /// </summary>
        public static readonly DependencyProperty IsEnableTabFillProperty = DependencyProperty.Register(
            "IsEnableTabFill", typeof(bool), typeof(FTabControl), new PropertyMetadata(false));

        /// <summary>
        ///     是否将标签填充
        /// </summary>
        public bool IsEnableTabFill
        {
            get => (bool)GetValue(IsEnableTabFillProperty);
            set => SetValue(IsEnableTabFillProperty, value);
        }

        /// <summary>
        ///     标签宽度
        /// </summary>
        public static readonly DependencyProperty TabItemWidthProperty = DependencyProperty.Register(
            "TabItemWidth", typeof(double), typeof(FTabControl), new PropertyMetadata(200.0));

        /// <summary>
        ///     标签宽度
        /// </summary>
        public double TabItemWidth
        {
            get => (double)GetValue(TabItemWidthProperty);
            set => SetValue(TabItemWidthProperty, value);
        }

        /// <summary>
        ///     标签高度
        /// </summary>
        public static readonly DependencyProperty TabItemHeightProperty = DependencyProperty.Register(
            "TabItemHeight", typeof(double), typeof(FTabControl), new PropertyMetadata(30.0));

        /// <summary>
        ///     标签高度
        /// </summary>
        public double TabItemHeight
        {
            get => (double)GetValue(TabItemHeightProperty);
            set => SetValue(TabItemHeightProperty, value);
        }

        /// <summary>
        ///     是否可以滚动
        /// </summary>
        public static readonly DependencyProperty IsScrollableProperty = DependencyProperty.Register(
            "IsScrollable", typeof(bool), typeof(FTabControl), new PropertyMetadata(false));

        /// <summary>
        ///     是否可以滚动
        /// </summary>
        public bool IsScrollable
        {
            get => (bool)GetValue(IsScrollableProperty);
            set => SetValue(IsScrollableProperty, value);
        }

        /// <summary>
        ///     是否显示溢出按钮
        /// </summary>
        public static readonly DependencyProperty ShowOverflowButtonProperty = DependencyProperty.Register(
            "ShowOverflowButton", typeof(bool), typeof(FTabControl), new PropertyMetadata(true));

        /// <summary>
        ///     是否显示溢出按钮
        /// </summary>
        public bool ShowOverflowButton
        {
            get => (bool)GetValue(ShowOverflowButtonProperty);
            set => SetValue(ShowOverflowButtonProperty, value);
        }

        /// <summary>
        ///     是否显示滚动按钮
        /// </summary>
        public static readonly DependencyProperty ShowScrollButtonProperty = DependencyProperty.Register(
            "ShowScrollButton", typeof(bool), typeof(FTabControl), new PropertyMetadata(false));

        /// <summary>
        ///     是否显示滚动按钮
        /// </summary>
        public bool ShowScrollButton
        {
            get => (bool)GetValue(ShowScrollButtonProperty);
            set => SetValue(ShowScrollButtonProperty, value);
        }

        /// <summary>
        ///     可见的标签数量
        /// </summary>
        private int _itemShowCount;

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (HeaderPanel == null)
            {
                IsInternalAction = false;
                return;
            }

            if (!IsEnableTabFill)
            {
                _itemShowCount = (int)(ActualWidth / TabItemWidth);
                _buttonOverflow.Visibility = ShowOverflowButton && Items.Count > 0 && Items.Count >= _itemShowCount ? Visibility.Visible : Visibility.Collapsed;
            }

            if (IsInternalAction)
            {
                IsInternalAction = false;
                return;
            }

            if (IsEnableAnimation)
            {
                HeaderPanel.SetCurrentValue(FTabPanel.FluidMoveDurationProperty, new Duration(TimeSpan.FromMilliseconds(200)));
            }
            else
            {
                HeaderPanel.FluidMoveDuration = new Duration(TimeSpan.FromSeconds(0));
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                for (var i = 0; i < Items.Count; i++)
                {
                    if (!(ItemContainerGenerator.ContainerFromIndex(i) is FTabItem item)) return;
                    item.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    item.FTabPanel = HeaderPanel;
                }
            }

            _headerBorder?.InvalidateMeasure();

            IsInternalAction = false;
        }

        public override void OnApplyTemplate()
        {
            if (_buttonOverflow != null)
            {
                if (_buttonOverflow.Menu != null)
                {
                    _buttonOverflow.Menu.Closed -= Menu_Closed;
                    _buttonOverflow.Menu = null;
                }

                _buttonOverflow.Click -= ButtonOverflow_Click;
            }

            //  ToDo：说明

            //if (_buttonScrollLeft != null) _buttonScrollLeft.Click -= ButtonScrollLeft_Click;
            //if (_buttonScrollRight != null) _buttonScrollRight.Click -= ButtonScrollRight_Click;

            base.OnApplyTemplate();
            HeaderPanel = GetTemplateChild(HeaderPanelKey) as FTabPanel;

            if (IsEnableTabFill) return;

            _buttonOverflow = GetTemplateChild(OverflowButtonKey) as ContextMenuToggleButton;
            _scrollViewerOverflow = GetTemplateChild(OverflowScrollviewer) as FScrollViewer;
            _buttonScrollLeft = GetTemplateChild(ScrollButtonLeft) as System.Windows.Controls.Primitives.ButtonBase;
            _buttonScrollRight = GetTemplateChild(ScrollButtonRight) as ButtonBase;
            _headerBorder = GetTemplateChild(HeaderBorder) as Border;

            //  ToDo：说明

            //if (_buttonScrollLeft != null) _buttonScrollLeft.Click += ButtonScrollLeft_Click;
            //if (_buttonScrollRight != null) _buttonScrollRight.Click += ButtonScrollRight_Click;

            if (_buttonOverflow != null)
            {
                if (ItemsSource != null)
                {
                    Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                }

                var size = DesiredSize;
                _itemShowCount = (int)(size.Width / TabItemWidth);
                _buttonOverflow.Visibility = ShowOverflowButton && Items.Count > 0 && Items.Count >= _itemShowCount ? Visibility.Visible : Visibility.Collapsed;

                var menu = new ContextMenu
                {
                    Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom,
                    PlacementTarget = _buttonOverflow
                };
                menu.Closed += Menu_Closed;
                _buttonOverflow.Menu = menu;
                _buttonOverflow.Click += ButtonOverflow_Click;
            }
        }

        private void Menu_Closed(object sender, RoutedEventArgs e) => _buttonOverflow.IsChecked = false;

        //private void ButtonScrollRight_Click(object sender, RoutedEventArgs e) =>
        //    _scrollViewerOverflow.ScrollToHorizontalOffsetInternal(Math.Min(
        //        _scrollViewerOverflow.CurrentHorizontalOffset + TabItemWidth, _scrollViewerOverflow.ScrollableWidth));

        //private void ButtonScrollLeft_Click(object sender, RoutedEventArgs e) =>
        //    _scrollViewerOverflow.ScrollToHorizontalOffsetInternal(Math.Max(
        //        _scrollViewerOverflow.CurrentHorizontalOffset - TabItemWidth, 0));

        private void ButtonOverflow_Click(object sender, RoutedEventArgs e)
        {
            if (_buttonOverflow.IsChecked == true)
            {
                _buttonOverflow.Menu.Items.Clear();
                for (var i = 0; i < Items.Count; i++)
                {
                    if (!(ItemContainerGenerator.ContainerFromIndex(i) is FTabItem item)) continue;

                    var menuItem = new MenuItem
                    {
                        HeaderStringFormat = ItemStringFormat,
                        HeaderTemplate = ItemTemplate,
                        HeaderTemplateSelector = ItemTemplateSelector,
                        Header = item.Header,
                        Width = TabItemWidth,
                        IsChecked = item.IsSelected,
                        IsCheckable = true
                    };

                    menuItem.Click += delegate
                    {
                        _buttonOverflow.IsChecked = false;

                        var list = GetActuaList();
                        if (list == null) return;

                        var actualItem = ItemContainerGenerator.ItemFromContainer(item);
                        if (actualItem == null) return;

                        var index = list.IndexOf(actualItem);
                        if (index >= _itemShowCount)
                        {
                            list.Remove(actualItem);
                            list.Insert(0, actualItem);
                            if (IsEnableAnimation)
                            {
                                HeaderPanel.SetCurrentValue(FTabPanel.FluidMoveDurationProperty, new Duration(TimeSpan.FromMilliseconds(200)));
                            }
                            else
                            {
                                HeaderPanel.FluidMoveDuration = new Duration(TimeSpan.FromSeconds(0));
                            }
                            HeaderPanel.ForceUpdate = true;
                            HeaderPanel.Measure(new Size(HeaderPanel.DesiredSize.Width, ActualHeight));
                            HeaderPanel.ForceUpdate = false;
                            SetCurrentValue(SelectedIndexProperty, 0);
                        }

                        item.IsSelected = true;
                    };
                    _buttonOverflow.Menu.Items.Add(menuItem);
                }
            }
        }

        internal double GetHorizontalOffset() => _scrollViewerOverflow?.CurrentHorizontalOffset ?? 0;

        internal void UpdateScroll() => _scrollViewerOverflow?.RaiseEvent(new MouseWheelEventArgs(Mouse.PrimaryDevice, Environment.TickCount, 0)
        {
            RoutedEvent = MouseWheelEvent
        });

        internal void CloseAllItems() => CloseOtherItems(null);

        internal void CloseOtherItems(FTabItem currentItem)
        {
            var actualItem = currentItem != null ? ItemContainerGenerator.ItemFromContainer(currentItem) : null;

            var list = GetActuaList();
            if (list == null) return;

            IsInternalAction = true;

            for (var i = 0; i < Items.Count; i++)
            {
                var item = list[i];
                if (!Equals(item, actualItem) && item != null)
                {
                    var argsClosing = new CancelRoutedEventArgs(FTabItem.ClosingEvent, item);

                    if (!(ItemContainerGenerator.ContainerFromItem(item) is FTabItem tabItem)) continue;

                    tabItem.RaiseEvent(argsClosing);
                    if (argsClosing.Cancel) return;

                    tabItem.RaiseEvent(new RoutedEventArgs(FTabItem.ClosedEvent, item));
                    list.Remove(item);

                    i--;
                }
            }

            SetCurrentValue(SelectedIndexProperty, Items.Count == 0 ? -1 : 0);
        }

        internal IList GetActuaList()
        {
            IList list;
            if (ItemsSource != null)
            {
                list = ItemsSource as IList;
            }
            else
            {
                list = Items;
            }

            return list;
        }

        protected override bool IsItemItsOwnContainerOverride(object item) => item is FTabItem;

        protected override DependencyObject GetContainerForItemOverride() => new FTabItem();
    }


    public class FTabItem : System.Windows.Controls.TabItem
    {
        /// <summary>
        ///     动画速度
        /// </summary>
        private const int AnimationSpeed = 150;

        /// <summary>
        ///     选项卡是否处于拖动状态
        /// </summary>
        private static bool ItemIsDragging;

        /// <summary>
        ///     选项卡是否等待被拖动
        /// </summary>
        private bool _isWaiting;

        /// <summary>
        ///     拖动中的选项卡坐标
        /// </summary>
        private Point _dragPoint;

        /// <summary>
        ///     鼠标按下时选项卡位置
        /// </summary>
        private int _mouseDownIndex;

        /// <summary>
        ///     鼠标按下时选项卡横向偏移
        /// </summary>
        private double _mouseDownOffsetX;

        /// <summary>
        ///     鼠标按下时的坐标
        /// </summary>
        private Point _mouseDownPoint;

        /// <summary>
        ///     右侧可移动的最大值
        /// </summary>
        private double _maxMoveRight;

        /// <summary>
        ///     左侧可移动的最大值
        /// </summary>
        private double _maxMoveLeft;

        /// <summary>
        ///     选项卡宽度
        /// </summary>
        public double ItemWidth { get; internal set; }

        /// <summary>
        ///     选项卡拖动等待距离（在鼠标移动了超过20个像素无关单位后，选项卡才开始被拖动）
        /// </summary>
        private const double WaitLength = 20;

        /// <summary>
        ///     选项卡是否处于拖动状态
        /// </summary>
        private bool _isDragging;

        /// <summary>
        ///     选项卡是否已经被拖动
        /// </summary>
        private bool _isDragged;

        /// <summary>
        ///     目标横向位移
        /// </summary>
        internal double TargetOffsetX { get; set; }

        /// <summary>
        ///     当前编号
        /// </summary>
        private int _currentIndex;

        /// <summary>
        ///     标签容器横向滚动距离
        /// </summary>
        private double _scrollHorizontalOffset;

        private FTabPanel _FTabPanel;
 
        /// <summary>
        ///     标签容器
        /// </summary>
        internal FTabPanel FTabPanel
        {
            //get => _FTabPanel ??= TabControlParent.HeaderPanel;
            get
            {
                if(_FTabPanel==null)
                {
                    _FTabPanel = TabControlParent.HeaderPanel;
                }

                return _FTabPanel;
            }
            set
            {
                _FTabPanel = value;
            }
        }

        /// <summary>
        ///     当前编号
        /// </summary>
        internal int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (_currentIndex == value || value < 0) return;
                var oldIndex = _currentIndex;
                _currentIndex = value;
                UpdateItemOffsetX(oldIndex);
            }
        }

        /// <summary>
        ///     是否显示关闭按钮
        /// </summary>
        public static readonly DependencyProperty ShowCloseButtonProperty =
            FTabControl.ShowCloseButtonProperty.AddOwner(typeof(FTabItem));

        /// <summary>
        ///     是否显示关闭按钮
        /// </summary>
        public bool ShowCloseButton
        {
            get => (bool)GetValue(ShowCloseButtonProperty);
            set => SetValue(ShowCloseButtonProperty, value);
        }

        public static void SetShowCloseButton(DependencyObject element, bool value)
            => element.SetValue(ShowCloseButtonProperty, value);

        public static bool GetShowCloseButton(DependencyObject element)
            => (bool)element.GetValue(ShowCloseButtonProperty);

        /// <summary>
        ///     是否显示上下文菜单
        /// </summary>
        public static readonly DependencyProperty ShowContextMenuProperty =
            FTabControl.ShowContextMenuProperty.AddOwner(typeof(FTabItem), new FrameworkPropertyMetadata(OnShowContextMenuChanged));

        private static void OnShowContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (FTabItem)d;
            if (ctl.Menu != null)
            {
                var show = (bool)e.NewValue;
                ctl.Menu.IsEnabled = show;
                ctl.Menu.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        ///     是否显示上下文菜单
        /// </summary>
        public bool ShowContextMenu
        {
            get => (bool)GetValue(ShowContextMenuProperty);
            set => SetValue(ShowContextMenuProperty, value);
        }

        public static void SetShowContextMenu(DependencyObject element, bool value)
            => element.SetValue(ShowContextMenuProperty, value);

        public static bool GetShowContextMenu(DependencyObject element)
            => (bool)element.GetValue(ShowContextMenuProperty);

        public static readonly DependencyProperty MenuProperty = DependencyProperty.Register(
            "Menu", typeof(ContextMenu), typeof(FTabItem), new PropertyMetadata(default(ContextMenu), OnMenuChanged));

        private static void OnMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (FTabItem)d;
            ctl.OnMenuChanged(e.NewValue as ContextMenu);
        }

        private void OnMenuChanged(ContextMenu menu)
        {
            if (IsLoaded && menu != null)
            {
                var parent = TabControlParent;
                if (parent == null) return;

                var item = parent.ItemContainerGenerator.ItemFromContainer(this);

                menu.DataContext = item;
                menu.SetBinding(IsEnabledProperty, new Binding(ShowContextMenuProperty.Name)
                {
                    Source = this
                });
                menu.SetBinding(VisibilityProperty, new Binding(ShowContextMenuProperty.Name)
                {
                    Source = this,
                    //Converter = Application.Current.FindResource("Boolean2VisibilityConverter") as IValueConverter
                    Converter = XConverter.BooleanToVisibilityConverter

                });
            }
        }

        public ContextMenu Menu
        {
            get => (ContextMenu)GetValue(MenuProperty);
            set => SetValue(MenuProperty, value);
        }

        /// <summary>
        ///     更新选项卡横向偏移
        /// </summary>
        /// <param name="oldIndex"></param>
        private void UpdateItemOffsetX(int oldIndex)
        {
            if (!_isDragging) return;
            var moveItem = FTabPanel.ItemDic[CurrentIndex];
            moveItem.CurrentIndex -= CurrentIndex - oldIndex;
            var offsetX = moveItem.TargetOffsetX;
            var resultX = offsetX + (oldIndex - CurrentIndex) * ItemWidth;
            FTabPanel.ItemDic[CurrentIndex] = this;
            FTabPanel.ItemDic[moveItem.CurrentIndex] = moveItem;
            moveItem.CreateAnimation(offsetX, resultX);
        }

        public FTabItem()
        {

            CommandBindings.Add(new CommandBinding(CommandService.Close, (s, e) => Close()));
            CommandBindings.Add(new CommandBinding(CommandService.CloseAll,
                (s, e) => { TabControlParent.CloseAllItems(); }));
            CommandBindings.Add(new CommandBinding(CommandService.CloseOther,
                (s, e) => { TabControlParent.CloseOtherItems(this); }));

            Loaded += (s, e) => OnMenuChanged(Menu);
        }

        private FTabControl TabControlParent => ItemsControl.ItemsControlFromItemContainer(this) as FTabControl;

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            IsSelected = true;
            Focus();
        }

        protected override void OnHeaderChanged(object oldHeader, object newHeader)
        {
            base.OnHeaderChanged(oldHeader, newHeader);

            if (FTabPanel != null)
            {
                FTabPanel.ForceUpdate = true;
                InvalidateMeasure();
                FTabPanel.ForceUpdate = true;
            }
        }

        internal void Close()
        {
            var parent = TabControlParent;

            if (parent == null) return;

            var item = parent.ItemContainerGenerator.ItemFromContainer(this);

            var argsClosing = new CancelRoutedEventArgs(ClosingEvent, item);

            RaiseEvent(argsClosing);

            if (argsClosing.Cancel) return;

            FTabPanel.SetCurrentValue(FTabPanel.FluidMoveDurationProperty, parent.IsEnableAnimation
                    ? new Duration(TimeSpan.FromMilliseconds(200))
                    : new Duration(TimeSpan.FromMilliseconds(1)));

            parent.IsInternalAction = true;

            RaiseEvent(new RoutedEventArgs(ClosedEvent, item));

            var list = parent.GetActuaList();

            list?.Remove(item);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            var parent = TabControlParent;
            if (parent == null) return;

            if (parent.IsDraggable && !ItemIsDragging && !_isDragging)
            {
                parent.UpdateScroll();
                FTabPanel.FluidMoveDuration = new Duration(TimeSpan.FromSeconds(0));
                _mouseDownOffsetX = RenderTransform.Value.OffsetX;
                _scrollHorizontalOffset = parent.GetHorizontalOffset();
                var mx = TranslatePoint(new Point(), parent).X + _scrollHorizontalOffset;
                _mouseDownIndex = CalLocationIndex(mx);
                var subIndex = _mouseDownIndex - CalLocationIndex(_scrollHorizontalOffset);
                _maxMoveLeft = -subIndex * ItemWidth;
                _maxMoveRight = parent.ActualWidth - ActualWidth + _maxMoveLeft;

                _isDragging = true;
                ItemIsDragging = true;
                _isWaiting = true;
                _dragPoint = e.GetPosition(parent);
                _dragPoint = new Point(_dragPoint.X + +_scrollHorizontalOffset, _dragPoint.Y);
                _mouseDownPoint = _dragPoint;
                CaptureMouse();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ItemIsDragging && _isDragging)
            {
                var parent = TabControlParent;
                if (parent == null) return;

                var subX = TranslatePoint(new Point(), parent).X + _scrollHorizontalOffset;
                CurrentIndex = CalLocationIndex(subX);

                var p = e.GetPosition(parent);
                p = new Point(p.X + _scrollHorizontalOffset, p.Y);

                var subLeft = p.X - _dragPoint.X;
                var totalLeft = p.X - _mouseDownPoint.X;

                if (Math.Abs(subLeft) <= WaitLength && _isWaiting) return;

                _isWaiting = false;
                _isDragged = true;

                var left = subLeft + RenderTransform.Value.OffsetX;
                if (totalLeft < _maxMoveLeft)
                {
                    left = _maxMoveLeft + _mouseDownOffsetX;
                }
                else if (totalLeft > _maxMoveRight)
                {
                    left = _maxMoveRight + _mouseDownOffsetX;
                }

                var t = new TranslateTransform(left, 0);
                RenderTransform = t;
                _dragPoint = p;
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            ReleaseMouseCapture();

            if (_isDragged)
            {
                var parent = TabControlParent;
                if (parent == null) return;

                var subX = TranslatePoint(new Point(), parent).X + _scrollHorizontalOffset;
                var index = CalLocationIndex(subX);
                var left = index * ItemWidth;
                var offsetX = RenderTransform.Value.OffsetX;
                CreateAnimation(offsetX, offsetX - subX + left, index);
            }

            _isDragging = false;
            ItemIsDragging = false;
            _isDragged = false;
        }

        /// <summary>
        ///     创建动画
        /// </summary>
        internal void CreateAnimation(double offsetX, double resultX, int index = -1)
        {
            var parent = TabControlParent;

            void AnimationCompleted()
            {
                RenderTransform = new TranslateTransform(resultX, 0);
                if (index == -1) return;

                var list = parent.GetActuaList();
                if (list == null) return;

                var item = parent.ItemContainerGenerator.ItemFromContainer(this);
                if (item == null) return;

                FTabPanel.CanUpdate = false;
                parent.IsInternalAction = true;

                list.Remove(item);
                parent.IsInternalAction = true;
                list.Insert(index, item);
                FTabPanel.CanUpdate = true;
                FTabPanel.ForceUpdate = true;
                FTabPanel.Measure(new Size(FTabPanel.DesiredSize.Width, ActualHeight));
                FTabPanel.ForceUpdate = false;

                Focus();
                IsSelected = true;

                if (!IsMouseCaptured)
                {
                    parent.SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedIndexProperty, _currentIndex);
                }
            }

            TargetOffsetX = resultX;
            if (!parent.IsEnableAnimation)
            {
                AnimationCompleted();
                return;
            }

            var animation = AnimationHelper.CreateAnimation(resultX, AnimationSpeed);
            animation.FillBehavior = FillBehavior.Stop;
            animation.Completed += (s1, e1) => AnimationCompleted();
            var f = new TranslateTransform(offsetX, 0);
            RenderTransform = f;
            f.BeginAnimation(TranslateTransform.XProperty, animation, HandoffBehavior.Compose);
        }

        /// <summary>
        ///     计算选项卡当前合适的位置编号
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        private int CalLocationIndex(double left)
        {
            var maxIndex = TabControlParent.Items.Count - 1;
            var div = (int)(left / ItemWidth);
            var rest = left % ItemWidth;
            var result = rest / ItemWidth > .5 ? div + 1 : div;

            return result > maxIndex ? maxIndex : result;
        }

        public static readonly RoutedEvent ClosingEvent = EventManager.RegisterRoutedEvent("Closing", RoutingStrategy.Bubble, typeof(EventHandler), typeof(FTabItem));

        public event EventHandler Closing
        {
            add => AddHandler(ClosingEvent, value);
            remove => RemoveHandler(ClosingEvent, value);
        }

        public static readonly RoutedEvent ClosedEvent = EventManager.RegisterRoutedEvent("Closed", RoutingStrategy.Bubble, typeof(EventHandler), typeof(FTabItem));

        public event EventHandler Closed
        {
            add => AddHandler(ClosedEvent, value);
            remove => RemoveHandler(ClosedEvent, value);
        }
    }

    /// <summary>
    ///     带上下文菜单的切换按钮
    /// </summary>
    public class ContextMenuToggleButton : System.Windows.Controls.Primitives.ToggleButton
    {
        public ContextMenu Menu { get; set; }

        protected override void OnClick()
        {
            base.OnClick();
            if (Menu != null)
            {
                if (IsChecked == true)
                {
                    Menu.PlacementTarget = this;
                    Menu.IsOpen = true;
                }
                else
                {
                    Menu.IsOpen = false;
                }
            }
        }
    }

    public class FTabPanel : Panel
    {
        private int _itemCount;

        /// <summary>
        ///     是否可以更新
        /// </summary>
        internal bool CanUpdate = true;

        /// <summary>
        ///     选项卡字典
        /// </summary>
        internal Dictionary<int, FTabItem> ItemDic = new Dictionary<int, FTabItem>();

        /// <summary>
        ///     流式行为持续时间
        /// </summary>
        public static readonly DependencyProperty FluidMoveDurationProperty = DependencyProperty.Register(
            "FluidMoveDuration", typeof(Duration), typeof(FTabPanel), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(200))));

        /// <summary>
        ///     流式行为持续时间
        /// </summary>
        public Duration FluidMoveDuration
        {
            get => (Duration)GetValue(FluidMoveDurationProperty);
            set => SetValue(FluidMoveDurationProperty, value);
        }

        /// <summary>
        ///     是否将标签填充
        /// </summary>
        public static readonly DependencyProperty IsEnableTabFillProperty = DependencyProperty.Register(
            "IsEnableTabFill", typeof(bool), typeof(FTabPanel), new PropertyMetadata(false));

        /// <summary>
        ///     是否将标签填充
        /// </summary>
        public bool IsEnableTabFill
        {
            get => (bool)GetValue(IsEnableTabFillProperty);
            set => SetValue(IsEnableTabFillProperty, value);
        }

        /// <summary>
        ///     标签宽度
        /// </summary>
        public static readonly DependencyProperty TabItemWidthProperty = DependencyProperty.Register(
            "TabItemWidth", typeof(double), typeof(FTabPanel), new PropertyMetadata(200.0));

        /// <summary>
        ///     标签宽度
        /// </summary>
        public double TabItemWidth
        {
            get => (double)GetValue(TabItemWidthProperty);
            set => SetValue(TabItemWidthProperty, value);
        }

        /// <summary>
        ///     标签高度
        /// </summary>
        public static readonly DependencyProperty TabItemHeightProperty = DependencyProperty.Register(
            "TabItemHeight", typeof(double), typeof(FTabPanel), new PropertyMetadata(30.0));

        /// <summary>
        ///     标签高度
        /// </summary>
        public double TabItemHeight
        {
            get => (double)GetValue(TabItemHeightProperty);
            set => SetValue(TabItemHeightProperty, value);
        }

        /// <summary>
        ///     是否可以强制更新
        /// </summary>
        internal bool ForceUpdate;

        private Size _oldSize;

        /// <summary>
        ///     是否已经加载
        /// </summary>
        private bool _isLoaded;

        protected override Size MeasureOverride(Size constraint)
        {
            if ((_itemCount == InternalChildren.Count || !CanUpdate) && !ForceUpdate) return _oldSize;
            constraint.Height = TabItemHeight;
            _itemCount = InternalChildren.Count;

            var size = new Size();

            ItemDic.Clear();

            var count = InternalChildren.Count;
            if (count == 0)
            {
                _oldSize = new Size();
                return _oldSize;
            }
            constraint.Width += InternalChildren.Count;

            var itemWidth = .0;
            var arr = new int[count];

            if (!IsEnableTabFill)
            {
                itemWidth = TabItemWidth;
            }
            else
            {
                if (TemplatedParent is FTabControl tabControl)
                {
                    arr = ArithmeticHelper.DivideInt2Arr((int)tabControl.ActualWidth + InternalChildren.Count, count);
                }
            }

            for (var index = 0; index < count; index++)
            {
                if (IsEnableTabFill)
                {
                    itemWidth = arr[index];
                }
                if (InternalChildren[index] is FTabItem tabItem)
                {
                    tabItem.RenderTransform = new TranslateTransform();
                    tabItem.MaxWidth = itemWidth;
                    var rect = new Rect
                    {
                        X = size.Width - tabItem.BorderThickness.Left,
                        Width = itemWidth,
                        Height = TabItemHeight
                    };
                    tabItem.Arrange(rect);
                    tabItem.ItemWidth = itemWidth - tabItem.BorderThickness.Left;
                    tabItem.CurrentIndex = index;
                    tabItem.TargetOffsetX = 0;
                    ItemDic[index] = tabItem;
                    size.Width += tabItem.ItemWidth;
                }
            }
            size.Height = constraint.Height;
            _oldSize = size;
            return _oldSize;
        }

        public FTabPanel()
        {
            Loaded += (s, e) =>
            {
                if (_isLoaded) return;
                ForceUpdate = true;
                Measure(new Size(DesiredSize.Width, ActualHeight));
                ForceUpdate = false;
                foreach (var item in ItemDic.Values)
                {
                    item.FTabPanel = this;
                }
                _isLoaded = true;
            };
        }
    }

    /// <summary>
    ///     包含内部使用的一些简单算法
    /// </summary>
    internal class ArithmeticHelper
    {
        /// <summary>
        ///     平分一个整数到一个数组中
        /// </summary>
        /// <param name="num"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] DivideInt2Arr(int num, int count)
        {
            var arr = new int[count];
            var div = num / count;
            var rest = num % count;
            for (var i = 0; i < count; i++)
            {
                arr[i] = div;
            }
            for (var i = 0; i < rest; i++)
            {
                arr[i] += 1;
            }
            return arr;
        }

        /// <summary>
        ///     计算控件在窗口中的可见坐标
        /// </summary>
        public static Point CalSafePoint(FrameworkElement element, FrameworkElement showElement, Thickness thickness = default)
        {
            if (element == null || showElement == null) return default;
            var point = element.PointToScreen(new Point(0, 0));

            if (point.X < 0) point.X = 0;
            if (point.Y < 0) point.Y = 0;

            var maxLeft = SystemParameters.WorkArea.Width -
                          ((double.IsNaN(showElement.Width) ? showElement.ActualWidth : showElement.Width) +
                           thickness.Left + thickness.Right);
            var maxTop = SystemParameters.WorkArea.Height -
                         ((double.IsNaN(showElement.Height) ? showElement.ActualHeight : showElement.Height) +
                          thickness.Top + thickness.Bottom);
            return new Point(maxLeft > point.X ? point.X : maxLeft, maxTop > point.Y ? point.Y : maxTop);
        }

        /// <summary>
        ///     获取布局范围框
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Rect GetLayoutRect(FrameworkElement element)
        {
            var num1 = element.ActualWidth;
            var num2 = element.ActualHeight;
            if (element is Image || element is MediaElement)
                if (element.Parent is Canvas)
                {
                    num1 = double.IsNaN(element.Width) ? num1 : element.Width;
                    num2 = double.IsNaN(element.Height) ? num2 : element.Height;
                }
                else
                {
                    num1 = element.RenderSize.Width;
                    num2 = element.RenderSize.Height;
                }
            var width = element.Visibility == Visibility.Collapsed ? 0.0 : num1;
            var height = element.Visibility == Visibility.Collapsed ? 0.0 : num2;
            var margin = element.Margin;
            var layoutSlot = LayoutInformation.GetLayoutSlot(element);
            var x = 0.0;
            var y = 0.0;
            switch (element.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = layoutSlot.Left + margin.Left;
                    break;
                case HorizontalAlignment.Center:
                    x = (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - width / 2.0;
                    break;
                case HorizontalAlignment.Right:
                    x = layoutSlot.Right - margin.Right - width;
                    break;
                case HorizontalAlignment.Stretch:
                    x = Math.Max(layoutSlot.Left + margin.Left,
                        (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - width / 2.0);
                    break;
            }
            switch (element.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = layoutSlot.Top + margin.Top;
                    break;
                case VerticalAlignment.Center:
                    y = (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - height / 2.0;
                    break;
                case VerticalAlignment.Bottom:
                    y = layoutSlot.Bottom - margin.Bottom - height;
                    break;
                case VerticalAlignment.Stretch:
                    y = Math.Max(layoutSlot.Top + margin.Top,
                        (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - height / 2.0);
                    break;
            }
            return new Rect(x, y, width, height);
        }

        /// <summary>
        ///     计算两点的连线和x轴的夹角
        /// </summary>
        /// <param name="center"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double CalAngle(Point center, Point p) => Math.Atan2(p.Y - center.Y, p.X - center.X) * 180 / Math.PI;

        /// <summary>
        ///     计算法线
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Vector3D CalNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            var v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            var v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }
    }

    /// <summary>
    ///     包含一些常用的动画辅助方法
    /// </summary>
    public class AnimationHelper
    {
        /// <summary>
        ///     创建一个Thickness动画
        /// </summary>
        /// <param name="thickness"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static ThicknessAnimation CreateAnimation(Thickness thickness = default, double milliseconds = 200)
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

        internal static void DecomposeGeometryStr(string geometryStr, out double[] arr)
        {
            var collection = Regex.Matches(geometryStr, @"[+-]?\d*\.?\d+(?:\.\d+)?(?:[eE][+-]?\d+)?");
            arr = new double[collection.Count];
            for (var i = 0; i < collection.Count; i++)
            {
                arr[i] = Convert.ToDouble(collection[i].Value);
            }
        }

        internal static Geometry ComposeGeometry(string[] strings, double[] arr)
        {
            var builder = new StringBuilder(strings[0]);
            for (var i = 0; i < arr.Length; i++)
            {
                var s = strings[i + 1];
                var n = arr[i];
                if (!double.IsNaN(n))
                {
                    builder.Append(n).Append(s);
                }
            }

            return Geometry.Parse(builder.ToString());
        }

        internal static Geometry InterpolateGeometry(double[] from, double[] to, double progress, string[] strings)
        {
            var accumulated = new double[to.Length];
            for (var i = 0; i < to.Length; i++)
            {
                var fromValue = from[i];
                accumulated[i] = fromValue + (to[i] - fromValue) * progress;
            }

            return ComposeGeometry(strings, accumulated);
        }

        internal static double[] InterpolateGeometryValue(double[] from, double[] to, double progress)
        {
            var accumulated = new double[to.Length];
            for (var i = 0; i < to.Length; i++)
            {
                var fromValue = from[i];
                accumulated[i] = fromValue + (to[i] - fromValue) * progress;
            }

            return accumulated;
        }
    }

    public class FScrollViewer : System.Windows.Controls.ScrollViewer
    {
        private double _totalVerticalOffset;

        private double _totalHorizontalOffset;

        private bool _isRunning;

        /// <summary>
        ///     滚动方向
        /// </summary>
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
            "Orientation", typeof(Orientation), typeof(FScrollViewer), new PropertyMetadata(Orientation.Vertical));

        /// <summary>
        ///     滚动方向
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        ///     是否响应鼠标滚轮操作
        /// </summary>
        public static readonly DependencyProperty CanMouseWheelProperty = DependencyProperty.Register(
            "CanMouseWheel", typeof(bool), typeof(FScrollViewer), new PropertyMetadata(true));

        /// <summary>
        ///     是否响应鼠标滚轮操作
        /// </summary>
        public bool CanMouseWheel
        {
            get => (bool)GetValue(CanMouseWheelProperty);
            set => SetValue(CanMouseWheelProperty, value);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (!CanMouseWheel) return;

            if (!IsEnableInertia)
            {
                if (Orientation == Orientation.Vertical)
                {
                    base.OnMouseWheel(e);
                }
                else
                {
                    _totalHorizontalOffset = HorizontalOffset;
                    CurrentHorizontalOffset = HorizontalOffset;
                    _totalHorizontalOffset = Math.Min(Math.Max(0, _totalHorizontalOffset - e.Delta), ScrollableWidth);
                    CurrentHorizontalOffset = _totalHorizontalOffset;
                }
                return;
            }
            e.Handled = true;

            if (Orientation == Orientation.Vertical)
            {
                if (!_isRunning)
                {
                    _totalVerticalOffset = VerticalOffset;
                    CurrentVerticalOffset = VerticalOffset;
                }
                _totalVerticalOffset = Math.Min(Math.Max(0, _totalVerticalOffset - e.Delta), ScrollableHeight);
                ScrollToVerticalOffsetInternal(_totalVerticalOffset);
            }
            else
            {
                if (!_isRunning)
                {
                    _totalHorizontalOffset = HorizontalOffset;
                    CurrentHorizontalOffset = HorizontalOffset;
                }
                _totalHorizontalOffset = Math.Min(Math.Max(0, _totalHorizontalOffset - e.Delta), ScrollableWidth);
                ScrollToHorizontalOffsetInternal(_totalHorizontalOffset);
            }
        }

        internal void ScrollToTopInternal(double milliseconds = 500)
        {
            if (!_isRunning)
            {
                _totalVerticalOffset = VerticalOffset;
                CurrentVerticalOffset = VerticalOffset;
            }
            ScrollToVerticalOffsetInternal(0, milliseconds);
        }

        internal void ScrollToVerticalOffsetInternal(double offset, double milliseconds = 500)
        {
            var animation = AnimationHelper.CreateAnimation(offset, milliseconds);
            animation.EasingFunction = new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            };
            animation.FillBehavior = FillBehavior.Stop;
            animation.Completed += (s, e1) =>
            {
                CurrentVerticalOffset = offset;
                _isRunning = false;
            };
            _isRunning = true;

            BeginAnimation(CurrentVerticalOffsetProperty, animation, HandoffBehavior.Compose);
        }

        internal void ScrollToHorizontalOffsetInternal(double offset, double milliseconds = 500)
        {
            var animation = AnimationHelper.CreateAnimation(offset, milliseconds);

            animation.EasingFunction = new CubicEase
            {
                EasingMode = EasingMode.EaseOut
            };
            animation.FillBehavior = FillBehavior.Stop;
            animation.Completed += (s, e1) =>
            {
                CurrentHorizontalOffset = offset;
                _isRunning = false;
            };
            _isRunning = true;

            BeginAnimation(CurrentHorizontalOffsetProperty, animation, HandoffBehavior.Compose);
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) =>
            IsPenetrating ? null : base.HitTestCore(hitTestParameters);

        /// <summary>
        ///     是否支持惯性
        /// </summary>
        public static readonly DependencyProperty IsEnableInertiaProperty = DependencyProperty.RegisterAttached(
            "IsEnableInertia", typeof(bool), typeof(FScrollViewer), new PropertyMetadata(false));

        public static void SetIsEnableInertia(DependencyObject element, bool value)
        {
            element.SetValue(IsEnableInertiaProperty, value);
        }

        public static bool GetIsEnableInertia(DependencyObject element)
        {
            return (bool)element.GetValue(IsEnableInertiaProperty);
        }

        /// <summary>
        ///     是否支持惯性
        /// </summary>
        public bool IsEnableInertia
        {
            get => (bool)GetValue(IsEnableInertiaProperty);
            set => SetValue(IsEnableInertiaProperty, value);
        }

        /// <summary>
        ///     控件是否可以穿透点击
        /// </summary>
        public static readonly DependencyProperty IsPenetratingProperty = DependencyProperty.RegisterAttached(
            "IsPenetrating", typeof(bool), typeof(FScrollViewer), new PropertyMetadata(false));

        /// <summary>
        ///     控件是否可以穿透点击
        /// </summary>
        public bool IsPenetrating
        {
            get => (bool)GetValue(IsPenetratingProperty);
            set => SetValue(IsPenetratingProperty, value);
        }

        public static void SetIsPenetrating(DependencyObject element, bool value)
        {
            element.SetValue(IsPenetratingProperty, value);
        }

        public static bool GetIsPenetrating(DependencyObject element)
        {
            return (bool)element.GetValue(IsPenetratingProperty);
        }

        /// <summary>
        ///     当前垂直滚动偏移
        /// </summary>
        internal static readonly DependencyProperty CurrentVerticalOffsetProperty = DependencyProperty.Register(
            "CurrentVerticalOffset", typeof(double), typeof(FScrollViewer), new PropertyMetadata(0.0, OnCurrentVerticalOffsetChanged));

        private static void OnCurrentVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToVerticalOffset(v);
            }
        }

        /// <summary>
        ///     当前垂直滚动偏移
        /// </summary>
        internal double CurrentVerticalOffset
        {
            // ReSharper disable once UnusedMember.Local
            get => (double)GetValue(CurrentVerticalOffsetProperty);
            set => SetValue(CurrentVerticalOffsetProperty, value);
        }

        /// <summary>
        ///     当前水平滚动偏移
        /// </summary>
        internal static readonly DependencyProperty CurrentHorizontalOffsetProperty = DependencyProperty.Register(
            "CurrentHorizontalOffset", typeof(double), typeof(FScrollViewer), new PropertyMetadata(0.0, OnCurrentHorizontalOffsetChanged));

        private static void OnCurrentHorizontalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FScrollViewer ctl && e.NewValue is double v)
            {
                ctl.ScrollToHorizontalOffset(v);
            }
        }

        /// <summary>
        ///     当前水平滚动偏移
        /// </summary>
        internal double CurrentHorizontalOffset
        {
            get => (double)GetValue(CurrentHorizontalOffsetProperty);
            set => SetValue(CurrentHorizontalOffsetProperty, value);
        }
    }
}
