// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Guide
{
    public partial class GuideHost : ContentControl
    {
        static GuideHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GuideHost), new FrameworkPropertyMetadata(typeof(GuideHost)));
        }

        public static RoutedCommand NextCommand = new RoutedCommand();
        public static RoutedCommand SkipCommand = new RoutedCommand();
        private GuideTree _guideTree = null;
        private Border _conver = new Border();
        private Path _path = new Path();

        public GuideHost()
        {
            {
                CommandBinding binding = new CommandBinding(NextCommand);
                binding.Executed += (l, k) =>
                {
                    if (_guideTree.Next() == null)
                    {
                        this.IsGuide = false;
                        return;
                    }

                    this.CurrentIndex = _guideTree.CurrentIndex;

                    this.ScrollTo();
                    this.Click();
                    this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                               {
                                   this.RefreshClip();
                               }));

                };

                this.CommandBindings.Add(binding);
            }


            {
                CommandBinding binding = new CommandBinding(SkipCommand);
                binding.Executed += (l, k) =>
                {
                    this.IsGuide = false;
                };

                this.CommandBindings.Add(binding);
            }

            this.Loaded += (l, k) =>
              {
                  this.Refresh();
              };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this._conver.Background = this.CoverBackground;
            this._path.Style = this.PathStyle;
            this.AddVisualChild(_conver);
            this.AddVisualChild(_path);
            this.AddVisualChild(ContentControl);
        }


        public bool IsGuide
        {
            get { return (bool)GetValue(IsGuideProperty); }
            set { SetValue(IsGuideProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsGuideProperty =
            DependencyProperty.Register("IsGuide", typeof(bool), typeof(GuideHost), new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (d, e) =>
             {
                 GuideHost control = d as GuideHost;

                 if (control == null) return;

                 if (e.OldValue is bool o)
                 {

                 }



                 Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                            {
                                control.Refresh();

                                if (e.NewValue is bool n)
                                {
                                    if (n)
                                    {
                                        control.InitData();
                                        control.Click();
                                        control.RefreshClip();
                                    }
                                }
                            }));

             }));


        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(GuideHost), new FrameworkPropertyMetadata(default(Style), (d, e) =>
             {
                 GuideHost control = d as GuideHost;

                 if (control == null) return;

                 if (e.OldValue is Style o)
                 {

                 }

                 if (e.NewValue is Style n)
                 {

                 }

             }));

        public ContentControl ContentControl
        {
            get { return (ContentControl)GetValue(ContentControlProperty); }
            set { SetValue(ContentControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContentControlProperty =
            DependencyProperty.Register("ContentControl", typeof(ContentControl), typeof(GuideHost), new FrameworkPropertyMetadata(default(ContentControl), (d, e) =>
             {
                 GuideHost control = d as GuideHost;

                 if (control == null) return;

                 if (e.OldValue is ContentControl o)
                 {

                 }

                 if (e.NewValue is ContentControl n)
                 {

                 }

             }));


        public int CurrentIndex
        {
            get { return (int)GetValue(CurrentIndexProperty); }
            set { SetValue(CurrentIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentIndexProperty =
            DependencyProperty.Register("CurrentIndex", typeof(int), typeof(GuideHost), new FrameworkPropertyMetadata(default(int), (d, e) =>
             {
                 GuideHost control = d as GuideHost;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));



        public int Total
        {
            get { return (int)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(int), typeof(GuideHost), new FrameworkPropertyMetadata(default(int), (d, e) =>
             {
                 GuideHost control = d as GuideHost;

                 if (control == null) return;

                 if (e.OldValue is int o)
                 {

                 }

                 if (e.NewValue is int n)
                 {

                 }

             }));




        public void InitData()
        {
            List<UIElement> items = this.GetChildren<UIElement>(l => Cattach.GetUseGuide(l) && Cattach.GetGuideParentTitle(l) == null)?.ToList();

            IEnumerable<UIElement> roots = items.Where(l => Cattach.GetGuideParentTitle(l) == null && l.Visibility == Visibility.Visible);

            List<GuideTreeNode> guideTreeNodes = new List<GuideTreeNode>();

            Action<GuideTreeNode> build = null;
            build = parent =>
             {
                 //var gt = Cattach.GetGuideTitle(parent.Element);
                 IEnumerable<UIElement> children = items.Where(l => Cattach.GetGuideParentTitle(l) == Cattach.GetGuideTitle(parent.Element)?.ToString() && l.Visibility == Visibility.Visible);

                 foreach (UIElement child in children)
                 {
                     if (child.Visibility != Visibility.Visible)
                         continue;
                     if (child.IsVisible == false)
                         continue;
                     if (child.RenderSize.Width < 10 || child.RenderSize.Height < 10)
                         continue;
                     if (child is FrameworkElement framework)
                     {
                         if (framework.IsLoaded == false)
                             return;

                     }
                     GuideTreeNode childNode = new GuideTreeNode(child);
                     childNode.Parent = parent;
                     parent.Chidren.Add(childNode);
                     build.Invoke(childNode);
                 }
             };

            foreach (UIElement root in roots)
            {
                if (root.Visibility != Visibility.Visible)
                    continue;
                if (root.IsVisible == false)
                    continue;
                if (root.RenderSize.Width < 10 || root.RenderSize.Height < 10)
                    continue;
                GuideTreeNode rootNode = new GuideTreeNode(root);
                guideTreeNodes.Add(rootNode);
                build.Invoke(rootNode);
            }

            this._guideTree = new GuideTree(guideTreeNodes);

            this.CurrentIndex = 1;
            this.Total = items.Count;
        }
        public void Refresh()
        {
            RectangleGeometry outRect = new RectangleGeometry(new Rect(this.RenderSize));
            RectangleGeometry childRect = new RectangleGeometry(new Rect(0, 0, 0, 0), 1, 1);
            CombinedGeometry combinedGeometry = new CombinedGeometry(GeometryCombineMode.Exclude, outRect, childRect);

            this._conver.Background = this.CoverBackground;
            this._conver.Clip = combinedGeometry;

            this._path.Data = new RectangleGeometry() { Rect = new Rect() };

            this._conver.Visibility = this.IsGuide ? Visibility.Visible : Visibility.Collapsed;
            this._path.Visibility = this.IsGuide ? Visibility.Visible : Visibility.Collapsed;

            if (this.ContentControl == null)
                return;

            this.ContentControl.Visibility = this.IsGuide ? Visibility.Visible : Visibility.Collapsed;
        }


        public void Click()
        {
            if (!this.IsLoaded)
                return;
            if (_guideTree.Current == null)
                return;
            bool use = Cattach.GetGuideUseClick(_guideTree.Current.Element);

            if (!use)
                return;

            if (_guideTree.Current.Element is ListBoxItem listBoxItem)
            {
                listBoxItem.IsSelected = true;
            }

            if (_guideTree.Current.Element is TabItem tabItem)
            {
                tabItem.IsSelected = true;
            }

            //  Do ：加载本级节点 
            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                       {
                           List<UIElement> children = this.GetChildren<UIElement>(l => Cattach.GetUseGuide(l) && Cattach.GetGuideParentTitle(l) == Cattach.GetGuideTitle(_guideTree.Current.Element)?.ToString() && l.Visibility == Visibility.Visible)?.ToList();

                           _guideTree.Current.Chidren.Clear();
                           foreach (UIElement child in children)
                           {
                               GuideTreeNode childNode = new GuideTreeNode(child);
                               childNode.Parent = _guideTree.Current;
                               _guideTree.Current.Chidren.Add(childNode);
                           }
                       }));

        }


        public void ScrollTo()
        {
            ScrollViewer scrollViewer = _guideTree.Current.Element.GetParent<ScrollViewer>();
            if (scrollViewer == null) 
                return;
            double currentScrollPosition = scrollViewer.VerticalOffset;
            Point point = new Point(0, currentScrollPosition);
            Point targetPosition = _guideTree.Current.Element.TransformToVisual(scrollViewer).Transform(point);
            scrollViewer.ScrollToVerticalOffset(targetPosition.Y);
        }

        public void RefreshClip()
        {
            if (!this.IsLoaded)
                return;
            if (this._guideTree.Current == null)
                return;
            Point point = this._guideTree.Current.Element.TranslatePoint(new Point(0, 0), this);
            Rect rect = new Rect(point, this._guideTree.Current.Element.RenderSize);
            {
                RectAnimation da = new RectAnimation();
                da.To = rect;
                da.EasingFunction = EasingFunctionFactroy.PowerEase;
                Storyboard sb = StoryboardFactory.Create();
                sb.Duration = TimeSpan.FromMilliseconds(1000);
                sb.Children.Add(da);
                Storyboard.SetTarget(da, this._conver);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1).(2)", new DependencyProperty[] { UIElement.ClipProperty, CombinedGeometry.Geometry2Property, RectangleGeometry.RectProperty }));
                sb.Begin();
            }

            {
                double thickness = this._path.StrokeThickness;
                RectAnimation da = new RectAnimation();
                da.To = new Rect(new Point(point.X - thickness * 0.5, point.Y - thickness * 0.5), new Size(this._guideTree.Current.Element.RenderSize.Width + thickness * 1, this._guideTree.Current.Element.RenderSize.Height + thickness * 1));
                da.EasingFunction = EasingFunctionFactroy.PowerEase;
                Storyboard sb = StoryboardFactory.Create();
                sb.Duration = TimeSpan.FromMilliseconds(1000);
                sb.Children.Add(da);
                Storyboard.SetTarget(da, this._path);
                Storyboard.SetTargetProperty(da, new PropertyPath("(0).(1)", new DependencyProperty[] { Path.DataProperty, RectangleGeometry.RectProperty }));
                sb.Begin();

            }
            object title = Cattach.GetGuideTitle(this._guideTree.Current.Element);
            Cattach.SetGuideTitle(this.ContentControl, title);
            this.ContentControl.Content = Cattach.GetGuideData(this._guideTree.Current.Element);
            this.ContentControl.ContentTemplate = Cattach.GetGuideDataTemplate(this._guideTree.Current.Element);
            this.ContentControl.Measure(this.RenderSize);
            this.ContentControl.Arrange(new Rect(0, 0, this.ActualWidth, this.ActualHeight));
            double x = rect.Right;

            if (rect.Width > this.RenderSize.Width - rect.Right)
            {
                x = rect.Left - this.ContentControl.RenderSize.Width;
            }

            if (rect.Left > this.RenderSize.Width - rect.Right && rect.Left > rect.Width)
            {
                x = rect.Left - this.ContentControl.RenderSize.Width;
            }

            if (x + this.ContentControl.RenderSize.Width > this.RenderSize.Width)
            {
                x = this.RenderSize.Width - this.ContentControl.RenderSize.Width;
            }

            if (x < 0)
            {
                x = this.RenderSize.Width / 2 - this.ContentControl.RenderSize.Width / 2;
            }


            double y = rect.Bottom;

            if (rect.Height > this.RenderSize.Height - rect.Bottom)
            {
                y = rect.Top;
            }

            if (rect.Top > this.RenderSize.Height - rect.Bottom && rect.Top > rect.Width)
            {
                y = rect.Bottom - this.ContentControl.RenderSize.Height;
            }

            if (y + this.ContentControl.RenderSize.Height > this.RenderSize.Height)
            {
                y = this.RenderSize.Height - this.ContentControl.RenderSize.Height;
            }

            if (this.ContentControl.CheckDefaultTransformGroup())
            {
                this.ContentControl.BeginAnimationXY(x, y, GuideViewPresenter.Instance.AnimationDuration);
            }

        }

        public Brush CoverBackground
        {
            get { return (Brush)GetValue(CoverBackgroundProperty); }
            set { SetValue(CoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CoverBackgroundProperty =
            DependencyProperty.Register("CoverBackground", typeof(Brush), typeof(GuideHost), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
             {
                 GuideHost control = d as GuideHost;

                 if (control == null) return;

                 if (e.OldValue is Brush o)
                 {

                 }

                 if (e.NewValue is Brush n)
                 {

                 }

             }));

        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Diagnostics.Debug.WriteLine("GuideHost.ArrangeOverride:" + DateTime.Now);
            _conver.Arrange(new Rect(new Point(0, 0), finalSize));
            _path.Arrange(new Rect(new Point(0, 0), finalSize));
            this.ContentControl.Arrange(new Rect(new Point(0, 0), finalSize));
            return base.ArrangeOverride(finalSize);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            System.Diagnostics.Debug.WriteLine("GuideHost.MeasureOverride:" + DateTime.Now);
            _conver.Measure(availableSize);
            _path.Measure(availableSize);
            this.ContentControl.Measure(availableSize);
            return base.MeasureOverride(availableSize);
        }
        protected override Visual GetVisualChild(int index)
        {
            if (base.VisualChildrenCount == index)
                return this._conver;
            if (base.VisualChildrenCount + 1 == index)
                return this._path;
            if (base.VisualChildrenCount + 2 == index)
                return this.ContentControl;

            return base.GetVisualChild(index);
        }

        protected override int VisualChildrenCount => base.VisualChildrenCount + 3;
    }

    public class GuideTreeNode
    {
        public GuideTreeNode(UIElement element)
        {
            this.Element = element;
        }
        public UIElement Element { get; set; }

        public List<GuideTreeNode> Chidren { get; } = new List<GuideTreeNode>();

        public GuideTreeNode Parent { get; set; }

        public GuideTreeNode GetRoot()
        {
            if (this.Parent == null) return this;

            return this.Parent.GetRoot();
        }

        public GuideTreeNode GetRight()
        {
            if (this.Parent == null) return null;

            int c = this.Parent.Chidren.IndexOf(this);

            if (c >= this.Parent.Chidren.Count - 1)
            {
                return this.Parent.GetRight();
            }

            return this.Parent.Chidren[c + 1];
        }

        public GuideTreeNode GetNext()
        {
            if (this.Chidren.Count > 0)
            {
                return this.Chidren.First();
            }

            return this.GetRight();
        }
    }

    public class GuideTree
    {
        public GuideTree(List<GuideTreeNode> roots)
        {
            this.Roots = roots;
            this.Current = roots?.FirstOrDefault();
        }
        public List<GuideTreeNode> Roots { get; } = new List<GuideTreeNode>();

        public GuideTreeNode Current { get; private set; }

        public int CurrentIndex { get; private set; } = 1;

        public GuideTreeNode Next()
        {
            CurrentIndex++;
            if (this.Current == null) return null;
            GuideTreeNode next = this.Current.GetNext();
            if (next == null)
            {
                int c = this.Roots.IndexOf(this.Current.GetRoot());
                if (c >= this.Roots.Count - 1)
                {
                    return this.Current = null;
                }

                this.Current = this.Roots[c + 1];
                return this.Current;
            }
            return this.Current = next;
        }


        public void Foreach(Action<GuideTreeNode> action)
        {

        }

    }
}
