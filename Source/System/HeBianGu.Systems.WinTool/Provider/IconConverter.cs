using HeBianGu.Base.WpfBase;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.Systems.WinTool
{
    public static class WinToolConverter
    {
        public static ConverterBase<string, ImageSource> GetFileImage => new ConverterBase<string, ImageSource>(x =>
        {
            var icon = IconHelper.Instance.GetFileIcon(x);
            if (icon == null) return null;

            ImageSource imageSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        });
    }
}
