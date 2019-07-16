using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media; 

namespace HeBianGu.Base.WpfBase
{
    class ListDragBlendBehavior : Behavior<ListBox>
    {
        private ListBox _dragSource;
        private object _dragData;
        private Point _dragStart;

        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseLeftButtonDown += ListBoxOnPreviewMouseLeftButtonDown;
            AssociatedObject.PreviewMouseMove += ListBoxOnPreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonUp += ListBoxOnPreviewMouseLeftButtonUp;
        }

        private void ListBoxOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _dragStart = mouseButtonEventArgs.GetPosition(null);
            _dragSource = sender as ListBox;
            if (_dragSource == null) return;
            var i = IndexUnderDragCursor;
            _dragData = i != -1 ? _dragSource.Items.GetItemAt(i) : null;
        }

        void ListBoxOnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragData == null) return;

            var currentPosition = e.GetPosition(null);
            var difference = _dragStart - currentPosition;

            if ((MouseButtonState.Pressed == e.LeftButton) &&
                ((Math.Abs(difference.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                 (Math.Abs(difference.Y) > SystemParameters.MinimumVerticalDragDistance)))
            {
                var data = new DataObject("Custom", _dragData);
                DragDrop.DoDragDrop(_dragSource, data, DragDropEffects.Copy);

                _dragData = null;
            }
        }

        private void ListBoxOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            _dragData = null;
        }

        int IndexUnderDragCursor
        {
            get
            {
                var index = -1;
                for (var i = 0; i < AssociatedObject.Items.Count; ++i)
                {
                    var item = AssociatedObject.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;

                    if (item != null && item.IsMouseOver)
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }
        }
    }
}
