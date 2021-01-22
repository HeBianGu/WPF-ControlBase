using System.Windows;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public partial class LoginWindowBase : DialogWindowBase
    {
        public LoginWindowBase()
        {
            //this.ShowAnimation = l =>
            //{

            //    //l.RenderTransformOrigin = new Point(0.5, 0.5);

            //    //var engine2 = DoubleStoryboardEngine.Create(0.5, 1, 0.5, "Opacity");
            //    //var engine = DoubleStoryboardEngine.Create(0.1, 0.98, 1, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
            //    //var engine1 = DoubleStoryboardEngine.Create(0.1, 0.98, 1, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

            //    //engine.Start(l);
            //    //engine1.Start(l);
            //    //engine2.Start(l);

            //    this.Show(true);
            //};
        }
        public static readonly DependencyProperty LogoProperty = DependencyProperty.Register("Logo", typeof(ImageSource), typeof(LoginWindowBase), new PropertyMetadata(null));

        public ImageSource Logo
        {
            get { return (ImageSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        public bool IsLogined
        {
            get { return (bool)GetValue(IsLoginedProperty); }
            set { SetValue(IsLoginedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoginedProperty =
            DependencyProperty.Register("IsLogined", typeof(bool), typeof(LoginWindowBase), new PropertyMetadata(default(bool), (d, e) =>
            {
                LoginWindowBase control = d as LoginWindowBase;

                if (control == null) return;

                bool config = (bool)e.NewValue;

                if (config)
                {
                    //control.ScaleReduceWithAction(new Point(0.5, 0.5), 0.5, 5, () =>
                    //{
                    //    control.DialogResult = true;
                    //    control.Close();
                    //}); 


                    //control.RenderTransformOrigin = new Point(0.5, 0.5);

                    //var engine2 = DoubleStoryboardEngine.Create(1, 0.5, 0.5, "Opacity");
                    //var engine = DoubleStoryboardEngine.Create(1, 0.3, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
                    //var engine1 = DoubleStoryboardEngine.Create(1, 0.3, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

                    //engine.CompletedEvent += (s, k) =>
                    //{
                    //    control.DialogResult = true;
                    //    control.Close();
                    //};

                    //engine.Start(control);
                    //engine1.Start(control);
                    //engine2.Start(control);

                    control.Result = true;
                    control.Show(false);
                }

            }));

        public bool Result { get; set; }

    }


}
