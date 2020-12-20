using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{
    public partial class StartWindowBase : WindowBase
    {
        public StartWindowBase()
        {
            this.ShowAnimation = l =>
            {

                //l.RenderTransformOrigin = new Point(0.5, 0.5);

                //var engine2 = DoubleStoryboardEngine.Create(0.5, 1, 0.5, UIElement.OpacityProperty.Name);
                //var engine = DoubleStoryboardEngine.Create(0.1, 0.96, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
                //var engine1 = DoubleStoryboardEngine.Create(0.1, 0.96, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

                //engine.Start(l);
                //engine1.Start(l);
                //engine2.Start(l);


                //l.ScaleEnlarge(new Point(0.5, 0.5), 0.5, 5);

                this.Show(true);

            };

            this.CloseAnimation = l =>
            {
                //l.RenderTransformOrigin = new Point(0.5, 0.5);

                //var engine2 = DoubleStoryboardEngine.Create(1, 0.5, 0.3, "Opacity");
                //var engine = DoubleStoryboardEngine.Create(1, 0.1, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
                //var engine1 = DoubleStoryboardEngine.Create(1, 0.1, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

                //engine.CompletedEvent += (s, e) =>
                //{
                //    this.MouseDown -= DialogWindow_MouseDown;
                //    l.Close();
                //};

                //engine.Start(l);
                //engine1.Start(l);
                //engine2.Start(l);

                this.Show(false);

            };

            this.BindCommand(CommandService.Close, (l, k) =>
            {
                this.CloseAnimation?.Invoke(this);
            });

            this.MouseDown += DialogWindow_MouseDown;



        }

        private void DialogWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

    }


}
