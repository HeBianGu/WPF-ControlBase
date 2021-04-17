using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    //  		<ListBox x:Name="List" Grid.Row="3" ItemsSource="{Binding StringList}" Height="70" Margin="6" behaviors:BringIntoViewBehavior.IsBringIntoView="True">
    //          <e:Interaction.Behaviors>
    //              <behaviors:ListDragBlendBehavior/>
    //          </e:Interaction.Behaviors>
    //      </ListBox>
    //<ListBox x:Name="List2" Grid.Row="3" Grid.Column="1" Height="70" Margin="6">
    //          <e:Interaction.Behaviors>
    //              <behaviors:ListDropBlendBehavior/>
    //          </e:Interaction.Behaviors>
    //      </ListBox>
    public class ListDropBlendBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += AssociatedObjectOnDrop;

            AssociatedObject.DragEnter += AssociatedObject_DragEnter;

            //AssociatedObject.DragLeave += AssociatedObject_DragLeave;

            AssociatedObject.MouseMove += AssociatedObject_MouseMove;  
        }

        private void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (AssociatedObject.AllowDrop) return;

             AssociatedObject.AllowDrop = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            AssociatedObject.AllowDrop = true;
        } 
        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            AssociatedObject.AllowDrop = e.Data.GetDataPresent(this.DragGroup);

            Debug.WriteLine("设置类型一样才可以拖动");
        }

        /// <summary> 判断可否放置的分组 </summary>
        public string DragGroup
        {
            get { return (string)GetValue(DragGroupProperty); }
            set { SetValue(DragGroupProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DragGroupProperty =
            DependencyProperty.Register("DragGroup", typeof(string), typeof(ListDropBlendBehavior), new PropertyMetadata("DragGroup", (d, e) =>
            {
                ListDropBlendBehavior control = d as ListDropBlendBehavior;

                if (control == null) return;

                string config = e.NewValue as string;

            }));

        ///// <summary> 接受的类型 </summary>
        //public Type DataType
        //{
        //    get { return (Type)GetValue(DataTypeProperty); }
        //    set { SetValue(DataTypeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty DataTypeProperty =
        //    DependencyProperty.Register("DataType", typeof(Type), typeof(ListDropBlendBehavior), new PropertyMetadata(default(Type), (d, e) =>
        //     {
        //         ListDropBlendBehavior control = d as ListDropBlendBehavior;

        //         if (control == null) return;

        //         Type config = e.NewValue as Type;

        //     }));


        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            var dropTarget = sender as ListBox;

            if (dropTarget == null) return;

            if (!dragEventArgs.Data.GetDataPresent(this.DragGroup)) return;

            //dropTarget.Items.Add(dragEventArgs.Data.GetData("Custom"));

            var data = this.GetDataByMousePoint(dragEventArgs.GetPosition(this.AssociatedObject));

            ////  Do ：设置类型一样才可以拖动
            //AssociatedObject.AllowDrop = data.GetType() == this.DataType || this.DataType == null;

            int index = data == null ? (this.AssociatedObject.ItemsSource as IList).Count : (this.AssociatedObject.ItemsSource as IList).IndexOf(data);

            //(dropTarget.ItemsSource as IList).Add(dragEventArgs.Data.GetData("Custom"));

            (dropTarget.ItemsSource as IList).Insert(index, dragEventArgs.Data.GetData(this.DragGroup));
        }

        object GetDataByMousePoint(Point point)
        {
            UIElement element = this.AssociatedObject.InputHitTest(point) as UIElement;

            if (element == null)
            {
                element = this.AssociatedObject.InputHitTest(new Point(point.X, point.Y - 10)) as UIElement;
            }

            object data = DependencyProperty.UnsetValue;

            while (data == DependencyProperty.UnsetValue)
            {
                data = this.AssociatedObject.ItemContainerGenerator.ItemFromContainer(element);

                if (data == DependencyProperty.UnsetValue)
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }

                if (element == this.AssociatedObject) return null;
            }

            return data == DependencyProperty.UnsetValue ? null : data;
        }
    }
}
