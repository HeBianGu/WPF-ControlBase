using HeBianGu.Base.WpfBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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
using System.Windows.Navigation;
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

    public class LayerBase : Canvas, IDraw
    {
        public LayerBase()
        {
            this.SizeChanged += (l, k) =>
            {
                if (this.IsLoaded == false) return;

                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                //{
                //    this.TryDraw();
                //}));

                this.TryDraw();
            };

            this.Loaded += (l, k) =>
            {
                //Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, new Action(() =>
                //{
                //    this.TryDraw();
                //}));

                this.TryDraw();
            };

            this.MouseDown += (l, k) =>
              {
                  Debug.WriteLine(this.GetType().FullName);
              };

        }

        public virtual void Draw(Canvas canvas)
        {
            this.Clear(); 
        }

        public virtual void Refresh()
        {

        }

        public virtual void Clear()
        {
            this.Children.Clear();
        }

        ///// <summary> 冻结修改属性刷新 </summary>
        //public bool TryFreeze { get; set; }

        public bool TryFreeze
        {
            get { return (bool)GetValue(TryFreezeProperty); }
            set { SetValue(TryFreezeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TryFreezeProperty =
            DependencyProperty.Register("TryFreeze", typeof(bool), typeof(LayerBase), new PropertyMetadata(default(bool), (d, e) =>
             {
                 LayerBase control = d as LayerBase;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        public virtual void TryDraw()
        {
            try
            {
                if (this.TryFreeze) return;

                if (!this.IsLoaded) return;

                this.Draw(this);

                this.OnDrawed();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                //Trace.Fail(ex.Message);
            }
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

    public class AnimationLayer : LayerBase
    {
        public AnimationLayer()
        {
            this.Drawed += AssociatedObject_Drawed;
        }

        public ArrayList Timelines
        {
            get { return (ArrayList)GetValue(TimelinesProperty); }
            set { SetValue(TimelinesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimelinesProperty =
            DependencyProperty.Register("Timelines", typeof(ArrayList), typeof(AnimationLayer), new PropertyMetadata(new ArrayList(), (d, e) =>
            {
                AnimationLayer control = d as AnimationLayer;

                if (control == null) return;

                ArrayList config = e.NewValue as ArrayList;

                control.TryDraw();

            }));


        void AssociatedObject_Drawed(object sender, RoutedEventArgs e)
        {
            var items = this.GetChildren<UIElement>().Where(l => l.RenderTransform is TransformGroup);

            items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

            var controls = items?.ToList();

            if (controls == null || controls.Count == 0) return;

            Storyboard storyboard = new Storyboard();

            for (int i = 0; i < controls.Count; i++)
            {
                foreach (var item in Timelines.OfType<DoubleAnimation>())
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

                foreach (var item in Timelines.OfType<ThicknessAnimation>())
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


        Storyboard _storyboard;

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
            _storyboard = new Storyboard();

            var frames = new DoubleAnimationUsingKeyFrames();

            var frame0 = new LinearDoubleKeyFrame
            {
                Value = _pathLength,
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero)
            };

            var frame1 = new LinearDoubleKeyFrame
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

            var bottom = ((value - this.minX) / (this.maxX - this.minX)) * width;

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

            var bottom = height - ((value - this.minY) / (this.maxY - this.minY)) * height;

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

        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> xAxis
        {
            get { return (ObservableCollection<double>)GetValue(xAxisProperty); }
            set { SetValue(xAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty xAxisProperty =
            DependencyProperty.Register("xAxis", typeof(ObservableCollection<double>), typeof(XyLayer), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

                control.TryDraw();

            }));

        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> yAxis
        {
            get { return (ObservableCollection<double>)GetValue(yAxisProperty); }
            set { SetValue(yAxisProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty yAxisProperty =
            DependencyProperty.Register("yAxis", typeof(ObservableCollection<double>), typeof(XyLayer), new FrameworkPropertyMetadata(new ObservableCollection<double>(), FrameworkPropertyMetadataOptions.Inherits, (d, e) =>
            {
                XyLayer control = d as XyLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

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

    }


    public class DataLayer : XyLayer
    {
        [TypeConverter(typeof(DataTypeConverter))]
        public ObservableCollection<double> Data
        {
            get { return (ObservableCollection<double>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<double>), typeof(DataLayer), new PropertyMetadata(new ObservableCollection<double>(), (d, e) =>
            {
                DataLayer control = d as DataLayer;

                if (control == null) return;

                ObservableCollection<double> config = e.NewValue as ObservableCollection<double>;

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

    public class MapLayer: DataLayer
    {
        public MapLayer() : base()
        {
            this.Loaded += (l, k) =>
            {
                var group = this.GetParent<Chart>();

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
            if (this.maxY == this.minY) return this.minY;

            var find = this.yAxises?.FirstOrDefault(l => l.Value == x);

            if (find == null) return value;

            double min = find.yAxis.Min();

            double max = find.yAxis.Max();


            return ((value - min) / (max - min)) * (this.maxY - this.minY) + this.minY;
        }



        public double GetMapX(double y, double value)
        {
            if (this.maxX == this.minX) return this.minX;

            var find = this.xAxises?.FirstOrDefault(l => l.Value == y);

            if (find == null) return value;

            double min = find.yAxis.Min();

            double max = find.yAxis.Max();


            return ((value - min) / (max - min)) * (this.maxX - this.minX) + this.minX;
        }
    }

    /// <summary> 数据列表 </summary>
    public class Layer : MapLayer
    {
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

        public override void TryDraw()
        {
            //  Do ：重写刷新数据在空闲时刷新
            this.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(() =>
            {
                base.TryDraw();
            }));

        }
    }

}
