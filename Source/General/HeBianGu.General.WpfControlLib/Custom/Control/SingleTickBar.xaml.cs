using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    public class SingleTickBar : TickBar
    {

        public Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundProperty =
            DependencyProperty.Register("Foreground", typeof(Brush), typeof(SingleTickBar), new PropertyMetadata(Brushes.Black, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                Brush config = e.NewValue as Brush;

            }));


        public double SplitValue
        {
            get { return (double)GetValue(SplitValueProperty); }
            set { SetValue(SplitValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SplitValueProperty =
            DependencyProperty.Register("SplitValue", typeof(double), typeof(SingleTickBar), new PropertyMetadata(10.0, (d, e) =>
            {
                SingleTickBar control = d as SingleTickBar;

                if (control == null) return;

                //double config = e.NewValue as double;

            }));



        protected override void OnRender(DrawingContext dc)
        {
            Size size = new Size(base.ActualWidth, base.ActualHeight);

            int tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency) + 1;

            if ((this.Maximum - this.Minimum) % this.TickFrequency == 0)
                tickCount -= 1;

            Double tickFrequencySize;

            tickFrequencySize = (size.Width * this.TickFrequency / (this.Maximum - this.Minimum));

            string text = "";

            FormattedText formattedText = null;

            double num = this.Maximum - this.Minimum;

            int i = 0;

            for (i = 0; i <= tickCount; i++)
            {

                text = Convert.ToString(Convert.ToInt32(this.Minimum + this.TickFrequency * i), 10);

                formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Verdana"), 8, this.Foreground);

                double x = tickFrequencySize * i;



                if (i == this.Maximum)
                {
                    x = x - 5;
                }

                if (i % this.SplitValue == 0)
                {
                    double y = 10;

                    Pen pen = new Pen(this.Foreground, 1);

                    dc.DrawLine(pen, new Point(x + 2, 0), new Point(x + 2, y));

                    dc.DrawText(formattedText, new Point(x, y));
                }
                else
                {
                    double y = 5;

                    Pen pen = new Pen(this.Foreground, 0.5);

                    dc.DrawLine(pen, new Point(x + 2, 0), new Point(x + 2, y));
                }
            }

        }
    }


}
