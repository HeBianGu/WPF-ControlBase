#region Header
//
//   Project:           WriteableBitmapEx - WriteableBitmap extensions
//   Description:       Collection of extension methods for the WriteableBitmap class.
//
//   Changed by:        $Author: unknown $
//   Changed on:        $Date: 2015-03-05 18:18:24 +0100 (Do, 05 Mrz 2015) $
//   Changed in:        $Revision: 113191 $
//   Project:           $URL: https://writeablebitmapex.svn.codeplex.com/svn/trunk/Source/WriteableBitmapEx/BitmapFactory.cs $
//   Id:                $Id: BitmapFactory.cs 113191 2015-03-05 17:18:24Z unknown $
//
//
//   Copyright © 2009-2015 Rene Schulte and WriteableBitmapEx Contributors
//
//   This code is open source. Please read the License.txt for details. No worries, we won't sue you! ;)
//
#endregion

using System;
using System.IO;
using System.Reflection;

#if NETFX_CORE
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Windows.UI.Xaml.Media.Imaging
#else
namespace System.Windows.Media.Imaging
#endif
{
    /// <summary>
    /// Cross-platform factory for WriteableBitmaps
    /// </summary>
    public static class BitmapFactory
    {
        /// <summary>
        /// Creates a new WriteableBitmap of the specified width and height
        /// </summary>
        /// <remarks>For WPF the default DPI is 96x96 and PixelFormat is Pbgra32</remarks>
        /// <param name="pixelWidth"></param>
        /// <param name="pixelHeight"></param>
        /// <returns></returns>
        public static WriteableBitmap New(int pixelWidth, int pixelHeight)
        {
            if (pixelHeight < 1) pixelHeight = 1;
            if (pixelWidth < 1) pixelWidth = 1;


            return new WriteableBitmap(pixelWidth, pixelHeight, 96.0, 96.0, PixelFormats.Pbgra32, null);

        }


        /// <summary>
        /// Converts the input BitmapSource to the Pbgra32 format WriteableBitmap which is internally used by the WriteableBitmapEx.
        /// </summary>
        /// <param name="source">The source bitmap.</param>
        /// <returns></returns>
        public static WriteableBitmap ConvertToPbgra32Format(BitmapSource source)
        {
            // Convert to Pbgra32 if it's a different format
            if (source.Format == PixelFormats.Pbgra32)
            {
                return new WriteableBitmap(source);
            }

            var formatedBitmapSource = new FormatConvertedBitmap();
            formatedBitmapSource.BeginInit();
            formatedBitmapSource.Source = source;
            formatedBitmapSource.DestinationFormat = PixelFormats.Pbgra32;
            formatedBitmapSource.EndInit();
            return new WriteableBitmap(formatedBitmapSource);
        }

        /// <summary>
        /// Loads an image from the applications resource file and returns a new WriteableBitmap.
        /// </summary>
        /// <param name="relativePath">Only the relative path to the resource file. The assembly name is retrieved automatically.</param>
        /// <returns>A new WriteableBitmap containing the pixel data.</returns>
        public static WriteableBitmap FromResource(string relativePath)
        {
            var fullName = Assembly.GetCallingAssembly().FullName;
            var asmName = new AssemblyName(fullName).Name;
            return FromContent(asmName + ";component/" + relativePath);
        }

        /// <summary>
        /// Loads an image from the applications content and returns a new WriteableBitmap.
        /// </summary>
        /// <param name="relativePath">Only the relative path to the content file.</param>
        /// <returns>A new WriteableBitmap containing the pixel data.</returns>
        public static WriteableBitmap FromContent(string relativePath)
        {
            using (var bmpStream = Application.GetResourceStream(new Uri(relativePath, UriKind.Relative)).Stream)
            {
                return FromStream(bmpStream);
            }
        }

        /// <summary>
        /// Loads the data from an image stream and returns a new WriteableBitmap.
        /// </summary>
        /// <param name="stream">The stream with the image data.</param>
        /// <returns>A new WriteableBitmap containing the pixel data.</returns>
        public static WriteableBitmap FromStream(Stream stream)
        {
            var bmpi = new BitmapImage();

            bmpi.BeginInit();
            bmpi.CreateOptions = BitmapCreateOptions.None;
            bmpi.StreamSource = stream;
            bmpi.EndInit();

            var bmp = new WriteableBitmap(bmpi);
            bmpi.UriSource = null;
            return bmp;
        }

    }
}