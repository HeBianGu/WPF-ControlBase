// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace HeBianGu.Base.WpfBase
{
    public class ImageService
    {

        private static BitmapImage ConvertToBitmap(byte[] bytes)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.EndInit();
            return bitmapImage;
        }
        private static byte[] ConvertToBytes(BitmapSource bitmapSource)
        {
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
            if (bytes == null) return null;
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
    }
}
