// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.Chart2D
{
    public interface IDraw
    {
        void Draw(Canvas canvas);

        void Refresh();

        void Clear();
    }

    public abstract class DrawingCanvas : Canvas, IDraw
    {
        public bool UseVisual
        {
            get { return (bool)GetValue(UseVisualProperty); }
            set { SetValue(UseVisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseVisualProperty =
            DependencyProperty.Register("UseVisual", typeof(bool), typeof(DrawingCanvas), new PropertyMetadata(default(bool), (d, e) =>
             {
                 DrawingCanvas control = d as DrawingCanvas;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        private List<Visual> visuals = new List<Visual>();

        //获取Visual的个数
        protected override int VisualChildrenCount
        {
            get
            {
                if (UseVisual)
                {
                    return visuals.Count;
                }
                else
                {
                    return base.VisualChildrenCount;
                }
            }
        }

        //获取Visual
        protected override Visual GetVisualChild(int index)
        {
            if (UseVisual)
            {
                return visuals[index];
            }
            else
            {
                return base.GetVisualChild(index);
            }
        }

        //添加Visual
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);

            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        ////删除Visual
        //public void RemoveVisual(Visual visual)
        //{
        //    //visuals.Remove(visual);

        //    base.RemoveVisualChild(visual);
        //    base.RemoveLogicalChild(visual);
        //}

        //命中测试
        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }

        //使用DrawVisual画Polyline
        public Visual Polyline(PointCollection points, Brush brush, double thinkness)
        {
            DrawingVisual visual = new DrawingVisual();
            DrawingContext dc = visual.RenderOpen();

            Pen pen = new Pen(brush, thinkness);
            pen.Freeze();  //冻结画笔，这样能加快绘图速度

            for (int i = 0; i < points.Count - 1; i++)
            {
                dc.DrawLine(pen, points[i], points[i + 1]);
            }

            dc.Close();
            return visual;
        }

        public virtual void Clear()
        {
            this.Children.Clear();

            if (this.UseVisual)
            {
                foreach (Visual item in this.visuals)
                {
                    base.RemoveVisualChild(item);
                    base.RemoveLogicalChild(item);
                }

                this.visuals.Clear();
            }
        }

        public virtual void Draw(Canvas canvas)
        {
            this.Clear();
        }

        public virtual void Refresh()
        {

        }

        //public virtual void Clear()
        //{
        //    this.Children.Clear();
        //}
    }

    public class LayerBase : DrawingCanvas
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(LayerBase), "S.LayerBase.Default");

        static LayerBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayerBase), new FrameworkPropertyMetadata(typeof(LayerBase)));
        }

        public LayerBase()
        {
            this.SizeChanged += (l, k) =>
            {
                //if (this.IsLoaded == false) return;

                ////Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                ////{
                ////    this.TryDraw();
                ////})); 

                //try
                //{ 
                //    if (!this.IsLoaded) return;

                //    this.Draw(this);

                //    this.OnDrawed();
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(ex);

                //    //Trace.Fail(ex.Message);
                //}

                //  Do ：触发绘制动画
                this.OnDrawed();
            };

            this.Loaded += (l, k) =>
            {
                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                //{
                //    this.TryDraw();
                //}));

                //try
                //{
                //    this.Draw(this);

                //    this.OnDrawed();
                //}
                //catch (Exception ex)
                //{
                //    Debug.WriteLine(ex);

                //    //Trace.Fail(ex.Message);
                //}


            };

            this.MouseDown += (l, k) =>
              {
                  //Debug.WriteLine(this.GetType().FullName);
              };

        }

        ///// <summary>  </summary>
        //public bool TryFreeze { get; set; }

        /// <summary>
        /// 冻结修改属性刷新 需要手动调用刷新 手动调用DrawOnce
        /// </summary>
        public bool TryFreeze
        {
            get { return (bool)GetValue(TryFreezeProperty); }
            set { SetValue(TryFreezeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TryFreezeProperty =
            DependencyProperty.Register("TryFreeze", typeof(bool), typeof(LayerBase), new PropertyMetadata(true, (d, e) =>
             {
                 LayerBase control = d as LayerBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));

        public virtual void ForceDraw()
        {
            //try
            //{
            if (!this.IsLoaded) return;

            this.Draw(this);

            this.OnDrawed();
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);

            //    //Trace.Fail(ex.Message);
            //}
        }

        public virtual void TryDraw()
        {
            //try
            //{
            if (this.TryFreeze) 
                return;

            if (!this.IsLoaded) 
                return;

            if (!this.IsVisible)
                return;
            this.Draw(this);

            this.OnDrawed();
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);

            //    //Trace.Fail(ex.Message);
            //}
        }


        //声明和注册路由事件
        public static readonly RoutedEvent DrawedRoutedEvent =
            EventManager.RegisterRoutedEvent("Drawed", RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(LayerBase));
        //CLR事件包装
        public event RoutedEventHandler Drawed
        {
            add { this.AddHandler(DrawedRoutedEvent, value); }
            remove { this.RemoveHandler(DrawedRoutedEvent, value); }
        }

        //激发路由事件,借用Click事件的激发方法

        protected void OnDrawed()
        {
            RoutedEventArgs args = new RoutedEventArgs(DrawedRoutedEvent, this);
            this.RaiseEvent(args);
        }
    }


    public class Timelines : List<Timeline>
    {

    }

    public class AnimationLayer : LayerBase
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(AnimationLayer), "S.AnimationLayer.Default");

        static AnimationLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimationLayer), new FrameworkPropertyMetadata(typeof(AnimationLayer)));
        }

        public AnimationLayer()
        {
            this.Drawed += AssociatedObject_Drawed;
        }

        public Timelines Timelines
        {
            get { return (Timelines)GetValue(TimelinesProperty); }
            set { SetValue(TimelinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimelinesProperty =
            DependencyProperty.Register("Timelines", typeof(Timelines), typeof(AnimationLayer), new PropertyMetadata(new Timelines(), (d, e) =>
            {
                AnimationLayer control = d as AnimationLayer;

                if (control == null) return;

                Timelines config = e.NewValue as Timelines;

                control.TryDraw();

            }));

        private void AssociatedObject_Drawed(object sender, RoutedEventArgs e)
        {
            if (Timelines == null) return;
            IEnumerable<UIElement> items = this.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            List<UIElement> controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = StoryboardFactory.Create();

            for (int i = 0; i < controls.Count; i++)
            {
                foreach (DoubleAnimation item in Timelines.OfType<DoubleAnimation>())
                {
                    TimeSpan span = TimeSpan.FromMilliseconds(i * (SplitMilliSecond + item.Duration.TimeSpan.TotalMilliseconds));

                    TimeSpan end = item.Duration.TimeSpan + span;

                    DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

                    EasingDoubleKeyFrame key1 = new EasingDoubleKeyFrame(item.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));

                    frames.KeyFrames.Add(key1);
                    Storyboard.SetTarget(frames, controls[i]);
                    Storyboard.SetTargetProperty(frames, Storyboard.GetTargetProperty(item));
                    storyboard.Children.Add(frames);

                    DoubleAnimation animation = item.Clone();
                    animation.BeginTime = span;
                    Storyboard.SetTarget(animation, controls[i]);

                    storyboard.Children.Add(animation);
                }

                foreach (ThicknessAnimation item in Timelines.OfType<ThicknessAnimation>())
                {
                    TimeSpan span = TimeSpan.FromMilliseconds(i * (SplitMilliSecond + item.Duration.TimeSpan.TotalMilliseconds));

                    TimeSpan end = item.Duration.TimeSpan + span;

                    ThicknessAnimationUsingKeyFrames frames = new ThicknessAnimationUsingKeyFrames();

                    EasingThicknessKeyFrame key1 = new EasingThicknessKeyFrame(item.From.Value, KeyTime.FromTimeSpan(TimeSpan.Zero));

                    frames.KeyFrames.Add(key1);
                    Storyboard.SetTarget(frames, controls[i]);
                    Storyboard.SetTargetProperty(frames, Storyboard.GetTargetProperty(item));
                    storyboard.Children.Add(frames);

                    ThicknessAnimation animation = item.Clone();
                    animation.BeginTime = span;
                    Storyboard.SetTarget(animation, controls[i]);

                    storyboard.Children.Add(animation);
                }
            }

            storyboard.FillBehavior = FillBehavior.HoldEnd;
            storyboard.Begin();
        }

        public double SplitMilliSecond
        {
            get { return (double)GetValue(SplitMilliSecondProperty); }
            set { SetValue(SplitMilliSecondProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitMilliSecondProperty =
            DependencyProperty.Register("SplitMilliSecond", typeof(double), typeof(AnimationLayer), new PropertyMetadata(5.0, (d, e) =>
            {
                AnimationLayer control = d as AnimationLayer;

                if (control == null) return;

                control.TryDraw();
            }));


        public bool IsUseAnimation
        {
            get { return (bool)GetValue(IsUseAnimationProperty); }
            set { SetValue(IsUseAnimationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseAnimationProperty =
            DependencyProperty.Register("IsUseAnimation", typeof(bool), typeof(AnimationLayer), new PropertyMetadata(true, (d, e) =>
             {
                 AnimationLayer control = d as AnimationLayer;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));
        private Storyboard _storyboard;

        /// <summary>
        ///     更新路径
        /// </summary>
        protected void RunPath(Path path, double MilliSecond = 1000)
        {
            if (!this.IsUseAnimation) return;

            double _pathLength = path.Data.GetTotalLength(new Size(path.ActualWidth, path.ActualHeight), path.StrokeThickness) * 2;

            if (Math.Abs(_pathLength) < 1E-06) return;

            path.StrokeDashOffset = _pathLength;

            path.StrokeDashArray = new DoubleCollection(new List<double>
            {
                _pathLength,
                _pathLength
            });

            //定义动画
            if (_storyboard != null)
            {
                _storyboard.Stop();
            }
            _storyboard = StoryboardFactory.Create();

            DoubleAnimationUsingKeyFrames frames = new DoubleAnimationUsingKeyFrames();

            LinearDoubleKeyFrame frame0 = new LinearDoubleKeyFrame
            {
                Value = _pathLength,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero)
            };

            LinearDoubleKeyFrame frame1 = new LinearDoubleKeyFrame
            {
                Value = 0,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(MilliSecond))
            };
            frames.KeyFrames.Add(frame0);
            frames.KeyFrames.Add(frame1);

            Storyboard.SetTarget(frames, path);
            Storyboard.SetTargetProperty(frames, new PropertyPath(Path.StrokeDashOffsetProperty));
            _storyboard.Children.Add(frames);

            _storyboard.Begin();
        }

    }

    public class XyLayer : AnimationLayer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(XyLayer), "S.XyLayer.Default");

        static XyLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XyLayer), new FrameworkPropertyMetadata(typeof(XyLayer)));
        }
        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(XyLayer), new PropertyMetadata(Brushes.Black, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

                control.TryDraw();

            }));

        public override void Draw(Canvas canvas)
        {
            this.InitX();

            this.InitY();

            base.Draw(canvas);
        }

        protected double minX;
        protected double maxX;

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetX(double value, double width)
        {
            if (this.maxX == this.minX) return this.minX;

            double bottom = ((value - this.minX) / (this.maxX - this.minX)) * width;

            return bottom;
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetX(double value)
        {
            return this.GetX(value, this.ActualWidth);
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetY(double value, double height)
        {
            if (this.maxY == this.minY) return this.minY;

            double bottom = height - ((value - this.minY) / (this.maxY - this.minY)) * height;

            return bottom;
        }

        /// <summary> 获取Canvas位置对应的y值 </summary>
        public virtual double GetValueY(double value)
        {
            if (this.maxY == this.minY) return this.minY;

            double bottom = ((this.ActualHeight - value) / this.ActualHeight) * (this.maxY - this.minY) + this.minY;

            return bottom;
        }

        /// <summary> 获取值对应Canvas的位置 </summary>
        public virtual double GetY(double value)
        {
            return this.GetY(value, this.ActualHeight);
        }

        protected double minY;
        protected double maxY;

        protected virtual void InitY()
        {
            if (this.yAxis == null || this.yAxis.Count == 0) return;

            this.minY = this.yAxis.Min();
            this.maxY = this.yAxis.Max();


        }
        protected virtual void InitX()
        {
            if (this.xAxis == null || this.xAxis.Count == 0) return;

            this.minX = this.xAxis.Min();
            this.maxX = this.xAxis.Max();
        }

        public Style LineStyle
        {
            get { return (Style)GetValue(LineStyleProperty); }
            set { SetValue(LineStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineStyleProperty =
            DependencyProperty.Register("LineStyle", typeof(Style), typeof(XyLayer), new PropertyMetadata(default(Style), (d, e) =>
            {
                Grid control = d as Grid;

                if (control == null) return;

                Style config = e.NewValue as Style;

                control.TryDraw();

            }));

        //[TypeConverter(typeof(DataTypeConverter))]
        public DoubleCollection xAxis
        {
            get { return (DoubleCollection)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(DoubleCollection), typeof(XyLayer), new FrameworkPropertyMetadata(new DoubleCollection(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

                control.TryDraw();

            }));

        //[TypeConverter(typeof(DataTypeConverter))]
        public DoubleCollection yAxis
        {
            get { return (DoubleCollection)GetValue(yAxisProperty); }
            set { SetValue(yAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisProperty =
            DependencyProperty.Register("yAxis", typeof(DoubleCollection), typeof(XyLayer), new FrameworkPropertyMetadata(new DoubleCollection(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                DoubleCollection config = e.NewValue as DoubleCollection;

                control.TryDraw();

            }));


        [TypeConverter(typeof(DisplayTypeConverter))]
        public ObservableCollection<string> xDisplay
        {
            get { return (ObservableCollection<string>)GetValue(xDisplayProperty); }
            set { SetValue(xDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xDisplayProperty =
            DependencyProperty.Register("xDisplay", typeof(ObservableCollection<string>), typeof(XyLayer), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

                control.TryDraw();

            }));

        [TypeConverter(typeof(DisplayTypeConverter))]

        public ObservableCollection<string> yDisplay
        {
            get { return (ObservableCollection<string>)GetValue(yDisplayProperty); }
            set { SetValue(yDisplayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yDisplayProperty =
            DependencyProperty.Register("yDisplay", typeof(ObservableCollection<string>), typeof(XyLayer), new PropertyMetadata(new ObservableCollection<string>(), (d, e) =>
             {
                 XyLayer control = d as XyLayer;

                 if (control == null) return;

                 ObservableCollection<string> config = e.NewValue as ObservableCollection<string>;

                 control.TryDraw();

             }));

        [TypeConverter(typeof(DataArrayTypeConverter))]
        public ObservableCollection<double[]> xMap
        {
            get { return (ObservableCollection<double[]>)GetValue(xMapProperty); }
            set { SetValue(xMapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xMapProperty =
            DependencyProperty.Register("xMap", typeof(ObservableCollection<double[]>), typeof(XyLayer), new PropertyMetadata(default(ObservableCollection<double[]>), (d, e) =>
             {
                 XyLayer control = d as XyLayer;

                 if (control == null) return;

                 ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

             }));

        [TypeConverter(typeof(DataArrayTypeConverter))]
        public ObservableCollection<double[]> yMap
        {
            get { return (ObservableCollection<double[]>)GetValue(yMapProperty); }
            set { SetValue(yMapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yMapProperty =
            DependencyProperty.Register("yMap", typeof(ObservableCollection<double[]>), typeof(XyLayer), new PropertyMetadata(default(ObservableCollection<double[]>), (d, e) =>
             {
                 XyLayer control = d as XyLayer;

                 if (control == null) return;

                 ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

             }));



        public Func<double, string> xConvert
        {
            get { return (Func<double, string>)GetValue(xConvertProperty); }
            set { SetValue(xConvertProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xConvertProperty =
            DependencyProperty.Register("xConvert", typeof(Func<double, string>), typeof(XyLayer), new PropertyMetadata(default(Func<double, string>), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                Func<double, string> config = e.NewValue as Func<double, string>;

            }));


        public Func<double, string> yConvert
        {
            get { return (Func<double, string>)GetValue(yConvertProperty); }
            set { SetValue(yConvertProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yConvertProperty =
            DependencyProperty.Register("yConvert", typeof(Func<double, string>), typeof(XyLayer), new PropertyMetadata(default(Func<double, string>), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                Func<double, string> config = e.NewValue as Func<double, string>;

            }));


    }

    public class DataLayer : XyLayer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DataLayer), "S.DataLayer.Default");

        static DataLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DataLayer), new FrameworkPropertyMetadata(typeof(DataLayer)));
        }

        //[TypeConverter(typeof(DataTypeConverter))]
        public DoubleCollection Data
        {
            get { return (DoubleCollection)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(DoubleCollection), typeof(DataLayer), new PropertyMetadata(new DoubleCollection(), (d, e) =>
            {
                DataLayer control = d as DataLayer;
                if (control == null) 
                    return;
                DoubleCollection config = e.NewValue as DoubleCollection;
                if(config==null) 
                    return;
                control.InitDataBoolean(config.Count);
                control.TryDraw();

            }));

        protected virtual void InitDataBoolean(int count, bool value = true)
        {
            this.DataBoolean = Enumerable.Range(0, count).Select(l => value)?.ToObservable();
        }

        //[TypeConverter(typeof(DataBoolenTypeConverter))]
        public ObservableCollection<bool> DataBoolean
        {
            get { return (ObservableCollection<bool>)GetValue(DataBooleanProperty); }
            set { SetValue(DataBooleanProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataBooleanProperty =
            DependencyProperty.Register("DataBoolean", typeof(ObservableCollection<bool>), typeof(DataLayer), new PropertyMetadata(new ObservableCollection<bool>(), (d, e) =>
             {
                 DataLayer control = d as DataLayer;

                 if (control == null) return;

                 ObservableCollection<bool> config = e.NewValue as ObservableCollection<bool>;

             }));

        [TypeConverter(typeof(DataArrayTypeConverter))]
        public ObservableCollection<double[]> DataMap
        {
            get { return (ObservableCollection<double[]>)GetValue(DataMapProperty); }
            set { SetValue(DataMapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataMapProperty =
            DependencyProperty.Register("DataMap", typeof(ObservableCollection<double[]>), typeof(DataLayer), new PropertyMetadata(default(ObservableCollection<double[]>), (d, e) =>
             {
                 DataLayer control = d as DataLayer;

                 if (control == null) return;

                 ObservableCollection<double[]> config = e.NewValue as ObservableCollection<double[]>;

             }));



    }

    public class MapLayer : DataLayer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(MapLayer), "S.MapLayer.Default");

        static MapLayer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MapLayer), new FrameworkPropertyMetadata(typeof(MapLayer)));
        }

        public MapLayer() : base()
        {
            this.Loaded += (l, k) =>
            {
                Chart group = this.GetParent<Chart>();

                this.yAxises = group?.GetChildren<yAxis>()?.ToObservable();

                this.xAxises = group?.GetChildren<xAxis>()?.ToObservable();

                this.TryDraw();
            };

        }

        public ObservableCollection<yAxis> yAxises
        {
            get { return (ObservableCollection<yAxis>)GetValue(yAxisesProperty); }
            set { SetValue(yAxisesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisesProperty =
            DependencyProperty.Register("yAxises", typeof(ObservableCollection<yAxis>), typeof(MapLayer), new PropertyMetadata(default(ObservableCollection<yAxis>), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<yAxis> config = e.NewValue as ObservableCollection<yAxis>;

            }));


        public ObservableCollection<xAxis> xAxises
        {
            get { return (ObservableCollection<xAxis>)GetValue(xAxisesProperty); }
            set { SetValue(xAxisesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisesProperty =
            DependencyProperty.Register("xAxises", typeof(ObservableCollection<xAxis>), typeof(MapLayer), new PropertyMetadata(default(ObservableCollection<xAxis>), (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<xAxis> config = e.NewValue as ObservableCollection<xAxis>;

            }));


        public double GetMapY(double x, double value)
        {
            if (this.maxY == this.minY) 
                return this.minY;
            yAxis find = this.yAxises?.FirstOrDefault(l => l.Value == x);
            if (find == null) 
                return value;
            if(find.yAxis.Count==0)
                return value;
            double min = find.yAxis.Min();
            double max = find.yAxis.Max();
            return ((value - min) / (max - min)) * (this.maxY - this.minY) + this.minY;
        }

        public double GetMapX(double y, double value)
        {
            if (this.maxX == this.minX) 
                return this.minX;
            xAxis find = this.xAxises?.FirstOrDefault(l => l.Value == y);
            if (find == null)
                return value;
            if (find.yAxis.Count == 0)
                return value;
            double min = find.yAxis.Min();

            double max = find.yAxis.Max();


            return ((value - min) / (max - min)) * (this.maxX - this.minX) + this.minX;
        }
    }

    public class Layer : MapLayer
    {
        public static new ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(Layer), "S.Layer.Default");

        static Layer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Layer), new FrameworkPropertyMetadata(typeof(Layer)));
        }

        public Style MarkStyle
        {
            get { return (Style)GetValue(MarkStyleProperty); }
            set { SetValue(MarkStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MarkStyleProperty =
            DependencyProperty.Register("MarkStyle", typeof(Style), typeof(Layer), new PropertyMetadata(default(Style), (d, e) =>
            {
                Layer control = d as Layer;

                if (control == null) return;

                Style config = e.NewValue as Style;

                control.TryDraw();

            }));


        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.Register("LabelStyle", typeof(Style), typeof(Layer), new PropertyMetadata(default(Style), (d, e) =>
             {
                 Layer control = d as Layer;

                 if (control == null) return;

                 Style config = e.NewValue as Style;

                 control.TryDraw();

             }));


        protected override void InitX()
        {
            if (this.xAxis.Count > 0)
            {
                this.minX = this.xAxis.Min();

                this.maxX = this.xAxis.Max();
            }
        }

        protected override void InitY()
        {
            if (this.yAxis.Count > 0)
            {
                this.minY = this.yAxis.Min();

                this.maxY = this.yAxis.Max();
            }
        }

        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PathStyleProperty =
            DependencyProperty.Register("PathStyle", typeof(Style), typeof(Layer), new PropertyMetadata(default(Style), (d, e) =>
            {
                Layer control = d as Layer;

                if (control == null) return;

                Style config = e.NewValue as Style;

                control.TryDraw();

            }));

        bool _refreshing = false;
        public override void TryDraw()
        {
            if (_refreshing)
                return;
            _refreshing = true;
            //  Do ：重写刷新数据在空闲时刷新
            this.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() =>
            {
                _refreshing = false;
                base.TryDraw();

            }));
        }
    }

}
