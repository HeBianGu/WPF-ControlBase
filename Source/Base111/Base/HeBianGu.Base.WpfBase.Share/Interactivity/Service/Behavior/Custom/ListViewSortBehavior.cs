using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Data; 
using System.Windows.Controls;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media; 

namespace HeBianGu.Base.WpfBase
{
    /// <summary>
    /// This adorner draws the sorting arrow onto the ListView column
    /// header and provides the visual feedback for the sorting direction.
    /// </summary>
    internal class SortAdorner : Adorner
    {
        /// <summary>
        /// The direction to draw the arrow (up vs. down)
        /// </summary>
        public ListSortDirection Direction { get; set; }

        /// <summary>
        /// The color of the arrow
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(SortAdorner),
                                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The color of the arrow
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="adornedElement">Element (ColumnHeader) to adorn</param>
        public SortAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        /// <summary>
        /// Pen used to draw geometry (none)
        /// </summary>
        private static readonly Pen NoPen = new Pen();

        /// <summary>
        /// The geometry for the up arrow
        /// </summary>
        private static readonly Geometry UpGeometry = Geometry.Parse("M4,0 L0,6 L8,6 z");
        /// <summary>
        /// The geometry for the down arrow
        /// </summary>
        private static readonly Geometry DownGeometry = Geometry.Parse("M0,0 L8,0 L4,6 z");

        /// <summary>
        /// When overridden in a derived class, participates in rendering operations that are directed by the layout system. The rendering instructions for this element are not used directly when this method is invoked, and are instead preserved for later asynchronous use by layout and drawing. 
        /// </summary>
        /// <param name="dc">The drawing instructions for a specific element. This context is provided to the layout system.</param>
        protected override void OnRender(DrawingContext dc)
        {
            GridViewColumnHeader fe = (GridViewColumnHeader)AdornedElement;
            if (fe.ActualWidth - fe.DesiredSize.Width > 20)
            {
                dc.PushTransform(new TranslateTransform(fe.ActualWidth - 15, fe.ActualHeight / 2 - 3));
                dc.DrawGeometry(Fill, NoPen, Direction == ListSortDirection.Ascending ? UpGeometry : DownGeometry);
                dc.Pop();
            }
            base.OnRender(dc);
        }
    }

    /// <summary>
    /// Event arguments when using code behind to sort headers.
    /// </summary>
    public class SortHeaderEventArgs : EventArgs
    {
        /// <summary>
        /// The column that was clicked.
        /// </summary>
        public GridViewColumn Column { get; private set; }

        /// <summary>
        /// The direction to sort
        /// </summary>
        public ListSortDirection SortDirection { get; private set; }

        /// <summary>
        /// True to cancel sort (removes adorner)
        /// False if sort event handler provided sort
        /// </summary>
        public bool Canceled { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="column">Column</param>
        /// <param name="sortDirection">Desire sort direction</param>
        internal SortHeaderEventArgs(GridViewColumn column, ListSortDirection sortDirection)
        {
            Column = column;
            SortDirection = sortDirection;
            Canceled = false;
        }
    }

    /// <summary>
    /// Behavior to provide automatic sorting of a ListView.
    /// </summary>
    public class ListViewSortBehavior : Behavior<ListView>
    {
        private GridViewColumnHeader _sortingColumn;
        private SortAdorner _adorner;
        private volatile bool _currentlySorting;

        /// <summary>
        /// Initial column index
        /// </summary>
        public static readonly DependencyProperty InitialColumnIndexProperty =
            DependencyProperty.Register("InitialColumnIndex", typeof(int), typeof(ListViewSortBehavior),
                                        new PropertyMetadata(-1));

        /// <summary>
        /// Initial sorting column
        /// </summary>
        public int InitialColumnIndex
        {
            get { return (int)base.GetValue(InitialColumnIndexProperty); }
            set { base.SetValue(InitialColumnIndexProperty, value); }
        }

        /// <summary>
        /// Reset the sort visual when the underlying collection changes.
        /// </summary>
        public static readonly DependencyProperty ResetOnCollectionChangeProperty =
            DependencyProperty.Register("ResetOnCollectionChange", typeof(bool), typeof(ListViewSortBehavior),
                new PropertyMetadata(true, OnResetSortCollectionChanged));

        /// <summary>
        /// True to reset the sort when the underlying collection changes
        /// </summary>
        public bool ResetOnCollectionChange
        {
            get { return (bool)GetValue(ResetOnCollectionChangeProperty); }
            set { SetValue(ResetOnCollectionChangeProperty, value); }
        }

        /// <summary>
        /// Sort direction
        /// </summary>
        public static readonly DependencyProperty SortDirectionProperty =
            DependencyProperty.Register("SortDirection", typeof(ListSortDirection), typeof(ListViewSortBehavior),
                                        new PropertyMetadata(ListSortDirection.Ascending));

        /// <summary>
        /// Sort direction
        /// </summary>
        public ListSortDirection SortDirection
        {
            get { return (ListSortDirection)base.GetValue(SortDirectionProperty); }
            set { base.SetValue(SortDirectionProperty, value); }
        }

        /// <summary>
        /// The color of the arrow
        /// </summary>
        public static readonly DependencyProperty ArrowFillProperty = DependencyProperty.Register("ArrowFill", typeof(Brush), typeof(ListViewSortBehavior),
                                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// The color of the arrow
        /// </summary>
        public Brush ArrowFill
        {
            get { return (Brush)GetValue(ArrowFillProperty); }
            set { SetValue(ArrowFillProperty, value); }
        }

        /// <summary>
        /// Event used to manage sorting by code behind
        /// </summary>
        public event EventHandler<SortHeaderEventArgs> SortHeaderClicked;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OnSortHeaderClick));
            AssociatedObject.Loaded += AssociatedObjectLoaded;

            // Ensure it's always called.
            if (AssociatedObject.IsLoaded)
                AssociatedObjectLoaded(AssociatedObject, null);
        }

        /// <summary>
        /// Called when the ListView has completely loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            // Wire up change events to the collection; this removes any existing 
            // handler prior to creating a new one so it's safe to call multiple times.
            if (ResetOnCollectionChange)
                SetupSourceCollectionChangedHandlers(true);

            // See if we can determine an initial sorting column if we don't have one yet.
            if (_sortingColumn == null)
            {
                // No initial sort applied?
                if (InitialColumnIndex < 0)
                    return;

                // Must be a GridView to see columns.
                GridView gridView = AssociatedObject.View as GridView;
                if (gridView == null)
                    return;

                // Bad initial column index?
                if (InitialColumnIndex >= gridView.Columns.Count)
                    return;

                // Get the logical column descriptor and match that to the visual column.
                GridViewColumn startingColumn = gridView.Columns[InitialColumnIndex];
                _sortingColumn = AssociatedObject.GetChildren<GridViewColumnHeader>(gvch => gvch.Column == startingColumn).FirstOrDefault();
            }

            // Either have an existing column, or found the initial column.. do the sort!
            if (_sortingColumn != null)
            {
                // Reverse the sort direction - SortByColumn will invert this again.
                SortDirection = (SortDirection == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                SortByColumn(_sortingColumn);
            }
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            SetupSourceCollectionChangedHandlers(false);
            AssociatedObject.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(OnSortHeaderClick));
            AssociatedObject.Loaded -= AssociatedObjectLoaded;
        }

        /// <summary>
        /// This is called when a Button.Click event occurs inside
        /// the ListView. Here we filter to the column headers and
        /// then provide the sorting when that happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSortHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader cHeader = e.OriginalSource as GridViewColumnHeader;
            if (cHeader != null)
            {
                if (cHeader.Role == GridViewColumnHeaderRole.Normal)
                {
                    SortByColumn(cHeader);
                }
            }
        }

        /// <summary>
        /// This method provides the actual sorting behavior.
        /// </summary>
        /// <param name="sortingColumn"></param>
        private void SortByColumn(GridViewColumnHeader sortingColumn)
        {
            _currentlySorting = true;
            try
            {
                ListSortDirection newSortDirection;
                if (sortingColumn == _sortingColumn)
                {
                    newSortDirection = (SortDirection == ListSortDirection.Ascending)
                                           ? ListSortDirection.Descending
                                           : ListSortDirection.Ascending;
                }
                else
                {
                    newSortDirection = ListSortDirection.Ascending;
                }

                // See if we have an event wired up
                if (SortHeaderClicked != null)
                {
                    SortHeaderEventArgs e = new SortHeaderEventArgs(sortingColumn.Column, newSortDirection);
                    SortHeaderClicked(this, e);
                    if (!e.Canceled)
                    {
                        ChangeAdorner(sortingColumn, newSortDirection);
                    }
                }
                else
                {
                    // Look for a column binding next -- here we will sort using a CollectionView.
                    string sortPath = null;
                    Binding binding = sortingColumn.Column.DisplayMemberBinding as Binding;
                    if (binding != null)
                        sortPath = binding.Path.Path;

                    // If no column binding is present, then we can't sort it.
                    if (string.IsNullOrEmpty(sortPath))
                        return;

                    // Pickup either the data bound source, or the ListView collection itself.
                    object data = AssociatedObject.ItemsSource ?? AssociatedObject.Items;
                    if (data != null)
                    {
                        ChangeAdorner(sortingColumn, newSortDirection);

                        ICollectionView view = CollectionViewSource.GetDefaultView(data);
                        if (view != null && view.CanSort)
                        {
                            view.SortDescriptions.Clear();
                            view.SortDescriptions.Add(new SortDescription(sortPath, SortDirection));
                        }
                    }
                }
            }
            finally
            {
                _currentlySorting = false;
            }
        }

        /// <summary>
        /// Changes the visual adorner on the column header
        /// </summary>
        /// <param name="sortingColumn"></param>
        /// <param name="sortDirection"></param>
        private void ChangeAdorner(GridViewColumnHeader sortingColumn, ListSortDirection sortDirection)
        {
            // Remove existing arrow
            if (_adorner != null)
            {
                var oldLayer = AdornerLayer.GetAdornerLayer(_sortingColumn);
                if (oldLayer != null)
                {
                    oldLayer.Remove(_adorner);
                }
            }

            // Set our new settings
            _sortingColumn = sortingColumn;
            SortDirection = sortDirection;
            _adorner = new SortAdorner(_sortingColumn) { Fill = ArrowFill ?? _sortingColumn.Foreground, Direction = SortDirection };

            // Determine the direction and add the arrow.
            var newLayer = AdornerLayer.GetAdornerLayer(_sortingColumn);
            if (newLayer != null)
            {
                newLayer.Add(_adorner);
            }
        }

        /// <summary>
        /// Called when the ResetOnCollectionChanged is altered.
        /// </summary>
        /// <param name="dpo"></param>
        /// <param name="e"></param>
        private static void OnResetSortCollectionChanged(DependencyObject dpo, DependencyPropertyChangedEventArgs e)
        {
            ListViewSortBehavior lsb = (ListViewSortBehavior)dpo;
            if (lsb.AssociatedObject != null)
                lsb.SetupSourceCollectionChangedHandlers((bool)e.NewValue);
        }

        /// <summary>
        /// Helper to setup handler
        /// </summary>
        /// <param name="addHandler"></param>
        private void SetupSourceCollectionChangedHandlers(bool addHandler)
        {
            ICollectionView collectionView = CollectionViewSource.GetDefaultView(AssociatedObject.Items);
            if (collectionView != null)
            {
                collectionView.CollectionChanged -= OnSourceCollectionChanged;
                if (addHandler)
                {
                    collectionView.CollectionChanged += OnSourceCollectionChanged;
                }
            }
        }

        /// <summary>
        /// Called when the source collection changes AND reset sort is true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_currentlySorting)
                return;

            if (_sortingColumn != null)
            {
                if (e.Action != NotifyCollectionChangedAction.Reset)
                {
                    if (_adorner != null)
                    {
                        var layer = AdornerLayer.GetAdornerLayer(_sortingColumn);
                        if (layer != null)
                            layer.Remove(_adorner);
                        
                        _adorner = null;
                    }
                }
                // Already sorted -- reset
                else 
                {
                    // Reverse the sort direction - SortByColumn will invert this again.
                    SortDirection = (SortDirection == ListSortDirection.Ascending) ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    SortByColumn(_sortingColumn);
                }
            }
        }
    }
}
