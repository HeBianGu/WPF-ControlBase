// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;


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

        private void OnDrop(object sender, DragEventArgs e)
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

        private static void OnCheckDropTarget(object sender, DragEventArgs e)
        {
            UIElement source = e.OriginalSource as UIElement;
            DataGridRow row = (source != null) ? source.GetParent<DataGridRow>() : null;
            if (row == null || row.Item == null)
                e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
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
                            EventHandler<DataGridRowDropEventArgs> dropStarted = DropStarted;
                            if (dropStarted != null)
                                dropStarted.Invoke(this, new DataGridRowDropEventArgs(row.Item, oldIndex, newIndex));

                            Type type = collection.GetType();
                            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ObservableCollection<>))
                            {
                                IList dCollection = collection;

                                //dCollection.Move(oldIndex, newIndex); 

                            }
                            else
                            {
                                collection.RemoveAt(oldIndex);
                                collection.Insert(newIndex, row.Item);
                            }

                            EventHandler<DataGridRowDropEventArgs> dropFinished = DropFinished;
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


    public class DataGridRowIndexBehavior : Behavior<TextBlock>
    {
        /// <summary>
        /// Internal target property used to track current item of DataGridRow.
        /// </summary>
        private static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("_Target", typeof(object), typeof(DataGridRowIndexBehavior), new FrameworkPropertyMetadata(OnItemPropertyChanged));

        private object Target
        {
            get { return GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// DataGridRow this behavior uses to determine the proper row index.
        /// </summary>
        public static readonly DependencyProperty DataGridRowProperty =
            DependencyProperty.Register("DataGridRow", typeof(DataGridRow), typeof(DataGridRowIndexBehavior), new FrameworkPropertyMetadata(OnDataGridRowChanged));

        /// <summary>
        /// DataGridRow this behavior uses to determine the proper row index.
        /// </summary>
        public DataGridRow DataGridRow
        {
            get { return (DataGridRow)GetValue(DataGridRowProperty); }
            set { SetValue(DataGridRowProperty, value); }
        }

        /// <summary>
        /// This is called when the DataGridRow property is changed.  It generally means
        /// a new row has been added into the grid and we need to set the initial row
        /// value for it.  We won't see a CollectionChange for this item - the event isn't
        /// hooked up yet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnDataGridRowChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
                ((DataGridRowIndexBehavior)sender).OnDetachFromDataGridRow((DataGridRow)e.OldValue);

            if (e.NewValue != null)
                ((DataGridRowIndexBehavior)sender).OnAttachToDataGridRow((DataGridRow)e.NewValue);
        }

        private void OnAttachToDataGridRow(DataGridRow dgr)
        {
            int index = dgr.GetIndex() + 1;
            if (AssociatedObject != null)
                AssociatedObject.Text = index.ToString();

            // Hook into the DataGridRow
            BindingOperations.SetBinding(this, TargetProperty, new Binding("Item") { Source = dgr });

            // Hook into the DataGrid owner.
            DataGrid dg = dgr.GetParent<DataGrid>();
            dg.RowEditEnding += DgRowEditEnding;
            System.ComponentModel.ICollectionView cv = CollectionViewSource.GetDefaultView(dg.ItemsSource);
            if (cv != null)
                cv.CollectionChanged += CvCollectionChanged;
        }

        /// <summary>
        /// Detaches object from row - called when being unloaded.
        /// </summary>
        /// <param name="dgr"></param>
        private void OnDetachFromDataGridRow(DataGridRow dgr)
        {
            BindingOperations.ClearBinding(this, TargetProperty);
        }

        /// <summary>
        /// This is called when the item associated with the DG row changes; this possibly
        /// requires a change in the index.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnItemPropertyChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DataGridRowIndexBehavior dgri = (DataGridRowIndexBehavior)dpo;
                int index = dgri.DataGridRow.GetIndex() + 1;
                if (dgri.AssociatedObject != null)
                    dgri.AssociatedObject.Text = index.ToString();
            }
        }

        /// <summary>
        /// This is used to reset the header on the NewItemPlaceholder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DgRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            dg.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(delegate
            {
                // Toggle the state so we insert the placeholder again.
                dg.CanUserAddRows = false;
                dg.CanUserAddRows = true;
                dg.SelectedIndex = dg.Items.Count - 1;
            }));
        }

        /// <summary>
        /// This is called when the ItemsSource collection changes - i.e. it is sorted,
        /// items are removed, inserted, etc.  It updates the *existing* row numbers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CvCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataGridRow != null)
            {
                int index = this.DataGridRow.GetIndex() + 1;
                AssociatedObject.Text = index.ToString();
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            if (this.DataGridRow != null)
            {
                DataGrid dg = DataGridRow.GetParent<DataGrid>();
                System.ComponentModel.ICollectionView cv = CollectionViewSource.GetDefaultView(dg.ItemsSource);
                if (cv != null)
                    cv.CollectionChanged -= CvCollectionChanged;
                dg.RowEditEnding -= DgRowEditEnding;
            }

            base.OnDetaching();
        }
    }

}
