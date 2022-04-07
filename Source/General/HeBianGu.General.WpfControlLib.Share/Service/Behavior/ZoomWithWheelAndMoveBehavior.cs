// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.General.WpfControlLib
{
    ///// <summary> Zoom带有鼠标移动平移和滚轮定点放大效果 </summary>
    //public class ZoomWithWheelAndMoveBehavior : Behavior<FrameworkElement>
    //{
    //    //  Message：外部需要嵌套Grid
    //    Grid parent;

    //    //  Message：Zoom控件
    //    ZoomableCanvas zoomable;

    //    /// <summary> 是否在父容器中也使用平移和缩放 </summary>
    //    public bool UseInParent
    //    {
    //        get { return (bool)GetValue(UseInParentProperty); }
    //        set { SetValue(UseInParentProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty UseInParentProperty =
    //        DependencyProperty.Register("UseInParent", typeof(bool), typeof(ZoomWithWheelAndMoveBehavior), new PropertyMetadata(default(bool), (d, e) =>
    //        {
    //            ZoomWithWheelAndMoveBehavior control = d as ZoomWithWheelAndMoveBehavior;

    //            if (control == null) return;

    //            control.RefreshEvent();

    //        }));


    //    public bool IsCenterInZoom
    //    {
    //        get { return (bool)GetValue(IsCenterInZoomProperty); }
    //        set { SetValue(IsCenterInZoomProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty IsCenterInZoomProperty =
    //        DependencyProperty.Register("IsCenterInZoom", typeof(bool), typeof(ZoomWithWheelAndMoveBehavior), new PropertyMetadata(default(bool), (d, e) =>
    //        {
    //            ZoomWithWheelAndMoveBehavior control = d as ZoomWithWheelAndMoveBehavior;

    //            if (control == null) return;

    //            control.RefreshLocation();

    //        }));


    //    public bool IsReturn
    //    {
    //        get { return (bool)GetValue(IsReturnProperty); }
    //        set { SetValue(IsReturnProperty, value); }
    //    }

    //    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty IsReturnProperty =
    //        DependencyProperty.Register("IsReturn", typeof(bool), typeof(ZoomWithWheelAndMoveBehavior), new PropertyMetadata(default(bool), (d, e) =>
    //         {
    //             ZoomWithWheelAndMoveBehavior control = d as ZoomWithWheelAndMoveBehavior;

    //             if (control == null) return;

    //             control.RefreshReturn();

    //         }));


    //    void RefreshReturn()
    //    {
    //        if (zoomable == null) return;

    //        zoomable.Scale=1;
    //        zoomable.Offset =new Point(0,0);
    //    }

    //    /// <summary> 更新UseInParent刷新事件 </summary>
    //    void RefreshEvent()
    //    {
    //        if (this.parent == null || this.zoomable == null) return;

    //        if (!UseInParent)
    //        {
    //            parent.MouseWheel -= OnMouseWheel;
    //            parent.MouseMove -= OnMouseMove;
    //        }
    //        else
    //        {
    //            zoomable.MouseWheel -= OnMouseWheel;
    //            zoomable.MouseMove -= OnMouseMove;
    //        }

    //        if (UseInParent)
    //        {
    //            parent.MouseWheel += OnMouseWheel;
    //            parent.MouseMove += OnMouseMove;
    //        }
    //        else
    //        {
    //            zoomable.MouseWheel += OnMouseWheel;
    //            zoomable.MouseMove += OnMouseMove;
    //        }
    //    }

    //    void RefreshLocation()
    //    {
    //        if (this.zoomable == null) return;
    //        if (this.IsCenterInZoom)
    //        {
    //            var width = this.zoomable.ActualWidth - this.AssociatedObject.Width;

    //            var height = this.zoomable.ActualHeight - this.AssociatedObject.Height;

    //            Canvas.SetTop(this.AssociatedObject, height / 2);

    //            Canvas.SetLeft(this.AssociatedObject, width / 2);
    //        }
    //        else
    //        {
    //            Canvas.SetTop(this.AssociatedObject, 0);

    //            Canvas.SetLeft(this.AssociatedObject, 0);
    //        }
    //    }


    //    protected override void OnAttached()
    //    {
    //        parent = AssociatedObject.GetParent<Grid>();

    //        parent.Children.Remove(AssociatedObject);

    //        zoomable = new ZoomableCanvas();

    //        zoomable.Children.Add(AssociatedObject);

    //        zoomable.Loaded += Zoomable_Loaded;

    //        parent.Children.Add(zoomable);

    //        if (UseInParent)
    //        {
    //            parent.MouseWheel += OnMouseWheel;
    //            parent.MouseMove += OnMouseMove;
    //        }
    //        else
    //        {
    //            zoomable.MouseWheel += OnMouseWheel;
    //            zoomable.MouseMove += OnMouseMove;
    //        }
    //    }

    //    private void Zoomable_Loaded(object sender, RoutedEventArgs e)
    //    {
    //        this.RefreshLocation();
    //    }

    //    protected override void OnDetaching()
    //    {
    //        if (UseInParent)
    //        {
    //            parent.MouseWheel -= OnMouseWheel;
    //            parent.MouseMove -= OnMouseMove;
    //        }
    //        else
    //        {
    //            zoomable.MouseWheel -= OnMouseWheel;
    //            zoomable.MouseMove -= OnMouseMove;
    //        }

    //        zoomable.Loaded -= Zoomable_Loaded;
    //    }

    //    private void OnMouseWheel(object sender, MouseWheelEventArgs e)
    //    {

    //        var x = Math.Pow(2, e.Delta / 3.0 / Mouse.MouseWheelDeltaForOneLine);
    //        zoomable.Scale *= x;

    //        // Adjust the offset to make the point under the mouse stay still.
    //        var position = (Vector)e.GetPosition(parent);
    //        zoomable.Offset = (Point)((Vector)(zoomable.Offset + position) * x - position);

    //        e.Handled = true;
    //    }

    //    private Point LastMousePosition;

    //    private void OnMouseMove(object sender, MouseEventArgs e)
    //    {
    //        var position = e.GetPosition(this.parent);

    //        if (e.LeftButton == MouseButtonState.Pressed)
    //        {
    //            this.zoomable.Offset -= position - LastMousePosition;
    //        }
    //        LastMousePosition = position;
    //    }
    //}
}
