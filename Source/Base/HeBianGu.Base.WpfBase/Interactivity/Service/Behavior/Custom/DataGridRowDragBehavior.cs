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
//    /// <summary>
//    /// This class enables a DataGrid to drag/drop to reorder rows.
//    /// </summary>
//    [Obsolete("Prefer to ItemsControlDragDropBehavior instead of DataGridDragRowBehavior.")]
//    public class DataGridDragRowBehavior : Behavior<DataGrid>
//    {
//        /// <summary>
//        /// Row drag target
//        /// </summary>
//        private object _target;

//        ///<summary>
//        /// This is invoked when the drop is started (prior to moving the item)
//        ///</summary>
//        public event EventHandler<DataGridRowDropEventArgs> DropStarted;

//        /// <summary>
//        /// This is invoked after the item has been moved.
//        /// </summary>
//        public event EventHandler<DataGridRowDropEventArgs> DropFinished;

//        /// <summary>
//        /// Called after the behavior is attached to an AssociatedObject.
//        /// </summary>
//        /// <remarks>
//        /// Override this to hook up functionality to the AssociatedObject.
//        /// </remarks>
//        protected override void OnAttached()
//        {
//            AssociatedObject.AllowDrop = true;

//            AssociatedObject.MouseMove += OnMouseMove;
//            AssociatedObject.DragEnter += OnCheckDropTarget;
//            AssociatedObject.DragOver += OnCheckDropTarget;
//            AssociatedObject.DragLeave += OnCheckDropTarget;
//            AssociatedObject.Drop += OnDrop;

//            base.OnAttached();
//        }

//        /// <summary>
//        /// This is used to drop the target
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        void OnDrop(object sender, DragEventArgs e)
//        {
//            // Default to no drop
//            e.Effects = DragDropEffects.None;

//            // Locate the item being dropped.
//            UIElement source = e.OriginalSource as UIElement;
//            if (source != null)
//            {
//                DataGridRow row = source.GetParent<DataGridRow>();
//                if (row != null)
//                {
//                    // This is the item we are now over - the drop target position
//                    // If there is an item, then set the effect to MOVE.
//                    _target = row.Item;
//                    if (_target != null)
//                        e.Effects = DragDropEffects.Move;
//                }
//            }

//            e.Handled = true;
//        }

//        /// <summary>
//        /// This is used to verify the location of the drop.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        static void OnCheckDropTarget(object sender, DragEventArgs e)
//        {
//            UIElement source = e.OriginalSource as UIElement;
//            DataGridRow row = (source != null) ? source.GetParent<DataGridRow>() : null;
//            if (row == null || row.Item == null)
//                e.Effects = DragDropEffects.None;
//            e.Handled = true;
//        }

//        /// <summary>
//        /// This handler monitors the mouse movement and initiates the drag/drop operation.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        void OnMouseMove(object sender, MouseEventArgs e)
//        {
//            IList collection = (IList)AssociatedObject.ItemsSource ?? AssociatedObject.Items;

//            // If the mouse is moving with the mouse button down, then initiate the drag operation.
//            if (e.LeftButton == MouseButtonState.Pressed)
//            {
//                // Find the row and only drag it if it is already selected.
//                UIElement source = e.OriginalSource as UIElement;
//                DataGridRow row = (source != null) ? source.GetParent<DataGridRow>() : null;
//                if ((row != null) && row.IsSelected)
//                {
//                    // Perform the drag operation
//                    DragDropEffects finalDropEffect = DragDrop.DoDragDrop(row, row.Item, DragDropEffects.Move);
//                    if ((finalDropEffect == DragDropEffects.Move) && (_target != null))
//                    {
//                        // A Move drop was accepted
//                        // Determine the index of the item being dragged and the drop
//                        // location. If they are difference, then move the selected
//                        // item to the new location.
//                        int oldIndex = collection.IndexOf(row.Item);
//                        int newIndex = collection.IndexOf(_target);
//                        if (newIndex == -1 && _target == CollectionView.NewItemPlaceholder)
//                            newIndex = collection.Count - 1;

//                        if (oldIndex != newIndex && oldIndex >= 0 && newIndex >= 0)
//                        {
//                            var dropStarted = DropStarted;
//                            if (dropStarted != null)
//                                dropStarted.Invoke(this, new DataGridRowDropEventArgs(row.Item, oldIndex, newIndex));

//                            Type type = collection.GetType();
//                            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
//                            {
//#if !WPF_TOOLKIT
//                                dynamic dCollection = collection;
//                                dCollection.Move(oldIndex, newIndex);
//#else
//                                var methodInfo = type.GetMethod("Move");
//                                methodInfo.Invoke(collection, new object[] {oldIndex, newIndex});
//#endif
//                            }
//                            else
//                            {
//                                collection.RemoveAt(oldIndex);
//                                collection.Insert(newIndex, row.Item);
//                            }

//                            var dropFinished = DropFinished;
//                            if (dropFinished != null)
//                                dropFinished.Invoke(this, new DataGridRowDropEventArgs(row.Item, oldIndex, newIndex));
//                        }
//                        _target = null;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
//        /// </summary>
//        /// <remarks>
//        /// Override this to unhook functionality from the AssociatedObject.
//        /// </remarks>
//        protected override void OnDetaching()
//        {
//            AssociatedObject.MouseMove -= OnMouseMove;
//            AssociatedObject.DragEnter -= OnCheckDropTarget;
//            AssociatedObject.DragOver -= OnCheckDropTarget;
//            AssociatedObject.DragLeave -= OnCheckDropTarget;
//            AssociatedObject.Drop -= OnDrop;

//            base.OnDetaching();
//        }
//    }

//    /// <summary>
//    /// The EventArgs parameter passed on the drop
//    /// </summary>
//    public class DataGridRowDropEventArgs : EventArgs
//    {
//        /// <summary>
//        /// Item being repositioned in the DataGrid
//        /// </summary>
//        public object Item { get; private set; }
//        /// <summary>
//        /// Old index of the item
//        /// </summary>
//        public int OldIndex { get; private set; }
//        /// <summary>
//        /// New index of the item
//        /// </summary>
//        public int NewIndex { get; private set; }

//        /// <summary>
//        /// EventArgs passed with DropStarted and DropFinished
//        /// </summary>
//        /// <param name="item">Item being moved</param>
//        /// <param name="oldIndex">Old index</param>
//        /// <param name="newIndex">New index</param>
//        public DataGridRowDropEventArgs(object item, int oldIndex, int newIndex)
//        {
//            Item = item;
//            OldIndex = oldIndex;
//            NewIndex = newIndex;
//        }
//    }

}
