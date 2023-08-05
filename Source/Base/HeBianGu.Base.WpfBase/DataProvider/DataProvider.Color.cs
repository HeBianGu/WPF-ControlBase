using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public class ColorFactory
    {
        public static IEnumerable<Color> CreateStandardColors()
        {
            yield return Colors.Transparent;
            yield return Colors.White;
            yield return Colors.Gray;
            yield return Colors.Black;
            yield return Colors.Red;
            yield return Colors.Green;
            yield return Colors.Blue;
            yield return Colors.Yellow;
            yield return Colors.Orange;
            yield return Colors.Purple;
        }

        public static IEnumerable<Color> CreateAvailableColors()
        {
            List<Color> result = typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<Color>().ToList();
            result.Remove(Colors.Transparent);
            result.Sort(new ColorSorter());
            return result;
        }

        public static IEnumerable<Color> CreateSystemColors()
        {
            return typeof(Colors).GetProperties().Select(x => x.GetValue(null)).OfType<Color>();
        }

        public static IEnumerable<SolidColorBrush> CreateSystemSolidColorBrushes()
        {
            return typeof(Brushes).GetProperties().Select(x => x.GetValue(null)).OfType<SolidColorBrush>();
        }

        public static IEnumerable<Color> CreateDepths(params double[] values)
        {
            foreach (var value in values)
            {
                foreach (var item in CreateDepthB(value))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<Color> CreateDepthB(params double[] bs)
        {
            return CreateDepthB(3, 0, 100, bs);
        }
        public static IEnumerable<Color> CreateDepthB(int from, int to, int step, params double[] values)
        {
            //for (int i = from; i < to; i++)
            //{
            //    if (i % step == 0)
            //    {
            //        foreach (var b in bs)
            //        {
            //            yield return new HsbaColor(i, 1.0, b, 1.0).Color;
            //        }
            //    }
            //}

            return CreateDepth(from, to, step, (i, v) => new HsbaColor(i, 1.0, v, 1.0), values);
        }

        public static IEnumerable<Color> CreateDepthA(int from, int to, int step, params double[] values)
        {
            //for (int i = from; i < to; i++)
            //{
            //    if (i % step == 0)
            //    {
            //        foreach (var b in bs)
            //        {
            //            yield return new HsbaColor(i, 1.0, b, 1.0).Color;
            //        }
            //    }
            //}

            return CreateDepth(from, to, step, (i, v) => new HsbaColor(i, 1.0, 1.0, v), values);
        }

        public static IEnumerable<Color> CreateDepth(int from, int to, int step, Func<int, double, HsbaColor> create, params double[] values)
        {
            for (int i = from; i <= to; i = i + step)
            {
                foreach (var value in values)
                {
                    yield return create.Invoke(i, value).Color;
                }
            }
        }

        public static IEnumerable<Color> CreateDepth(int from, int to, int step, double vStep = 0.2)
        {
            for (int i = from; i < to; i++)
            {
                if (i % step == 0)
                {
                    for (double r = 0; r < 1.0; r = r + vStep)
                    {
                        for (double g = 0; g < 1.0; g = g + vStep)
                        {
                            for (double b = 0; b < 1.0; b = b + vStep)
                            {
                                yield return new HsbaColor(i, r, g, b).Color;
                            }
                        }
                    }
                }
            }
        }
    }

    public class ColorSorter : IComparer<Color>
    {
        public int Compare(Color firstItem, Color secondItem)
        {
            if (firstItem == null || secondItem == null)
                return -1;

            Color colorItem1 = (Color)firstItem;
            Color colorItem2 = (Color)secondItem;
            System.Drawing.Color drawingColor1 = System.Drawing.Color.FromArgb(colorItem1.A, colorItem1.R, colorItem1.G, colorItem1.B);
            System.Drawing.Color drawingColor2 = System.Drawing.Color.FromArgb(colorItem2.A, colorItem2.R, colorItem2.G, colorItem2.B);

            // Compare Hue
            double hueColor1 = Math.Round((double)drawingColor1.GetHue(), 3);
            double hueColor2 = Math.Round((double)drawingColor2.GetHue(), 3);

            if (hueColor1 > hueColor2)
                return 1;
            else if (hueColor1 < hueColor2)
                return -1;
            else
            {
                // Hue is equal, compare Saturation
                double satColor1 = Math.Round((double)drawingColor1.GetSaturation(), 3);
                double satColor2 = Math.Round((double)drawingColor2.GetSaturation(), 3);

                if (satColor1 > satColor2)
                    return 1;
                else if (satColor1 < satColor2)
                    return -1;
                else
                {
                    // Saturation is equal, compare Brightness
                    double brightColor1 = Math.Round((double)drawingColor1.GetBrightness(), 3);
                    double brightColor2 = Math.Round((double)drawingColor2.GetBrightness(), 3);

                    if (brightColor1 > brightColor2)
                        return 1;
                    else if (brightColor1 < brightColor2)
                        return -1;
                }
            }

            return 0;
        }
    }

    public class GetDepthColorsExtension : MarkupExtension
    {
        public DoubleCollection Depthes { get; set; } = new DoubleCollection(new double[] { 1.0, 0.8, 0.6, 0.4, 0.2 });

        public int From { get; set; } = 0;

        public int To { get; set; } = 360;

        public int Step { get; set; } = 48;

        public double B { get; set; } = 1.0;

        public double A { get; set; } = 1.0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            List<Color> list = new List<Color>();
            list.AddRange(ColorFactory.CreateDepth(this.From, this.To, this.Step, (i, v) => new HsbaColor(i, v, this.B, this.A), this.Depthes.ToArray()));
            //  Do ：黑白
            for (int i = 0; i < this.Depthes.Count; i++)
            {
                byte v = Convert.ToByte((i * 1.0 / (this.Depthes.Count - 1)) * 255);
                list.Add(Color.FromRgb(v, v, v));
            }

            list.Add(Colors.Transparent);
            return list;
        }
    }
}
