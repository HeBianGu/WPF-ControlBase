// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> 颜色选择控件 </summary>
    public partial class MetroColorPicker : UserControl
    {

        public MetroColorPicker()
        {
            InitializeComponent();

            Initialize(Background == null ? new HsbaColor(0.0, 1.0, 1.0, 1.0) : new HsbaColor(Background));

        }

        public MetroColorPicker(string hexColor)
        {
            InitializeComponent();

            Initialize(new HsbaColor(hexColor));

        }

        public MetroColorPicker(int r, int g, int b, int a)
        {
            InitializeComponent();

            Initialize(new HsbaColor(r, g, b, a));
        }

        public event EventHandler ColorChange;

        //public Brush DefaultColor
        //{
        //    get { return defaultColor.SolidColorBrush; }
        //    set { defaultColor = new HsbaColor(value); }
        //}

        public HsbaColor CurrentColor
        {
            get { return (HsbaColor)GetValue(CurrentColorProperty); }
            set { SetValue(CurrentColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(HsbaColor), typeof(MetroColorPicker), new PropertyMetadata(default(HsbaColor), (d, e) =>
             {
                 MetroColorPicker control = d as MetroColorPicker;

                 if (control == null) return;

                 HsbaColor config = e.NewValue as HsbaColor;


             }));

        //private HsbaColor currentColor { get; set; }

        private RgbaColor currentRgbaColor { get; set; }

        private HsbaColor defaultColor { get; set; } = new HsbaColor(0.0, 0.0, 0.0, 1.0);

        private HsbaColor selectColor { get; set; } = new HsbaColor(0, 1.0, 1.0, 1.0);

        private bool canTransparent = true;

        public bool CanTransparent
        {
            get { return canTransparent; }
            set
            {
                canTransparent = value;

                alpha1.IsEnabled = rgbaA.IsEnabled = hsbaA.IsEnabled = value;

                if (!value)
                {
                    rgbaA.Text = "255";
                    hex.MaxLength = 7;
                }
                else
                {
                    hex.MaxLength = 9;
                }
            }
        }


        public Action ColorPickerClosed { get; set; }

        private void Initialize(HsbaColor hsbaColor)
        {

            // 设置当前初始颜色
            CurrentColor = hsbaColor;

            // defaultColor = currentColor;

            // 界面初始化
            viewDefColor.Background = defaultColor.SolidColorBrush;
            viewLine1.Offset = 1.0 / 6.0 * 0.0;
            viewLine2.Offset = 1.0 / 6.0 * 1.0;
            viewLine3.Offset = 1.0 / 6.0 * 2.0;
            viewLine4.Offset = 1.0 / 6.0 * 3.0;
            viewLine5.Offset = 1.0 / 6.0 * 4.0;
            viewLine6.Offset = 1.0 / 6.0 * 5.0;
            viewLine7.Offset = 1.0 / 6.0 * 6.0;

            // 按钮事件
            //bool start = true;

            button.Click += delegate
            {
                polygon.Margin = new Thickness(ActualWidth / 2 - 5, -5.0, 0.0, 0.0);

                popup.IsOpen = true;

                //if (CurrentColor.Color != ((SolidColorBrush)this.Background).Color)
                //{
                //    ApplyColor(new HsbaColor(this.Background));

                //    //start = false;
                //}

                ApplyColor(new HsbaColor(this.Background));
            };


            popup.Closed += delegate { if (ColorPickerClosed != null) ColorPickerClosed(); };

            viewDefColor.Click += delegate { ApplyColor(new HsbaColor(viewDefColor.Background)); };

            //hex.ButtonClick += delegate { Clipboard.SetText(hex.Text); };

            // 视图被改变事件
            thumbSB.ValueChange += delegate { if (thumbSB.IsDragging) ViewChange(); };
            thumbH.ValueChange += delegate { if (thumbH.IsDragging) ViewChange(); };
            thumbA.ValueChange += delegate { if (thumbA.IsDragging) ViewChange(); };

            // RGBA操作事件
            rgbaR.TextChanged += delegate { if (rgbaR.IsSelectionActive) RgbaChange(); };
            rgbaG.TextChanged += delegate { if (rgbaG.IsSelectionActive) RgbaChange(); };
            rgbaB.TextChanged += delegate { if (rgbaB.IsSelectionActive) RgbaChange(); };
            rgbaA.TextChanged += delegate { if (rgbaA.IsSelectionActive) RgbaChange(); };

            // HSBA操作事件
            hsbaH.TextChanged += delegate { if (hsbaH.IsSelectionActive) HsbaChange(); };
            hsbaS.TextChanged += delegate { if (hsbaS.IsSelectionActive) HsbaChange(); };
            hsbaB.TextChanged += delegate { if (hsbaB.IsSelectionActive) HsbaChange(); };
            hsbaA.TextChanged += delegate { if (hsbaA.IsSelectionActive) HsbaChange(); };

            // HEX操作事件
            hex.TextChanged += delegate { if (hex.IsSelectionActive) HexChange(); };
        }

        private void ViewChange()
        {
            ApplyColor(new HsbaColor(360.0 * thumbH.YPercent, thumbSB.XPercent, Math.Abs(1 - thumbSB.YPercent), Math.Abs(1 - thumbA.YPercent)));
        }

        private void RgbaChange()
        {
            double doubleRgbaR = ConvertDouble(rgbaR.Text);
            double doubleRgbaG = ConvertDouble(rgbaG.Text);
            double doubleRgbaB = ConvertDouble(rgbaB.Text);
            double doubleRgbaA = ConvertDouble(rgbaA.Text);
            if (doubleRgbaR > 255) { rgbaR.Text = "255"; } else if (doubleRgbaR == -1) { rgbaR.Text = "0"; };
            if (doubleRgbaG > 255) { rgbaG.Text = "255"; } else if (doubleRgbaG == -1) { rgbaG.Text = "0"; };
            if (doubleRgbaB > 255) { rgbaB.Text = "255"; } else if (doubleRgbaB == -1) { rgbaB.Text = "0"; };
            if (doubleRgbaA > 255) { rgbaA.Text = "255"; } else if (doubleRgbaA == -1) { rgbaA.Text = "0"; };

            ApplyColor(new HsbaColor(ConvertInt(rgbaR.Text), ConvertInt(rgbaG.Text), ConvertInt(rgbaB.Text), ConvertInt(rgbaA.Text)));
        }

        private void HsbaChange()
        {
            double doubleHsbaH = ConvertDouble(hsbaH.Text);
            double doubleHsbaS = ConvertDouble(hsbaS.Text);
            double doubleHsbaB = ConvertDouble(hsbaB.Text);
            double doubleHsbaA = ConvertDouble(hsbaA.Text);
            if (doubleHsbaH > 100) { hsbaH.Text = "100"; } else if (doubleHsbaH == -1) { hsbaH.Text = "0"; };
            if (doubleHsbaS > 100) { hsbaS.Text = "100"; } else if (doubleHsbaS == -1) { hsbaS.Text = "0"; };
            if (doubleHsbaB > 100) { hsbaB.Text = "100"; } else if (doubleHsbaB == -1) { hsbaB.Text = "0"; };
            if (doubleHsbaA > 100) { hsbaA.Text = "100"; } else if (doubleHsbaA == -1) { hsbaA.Text = "0"; };

            ApplyColor(new HsbaColor(ConvertDouble(hsbaH.Text) * 3.6, ConvertDouble(hsbaS.Text) / 100.0, ConvertDouble(hsbaB.Text) / 100.0, ConvertDouble(hsbaA.Text) / 100.0));
        }

        private void HexChange()
        {
            ApplyColor(new HsbaColor(hex.Text));
        }

        private bool _isrefreshing = false;

        private void ApplyColor(HsbaColor hsba)
        {
            if (_isrefreshing == true)
                return;

            _isrefreshing = true;

            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
                       {
                           CurrentColor = hsba;
                           currentRgbaColor = CurrentColor.RgbaColor;

                           if (!rgbaR.IsSelectionActive) { rgbaR.Text = currentRgbaColor.R.ToString(); }
                           if (!rgbaG.IsSelectionActive) { rgbaG.Text = currentRgbaColor.G.ToString(); }
                           if (!rgbaB.IsSelectionActive) { rgbaB.Text = currentRgbaColor.B.ToString(); }
                           if (!rgbaA.IsSelectionActive) { rgbaA.Text = currentRgbaColor.A.ToString(); }

                           if (!hsbaH.IsSelectionActive) { hsbaH.Text = ((int)(CurrentColor.H / 3.6)).ToString(); }
                           if (!hsbaS.IsSelectionActive) { hsbaS.Text = ((int)(CurrentColor.S * 100)).ToString(); }
                           if (!hsbaB.IsSelectionActive) { hsbaB.Text = ((int)(CurrentColor.B * 100)).ToString(); }
                           if (!hsbaA.IsSelectionActive) { hsbaA.Text = ((int)(CurrentColor.A * 100)).ToString(); }

                           if (!hex.IsSelectionActive) { if (canTransparent) { hex.Text = CurrentColor.HexString; } else { hex.Text = string.Format("#{0:X2}{1:X2}{2:X2}", currentRgbaColor.R, currentRgbaColor.G, currentRgbaColor.B); } }

                           if (!thumbH.IsDragging) { thumbH.YPercent = CurrentColor.H / 360.0; }
                           if (!thumbSB.IsDragging) { thumbSB.XPercent = CurrentColor.S; thumbSB.YPercent = 1 - CurrentColor.B; }
                           if (!thumbA.IsDragging) { thumbA.YPercent = Math.Abs(1 - CurrentColor.A); }

                           selectColor.H = CurrentColor.H;
                           selectColor.A = CurrentColor.A;
                           viewSelectColor.Fill = selectColor.OpaqueSolidColorBrush;
                           if (canTransparent)
                           {
                               viewSelectColor.Opacity = viewSelectColor1.Opacity = viewSelectColor2.Opacity = 1 - thumbA.YPercent;
                           }
                           viewAlpha.Color = selectColor.OpaqueColor;

                           if (canTransparent)
                           {
                               Background = CurrentColor.SolidColorBrush;
                           }
                           else
                           {
                               Background = CurrentColor.OpaqueSolidColorBrush;
                           }

                           ColorChange?.Invoke(this, null);

                           _isrefreshing = false;
                       }));
        }

        private double ConvertDouble(string text)
        {
            try
            {
                return Convert.ToDouble(text);
            }
            catch
            {
                return -1;
            }
        }

        private int ConvertInt(string text)
        {
            try
            {
                return Convert.ToInt32(text);
            }
            catch
            {
                return -1;
            }
        }
    }

    //public class HsbaColor
    //{
    //    double h = 0, s = 0, b = 0, a = 0;
    //    /// <summary> 0 - 359，360 = 0  </summary>
    //    public double H { get { return h; } set { h = value < 0 ? 0 : value >= 360 ? 0 : value; } }

    //    /// <summary> 0 - 1 </summary>
    //    public double S { get { return s; } set { s = value < 0 ? 0 : value > 1 ? 1 : value; } }

    //    /// <summary> 0 - 1 </summary>
    //    public double B { get { return b; } set { b = value < 0 ? 0 : value > 1 ? 1 : value; } }

    //    /// <summary> 0 - 1 </summary>
    //    public double A { get { return a; } set { a = value < 0 ? 0 : value > 1 ? 1 : value; } }

    //    /// <summary> 亮度 0 - 100 </summary>
    //    public int Y { get { return RgbaColor.Y; } }

    //    public HsbaColor() { H = 0; S = 0; B = 1; A = 1; }

    //    public HsbaColor(double h, double s, double b, double a = 1) { H = h; S = s; B = b; A = a; }

    //    public HsbaColor(int r, int g, int b, int a = 255)
    //    {
    //        HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(r, g, b, a));
    //        H = hsba.H;
    //        S = hsba.S;
    //        B = hsba.B;
    //        A = hsba.A;
    //    }

    //    public HsbaColor(Brush brush)
    //    {
    //        HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(brush));
    //        H = hsba.H;
    //        S = hsba.S;
    //        B = hsba.B;
    //        A = hsba.A;
    //    }

    //    public HsbaColor(string hexColor)
    //    {
    //        HsbaColor hsba = Utility.RgbaToHsba(new RgbaColor(hexColor));
    //        H = hsba.H;
    //        S = hsba.S;
    //        B = hsba.B;
    //        A = hsba.A;
    //    }

    //    public Color Color
    //    {
    //        get { return RgbaColor.Color; }
    //    }

    //    public Color OpaqueColor
    //    {
    //        get { return RgbaColor.OpaqueColor; }
    //    }
    //    public SolidColorBrush SolidColorBrush
    //    {
    //        get { return RgbaColor.SolidColorBrush; }
    //    }

    //    public SolidColorBrush OpaqueSolidColorBrush
    //    {
    //        get { return RgbaColor.OpaqueSolidColorBrush; }
    //    }

    //    public string HexString
    //    {
    //        get { return Color.ToString(); }
    //    }
    //    public string RgbaString
    //    {
    //        get { return RgbaColor.RgbaString; }
    //    }

    //    public RgbaColor RgbaColor
    //    {
    //        get { return Utility.HsbaToRgba(this); }
    //    }
    //}

    //public class RgbaColor
    //{
    //    int r = 0, g = 0, b = 0, a = 0;
    //    /// <summary> 0 - 255 </summary>
    //    public int R { get { return r; } set { r = value < 0 ? 0 : value > 255 ? 255 : value; } }

    //    /// <summary> 0 - 255 </summary>
    //    public int G { get { return g; } set { g = value < 0 ? 0 : value > 255 ? 255 : value; } }

    //    /// <summary> 0 - 255 </summary>
    //    public int B { get { return b; } set { b = value < 0 ? 0 : value > 255 ? 255 : value; } }

    //    /// <summary> 0 - 255 </summary>
    //    public int A { get { return a; } set { a = value < 0 ? 0 : value > 255 ? 255 : value; } }

    //    /// <summary> 亮度 0 - 100 </summary>
    //    public int Y { get { return Utility.GetBrightness(R, G, B); } }

    //    public RgbaColor() { R = 255; G = 255; B = 255; A = 255; }

    //    public RgbaColor(int r, int g, int b, int a = 255) { R = r; G = g; B = b; A = a; }

    //    public RgbaColor(Brush brush)
    //    {
    //        if (brush != null)
    //        {
    //            R = ((SolidColorBrush)brush).Color.R;
    //            G = ((SolidColorBrush)brush).Color.G;
    //            B = ((SolidColorBrush)brush).Color.B;
    //            A = ((SolidColorBrush)brush).Color.A;
    //        }
    //        else
    //        {
    //            R = G = B = A = 255;
    //        }
    //    }

    //    public RgbaColor(double h, double s, double b, double a = 1)
    //    {
    //        RgbaColor rgba = Utility.HsbaToRgba(new HsbaColor(h, s, b, a));
    //        R = rgba.R;
    //        G = rgba.G;
    //        B = rgba.B;
    //        A = rgba.A;

    //    }

    //    public RgbaColor(string hexColor)
    //    {
    //        try
    //        {
    //            Color color;
    //            if (hexColor.Substring(0, 1) == "#") color = (Color)ColorConverter.ConvertFromString(hexColor);
    //            else color = (Color)ColorConverter.ConvertFromString("#" + hexColor);
    //            R = color.R;
    //            G = color.G;
    //            B = color.B;
    //            A = color.A;
    //        }
    //        catch
    //        {

    //        }
    //    }

    //    public Color Color { get { return Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B); } }

    //    public Color OpaqueColor { get { return Color.FromArgb((byte)255.0, (byte)R, (byte)G, (byte)B); } }

    //    public SolidColorBrush SolidColorBrush { get { return new SolidColorBrush(Color); } }

    //    public SolidColorBrush OpaqueSolidColorBrush { get { return new SolidColorBrush(OpaqueColor); } }

    //    public string HexString { get { return Color.ToString(); } }

    //    public string RgbaString { get { return R + "," + G + "," + B + "," + A; } }

    //    public HsbaColor HsbaColor { get { return Utility.RgbaToHsba(this); } }
    //}

    //internal class Utility
    //{
    //    /// <summary> Rgba转Hsba/ </summary>
    //    internal static HsbaColor RgbaToHsba(RgbaColor rgba)
    //    {
    //        int[] rgb = new int[] { rgba.R, rgba.G, rgba.B };
    //        Array.Sort(rgb);
    //        int max = rgb[2];
    //        int min = rgb[0];

    //        double hsbB = max / 255.0;
    //        double hsbS = max == 0 ? 0 : (max - min) / (double)max;
    //        double hsbH = 0;

    //        if (rgba.R == rgba.G && rgba.R == rgba.B)
    //        {

    //        }
    //        else
    //        {
    //            if (max == rgba.R && rgba.G >= rgba.B) hsbH = (rgba.G - rgba.B) * 60.0 / (max - min) + 0.0;
    //            else if (max == rgba.R && rgba.G < rgba.B) hsbH = (rgba.G - rgba.B) * 60.0 / (max - min) + 360.0;
    //            else if (max == rgba.G) hsbH = (rgba.B - rgba.R) * 60.0 / (max - min) + 120.0;
    //            else if (max == rgba.B) hsbH = (rgba.R - rgba.G) * 60.0 / (max - min) + 240.0;
    //        }

    //        return new HsbaColor(hsbH, hsbS, hsbB, rgba.A / 255.0);
    //    }

    //    /// <summary> Hsba转Rgba </summary>
    //    internal static RgbaColor HsbaToRgba(HsbaColor hsba)
    //    {
    //        double r = 0, g = 0, b = 0;
    //        int i = (int)((hsba.H / 60) % 6);
    //        double f = (hsba.H / 60) - i;
    //        double p = hsba.B * (1 - hsba.S);
    //        double q = hsba.B * (1 - f * hsba.S);
    //        double t = hsba.B * (1 - (1 - f) * hsba.S);
    //        switch (i)
    //        {
    //            case 0:
    //                r = hsba.B;
    //                g = t;
    //                b = p;
    //                break;
    //            case 1:
    //                r = q;
    //                g = hsba.B;
    //                b = p;
    //                break;
    //            case 2:
    //                r = p;
    //                g = hsba.B;
    //                b = t;
    //                break;
    //            case 3:
    //                r = p;
    //                g = q;
    //                b = hsba.B;
    //                break;
    //            case 4:
    //                r = t;
    //                g = p;
    //                b = hsba.B;
    //                break;
    //            case 5:
    //                r = hsba.B;
    //                g = p;
    //                b = q;
    //                break;
    //            default:
    //                break;
    //        }
    //        return new RgbaColor((int)(255.0 * r), (int)(255.0 * g), (int)(255.0 * b), (int)(255.0 * hsba.A));
    //    }

    //    /// <summary>/ 获取颜色亮度 </summary>
    //    internal static int GetBrightness(int r, int g, int b)
    //    {
    //        return (int)((0.2126 * r + 0.7152 * g + 0.0722 * b) / 2.55);
    //    }
    //}


    public class MetroThumb : Thumb
    {
        public double OldX { get; set; } = 0.0;

        public double OldY { get; set; } = 0.0;

        public double OffsetX { get; set; } = 0.0;

        public double OffsetY { get; set; } = 0.0;

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.Register("X", typeof(double), typeof(MetroThumb), new PropertyMetadata(-1.0, (d, e) =>
             {
                 MetroThumb control = d as MetroThumb;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.Register("Y", typeof(double), typeof(MetroThumb), new PropertyMetadata(-1.0, (d, e) =>
             {
                 MetroThumb control = d as MetroThumb;

                 if (control == null) return;

                 //double config = e.NewValue as double;

             }));



        public double X { get { return (double)GetValue(XProperty) - OffsetX; } set { SetValue(XProperty, value + OffsetX); Change(); } }
        public double Y { get { return (double)GetValue(YProperty) - OffsetY; } set { SetValue(YProperty, value + OffsetY); Change(); } }

        public event EventHandler ValueChange;

        public MetroThumb()
        {
            Focusable = true;

            FocusVisualStyle = null;

            Loaded += delegate
            {
                X = (double)GetValue(XProperty) == -1 ? 0 : X;
                Y = (double)GetValue(YProperty) == -1 ? 0 : Y;
            };
            DragStarted += delegate (object sender, DragStartedEventArgs e)
            {
                Focus();

                OldX = e.HorizontalOffset;

                OldY = e.VerticalOffset;

                X = OldX;
                Y = OldY;
            };
            DragDelta += delegate (object sender, DragDeltaEventArgs e)
            {
                double x = OldX + e.HorizontalChange;

                double y = OldY + e.VerticalChange;

                if (x < 0) X = 0;

                else if (x > ActualWidth) X = ActualWidth;

                else X = x;

                if (y < 0) Y = 0;

                else if (y > ActualHeight) Y = ActualHeight;

                else Y = y;
            };
        }

        public double XPercent
        {
            get { return X / ActualWidth; }
            set { X = ActualWidth * value; }
        }
        public double YPercent
        {
            get { return Y / ActualHeight; }
            set { Y = ActualHeight * value; }
        }

        private void Change()
        {
            if (ValueChange != null) { ValueChange(this, null); }
        }
    }

}