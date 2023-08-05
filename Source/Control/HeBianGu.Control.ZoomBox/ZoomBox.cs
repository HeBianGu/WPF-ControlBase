using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.Control.ZoomBox
{
    [TemplatePart(Name = "PART_LayerView")]
    [TemplatePart(Name = "PART_ThumbGrid")]
    public partial class ZoomBox : ContentControl
    {
        static ZoomBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoomBox), new FrameworkPropertyMetadata(typeof(ZoomBox)));
        }

        Point? _start;
        private Grid _thumbGrid = null;
        private ZoomView _zoomView;

        public ZoomBox()
        {
            {
                CommandBinding binding = new CommandBinding(ZoomCommands.ZoomToVisual);
                binding.Executed += (l, k) =>
                {
                    if (k.Parameter is UIElement element)
                    {
                        var matrix = new MatrixTransform(this._zoomView.Coord.CoordMatrixInv);
                        Rect rect = new Rect(element.RenderSize);
                        GeneralTransform transform = element.TransformToVisual(this._zoomView);
                        Rect dstRect = transform.TransformBounds(rect);
                        dstRect = matrix.TransformBounds(dstRect);
                        this._zoomView.PanZoomToView(dstRect);
                    }

                };
                this.CommandBindings.Add(binding);
            }

            {
                CommandBinding binding = new CommandBinding(ZoomCommands.CenterToVisual);
                binding.Executed += (l, k) =>
                {
                    if (k.Parameter is UIElement element)
                    {
                        var matrix = new MatrixTransform(this._zoomView.Coord.CoordMatrixInv);
                        Rect rect = new Rect(element.RenderSize);
                        Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
                        GeneralTransform transform = element.TransformToVisual(this._zoomView);
                        Point dstCenter = transform.Transform(center);
                        dstCenter = matrix.Transform(dstCenter);
                        this._zoomView.PanCenterTo(dstCenter);
                    }

                };
                this.CommandBindings.Add(binding);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _zoomView = this.Template.FindName("PART_LayerView", this) as ZoomView;
            _thumbGrid = this.Template.FindName("PART_ThumbGrid", this) as Grid;
            _thumbGrid.MouseDown += (l, k) =>
            {
                var point = k.GetPosition(_thumbGrid);
                this._zoomView.PanCenterTo(point);
                this._start = point;
            };
            _thumbGrid.MouseUp += (l, k) =>
            {
                this._start = null;
            };

            _thumbGrid.MouseMove += (l, k) =>
            {
                if (this._start == null)
                    return;

                if (k.LeftButton == MouseButtonState.Pressed)
                {
                    Point pt1 = k.GetPosition(_thumbGrid);
                    Point pt2 = this._start.Value;
                    Vector vector = pt1 - pt2;
                    this._zoomView.Coord.Offset -= vector * this._zoomView.Coord.Scale;
                    this._start = pt1;
                }
                else
                {
                    this._start = null;
                }
            };


            _thumbGrid.MouseWheel += (l, k) =>
            {
                Point ptc = k.GetPosition(_thumbGrid);
                this._zoomView.PanCenterTo(ptc);
                this._zoomView.Zoom(ptc, k.Delta > 0);
            };
        }



        public bool UseTools
        {
            get { return (bool)GetValue(UseToolsProperty); }
            set { SetValue(UseToolsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseToolsProperty =
            DependencyProperty.Register("UseTools", typeof(bool), typeof(ZoomBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                ZoomBox control = d as ZoomBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));




        public bool UseMessage
        {
            get { return (bool)GetValue(UseMessageProperty); }
            set { SetValue(UseMessageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseMessageProperty =
            DependencyProperty.Register("UseMessage", typeof(bool), typeof(ZoomBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                ZoomBox control = d as ZoomBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public bool UseThumbView
        {
            get { return (bool)GetValue(UseThumbViewProperty); }
            set { SetValue(UseThumbViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseThumbViewProperty =
            DependencyProperty.Register("UseThumbView", typeof(bool), typeof(ZoomBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                ZoomBox control = d as ZoomBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));

    }


}
