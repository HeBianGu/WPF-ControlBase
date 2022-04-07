// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows.Media;

namespace HeBianGu.Base.WpfBase
{
    public class HsbaColor
    {
        private double h = 0, s = 0, b = 0, a = 0;
        /// <summary> 0 - 359，360 = 0  </summary>
        public double H { get { return h; } set { h = value < 0 ? 0 : value >= 360 ? 0 : value; } }

        /// <summary> 0 - 1 </summary>
        public double S { get { return s; } set { s = value < 0 ? 0 : value > 1 ? 1 : value; } }

        /// <summary> 0 - 1 </summary>
        public double B { get { return b; } set { b = value < 0 ? 0 : value > 1 ? 1 : value; } }

        /// <summary> 0 - 1 </summary>
        public double A { get { return a; } set { a = value < 0 ? 0 : value > 1 ? 1 : value; } }

        /// <summary> 亮度 0 - 100 </summary>
        public int Y { get { return RgbaColor.Y; } }

        public HsbaColor() { H = 0; S = 0; B = 1; A = 1; }

        public HsbaColor(double h, double s, double b, double a = 1) { H = h; S = s; B = b; A = a; }

        public HsbaColor(int r, int g, int b, int a = 255)
        {
            HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(r, g, b, a));
            H = hsba.H;
            S = hsba.S;
            B = hsba.B;
            A = hsba.A;
        }

        public HsbaColor(Brush brush)
        {
            HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(brush));
            H = hsba.H;
            S = hsba.S;
            B = hsba.B;
            A = hsba.A;
        }

        public HsbaColor(string hexColor)
        {
            HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(hexColor));
            H = hsba.H;
            S = hsba.S;
            B = hsba.B;
            A = hsba.A;
        }

        public Color Color
        {
            get { return RgbaColor.Color; }
        }

        public Color OpaqueColor
        {
            get { return RgbaColor.OpaqueColor; }
        }
        public SolidColorBrush SolidColorBrush
        {
            get { return RgbaColor.SolidColorBrush; }
        }

        public SolidColorBrush OpaqueSolidColorBrush
        {
            get { return RgbaColor.OpaqueSolidColorBrush; }
        }

        public string HexString
        {
            get { return Color.ToString(); }
        }
        public string RgbaString
        {
            get { return RgbaColor.RgbaString; }
        }

        public RgbaColor RgbaColor
        {
            get { return Utility.HsbaToRgba(this); }
        }
    }

    public class RgbaColor
    {
        private int r = 0, g = 0, b = 0, a = 0;
        /// <summary> 0 - 255 </summary>
        public int R { get { return r; } set { r = value < 0 ? 0 : value > 255 ? 255 : value; } }

        /// <summary> 0 - 255 </summary>
        public int G { get { return g; } set { g = value < 0 ? 0 : value > 255 ? 255 : value; } }

        /// <summary> 0 - 255 </summary>
        public int B { get { return b; } set { b = value < 0 ? 0 : value > 255 ? 255 : value; } }

        /// <summary> 0 - 255 </summary>
        public int A { get { return a; } set { a = value < 0 ? 0 : value > 255 ? 255 : value; } }

        /// <summary> 亮度 0 - 100 </summary>
        public int Y { get { return Utility.GetBrightness(R, G, B); } }

        public RgbaColor() { R = 255; G = 255; B = 255; A = 255; }

        public RgbaColor(int r, int g, int b, int a = 255) { R = r; G = g; B = b; A = a; }

        public RgbaColor(Brush brush)
        {
            if (brush != null)
            {
                R = ((SolidColorBrush)brush).Color.R;
                G = ((SolidColorBrush)brush).Color.G;
                B = ((SolidColorBrush)brush).Color.B;
                A = ((SolidColorBrush)brush).Color.A;
            }
            else
            {
                R = G = B = A = 255;
            }
        }

        public RgbaColor(double h, double s, double b, double a = 1)
        {
            RgbaColor rgba = Utility.HsbaToRgba(new HsbaColor(h, s, b, a));
            R = rgba.R;
            G = rgba.G;
            B = rgba.B;
            A = rgba.A;

        }

        public RgbaColor(string hexColor)
        {
            try
            {
                Color color;
                if (hexColor.Substring(0, 1) == "#") color = (Color)ColorConverter.ConvertFromString(hexColor);
                else color = (Color)ColorConverter.ConvertFromString("#" + hexColor);
                R = color.R;
                G = color.G;
                B = color.B;
                A = color.A;
            }
            catch
            {

            }
        }

        public Color Color { get { return Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B); } }

        public Color OpaqueColor { get { return Color.FromArgb((byte)255.0, (byte)R, (byte)G, (byte)B); } }

        public SolidColorBrush SolidColorBrush { get { return new SolidColorBrush(Color); } }

        public SolidColorBrush OpaqueSolidColorBrush { get { return new SolidColorBrush(OpaqueColor); } }

        public string HexString { get { return Color.ToString(); } }

        public string RgbaString { get { return R + "," + G + "," + B + "," + A; } }

        public HsbaColor HsbaColor { get { return Utility.RgbaToHsba(this); } }
    }

    internal class Utility
    {
        /// <summary> Rgba转Hsba/ </summary>
        internal static HsbaColor RgbaToHsba(RgbaColor rgba)
        {
            int[] rgb = new int[] { rgba.R, rgba.G, rgba.B };
            Array.Sort(rgb);
            int max = rgb[2];
            int min = rgb[0];

            double hsbB = max / 255.0;
            double hsbS = max == 0 ? 0 : (max - min) / (double)max;
            double hsbH = 0;

            if (rgba.R == rgba.G && rgba.R == rgba.B)
            {

            }
            else
            {
                if (max == rgba.R && rgba.G >= rgba.B) hsbH = (rgba.G - rgba.B) * 60.0 / (max - min) + 0.0;
                else if (max == rgba.R && rgba.G < rgba.B) hsbH = (rgba.G - rgba.B) * 60.0 / (max - min) + 360.0;
                else if (max == rgba.G) hsbH = (rgba.B - rgba.R) * 60.0 / (max - min) + 120.0;
                else if (max == rgba.B) hsbH = (rgba.R - rgba.G) * 60.0 / (max - min) + 240.0;
            }

            return new HsbaColor(hsbH, hsbS, hsbB, rgba.A / 255.0);
        }

        /// <summary> Hsba转Rgba </summary>
        internal static RgbaColor HsbaToRgba(HsbaColor hsba)
        {
            double r = 0, g = 0, b = 0;
            int i = (int)((hsba.H / 60) % 6);
            double f = (hsba.H / 60) - i;
            double p = hsba.B * (1 - hsba.S);
            double q = hsba.B * (1 - f * hsba.S);
            double t = hsba.B * (1 - (1 - f) * hsba.S);
            switch (i)
            {
                case 0:
                    r = hsba.B;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = hsba.B;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = hsba.B;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = hsba.B;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = hsba.B;
                    break;
                case 5:
                    r = hsba.B;
                    g = p;
                    b = q;
                    break;
                default:
                    break;
            }
            return new RgbaColor((int)(255.0 * r), (int)(255.0 * g), (int)(255.0 * b), (int)(255.0 * hsba.A));
        }

        /// <summary>/ 获取颜色亮度 </summary>
        internal static int GetBrightness(int r, int g, int b)
        {
            return (int)((0.2126 * r + 0.7152 * g + 0.0722 * b) / 2.55);
        }
    }
}
