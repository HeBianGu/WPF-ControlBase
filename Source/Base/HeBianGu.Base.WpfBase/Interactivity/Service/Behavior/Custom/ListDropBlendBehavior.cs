using System;
using System.Collections;
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
        }

        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            var dropTarget = sender as ListBox;
            if ((dropTarget != null) && (dragEventArgs.Data.GetDataPresent("Custom")))
            {
                //dropTarget.Items.Add(dragEventArgs.Data.GetData("Custom"));

                var data = this.GetDataByMousePoint(dragEventArgs.GetPosition(this.AssociatedObject));

                int index = data == null ? (this.AssociatedObject.ItemsSource as IList).Count: (this.AssociatedObject.ItemsSource as IList).IndexOf(data);

                //(dropTarget.ItemsSource as IList).Add(dragEventArgs.Data.GetData("Custom"));

                (dropTarget.ItemsSource as IList).Insert(index,dragEventArgs.Data.GetData("Custom"));
            }
        }


        object GetDataByMousePoint(Point point)
        {
            UIElement element =  this.AssociatedObject.InputHitTest(point) as UIElement;

            if(element==null)
            {
                element = this.AssociatedObject.InputHitTest(new Point(point.X,point.Y-10)) as  UIElement;
            }

            object data = DependencyProperty.UnsetValue;

            while(data==DependencyProperty.UnsetValue)
            {
                data = this.AssociatedObject.ItemContainerGenerator.ItemFromContainer(element);

                if (data == DependencyProperty.UnsetValue)
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }

                if (element == this.AssociatedObject) return null;
            }

            return data==DependencyProperty.UnsetValue?null:data;
        }
    }
}
