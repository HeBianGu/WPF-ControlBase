//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;


//namespace HeBianGu.Base.WpfBase
//{dd       
//    public class VisibilityCollapsedOpacityBehavior : Behavior<FrameworkElement>
//    {

//        protected override void OnAttached()
//        {
//            AssociatedObject.IsVisibleChanged += AssociatedObject_IsVisibleChanged;
//        }

//        private void AssociatedObject_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
//        {
//            if (e.NewValue == e.OldValue) return;

//            if((bool)e.NewValue)
//            {

//            }
//        }

//        protected override void OnDetaching()
//        {
//            AssociatedObject.IsVisibleChanged -= AssociatedObject_IsVisibleChanged;
//        }

//    }
//}

//  ToDelete ：待删除
