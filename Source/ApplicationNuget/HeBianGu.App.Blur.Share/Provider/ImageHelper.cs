using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace HeBianGu.App.Blur
{
    public static class ImageHelper
    {
        public static IList<string> GetPaths()
        {
            IList<string> picturePaths = null;

            for (int i = 0; i < s_defaultPicturePaths.Length; i++)
            {
                picturePaths = GetPaths(s_defaultPicturePaths[i]);
                if (picturePaths.Count > 0)
                {
                    break;
                }
            }

            return picturePaths;
        }

        internal static IList<string> GetPaths(string sourceDirectory)
        {
            if (!string.IsNullOrEmpty(sourceDirectory))
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(sourceDirectory);
                    if (di.Exists)
                    {
                        List<string> imagePaths = new List<string>();
                        foreach (string imagePath in GetImageFiles(di))
                        {
                            imagePaths.Add(imagePath);
                            if (imagePaths.Count > c_maxImageReturnCount)
                            {
                                break;
                            }
                        }

                        return imagePaths;
                    }
                }
                catch (IOException)
                {
                }
                catch (ArgumentException)
                {
                }
                catch (SecurityException)
                {
                }
            }
            return new string[0];
        }

        internal static IEnumerable<string> GetImageFiles(DirectoryInfo directory)
        {
            foreach (FileInfo image in c_defaultImageSearchPattern.Split(':').SelectMany(s => directory.GetFiles(s)))
            {
                yield return image.FullName;
            }
            foreach (DirectoryInfo subDir in directory.GetDirectories())
            {
                foreach (string subDirImage in GetImageFiles(subDir))
                {
                    yield return subDirImage;
                }
            }
        }

        public static IList<BitmapImage> GetBitmapImages(int maxCount)
        {
            IList<string> imagePaths = GetPaths();
            if (maxCount < 0)
            {
                maxCount = imagePaths.Count;
            }

            BitmapImage[] images = new BitmapImage[Math.Min(imagePaths.Count, maxCount)];
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = new BitmapImage(new Uri(imagePaths[i]));
            }
            return images;
        }

        private static readonly string[] s_defaultPicturePaths = GetDefaultPicturePaths();

        private static string[] GetDefaultPicturePaths()
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                return new string[0];
            }
            return new[]
            {
                Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), @"Pictures\Sample Pictures\"),
                Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), @"Web\Screen")
            };
        }

        private const string c_defaultImageSearchPattern = @"*.jpg:*.png";
        private const int c_maxImageReturnCount = 50;
    }
}