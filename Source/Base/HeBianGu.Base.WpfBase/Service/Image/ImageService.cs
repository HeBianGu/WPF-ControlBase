// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
#if NETCOREAPP
using Microsoft.VisualBasic.FileIO;
#endif
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HeBianGu.Base.WpfBase
{
    public class ImageService
    {
        private static BitmapImage LoadBitmapImage(string filePath)
        {
            if (File.Exists(filePath))
                return null;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(filePath, UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private static BitmapImage ConvertToBitmap(byte[] bytes)
        {
            if (bytes == null || bytes.Count() == 0)
                return new BitmapImage();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        private static byte[] ConvertToBytes(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
                return new byte[0];
            byte[] buffer = null;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);
            memoryStream.Position = 0;
            if (memoryStream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(memoryStream))
                {
                    buffer = br.ReadBytes((int)memoryStream.Length);
                }
            }
            memoryStream.Close();
            return buffer;
        }
        private static BitmapImage ConvertToBitmap(BitmapSource bitmapSource)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);
            memoryStream.Position = 0;
            bImg.BeginInit();
            bImg.StreamSource = memoryStream;
            bImg.EndInit();
            memoryStream.Close();
            return bImg;
        }
        public static byte[] BitmapImageToBytes(BitmapImage bmp)
        {
            byte[] buffer = null;
            try
            {
                Stream stream = bmp.StreamSource;
                if (stream != null && stream.Length > 0)
                {
                    //很重要，因为Position经常位于Stream的末尾，导致下面读取到的长度为0。   
                    stream.Position = 0;
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        buffer = br.ReadBytes((int)stream.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return buffer;

        }

        public static string ByteToString(byte[] bytes)
        {
            if (bytes == null)
                return null;
            return Convert.ToBase64String(bytes);
        }

        public static string BitmapSourceToString(BitmapSource bmp)
        {
            return Convert.ToBase64String(ConvertToBytes(bmp));
        }

        public static BitmapSource StringToBitmapSource(string bmp)
        {
            byte[] byteArray = System.Convert.FromBase64String(bmp);

            return ConvertToBitmap(byteArray);
        }

        public static string FileToString(string filePath)
        {
            BitmapImage bmp = new BitmapImage(new Uri(filePath, UriKind.Absolute));
            return Convert.ToBase64String(ConvertToBytes(bmp));
        }

        public static BitmapImage StringToBitmapImage(string bmp)
        {
            byte[] byteArray = Convert.FromBase64String(bmp);

            return ConvertToBitmap(byteArray);
        }

        public static void SaveToFile(BitmapImage image, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }


#if NETCOREAPP
        public void DeleteToRecycleBin(string filePath)
        {
            FileSystem.DeleteFile(filePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }
#endif
        /// <summary>
        /// 切图
        /// </summary>
        /// <param name="bitmapSource">图源</param>
        /// <param name="cut">切割区域</param>
        /// <returns></returns>
        public static BitmapSource CutImage(BitmapSource bitmapSource, Int32Rect cut)
        {
            try
            {
                //计算Stride
                var stride = bitmapSource.Format.BitsPerPixel * cut.Width / 8;
                //声明字节数组
                byte[] data = new byte[cut.Height * stride];
                //调用CopyPixels
                bitmapSource.CopyPixels(cut, data, stride, 0);

                return BitmapSource.Create(cut.Width, cut.Height, 0, 0, PixelFormats.Bgr32, null, data, stride);

            }
            catch (Exception ex)
            {
                Logger.Instance?.Error(ex);
                return null;
            }
        }

        // ImageSource --> Bitmap
        public static Bitmap ImageSourceToBitmap(ImageSource imageSource)
        {
            BitmapSource m = (BitmapSource)imageSource;
            Bitmap bmp = new Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            BitmapData data = bmp.LockBits(new Rectangle(System.Drawing.Point.Empty, bmp.Size), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride); bmp.UnlockBits(data);
            return bmp;
        }

        // Bitmap --> BitmapImage
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap, Action<BitmapImage> option = null)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                option?.Invoke(result);
                result.EndInit();
                result.Freeze();

                return result;
            }
        }
    }
}
