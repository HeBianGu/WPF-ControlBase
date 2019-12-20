using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace HeBianGu.Base.WpfBase
{
    /// <summary> 容器内子控件加载时触发喷泉效果</summary>
    public class FountainAnimationBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsUseAll) 
            {

                var items = AssociatedObject.GetChildren<UIElement>().Where(l=>l.RenderTransform is TransformGroup);

                items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                StoryBoardService.FountainAnimation(items, PointLeft, PointTop, Mul, MiddleValue, EndValue);
            }
            else
            {
                if(AssociatedObject is Panel panel)
                {
                    var items = panel.Children?.Cast<UIElement>()?.Where(l => l.RenderTransform is TransformGroup);

                    items = items.Where(l => (l.RenderTransform as TransformGroup).Children.Count == 4);

                    StoryBoardService.FountainAnimation(items, PointLeft, PointTop, Mul, MiddleValue, EndValue);
                }
               
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
        }



        public bool IsUseAll
        {
            get { return (bool)GetValue(IsUseAllProperty); }
            set { SetValue(IsUseAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUseAllProperty =
            DependencyProperty.Register("IsUseAll", typeof(bool), typeof(FountainAnimationBehavior), new PropertyMetadata(false, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

                 //bool config = e.NewValue as bool;

             }));


        /// <summary> 喷出点左 </summary>
        public int PointLeft
        {
            get { return (int)GetValue(PointLeftProperty); }
            set { SetValue(PointLeftProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointLeftProperty =
            DependencyProperty.Register("PointLeft", typeof(int), typeof(FountainAnimationBehavior), new PropertyMetadata(-500, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 喷出点上 </summary>
        public int PointTop
        {
            get { return (int)GetValue(PointTopProperty); }
            set { SetValue(PointTopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointTopProperty =
            DependencyProperty.Register("PointTop", typeof(int), typeof(FountainAnimationBehavior), new PropertyMetadata(500, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 放大倍数 </summary>
        public double Mul
        {
            get { return (double)GetValue(MulProperty); }
            set { SetValue(MulProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MulProperty =
            DependencyProperty.Register("Mul", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(10.0, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 放大时间点 </summary>
        public double MiddleValue
        {
            get { return (double)GetValue(MiddleValueProperty); }
            set { SetValue(MiddleValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiddleValueProperty =
            DependencyProperty.Register("MiddleValue", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(0.1, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));

        /// <summary> 还原时间点 </summary>
        public double EndValue
        {
            get { return (double)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(double), typeof(FountainAnimationBehavior), new PropertyMetadata(1.0, (d, e) =>
             {
                 FountainAnimationBehavior control = d as FountainAnimationBehavior;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


    }
}