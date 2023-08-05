// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Service.TypeConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Control.Panel
{
    public class GridBase : Grid
    {
        public Pen GridLinePen
        {
            get { return (Pen)GetValue(GridLinePenProperty); }
            set { SetValue(GridLinePenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridLinePenProperty =
            DependencyProperty.Register("GridLinePen", typeof(Pen), typeof(GridBase), new FrameworkPropertyMetadata(new Pen(Brushes.Black, 1), (d, e) =>
            {
                GridBase control = d as GridBase;

                if (control == null) return;

                if (e.OldValue is Pen o)
                {

                }

                if (e.NewValue is Pen n)
                {

                }

                control.InvalidateVisual();
            }));


        public double MinRowHeight
        {
            get { return (double)GetValue(MinRowHeightProperty); }
            set { SetValue(MinRowHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinRowHeightProperty =
            DependencyProperty.Register("MinRowHeight", typeof(double), typeof(GridBase), new FrameworkPropertyMetadata(default(double), (d, e) =>
            {
                GridBase control = d as GridBase;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
                control.Refresh();
            }));



        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (this.GridLinePen == null)
                return;

            foreach (var item in this.RowDefinitions)
            {
                dc.DrawLine(this.GridLinePen, new Point(0, item.Offset), new Point(this.ActualWidth, item.Offset));
            }
            dc.DrawLine(this.GridLinePen, new Point(0, this.ActualHeight), new Point(this.ActualWidth, this.ActualHeight));

            foreach (var item in this.ColumnDefinitions)
            {
                dc.DrawLine(this.GridLinePen, new Point(item.Offset, 0), new Point(item.Offset, this.ActualHeight));
            }
            dc.DrawLine(this.GridLinePen, new Point(this.ActualWidth, 0), new Point(this.ActualWidth, this.ActualHeight));
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            this.InvalidateVisual();
            return base.ArrangeOverride(arrangeSize);
        }

        protected virtual void Refresh()
        {

        }


    }

    public class FixedGrid : GridBase
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(FixedGrid), "S.FixedGrid.Default");

        static FixedGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FixedGrid), new FrameworkPropertyMetadata(typeof(FixedGrid)));
        }
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(FixedGrid), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                FixedGrid control = d as FixedGrid;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }
                control.Refresh();
            }));


        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(FixedGrid), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                FixedGrid control = d as FixedGrid;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }
                control.Refresh();
            }));


        public GridLength RowGridLength
        {
            get { return (GridLength)GetValue(RowGridLengthProperty); }
            set { SetValue(RowGridLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowGridLengthProperty =
            DependencyProperty.Register("RowGridLength", typeof(GridLength), typeof(FixedGrid), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), (d, e) =>
            {
                FixedGrid control = d as FixedGrid;

                if (control == null) return;

                if (e.OldValue is GridLength o)
                {

                }

                if (e.NewValue is GridLength n)
                {

                }

                control.Refresh();

            }));


        public GridLength ColumnGridLength
        {
            get { return (GridLength)GetValue(ColumnGridLengthProperty); }
            set { SetValue(ColumnGridLengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnGridLengthProperty =
            DependencyProperty.Register("ColumnGridLength", typeof(GridLength), typeof(FixedGrid), new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), (d, e) =>
            {
                FixedGrid control = d as FixedGrid;

                if (control == null) return;

                if (e.OldValue is GridLength o)
                {

                }

                if (e.NewValue is GridLength n)
                {

                }

                control.Refresh();

            }));

        protected override void Refresh()
        {
            this.RowDefinitions.Clear();
            this.ColumnDefinitions.Clear();
            for (int i = 0; i < this.Rows; i++)
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = this.RowGridLength, MinHeight = this.MinRowHeight });
            }
            for (int j = 0; j < this.Columns; j++)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = this.ColumnGridLength });
            }
        }
    }

    public class DefinitionGrid : GridBase
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(DefinitionGrid), "S.DefinitionGrid.Default");
        static DefinitionGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DefinitionGrid), new FrameworkPropertyMetadata(typeof(DefinitionGrid)));
        }
        [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
        public ObservableCollection<GridLength> Rows
        {
            get { return (ObservableCollection<GridLength>)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(ObservableCollection<GridLength>), typeof(DefinitionGrid), new FrameworkPropertyMetadata(new ObservableCollection<GridLength>(), (d, e) =>
            {
                DefinitionGrid control = d as DefinitionGrid;

                if (control == null) return;

                if (e.OldValue is ObservableCollection<GridLength> o)
                {

                }

                if (e.NewValue is ObservableCollection<GridLength> n)
                {

                }
                control.Refresh();
            }));

        [TypeConverter(typeof(ObservableCollectionTypeConverter<GridLength, GridLengthConverter>))]
        public ObservableCollection<GridLength> Columns
        {
            get { return (ObservableCollection<GridLength>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(ObservableCollection<GridLength>), typeof(DefinitionGrid), new FrameworkPropertyMetadata(new ObservableCollection<GridLength>(), (d, e) =>
            {
                DefinitionGrid control = d as DefinitionGrid;

                if (control == null) return;

                if (e.OldValue is ObservableCollection<GridLength> o)
                {

                }

                if (e.NewValue is ObservableCollection<GridLength> n)
                {

                }
                control.Refresh();
            }));

        protected override void Refresh()
        {
            this.RowDefinitions.Clear();
            this.ColumnDefinitions.Clear();

            foreach (var item in this.Rows)
            {
                this.RowDefinitions.Add(new RowDefinition() { Height = item, MinHeight = this.MinRowHeight });
            }
            foreach (var item in this.Columns)
            {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = item });
            }
        }
    }
}
