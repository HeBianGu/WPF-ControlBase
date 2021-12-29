//using System;
//using System.Collections;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;

//namespace HeBianGu.Base.WpfBase
//{
//    public class ListDragBlendBehavior : Behavior<ListBox>
//    {
//        private ListBox _dragSource;
//        private object _dragData;
//        private Point _dragStart;

//        protected override void OnAttached()
//        {
//            AssociatedObject.PreviewMouseLeftButtonDown += ListBoxOnPreviewMouseLeftButtonDown;
//            AssociatedObject.PreviewMouseMove += ListBoxOnPreviewMouseMove;
//            AssociatedObject.PreviewMouseLeftButtonUp += ListBoxOnPreviewMouseLeftButtonUp;
//        }


//        /// <summary> 判断可否放置的分组 </summary>
//        public string DragGroup
//        {
//            get { return (string)GetValue(DragGroupProperty); }
//            set { SetValue(DragGroupProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty DragGroupProperty =
//            DependencyProperty.Register("DragGroup", typeof(string), typeof(ListDragBlendBehavior), new PropertyMetadata("DragGroup", (d, e) =>
//             {
//                 ListDragBlendBehavior control = d as ListDragBlendBehavior;

//                 if (control == null) return;

//                 string config = e.NewValue as string;

//             }));


//        public DragDropEffects DragDropEffects
//        {
//            get { return (DragDropEffects)GetValue(DragDropEffectsProperty); }
//            set { SetValue(DragDropEffectsProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty DragDropEffectsProperty =
//            DependencyProperty.Register("DragDropEffects", typeof(DragDropEffects), typeof(ListDragBlendBehavior), new PropertyMetadata(DragDropEffects.Move, (d, e) =>
//             {
//                 ListDragBlendBehavior control = d as ListDragBlendBehavior;

//                 if (control == null) return;

//                 //DragDropEffects config = e.NewValue as DragDropEffects;

//             }));



//        private void ListBoxOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
//        {
//            _dragStart = mouseButtonEventArgs.GetPosition(null);
//            _dragSource = sender as ListBox;
//            if (_dragSource == null) return;
//            var i = IndexUnderDragCursor;
//            _dragData = i != -1 ? _dragSource.Items.GetItemAt(i) : null;
//        }
//        DragAdorner adorner;

//        void ListBoxOnPreviewMouseMove(object sender, MouseEventArgs e)
//        {
//            if (_dragData == null) return;

//            var currentPosition = e.GetPosition(null);

//            var difference = _dragStart - currentPosition;

//            if ((MouseButtonState.Pressed == e.LeftButton) &&
//                ((Math.Abs(difference.X) > SystemParameters.MinimumHorizontalDragDistance) ||
//                 (Math.Abs(difference.Y) > SystemParameters.MinimumVerticalDragDistance)))
//            {
//             var element =   this.AssociatedObject.ItemContainerGenerator.ContainerFromItem(_dragData) as UIElement;

//                var data = new DataObject(this.DragGroup, _dragData); 

//                adorner = new DragAdorner(element, e.GetPosition(element)) { IsHitTestVisible = false, Opacity = 0.5 };

//                AdornerLayer.GetAdornerLayer(element).Add(adorner);

//                var dragData = new DataObject(this); 

//                var result = DragDrop.DoDragDrop(_dragSource, data, DragDropEffects);

//                AdornerLayer.GetAdornerLayer(element).Remove(adorner);

//                //  Do ：移动成功清理数据
//                if (result==DragDropEffects.Move)
//                {
//                    (_dragSource.ItemsSource as IList).Remove(_dragData);
//                }
//                _dragData = null;
//            }
//        }

//        private void ListBoxOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
//        {
//            _dragData = null;
//        }

//        int IndexUnderDragCursor
//        {
//            get
//            {
//                var index = -1;
//                for (var i = 0; i < AssociatedObject.Items.Count; ++i)
//                {
//                    var item = AssociatedObject.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

//                    if (item != null && item.IsMouseOver)
//                    {
//                        index = i;
//                        break;
//                    }
//                }
//                return index;
//            }
//        }
//    }
//}
