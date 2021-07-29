using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HeBianGu.General.WpfControlLib
{
    public class WaterMarkAdorner : Adorner
    {
        private Brush vbrush;

        public WaterMarkAdorner(UIElement adornedElement) : base(adornedElement)
        {
            Uri uri = new Uri("pack://application:,,,/HeBianGu.General.WpfControlLib;component/Resources/logo.png");

            ImageSource _source = new System.Windows.Media.Imaging.BitmapImage(uri);

            Grid grid = new Grid();
            grid.Width = 100;
            grid.Height = 30;

            grid.Background = this.FindResource("S.Brush.Accent") as Brush;

            ImageBrush brush = new ImageBrush(_source);

            grid.OpacityMask = brush;

            vbrush = new VisualBrush(grid);
        }

        protected override void OnRender(DrawingContext dc)
        {
            return;
            dc.DrawRectangle(vbrush, null, new Rect(this.RenderSize.Width - 100, this.RenderSize.Height - 30, 100, 30));
        }
    }
}
