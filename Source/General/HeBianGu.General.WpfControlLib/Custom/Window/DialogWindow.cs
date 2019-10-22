using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public partial class DialogWindow : WindowBase
    {
        public DialogWindow()
        { 
            this.ShowAnimation = l =>
              {

                  l.RenderTransformOrigin = new Point(0.5, 0.5);

                  var engine2 = DoubleStoryboardEngine.Create(0.5, 1, 0.5, "Opacity"); 
                  var engine = DoubleStoryboardEngine.Create(0.1, 0.98, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
                  var engine1 = DoubleStoryboardEngine.Create(0.1, 0.98, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

                  engine.Start(l);
                  engine1.Start(l);
                  engine2.Start(l);


                  //l.ScaleEnlarge(new Point(0.5, 0.5), 0.5, 5);

              };

            this.CloseAnimation = l =>
              {
                  l.RenderTransformOrigin = new Point(0.5, 0.5);

                  var engine2 = DoubleStoryboardEngine.Create(1, 0.5, 0.3, "Opacity");
                  var engine = DoubleStoryboardEngine.Create(1, 0.1, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)");
                  var engine1 = DoubleStoryboardEngine.Create(1, 0.1, 0.3, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)");

                  engine.CompletedEvent += (s, e) =>
                  {
                      this.MouseDown -= DialogWindow_MouseDown;
                      l.Close();
                  };

                  engine.Start(l);
                  engine1.Start(l);
                  engine2.Start(l);
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
