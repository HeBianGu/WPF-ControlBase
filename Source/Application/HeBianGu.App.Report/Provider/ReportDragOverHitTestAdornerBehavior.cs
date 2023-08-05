using HeBianGu.Base.WpfBase;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using HeBianGu.General.WpfControlLib;
using System.Collections;
using System.Windows.Controls;
using System.Xml.Linq;
using HeBianGu.Systems.Print;
using HeBianGu.Control.Adorner;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace HeBianGu.App.Report
{
    public class ReportDragOverHitTestAdornerBehavior : DragOverHitTestAdornerBehavior
    {
        public bool UsePreView
        {
            get { return (bool)GetValue(UsePreViewProperty); }
            set { SetValue(UsePreViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UsePreViewProperty =
            DependencyProperty.Register("UsePreView", typeof(bool), typeof(ReportDragOverHitTestAdornerBehavior), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                ReportDragOverHitTestAdornerBehavior control = d as ReportDragOverHitTestAdornerBehavior;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
            }));

        protected override void DragEnter(UIElement element, DragEventArgs e)
        {
            if (this.UsePreView)
            {
                this.Add(element, e);
            }
            else
            {
                base.DragEnter(element, e);
            }
        }

        protected override void DragLeave(UIElement element, DragEventArgs e)
        {
            if (this.UsePreView)
            {
                this.Remove(element, e);
            }
            else
            {
                base.DragLeave(element, e);
            }
        }

        protected override void Drop(object sender, DragEventArgs e)
        {
            if (this.UsePreView)
                return;
            this.Add(_temp, e);
        }

        void Add(UIElement element, DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.Move)
            {
                this.Remove(element, e);
            }
            System.Diagnostics.Debug.WriteLine("Add");
            IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
            var data = adorner.GetData();
            if (data is IPrintPagePresenter page)
            {
                if (page is ICloneable c)
                    this.AssociatedObject.GetItemsSource<IList>().Add(c.Clone());
                return;
            }

            var value = data is ICloneable cloneable ? cloneable.Clone() : data;
            if (element.GetContent() is ListPrintPagePresenter listPrintPage)
            {
                listPrintPage.Presenters.Add(value);
                return;
            }

            if (element.GetContent() is IHitTestElementDrop drag)
            {
                if (drag.CanDrop(element, e))
                    drag.Drop(element, e);
                return;
            }

            if (element == null)
                return;
            var pp = element.GetParent<ItemsControl>();
            if (pp == null)
                return;
            if (pp.ItemsSource is IList list)
            {
                var content = element.GetContent();
                int index = list.IndexOf(content);
                if (index != -1)
                {
                    list.Insert(index, value);
                }
            }
        }

        void Remove(UIElement from, DragEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Remove");
            IDragAdorner adorner = e.Data.GetData("DragGroup") as IDragAdorner;
            var data = adorner.GetData();
            var element = (adorner as Adorner).AdornedElement;
            if (element == from)
                return;
            if (data is IPrintPagePresenter page)
            {
                this.AssociatedObject.GetItemsSource<IList>().Remove(page);
                return;
            }

            if (element.GetContent() is ListPrintPagePresenter listPrintPage)
            {
                listPrintPage.Presenters.Remove(data);
                return;
            }
            if (element.GetContent() is IHitTestElementDrag drag)
            {
                drag.DragLeave(element, e);
                return;
            }
            if (element == null)
                return;
            var pp = element.GetParent<ItemsControl>();
            if (pp == null)
                return;
            if (pp.ItemsSource is IList list)
                list.Remove(data);

        }

        //protected override Adorner GetAdorner(UIElement elment)
        //{
        //    if (elment.GetContent() is ListPrintPagePresenter listPrintPagePresenter)
        //    {
        //        return new LineAdorner(elment) { Dock = Dock.Bottom };
        //    }
        //    return base.GetAdorner(elment);
        //}
    }
}
