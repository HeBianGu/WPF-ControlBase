using System;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Base.WpfBase
{
    /// <summary> DataGrid支持显示行号 </summary>
    public class DataGridRowIndexBehavior : Behavior<TextBlock>
    {
        /// <summary>
        /// Internal target property used to track current item of DataGridRow.
        /// </summary>
        static readonly DependencyProperty TargetProperty =
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
            DependencyProperty.Register("DataGridRow", typeof (DataGridRow), typeof (DataGridRowIndexBehavior), new FrameworkPropertyMetadata(OnDataGridRowChanged));
        
        /// <summary>
        /// DataGridRow this behavior uses to determine the proper row index.
        /// </summary>
        public DataGridRow DataGridRow
        {
            get { return (DataGridRow) GetValue(DataGridRowProperty); }
            set { SetValue(DataGridRowProperty, value);}
        }

        /// <summary>
        /// This is called when the DataGridRow property is changed.  It generally means
        /// a new row has been added into the grid and we need to set the initial row
        /// value for it.  We won't see a CollectionChange for this item - the event isn't
        /// hooked up yet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnDataGridRowChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
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
            BindingOperations.SetBinding(this, TargetProperty, new Binding("Item") {Source = dgr});

            // Hook into the DataGrid owner.
            DataGrid dg = dgr.GetParent<DataGrid>();
            dg.RowEditEnding += DgRowEditEnding;
            var cv = CollectionViewSource.GetDefaultView(dg.ItemsSource);
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
                var dgri = (DataGridRowIndexBehavior)dpo;
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
        static void DgRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = (DataGrid) sender;
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
        void CvCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
                var cv = CollectionViewSource.GetDefaultView(dg.ItemsSource);
                if (cv != null)
                    cv.CollectionChanged -= CvCollectionChanged;
                dg.RowEditEnding -= DgRowEditEnding;
            }

            base.OnDetaching();
        }
    }
}
