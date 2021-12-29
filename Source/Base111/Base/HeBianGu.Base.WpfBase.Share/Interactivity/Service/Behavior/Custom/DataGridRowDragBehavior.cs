using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
 

       //<DataGrid Grid.Row="1" Grid.Column= "2" Margin= "10,30,10,10" ItemsSource= "{Binding Products}" AutoGenerateColumns= "True" CanUserAddRows= "False"
       //           IsReadOnly= "True" CanUserDeleteRows= "False" CanUserResizeRows= "False" CanUserSortColumns= "True" >
       //     < Interaction:Interaction.Behaviors>
       //         <julmar:DataGridDragRowBehavior />
       //     </Interaction:Interaction.Behaviors>
       // </DataGrid>

namespace HeBianGu.Base.WpfBase
{
    /// <summary> DataGrid数据行支持拖动 </summary>
    public class DataGridDragRowBehavior : Behavior<DataGrid>
    {
        private object _target;

        public event EventHandler<DataGridRowDropEventArgs> DropStarted;


        public event EventHandler<DataGridRowDropEventArgs> DropFinished;

        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;

            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.DragEnter += OnCheckDropTarget;
            AssociatedObject.DragOver += OnCheckDropTarget;
            AssociatedObject.DragLeave += OnCheckDropTarget;
            AssociatedObject.Drop += OnDrop;

            base.OnAttached();
        }

        void OnDrop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            UIElement source = e.OriginalSource as UIElement;
            if (source != null)
            {
                DataGridRow row = source.GetParent<DataGridRow>();
                if (row != null)
                {
                    _target = row.Item;
                    if (_target != null)
                        e.Effects = DragDropEffects.Move;
                }
            }

            e.Handled = true;
        }

        static void OnCheckDropTarget(object sender, DragEventArgs e)
        {
            UIElement source = e.OriginalSource as UIElement;
            DataGridRow row = (source != null) ? source.GetParent<DataGridRow>() : null;
            if (row == null || row.Item == null)
                e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            IList collection = (IList)AssociatedObject.ItemsSource ?? AssociatedObject.Items;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                UIElement source = e.OriginalSource as UIElement;
                DataGridRow row = (source != null) ? source.GetParent<DataGridRow>() : null;
                if ((row != null) && row.IsSelected)
                {
                    DragDropEffects finalDropEffect = DragDrop.DoDragDrop(row, row.Item, DragDropEffects.Move);
                    if ((finalDropEffect == DragDropEffects.Move) && (_target != null))
                    {
                        int oldIndex = collection.IndexOf(row.Item);
                        int newIndex = collection.IndexOf(_target);
                        if (newIndex == -1 && _target == CollectionView.NewItemPlaceholder)
                            newIndex = collection.Count - 1;

                        if (oldIndex != newIndex && oldIndex >= 0 && newIndex >= 0)
                        {
                            var dropStarted = DropStarted;
                            if (dropStarted != null)
                                dropStarted.Invoke(this, new DataGridRowDropEventArgs(row.Item, oldIndex, newIndex));

                            Type type = collection.GetType();
                            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                            {
                                var dCollection = collection;

                                //dCollection.Move(oldIndex, newIndex); 

                            }
                            else
                            {
                                collection.RemoveAt(oldIndex);
                                collection.Insert(newIndex, row.Item);
                            }

                            var dropFinished = DropFinished;
                            if (dropFinished != null)
                                dropFinished.Invoke(this, new DataGridRowDropEventArgs(row.Item, oldIndex, newIndex));
                        }
                        _target = null;
                    }
                }
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.DragEnter -= OnCheckDropTarget;
            AssociatedObject.DragOver -= OnCheckDropTarget;
            AssociatedObject.DragLeave -= OnCheckDropTarget;
            AssociatedObject.Drop -= OnDrop;

            base.OnDetaching();
        }
    }

    public class DataGridRowDropEventArgs : EventArgs
    {
        public object Item { get; private set; }

        public int OldIndex { get; private set; }

        public int NewIndex { get; private set; }

        public DataGridRowDropEventArgs(object item, int oldIndex, int newIndex)
        {
            Item = item;
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }
    }

}
