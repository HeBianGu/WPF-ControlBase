using HeBianGu.Base.WpfBase;
using HeBianGu.Control.LayerBox;
using HeBianGu.Control.SharpDx;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace HeBianGu.Control.SharpDxLayer
{
    public abstract class SharpDxLayer : SharpDxImage, ILayer
    {
        public SharpDxLayer()
        {
            var attribute = this.GetType().GetCustomAttribute<DisplayerAttribute>();
            if (attribute != null)
            {
                this.Text = attribute.Name;
                this.Icon = attribute.Icon;
                this.Order = attribute.Order;
                this.Description = attribute.Description;
            }

            var cmdps = this.GetType().GetProperties().Where(x => typeof(ICommand).IsAssignableFrom(x.PropertyType));
            foreach (var cmdp in cmdps)
            {
                if (cmdp.CanRead == false)
                    continue;
                if (cmdp.GetCustomAttribute<BrowsableAttribute>()?.Browsable == false)
                    continue;
                ICommand command = cmdp.GetValue(this) as ICommand;
                this.Commands.Add(command);
            }
        }
        public ObservableCollection<ICommand> Commands { get; } = new ObservableCollection<ICommand>();

        public DisplayerCommand RefreshDrawCommand => new DisplayerCommand((s, e) =>
        {
            this.RefreshDraw();
        }, (s, e) => true)
        { Name = "刷新", Icon = Icons.Refresh };


        public int Order { get; private set; }

        [Display(Name = "文本", GroupName = "数据")]
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SharpDxLayer), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));

        [Display(Name = "图标", GroupName = "数据")]
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(SharpDxLayer), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(SharpDxLayer), new FrameworkPropertyMetadata(default(string), (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        [Display(Name = "背景颜色", GroupName = "数据")]
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(SharpDxLayer), new FrameworkPropertyMetadata(null, (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {
                }

                if (e.NewValue is Brush n)
                {
                    control.RefreshDraw();
                }

            }));

        [Display(Name = "是否显示", GroupName = "数据")]
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(SharpDxLayer), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;
                if (control == null) return;
                control.Clear();
                if (e.OldValue is bool o && e.NewValue is bool n)
                {
                    control.OnIsVisibleChanged(o, n);
                }
                control.RefreshDraw();
            }));


        protected virtual void OnIsVisibleChanged(bool o, bool n)
        {

        }


        [Display(Name = "是否选中", GroupName = "数据")]
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(SharpDxLayer), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is bool o)
                {
                }

                if (e.NewValue is bool n)
                {
                    control.RefreshDraw();
                }
            }));
        [Display(Name = "线条颜色", GroupName = "数据")]
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(SharpDxLayer), new FrameworkPropertyMetadata(Brushes.Black, (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {
                }

                if (e.NewValue is Brush n)
                {
                    control.RefreshDraw();
                }

            }));

        [Display(Name = "线条粗细", GroupName = "数据")]
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(SharpDxLayer), new FrameworkPropertyMetadata(0.5, (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is double o)
                {
                }

                if (e.NewValue is double n)
                {
                    control.RefreshDraw();
                }

            }));

        [Display(Name = "线条虚线", GroupName = "数据")]
        public DoubleCollection Dash
        {
            get { return (DoubleCollection)GetValue(DashProperty); }
            set { SetValue(DashProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DashProperty =
            DependencyProperty.Register("Dash", typeof(DoubleCollection), typeof(SharpDxLayer), new FrameworkPropertyMetadata(new DoubleCollection(new double[3] { 3, 5, 1 }), (d, e) =>
            {
                SharpDxLayer control = d as SharpDxLayer;

                if (control == null) return;

                if (e.OldValue is DoubleCollection o)
                {

                }

                if (e.NewValue is DoubleCollection n)
                {
                    control.RefreshDraw();
                }
            }));

        protected DashStyle GetDashStyle(DoubleCollection from)
        {
            if (from == null)
                return null;
            var ds = from.Select(x => x / this.LayerView.Coord.DPI);
            //DoubleCollection doubles = new DoubleCollection(ds);
            return new DashStyle() { Dashes = new DoubleCollection(from) };
        }

        public virtual Pen GetPen()
        {
            return new Pen(this.Stroke, this.StrokeThickness / this.LayerView.Coord.DPI) { DashStyle = this.GetDashStyle(this.Dash) };
        }

        public Pen GetDashPen() => new Pen(this.Stroke, this.StrokeThickness / this.LayerView.Coord.DPI) { DashStyle = this.GetDashStyle(ShapeDrawingSetting.Instance.DrawingDashStyle) };


        public virtual void RefreshDraw()
        {
            //this.DelayInvoke(() =>
            //{
            //    this.Draw(_layerView);
            //}, DispatcherPriority.Render);
            this.Draw(_layerView);
            //System.Diagnostics.Debug.WriteLine("RefreshDraw");
        }

        protected LayerView _layerView;
        protected LayerView LayerView => _layerView;

        public virtual Rect ContentBounds => new Rect(0, 0, this.ActualWidth, this.ActualHeight);

        protected SharpDxDrawing _drawing = new SharpDxDrawing();

        public virtual void Draw(LayerView element)
        {
            if (element == null)
                return;

            if (_layerView == null)
                _layerView = element;

            if (IsVisible == false)
                return;
            _drawing.RenderTarget = this.RenderTarget;
            _drawing.LayerView = element;
#if DEBUG
            int cnt = System.Environment.TickCount;
#endif
            using (var dc = RenderOpen())
            {
                if (IsVisible == false)
                    return;
                if (dc == null)
                    return;
                this.Draw(_drawing);
                _drawing.RenderTarget = null;
            }
#if DEBUG
            int timeSpan = System.Environment.TickCount - cnt;
            if (timeSpan > 0)
                System.Diagnostics.Trace.WriteLine($"D2dLayer Draw :{timeSpan}");
#endif
        }
        public virtual void BeginInvokeDraw(Action<IDrawing> action = null)
        {
            this.DelayInvoke(() =>
            {
                if (this.LayerView == null)
                    return;

                if (this.IsVisible == false)
                    return;

                //using (DrawingContext dc = RenderOpen())
                //{
                //    _drawing.DrawingContext = dc;
                //    _drawing.LayerView = this.LayerView;
                //    this.Draw(_drawing);
                //    if (this.IsSelected)
                //    {
                //        dc.DrawRectangle(null, new Pen(Brushes.Blue, 2), ContentBounds);
                //    }
                //    action?.Invoke(_drawing);
                //}
            }, DispatcherPriority.Input);

        }

        public virtual void Draw(IDrawing drawing)
        {
            this.Clear(drawing);
        }

        protected void DrawPolyLine(params Point[] points)
        {
            //using (var dc = RenderOpen())
            //{
            //    points.Aggregate((x1, x2) =>
            //    {
            //        dc.DrawLine(new Pen(Stroke, StrokeThickness), x1, x2);
            //        return x2;
            //    });
            //}
        }

        public virtual void Clear()
        {
            _drawing.RenderTarget?.Clear(new RawColor4(1.0f, 1.0f, 1.0f, 0.0f));
        }

        public virtual void Clear(IDrawing dc)
        {
            if (this.LayerView == null)
                return;
            this.Clear();
        }

        public virtual void OnScaleChanged(object sender, ObjectRoutedEventArgs<Tuple<double, double>> e)
        {

        }

        public Point GetLogicalMousePosition()
        {
            return this.LayerView.Coord.DeviceToLogical(Mouse.GetPosition(this.LayerView));
        }

        public virtual void OnCoordinateChanged(object sender, EventArgs e)
        {
            this.RenderTarget.Transform = this.LayerView.Coord.RotateCoordMatrix.ToRawMatrix3x2();
            //this.InvalidateRender();
            //this.CallRender(x =>
            //{
            //    this.RenderTarget.Transform = this.LayerView.Coord.RotateCoordMatrix.ToRawMatrix3x2();

            //});
            this.RefreshDraw();
        }
    }
}
