using HeBianGu.Base.WpfBase;
using HeBianGu.Control.LayerBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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

namespace HeBianGu.Control.WriteableBitmapLayer
{
    public abstract class WriteableBitmapLayer : BlueprintLayer
    {
        private WriteableBitmapDrawing _drawing = new WriteableBitmapDrawing();
        public override void Draw(LayerView element)
        {
            if (element == null)
                return;

            if (this.LayerView == null)
                this.LayerView = element;
            using (DrawingContext dc = RenderOpen())
            {
                if (IsVisible == false)
                    return;

                WriteableBitmap bitmap = new WriteableBitmap((int)this.Width, (int)this.Height, 48, 48, PixelFormats.Bgra32, null);
                //bitmap.CopyPixels(bytes.ToArray(), x * 4, 0);
                //int stride = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
                double scale = this.GetScale();
                bitmap.Clear(Colors.Green);
                //var offset = new Point(this.Center.X - scale * this.Width / 2, this.Center.Y - scale * this.Height / 2);
                //_drawing.Offset = new Point(offset.X * this.GetScale(), offset.Y * this.GetScale());
                _drawing.Offset = new Point(this.Center.X -  this.Width / 2, this.Center.Y -this.Height / 2);
                _drawing.Scale = this.GetScale();
                _drawing.WriteableBitmap = bitmap;
                _drawing.DrawingContext = dc;
                _drawing.LayerView = element;
                this.Draw(_drawing);
                if (IsSelected)
                {
                    dc.DrawRectangle(null, new Pen(Brushes.Blue, 2), ContentBounds);
                }
            }
        }

        public override void Draw(IDrawing drawing)
        {
            if (this._drawing.WriteableBitmap == null)
                return;
            if (drawing.LayerView.ActualHeight == 0 || drawing.LayerView.ActualHeight == 0)
                return;
            //Rect rect = new Rect(0, 0, this.Width, this.Height);
            Rect rect = new Rect(this.Center.X - this.Width / 2, this.Center.Y - this.Height / 2, this.Width, this.Height);
            ImageDrawing dm = new ImageDrawing(this._drawing.WriteableBitmap, rect);
            //dm.Freeze();
            drawing.DrawDrawing(dm);
        }

        public override void OnScaleChanged(object sender, ObjectRoutedEventArgs<Tuple<double, double>> e)
        {
            //base.OnScaleChanged(sender, e);
        }

        public double GetScale()
        {
            return Math.Max(1000.0 / this.Width, 1000.0 / this.Height);
        }

        public double GetWidth()
        {
            return this.Width * this.GetScale();
        }

        public double GetHeight()
        {
            return this.Height * this.GetScale();
        }
    }
}
