// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Control.Adorner
{
    /// <summary> 具有控件跟随效果的拖动样式 </summary>
    public class DragAdornerBehavior : Behavior<UIElement>
    {
        private Point startPoint;
        private IDragAdorner adorner;

        /// <summary> 拖拽时样式透明度 </summary>
        public double Opacity
        {
            get { return (double)GetValue(OpacityProperty); }
            set { SetValue(OpacityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpacityProperty =
            DependencyProperty.Register("Opacity", typeof(double), typeof(DragAdornerBehavior), new PropertyMetadata(0.5, (d, e) =>
             {
                 DragAdornerBehavior control = d as DragAdornerBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        public Type AncestorType
        {
            get { return (Type)GetValue(AncestorTypeProperty); }
            set { SetValue(AncestorTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AncestorTypeProperty =
            DependencyProperty.Register("AncestorType", typeof(Type), typeof(DragAdornerBehavior), new PropertyMetadata(typeof(ScrollViewer), (d, e) =>
             {
                 DragAdornerBehavior control = d as DragAdornerBehavior;

                 if (control == null) return;

                 Type config = e.NewValue as Type;
             }));

        /// <summary> 判断可否放置的分组 </summary>
        public string DragGroup
        {
            get { return (string)GetValue(DragGroupProperty); }
            set { SetValue(DragGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragGroupProperty =
            DependencyProperty.Register("DragGroup", typeof(string), typeof(DragAdornerBehavior), new PropertyMetadata("DragGroup", (d, e) =>
            {
                DragAdornerBehavior control = d as DragAdornerBehavior;

                if (control == null) return;

                string config = e.NewValue as string;

            }));


        public DragDropEffects DragDropEffects
        {
            get { return (DragDropEffects)GetValue(DragDropEffectsProperty); }
            set { SetValue(DragDropEffectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragDropEffectsProperty =
            DependencyProperty.Register("DragDropEffects", typeof(DragDropEffects), typeof(DragAdornerBehavior), new PropertyMetadata(DragDropEffects.Copy, (d, e) =>
             {
                 DragAdornerBehavior control = d as DragAdornerBehavior;

                 if (control == null) return;

                 //DragDropEffects config = e.NewValue as DragDropEffects;

             }));

        public RoutingStrategy RoutingStrategy { get; set; } = RoutingStrategy.Bubble;

        public DrapAdornerMode DropAdornerMode { get; set; }

        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;
            if (RoutingStrategy == RoutingStrategy.Bubble)
            {
                AssociatedObject.PreviewMouseDown += AssociatedObject_MouseDown;
                AssociatedObject.PreviewMouseUp += AssociatedObject_MouseUp;
                AssociatedObject.PreviewMouseMove += AssociatedObject_MouseMove;
            }
            else
            {
                AssociatedObject.MouseDown += AssociatedObject_MouseDown;
                AssociatedObject.MouseUp += AssociatedObject_MouseUp;
                AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            }

            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
            AssociatedObject.GiveFeedback += AssociatedObject_GiveFeedback;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.AllowDrop = false;

            AssociatedObject.PreviewMouseDown -= AssociatedObject_MouseDown;
            AssociatedObject.PreviewMouseUp -= AssociatedObject_MouseUp;
            AssociatedObject.PreviewMouseMove -= AssociatedObject_MouseMove;

            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            AssociatedObject.MouseUp -= AssociatedObject_MouseUp;
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;

            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;

            AssociatedObject.GiveFeedback -= AssociatedObject_GiveFeedback;
        }

        private void AssociatedObject_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (adorner != null)
            {
                Visual lbl = sender as Visual;
                var source = PresentationSource.FromVisual(lbl);
                if (source == null)
                {
                    Point pos = Application.Current.MainWindow.PointFromScreen(GetMousePosition());
                    adorner.UpdatePosition(pos);
                }
                else
                {
                    Point pos = lbl.PointFromScreen(GetMousePosition());
                    adorner.UpdatePosition(pos);
                }
            }
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_isCapture)
                return;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                UIElement source = sender as UIElement;
                UIElement element = sender as UIElement;
                Point current = e.GetPosition(element);
                Vector diff = startPoint - current;
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    UIElement control = this.AncestorType == null ? element : element.GetParent(this.AncestorType) as UIElement;
                    control = control == null ? element : control;
                    AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);
                    IEnumerable<System.Windows.Documents.Adorner> find = layer.GetAdorners(element)?.Where(l => l == adorner);

                    if (find != null && find.Count() > 0)
                    {
                        layer.Remove(adorner as System.Windows.Documents.Adorner);
                    }
                    adorner = this.CreateDragAdorner(element, e.GetPosition(element));
                    this.BeforeDoDragDrop(element);
                    layer.Add(adorner as System.Windows.Documents.Adorner);
                    this.DoDragDrop();
                    layer.Remove(adorner as System.Windows.Documents.Adorner);
                }
            }
        }

        protected virtual void BeforeDoDragDrop(UIElement element)
        {
            if (this.DragDropEffects == DragDropEffects.Move)
            {
                if (element.GetContent() is IDoDragDrop doDragDrop)
                {
                    doDragDrop.DoDragDrop(element, (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control ? DragDropEffects.Copy : DragDropEffects.Move);
                }
            }
        }

        protected virtual IDragAdorner CreateDragAdorner(UIElement element, Point point)
        {
            return new DragAdorner(element, point)
            {
                IsHitTestVisible = false,
                Opacity = this.Opacity,
                DropAdornerMode = this.DropAdornerMode
            };
        }

        /// <summary>
        /// 用于子类重写拖拽时放入的数据
        /// </summary>
        /// <returns></returns>
        protected virtual void DoDragDrop()
        {
            DataObject dragData = new DataObject(this.DragGroup, adorner);
            DragDrop.DoDragDrop(this.AssociatedObject, dragData, DragDropEffects);
        }

        private bool _isCapture;

        private void AssociatedObject_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.Arrow;
            _isCapture = false;
        }



        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.Arrow;
            _isCapture = false;
        }

        private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(sender as UIElement);
            _isCapture = true;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        public static bool GetIsUse(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsUseProperty);
        }

        public static void SetIsUse(DependencyObject obj, bool value)
        {
            obj.SetValue(IsUseProperty, value);
        }

        public static readonly DependencyProperty IsUseProperty =
            DependencyProperty.RegisterAttached("IsUse", typeof(bool), typeof(DragAdornerBehavior), new PropertyMetadata(default(bool), OnIsUseChanged));

        static public void OnIsUseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;
            bool n = (bool)e.NewValue;
            bool o = (bool)e.OldValue;
            RefreshUse(d, n);
        }

        static void RefreshUse(DependencyObject d, bool use)
        {
            var bevaviors = Interaction.GetBehaviors(d);
            var finds = bevaviors.Where(x => x.GetType() == typeof(DragAdornerBehavior));
            foreach (var item in finds)
            {
                bevaviors.Remove(item);
            }
            if (use)
            {
                bevaviors.Add(new DragAdornerBehavior());
            }
        }
    }

    public class DragDataTempalteAdornerBehaviour : DragAdornerBehavior
    {
        protected override IDragAdorner CreateDragAdorner(UIElement element, Point point)
        {
            return new DragDataTemplateAdorner(element, point)
            {
                IsHitTestVisible = false,
                Opacity = this.Opacity,
                DropAdornerMode = this.DropAdornerMode
            };
        }
    }

    public class DragControlTempalteAdornerBehaviour : DragAdornerBehavior
    {
        protected override IDragAdorner CreateDragAdorner(UIElement element, Point point)
        {
            return new DragControlTemplateAdorner(element, point)
            {
                IsHitTestVisible = false,
                Opacity = this.Opacity,
                DropAdornerMode = this.DropAdornerMode
            };
        }
    }

    public class DragGridAdorner : System.Windows.Documents.Adorner
    {
        public DragGridAdorner(UIElement adornedElement) : base(adornedElement)
        {
            vbrush = new VisualBrush(AdornedElement);
            vbrush.Opacity = .5;
        }

        protected override void OnRender(DrawingContext dc)
        {

            List<double> x = new List<double>();
            List<double> y = new List<double>();

            for (int i = 0; i < 10; i++)
            {
                x.Add(i * i * i);
                y.Add(i * i * i);
            }

            GuidelineSet guideline = new GuidelineSet(x.ToArray(), y.ToArray());

            //dc.DrawRectangle(vbrush, null, new Rect(p, this.RenderSize));

            //dc.PushGuidelineSet(guideline);
        }



        private Brush vbrush;

        //private Point location;

        /// <summary> 相对于拖动控件的拖动位置 </summary>
        public Point Offset { get; set; }

        public DrapAdornerMode DropAdornerMode { get; set; }
    }


    public interface IDoDragDrop
    {
        void DoDragDrop(UIElement element, DragDropEffects dragDropEffects);
    }
}



