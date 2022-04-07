// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace HeBianGu.Control.StoryBoard
{
    public interface IThumbBarItem : ICloneable
    {
        double Start { get; set; }

        double Size { get; set; }
    }

    public class ItemsBoardToolBar : ListBox
    {
        static ItemsBoardToolBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsBoardToolBar), new FrameworkPropertyMetadata(typeof(ItemsBoardToolBar)));
        }

        protected override void OnDrop(DragEventArgs e)
        {
            IDragAdorner adorner = e.Data.GetData("Equipment") as IDragAdorner;

            if (adorner == null) return;

            Point offset = adorner.Offset;

            Point location = e.GetPosition(this);

            IThumbBarItem obj = ((adorner as Adorner).AdornedElement as FrameworkElement).DataContext as IThumbBarItem;

            if (obj is IThumbBarItem item)
            {
                if (this.ItemsSource is IList list)
                {
                    IThumbBarItem n = obj.Clone() as IThumbBarItem;
                    n.Start = (long)location.X;
                    list.Add(n);
                }

                e.Handled = true;
                return;
            }

            throw new ArgumentException("没有实现IThumbBarItem接口");



            //if (this.AssociatedObject.NodesSource is IList collection)
            //{
            //    //PortNode node = PortNode.CreateWithPort<PortNode>();

            //    var content = obj.Clone();

            //    var node = this.Create(content as INodeData);

            //    node.Content = content;

            //    node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            //    node.Location = new Point(location.X - offset.X + node.DesiredSize.Width / 2, location.Y - offset.Y + node.DesiredSize.Height / 2);

            //    //NodeLayer.SetPosition(node,new Point(location.X - offset.X, location.Y - offset.Y));

            //    collection.Add(node);

            //    this.AssociatedObject.RefreshData();
            //}
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            IDragAdorner adorner = e.Data.GetData("Equipment") as IDragAdorner;

            if (adorner == null)
            {
                this.AllowDrop = false;
                e.Handled = true;
                return;
            }


            IThumbBarItem obj = ((adorner as Adorner).AdornedElement as FrameworkElement).DataContext as IThumbBarItem;

            if (obj == null)
            {
                this.AllowDrop = false;
                e.Handled = true;
                return;
            }


            Type type = this.ItemsSource?.GetType()?.GetGenericArguments()?.FirstOrDefault();

            if (type == null)
            {
                this.AllowDrop = false;
                e.Handled = true;
                return;
            }

            this.AllowDrop = type.IsAssignableFrom(obj?.GetType());
            e.Handled = true;

            Debug.WriteLine("设置类型一样才可以拖动");
        }



        private void ItemsBoardToolBar_Drop(object sender, DragEventArgs e)
        {
            IDragAdorner adorner = e.Data.GetData("Equipment") as IDragAdorner;

            if (adorner == null) return;

            Point offset = adorner.Offset;

            Point location = e.GetPosition(this);

            ICloneable obj = ((adorner as Adorner).AdornedElement as FrameworkElement).DataContext as ICloneable;

            if (obj == null)
            {
                throw new ArgumentException("没有实现ICloneable接口");
            }

            if (this.ItemsSource is IList list)
            {
                list.Add(obj);
            }

            //if (this.AssociatedObject.NodesSource is IList collection)
            //{
            //    //PortNode node = PortNode.CreateWithPort<PortNode>();

            //    var content = obj.Clone();

            //    var node = this.Create(content as INodeData);

            //    node.Content = content;

            //    node.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            //    node.Location = new Point(location.X - offset.X + node.DesiredSize.Width / 2, location.Y - offset.Y + node.DesiredSize.Height / 2);

            //    //NodeLayer.SetPosition(node,new Point(location.X - offset.X, location.Y - offset.Y));

            //    collection.Add(node);

            //    this.AssociatedObject.RefreshData();
            //}
        }


    }

    public class ItemThumbBar : Thumb
    {
        static ItemThumbBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemThumbBar), new FrameworkPropertyMetadata(typeof(ItemThumbBar)));
        }
        public ItemThumbBar()
        {
            this.DragDelta += ItemThumbBar_DragDelta;
        }

        private void ItemThumbBar_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ItemsBoardToolBar parent = this.GetParent<ItemsBoardToolBar>();

            System.Collections.Generic.IEnumerable<ItemThumbBar> children = parent.GetChildren<ItemThumbBar>().Where(x => x != this);

            if (e.HorizontalChange == 0) return;

            //  Do ：drag left
            if (e.HorizontalChange < 0)
            {
                double next = this.Left + e.HorizontalChange;

                System.Collections.Generic.IEnumerable<ItemThumbBar> lefts = children.Where(x => x.Right <= this.Left);

                if (lefts.Count() > 0)
                {
                    double rightLimit = lefts.Max(x => x.Right);
                    if (next < rightLimit)
                    {
                        this.Left = rightLimit;
                        return;
                    }
                }

                if (next < 0)
                {
                    this.Left = 0;
                    return;
                }
            }
            else
            {
                double next = this.Right + e.HorizontalChange;

                System.Collections.Generic.IEnumerable<ItemThumbBar> rights = children.Where(x => x.Left >= this.Right);

                if (rights.Count() > 0)
                {
                    double LeftLimit = rights.Min(x => x.Left);

                    if (next > LeftLimit)
                    {
                        this.Left = LeftLimit - this.Width;
                        return;
                    }
                }

                if (next > parent.ActualWidth)
                {
                    this.Left = parent.ActualWidth - this.Width;
                    return;
                }
            }

            this.Left += e.HorizontalChange;
        }

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(ItemThumbBar), new FrameworkPropertyMetadata(default(double), (d, e) =>
             {
                 ItemThumbBar control = d as ItemThumbBar;

                 if (control == null) return;

                 if (e.OldValue is double o)
                 {

                 }

                 if (e.NewValue is double n)
                 {

                 }

             }));


        public double Right => this.Left + this.Width;
    }
}