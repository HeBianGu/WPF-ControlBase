
using HeBianGu.Base.WpfBase;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using System.Globalization;
using System.Windows.Media;

namespace HeBianGu.Control.SharpDx
{
    //public class Service : IService
    //{

    //}

    //public interface IService
    //{

    //}

    //[SettingConfig(Name = "参数设置", Group = "基本设置")]
    //public class Setting : LazySettingInstance<Setting>
    //{

    //}

    public static class D2dExtension
    {
        public static SharpDX.Direct2D1.SolidColorBrush ToSolidColorBrush(this System.Windows.Media.SolidColorBrush solidColorBrush, RenderTarget target)
        {
            return new SharpDX.Direct2D1.SolidColorBrush(target, new SharpDX.Mathematics.Interop.RawColor4((float)(solidColorBrush.Color.R / 255.0), (float)(solidColorBrush.Color.G / 255.0), (float)(solidColorBrush.Color.B / 255.0), (float)solidColorBrush.Opacity));
        }

        public static SharpDX.Direct2D1.SolidColorBrush ToSolidColorBrush(this System.Windows.Media.Brush brush, RenderTarget target)
        {
            if (brush is System.Windows.Media.SolidColorBrush solidColorBrush)
                return solidColorBrush.ToSolidColorBrush(target);
            return null;
        }


        public static SharpDX.Mathematics.Interop.RawVector2 ToPoint(this System.Windows.Point point)
        {
            return new SharpDX.Mathematics.Interop.RawVector2() { X = (float)point.X, Y = (float)point.Y };
        }

        public static SharpDX.Mathematics.Interop.RawRectangleF ToRawRectangleF(this System.Windows.Rect rect)
        {
            return new SharpDX.Mathematics.Interop.RawRectangleF((float)rect.Left, (float)rect.Top, (float)rect.Right, (float)rect.Bottom);
        }

        //public static SharpDX.Mathematics.Interop.RawRectangleF ToEllipse(this System.Windows.Media.Geometry geo)
        //{
        //    //return new SharpDX.Direct2D1.Geometry();
        //}

        public static SharpDX.Mathematics.Interop.RawMatrix3x2 ToRawMatrix3x2(this System.Windows.Media.Matrix matrix)
        {
            return new SharpDX.Mathematics.Interop.RawMatrix3x2((float)matrix.M11, (float)matrix.M12, (float)matrix.M21, (float)matrix.M22, (float)matrix.OffsetX, (float)matrix.OffsetY);
        }
    }
}
