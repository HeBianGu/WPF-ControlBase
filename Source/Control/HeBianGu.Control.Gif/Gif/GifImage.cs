// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace HeBianGu.Control.Gif
{
    public class GifImage : Image, IDisposable
    {
        private static readonly Guid GifGuid = new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}");

        internal IntPtr NativeImage;

        private byte[] _rawData;

        private bool _isStart;

        private bool _isLoaded;

        static GifImage()
        {
            VisibilityProperty.OverrideMetadata(typeof(GifImage), new PropertyMetadata(default(Visibility), OnVisibilityChanged));
        }

        private static void OnVisibilityChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            GifImage ctl = (GifImage)s;
            Visibility v = (Visibility)e.NewValue;
            if (v != Visibility.Visible)
            {
                ctl.StopAnimate();
            }
            else if (!ctl._isStart)
            {
                ctl.StartAnimate();
            }
        }

        ~GifImage() => Dispose(false);

        public GifImage()
        {
            Loaded += (s, e) =>
            {
                if (DesignerProperties.GetIsInDesignMode(this)) return;
                if (_isLoaded) return;
                _isLoaded = true;
                if (Source is BitmapImage image)
                {
                    Uri uri = image.UriSource;
                    GetGifStreamFromPack(uri);
                    StartAnimate();
                }
            };
        }

        public GifImage(string filename)
        {
            Source = null;
            CreateSourceFromFile(filename);
            StartAnimate();
        }

        public GifImage(Stream stream)
        {
            Source = null;
            CreateSourceFromStream(stream);
            StartAnimate();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (NativeImage == IntPtr.Zero) return;
            try
            {
                StopAnimate();
                ExternDllHelper.Gdip.GdipDisposeImage(new HandleRef(this, NativeImage));
            }
            catch
            {
                // ignored
            }
            finally
            {
                NativeImage = IntPtr.Zero;
            }
        }

        private void CreateSourceFromFile(string filename)
        {
            filename = Path.GetFullPath(filename);

            int status = ExternDllHelper.Gdip.GdipCreateBitmapFromFile(filename, out IntPtr bitmap);

            if (status != ExternDllHelper.Gdip.Ok)
                throw ExternDllHelper.Gdip.StatusException(status);

            status = ExternDllHelper.Gdip.GdipImageForceValidation(new HandleRef(null, bitmap));

            if (status != ExternDllHelper.Gdip.Ok)
            {
                ExternDllHelper.Gdip.GdipDisposeImage(new HandleRef(null, bitmap));
                throw ExternDllHelper.Gdip.StatusException(status);
            }

            SetNativeImage(bitmap);

            EnsureSave(this, filename, null);
        }

        private void CreateSourceFromStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentException("stream null");

            int status = ExternDllHelper.Gdip.GdipCreateBitmapFromStream(new GPStream(stream), out IntPtr bitmap);

            if (status != ExternDllHelper.Gdip.Ok)
                throw ExternDllHelper.Gdip.StatusException(status);

            status = ExternDllHelper.Gdip.GdipImageForceValidation(new HandleRef(null, bitmap));

            if (status != ExternDllHelper.Gdip.Ok)
            {
                ExternDllHelper.Gdip.GdipDisposeImage(new HandleRef(null, bitmap));
                throw ExternDllHelper.Gdip.StatusException(status);
            }

            SetNativeImage(bitmap);

            EnsureSave(this, null, stream);
        }

        private void GetGifStreamFromPack(Uri uri)
        {
            try
            {
                StreamResourceInfo streamInfo;

                if (!uri.IsAbsoluteUri)
                {
                    streamInfo = Application.GetContentStream(uri) ?? Application.GetResourceStream(uri);
                }
                else
                {
                    if (uri.GetLeftPart(UriPartial.Authority).Contains("siteoforigin"))
                    {
                        streamInfo = Application.GetRemoteStream(uri);
                    }
                    else
                    {
                        streamInfo = Application.GetContentStream(uri) ?? Application.GetResourceStream(uri);
                    }
                }
                if (streamInfo == null)
                    throw new FileNotFoundException("Resource not found.", uri.ToString());
                CreateSourceFromStream(streamInfo.Stream);
            }
            catch
            {
                // ignored
            }
        }

        [ResourceExposure(ResourceScope.None)]
        [ResourceConsumption(ResourceScope.Machine, ResourceScope.Machine)]
        internal static void EnsureSave(GifImage image, string filename, Stream dataStream)
        {
            if (!image.RawGuid.Equals(GifGuid)) return;
            bool animatedGif = false;

            Guid[] dimensions = image.FrameDimensionsList;
            if (dimensions.Select(guid => new GifFrameDimension(guid)).Contains(GifFrameDimension.Time))
            {
                animatedGif = image.GetFrameCount(GifFrameDimension.Time) > 1;
            }

            if (!animatedGif) return;

            try
            {
                Stream created = null;
                long lastPos = 0;
                if (dataStream != null)
                {
                    lastPos = dataStream.Position;
                    dataStream.Position = 0;
                }

                try
                {
                    if (dataStream == null)
                    {
                        created = dataStream = File.OpenRead(filename);
                    }

                    image._rawData = new byte[(int)dataStream.Length];
                    dataStream.Read(image._rawData, 0, (int)dataStream.Length);
                }
                finally
                {
                    if (created != null)
                    {
                        created.Close();
                    }
                    else
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        dataStream.Position = lastPos;
                    }
                }
            }
            // possible exceptions for reading the filename
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (IOException)
            {
            }
            // possible exceptions for setting/getting the position inside dataStream
            catch (NotSupportedException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
            // possible exception when reading stuff into dataStream
            catch (ArgumentException)
            {
            }
        }

        internal void SetNativeImage(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                throw new ArgumentException("NativeHandle0");

            NativeImage = handle;
        }

        internal Guid RawGuid
        {
            get
            {
                Guid guid = new Guid();

                int status = ExternDllHelper.Gdip.GdipGetImageRawFormat(new HandleRef(this, NativeImage), ref guid);

                if (status != ExternDllHelper.Gdip.Ok)
                    throw ExternDllHelper.Gdip.StatusException(status);

                return guid;
            }
        }

        internal void StartAnimate()
        {
            ImageAnimator.Animate(this, OnFrameChanged);
            _isStart = true;
        }

        internal void StopAnimate()
        {
            ImageAnimator.StopAnimate(this, OnFrameChanged);
            _isStart = false;
        }

        internal Guid[] FrameDimensionsList
        {
            get
            {
                int status = ExternDllHelper.Gdip.GdipImageGetFrameDimensionsCount(new HandleRef(this, NativeImage), out int count);

                if (status != ExternDllHelper.Gdip.Ok)
                {
                    throw ExternDllHelper.Gdip.StatusException(status);
                }

                if (count <= 0)
                {
                    return new Guid[0];
                }

                int size = Marshal.SizeOf(typeof(Guid));

                IntPtr buffer = Marshal.AllocHGlobal(checked(size * count));
                if (buffer == IntPtr.Zero)
                {
                    throw ExternDllHelper.Gdip.StatusException(ExternDllHelper.Gdip.OutOfMemory);
                }

                status = ExternDllHelper.Gdip.GdipImageGetFrameDimensionsList(new HandleRef(this, NativeImage), buffer, count);

                if (status != ExternDllHelper.Gdip.Ok)
                {
                    Marshal.FreeHGlobal(buffer);
                    throw ExternDllHelper.Gdip.StatusException(status);
                }

                Guid[] guids = new Guid[count];

                try
                {
                    for (int i = 0; i < count; i++)
                    {
                        guids[i] = (Guid)ExternDllHelper.PtrToStructure((IntPtr)((long)buffer + size * i), typeof(Guid));
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }

                return guids;
            }
        }

        internal int GetFrameCount(GifFrameDimension dimension)
        {
            int[] count = new[] { 0 };

            Guid dimensionId = dimension.Guid;
            int status = ExternDllHelper.Gdip.GdipImageGetFrameCount(new HandleRef(this, NativeImage), ref dimensionId, count);

            if (status != ExternDllHelper.Gdip.Ok)
                throw ExternDllHelper.Gdip.StatusException(status);

            return count[0];
        }

        internal GifPropertyItem GetPropertyItem(int propid)
        {
            GifPropertyItem propitem;

            int status = ExternDllHelper.Gdip.GdipGetPropertyItemSize(new HandleRef(this, NativeImage), propid, out int size);

            if (status != ExternDllHelper.Gdip.Ok)
                throw ExternDllHelper.Gdip.StatusException(status);

            if (size == 0)
                return null;

            IntPtr propdata = Marshal.AllocHGlobal(size);

            if (propdata == IntPtr.Zero)
                throw ExternDllHelper.Gdip.StatusException(ExternDllHelper.Gdip.OutOfMemory);

            status = ExternDllHelper.Gdip.GdipGetPropertyItem(new HandleRef(this, NativeImage), propid, size, propdata);

            try
            {
                if (status != ExternDllHelper.Gdip.Ok)
                {
                    throw ExternDllHelper.Gdip.StatusException(status);
                }

                propitem = GifPropertyItemInternal.ConvertFromMemory(propdata, 1)[0];
            }
            finally
            {
                Marshal.FreeHGlobal(propdata);
            }

            return propitem;
        }

        internal int SelectActiveFrame(GifFrameDimension dimension, int frameIndex)
        {
            int[] count = new[] { 0 };

            Guid dimensionId = dimension.Guid;
            int status = ExternDllHelper.Gdip.GdipImageSelectActiveFrame(new HandleRef(this, NativeImage), ref dimensionId, frameIndex);

            if (status != ExternDllHelper.Gdip.Ok)
                throw ExternDllHelper.Gdip.StatusException(status);

            return count[0];
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        internal IntPtr GetHbitmap() => GetHbitmap(Colors.LightGray);

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        internal IntPtr GetHbitmap(Color background)
        {
            int status = ExternDllHelper.Gdip.GdipCreateHBITMAPFromBitmap(new HandleRef(this, NativeImage), out IntPtr hBitmap,
                ColorHelper.ToWin32(background));
            if (status == 2 && (Width >= short.MaxValue || Height >= short.MaxValue))
            {
                throw new ArgumentException("GdiplusInvalidSize");
            }

            if (status != ExternDllHelper.Gdip.Ok && NativeImage != IntPtr.Zero)
            {
                throw ExternDllHelper.Gdip.StatusException(status);
            }

            return hBitmap;
        }

        private void GetBitmapSource()
        {
            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = GetHbitmap();
                Source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                // ignored
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    ExternDllHelper.DeleteObject(handle);
                }
            }
        }

        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ImageAnimator.UpdateFrames();
                Source?.Freeze();
                GetBitmapSource();
                InvalidateVisual();
            }));
        }
    }

    internal class ColorHelper
    {
        private const int Win32RedShift = 0;
        private const int Win32GreenShift = 8;
        private const int Win32BlueShift = 16;

        public static int ToWin32(Color c) => c.R << Win32RedShift | c.G << Win32GreenShift | c.B << Win32BlueShift;

        public static Color ToColor(uint c)
        {
            byte[] bytes = BitConverter.GetBytes(c);
            return Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
        }
    }
}