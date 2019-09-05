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
    public partial class NotifyWindow : WindowBase
    {

        public NotifyWindow()
        {
            this.ShowAnimation = l =>
            {
                var engine = DoubleStoryboardEngine.Create(this.Height, 0, 0.5, "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)");
                engine.Start(l);
            };

            this.CloseAnimation = l =>
            {
                l.RenderTransformOrigin = new Point(0.5, 0.5);

                var engine = DoubleStoryboardEngine.Create(0, this.Height, 0.5, "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)");

                engine.CompletedEvent += (s, e) =>
                {
                    l.Close();
                };
                engine.Start(l);
            };

            this.BindCommand(CommandService.Close, (l, k) =>
            {
                this.CloseAnimation?.Invoke(this);
            });

            this.WindowStartupLocation = WindowStartupLocation.Manual;



            this.Loaded += (l, k) =>
            {
                double screeHeight = SystemParameters.WorkArea.Height;

                double screeWidth = SystemParameters.WorkArea.Width;

                this.Top = screeHeight - this.Height - 2;

                this.Left = screeWidth - this.Width - 5;
            };
        }


    }
}
