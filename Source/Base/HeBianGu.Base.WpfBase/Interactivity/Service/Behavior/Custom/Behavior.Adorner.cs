// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
#if NET
using System.Runtime.Intrinsics.X86;
#endif 
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml.Linq;

namespace HeBianGu.Base.WpfBase
{
    public abstract class AdornerBehaviorBase : Behavior<FrameworkElement>
    {
        public Type AdornerType
        {
            get { return (Type)GetValue(AdornerTypeProperty); }
            set { SetValue(AdornerTypeProperty, value); }
        }

        public static readonly DependencyProperty AdornerTypeProperty =
            DependencyProperty.Register("AdornerType", typeof(Type), typeof(AdornerBehaviorBase), new PropertyMetadata(default(Type), (d, e) =>
            {
                AdornerBehaviorBase control = d as AdornerBehaviorBase;

                if (control == null) return;

                Type config = e.NewValue as Type;

            }));


        public Visual AdornerVisual
        {
            get { return (Visual)GetValue(AdornerVisualProperty); }
            set { SetValue(AdornerVisualProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdornerVisualProperty =
            DependencyProperty.Register("AdornerVisual", typeof(Visual), typeof(AdornerBehaviorBase), new FrameworkPropertyMetadata(default(Visual), (d, e) =>
            {
                AdornerBehaviorBase control = d as AdornerBehaviorBase;

                if (control == null) return;

                if (e.OldValue is Visual o)
                {

                }

                if (e.NewValue is Visual n)
                {

                }
            }));

        public bool IsUse
        {
            get { return (bool)GetValue(IsUseProperty); }
            set { SetValue(IsUseProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseProperty =
            DependencyProperty.Register("IsUse", typeof(bool), typeof(AdornerBehaviorBase), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                AdornerBehaviorBase control = d as AdornerBehaviorBase;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
            }));


        public bool IsHitTestVisible
        {
            get { return (bool)GetValue(IsHitTestVisibleProperty); }
            set { SetValue(IsHitTestVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsHitTestVisibleProperty =
            DependencyProperty.Register("IsHitTestVisible", typeof(bool), typeof(AdornerBehaviorBase), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                AdornerBehaviorBase control = d as AdornerBehaviorBase;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


    }

    public class LoadedAdornerBehavior : AdornerBehaviorBase
    {
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }

        void Refresh()
        {
            if (this.AdornerType == null) return;

            if (this.AdornerVisual == null)
                this.AdornerVisual = this.AssociatedObject;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AdornerVisual);
            if (layer == null) return;

            IEnumerable<Adorner> adorners = layer.GetAdorners(this.AdornerVisual as UIElement)?.Where(l => l.GetType() == this.AdornerType);

            if (adorners != null)
            {
                foreach (var item in adorners)
                {
                    layer.Remove(item);
                }
            }

            if (this.IsUse)
            {
                Adorner adorner = Activator.CreateInstance(this.AdornerType, this.AssociatedObject) as Adorner;
                if (adorner == null)
                    return;
                layer.Add(adorner);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }

    }

    [ContentProperty("Parameters")]
    [DefaultProperty("Parameters")]
    public class MouseOverAdornerBehavior : AdornerBehaviorBase
    {
        Adorner _adorner;
        public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

        protected override void OnAttached()
        {
            AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseLeave");
            this.ClearAdorner();
        }

        private void AssociatedObject_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseEnter");

            if (this.AdornerType == null)
                return;

            if (this.AdornerVisual == null)
                this.AdornerVisual = this.AssociatedObject;

            if (_adorner != null)
                return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
            if (layer == null)
                return;

            IEnumerable<Adorner> adorners = layer.GetAdorners(this.AssociatedObject)?.Where(l => l.GetType() == this.AdornerType);
            if (adorners != null)
            {
                foreach (var item in adorners)
                {
                    layer.Remove(item);
                }
            }
            if (this.IsUse)
            {
                Adorner adorner = Activator.CreateInstance(this.AdornerType, this.AdornerVisual) as Adorner;
                if (adorner == null)
                    return;
                adorner.MouseLeave -= AssociatedObject_MouseLeave;
                adorner.MouseLeave += AssociatedObject_MouseLeave;
                _adorner = adorner;
                adorner.IsHitTestVisible = false;
                layer.Add(adorner);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
            this.ClearAdorner();
        }

        void ClearAdorner()
        {
            if (_adorner == null)
                return;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AdornerVisual);
            if (layer == null)
                return;
            _adorner.MouseLeave -= AssociatedObject_MouseLeave;
            layer.Remove(_adorner);
            _adorner = null;
        }
    }

    public abstract class HitTestAdornerBehavior : AdornerBehaviorBase
    {
        public static Type GetHitTestAdornerType(DependencyObject obj)
        {
            return (Type)obj.GetValue(HitTestAdornerTypeProperty);
        }

        public static void SetHitTestAdornerType(DependencyObject obj, Type value)
        {
            obj.SetValue(HitTestAdornerTypeProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty HitTestAdornerTypeProperty =
            DependencyProperty.RegisterAttached("HitTestAdornerType", typeof(Type), typeof(HitTestAdornerBehavior), new PropertyMetadata(default(Type), OnHitTestAdornerTypeChanged));

        static public void OnHitTestAdornerTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            Type n = (Type)e.NewValue;

            Type o = (Type)e.OldValue;
        }


        protected UIElement _temp = null;
        //protected UIElement _visualHit = null;

        public static bool GetIsHitTest(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsHitTestProperty);
        }

        public static void SetIsHitTest(DependencyObject obj, bool value)
        {
            obj.SetValue(IsHitTestProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsHitTestProperty =
            DependencyProperty.RegisterAttached("IsHitTest", typeof(bool), typeof(HitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsHitTestChanged));

        static public void OnIsHitTestChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }

        protected virtual void Clear()
        {
            this._temp?.ClearAdorner(x => x.GetType() == this.AdornerType);
            if (this._temp != null)
                MouseOverHitTestAdornerBehavior.SetIsMouseOver(this._temp, false);
            if (this._temp.GetDataContext() is IGetDropAdorner drop)
            {
                drop.RemoveDropAdorner(this._temp);
            }
        }

        //HitTestResultBehavior HitTestCallBack(HitTestResult result)
        //{
        //    if (HitTestAdornerBehavior.GetIsHitTest(result.VisualHit))
        //    {
        //        return HitTestResultBehavior.Stop;
        //    }
        //    return HitTestResultBehavior.Continue;
        //}

        //HitTestFilterBehavior HitTestFilter(DependencyObject obj)
        //{
        //    Type type = obj.GetType();
        //    if (type.Name == this.GetType().Name)
        //        return HitTestFilterBehavior.ContinueSkipSelf;
        //    if (HitTestAdornerBehavior.GetIsHitTest(obj))
        //    {
        //        _visualHit = obj as UIElement;
        //    }

        //    return HitTestFilterBehavior.Continue;
        //}


        protected virtual void AddAdorner(UIElement elment)
        {
            if (_temp == elment)
                return;
            if (this.AdornerType == null)
                return;

            if (this.CheckAdorner(elment) == false)
                return;

            if (this.IsUse)
            {
                Adorner adorner = this.GetAdorner(elment);
                if (adorner == null)
                    return;
                adorner.IsHitTestVisible = this.IsHitTestVisible;

                System.Diagnostics.Debug.WriteLine("IsHitTestVisible:" + adorner.IsHitTestVisible);

                elment.AddAdorner(adorner);
            }
        }

        protected virtual bool CheckAdorner(UIElement elment)
        {
            var custom = GetHitTestAdornerType(elment);
            if (custom != null)
                return elment.GetAdorner(x => x.GetType() == custom) == null;
            return elment.GetAdorner(x => x.GetType() == this.AdornerType) == null;
        }

        protected virtual Adorner GetAdorner(UIElement elment)
        {
            var custom = GetHitTestAdornerType(elment);
            if (custom != null)
                return Activator.CreateInstance(custom, elment) as Adorner;
            return Activator.CreateInstance(this.AdornerType, elment) as Adorner;
        }

        protected override void OnDetaching()
        {
            this.Clear();
        }
    }

    public class MouseOverHitTestAdornerBehavior : HitTestAdornerBehavior
    {
        public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

        public static bool GetIsMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMouseOverProperty);
        }

        public static void SetIsMouseOver(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMouseOverProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsMouseOverProperty =
            DependencyProperty.RegisterAttached("IsMouseOver", typeof(bool), typeof(MouseOverHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsMouseOverChanged));

        static public void OnIsMouseOverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseMove += AssociatedObject_MouseMove;
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.AdornerType == null)
                return;
            if (this.AdornerVisual == null)
                this.AdornerVisual = this.AssociatedObject;
            Point point = e.GetPosition(this.AssociatedObject);
            var visualHit = this.AssociatedObject.HitTest<UIElement>(point, x => MouseOverHitTestAdornerBehavior.GetIsHitTest(x));
            if (visualHit == null)
            {
                this.Clear();
            }
            else
            {
                if (visualHit != _temp)
                    this.Clear();
                this.AddAdorner(visualHit);
                MouseOverHitTestAdornerBehavior.SetIsMouseOver(visualHit, true);
            }
            _temp = visualHit;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.Clear();
        }

        protected override void Clear()
        {
            if (this._temp != null)
                MouseOverHitTestAdornerBehavior.SetIsMouseOver(this._temp, false);
            base.Clear();
        }
    }

    public class SelectedHitTestAdornerBehavior : HitTestAdornerBehavior
    {
        public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

        public static bool GetIsSelected(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedProperty);
        }

        public static void SetIsSelected(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(SelectedHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsSelectedChanged));

        static public void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }



        protected override void OnAttached()
        {
            AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown), true);
        }
        protected override void OnDetaching()
        {
            AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown));
            this.Clear();
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.AdornerType == null)
                return;
            if (this.AdornerVisual == null)
                this.AdornerVisual = this.AssociatedObject;
            Point point = e.GetPosition(this.AssociatedObject);
            var visualHit = this.AssociatedObject.HitTest<UIElement>(point, x => SelectedHitTestAdornerBehavior.GetIsHitTest(x));
            if (visualHit == null)
            {
                this.Clear();
                if (_temp != null)
                {
                    SelectedHitTestAdornerBehavior.SetIsSelected(_temp, false);
                    Cattach.SetIsSelected(_temp, false);
                }
            }
            else
            {
                if (visualHit != _temp)
                    this.Clear();
                this.AddAdorner(visualHit);
                SelectedHitTestAdornerBehavior.SetIsSelected(visualHit, true);
                Cattach.SetIsSelected(visualHit, true);
            }
            _temp = visualHit;
        }
    }

    public class DragOverHitTestAdornerBehavior : HitTestAdornerBehavior
    {
        public Type AdornerDropErrorType
        {
            get { return (Type)GetValue(AdornerDropErrorTypeProperty); }
            set { SetValue(AdornerDropErrorTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AdornerDropErrorTypeProperty =
            DependencyProperty.Register("AdornerDropErrorType", typeof(Type), typeof(DragOverHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(Type), (d, e) =>
            {
                DragOverHitTestAdornerBehavior control = d as DragOverHitTestAdornerBehavior;

                if (control == null) return;

                if (e.OldValue is Type o)
                {

                }

                if (e.NewValue is Type n)
                {

                }

            }));

        protected override void OnAttached()
        {
            AssociatedObject.DragOver += AssociatedObject_DragOver;
            AssociatedObject.Drop += AssociatedObject_Drop;
            //AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            //AssociatedObject.DragLeave += AssociatedObject_DragLeave;
        }


        public static bool GetIsPreviewing(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPreviewingProperty);
        }

        public static void SetIsPreviewing(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPreviewingProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsPreviewingProperty =
            DependencyProperty.RegisterAttached("IsPreviewing", typeof(bool), typeof(DragOverHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsPreviewingChanged));

        static public void OnIsPreviewingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d as DependencyObject;

            bool n = (bool)e.NewValue;

            bool o = (bool)e.OldValue;
        }


        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            //this.Clear();
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {

        }

        protected virtual void Drop(object sender, DragEventArgs e)
        {
            if (_temp.GetContent() is IHitTestElementDrop drag)
            {
                if (drag.CanDrop(_temp, e))
                    drag.Drop(_temp, e);
            }
            else if (this.AssociatedObject is IHitTestElementDrop drop)
            {
                if (drop.CanDrop(_temp, e))
                    drop.Drop(_temp, e);
            }
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            this.Drop(sender, e);
            this.Clear();
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (this.AdornerType == null)
                return;
            if (this.AdornerVisual == null)
                this.AdornerVisual = this.AssociatedObject;

            Point point = e.GetPosition(this.AssociatedObject);
            var visualHit = this.AssociatedObject.HitTest<UIElement>(point, x =>
            {
                if (DragOverHitTestAdornerBehavior.GetIsHitTest(x) == false)
                    return false;
                if (_temp.GetContent() is IHitTestElementDrop drop)
                    return drop.IsHitTest(x, e);
                return true;
            });
            if (visualHit == null)
            {
                this.Clear();
                if (this.AssociatedObject != _temp)
                {
                    this.DragEnter(this.AssociatedObject, e);
                    this.DragLeave(_temp, e);
                }
                _temp = this.AssociatedObject;
            }
            else
            {
                if (visualHit != _temp)
                {
                    this.Clear();
                    this.DragLeave(_temp, e);
                    this.DragEnter(visualHit, e);
                }
                this.AddAdorner(visualHit);
                _temp = visualHit;
            }

            if (_temp.GetContent() is IHitTestElementDrag drag)
            {
                drag.DragOver(_temp, e);
            }
        }

        protected virtual void DragEnter(UIElement element, DragEventArgs e)
        {
            if (element.GetContent() is IHitTestElementDrag newDrag)
            {
                newDrag.DragEnter(element, e);
            }
        }

        protected virtual void DragLeave(UIElement element, DragEventArgs e)
        {
            if (_temp.GetContent() is IHitTestElementDrag oldDrag)
            {
                oldDrag.DragLeave(_temp, e);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DragOver -= AssociatedObject_DragOver;
            AssociatedObject.Drop -= AssociatedObject_Drop;
            //AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
            //AssociatedObject.DragLeave -= AssociatedObject_DragLeave;
            this.Clear();
        }

        protected override bool CheckAdorner(UIElement elment)
        {
            return elment.GetAdorner(x => x.GetType() == this.AdornerType) == null;
        }

        protected override Adorner GetAdorner(UIElement elment)
        {
            Adorner adorner = Activator.CreateInstance(this.AdornerType, elment) as Adorner;
            var data = elment.GetDataContext();
            if (data is IHitTestElementDrag drag)
            {
                if (drag.CanDrop(elment, null))
                    adorner = drag.GetDragAdorner(elment);
                else
                    adorner = Activator.CreateInstance(this.AdornerDropErrorType, elment) as Adorner;
            }

            if (data is IGetDropAdorner gdrag)
            {
                return gdrag.GetDropAdorner(elment);
            }
            return adorner;
        }
    }

    public interface IHitTestElementDrag : IHitTestElementDrop, IGetDragAdorner
    {
        void DragEnter(UIElement element, DragEventArgs e);
        void DragLeave(UIElement element, DragEventArgs e);
        void DragOver(UIElement element, DragEventArgs e);
    }

    public interface IGetDropAdorner
    {
        Adorner GetDropAdorner(UIElement element);
        void RemoveDropAdorner(UIElement element);
    }

    public interface IGetDragAdorner
    {
        Adorner GetDragAdorner(UIElement element);
        void RemoveDragAdorner(UIElement element);
    }

    public interface IHitTestElementDrop : IGetDropAdorner
    {
        bool CanDrop(UIElement element, DragEventArgs e);
        void Drop(UIElement element, DragEventArgs e);
        bool IsHitTest(UIElement element, DragEventArgs e);
    }
}



