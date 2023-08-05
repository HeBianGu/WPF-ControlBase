using System;
using System.Collections.Generic;


namespace System.Windows.Media.Imaging
{
    /// <summary>
    /// Read Write Mode for the BitmapContext.
    /// </summary>
    public enum ReadWriteMode
    {
        /// <summary>
        /// On Dispose of a BitmapContext, do not Invalidate
        /// </summary>
        ReadOnly,

        /// <summary>
        /// On Dispose of a BitmapContext, invalidate the bitmap
        /// </summary>
        ReadWrite
    }

    /// <summary>
    /// A disposable cross-platform wrapper around a WriteableBitmap, allowing a common API for Silverlight + WPF with locking + unlocking if necessary
    /// </summary>
    /// <remarks>Attempting to put as many preprocessor hacks in this file, to keep the rest of the codebase relatively clean</remarks>
    public unsafe struct BitmapContext : IDisposable
    {
        private readonly WriteableBitmap _writeableBitmap;
        private readonly ReadWriteMode _mode;

        private readonly int _pixelWidth;
        private readonly int _pixelHeight;

      private readonly static IDictionary<WriteableBitmap, int> UpdateCountByBmp = new System.Collections.Concurrent.ConcurrentDictionary<WriteableBitmap, int>();

      private readonly int _length;
      private readonly int* _backBuffer;


        /// <summary>
        /// The Bitmap
        /// </summary>
        public WriteableBitmap WriteableBitmap { get { return _writeableBitmap; } }

        /// <summary>
        /// Width of the bitmap
        /// </summary>
        public int Width { get { return _writeableBitmap.PixelWidth; } }

        /// <summary>
        /// Height of the bitmap
        /// </summary>
        public int Height { get { return _writeableBitmap.PixelHeight; } }

        /// <summary>
        /// Creates an instance of a BitmapContext, with default mode = ReadWrite
        /// </summary>
        /// <param name="writeableBitmap"></param>
        public BitmapContext(WriteableBitmap writeableBitmap)
            : this(writeableBitmap, ReadWriteMode.ReadWrite)
        {
        }

        /// <summary>
        /// Creates an instance of a BitmapContext, with specified ReadWriteMode
        /// </summary>
        /// <param name="writeableBitmap"></param>
        /// <param name="mode"></param>
        public BitmapContext(WriteableBitmap writeableBitmap, ReadWriteMode mode)
        {
            _writeableBitmap = writeableBitmap;
            _mode = mode;

            _pixelWidth = _writeableBitmap.PixelWidth;
            _pixelHeight = _writeableBitmap.PixelHeight;

         //// Check if it's the Pbgra32 pixel format
         //if (writeableBitmap.Format != PixelFormats.Pbgra32)
         //{
         //   throw new ArgumentException("The input WriteableBitmap needs to have the Pbgra32 pixel format. Use the BitmapFactory.ConvertToPbgra32Format method to automatically convert any input BitmapSource to the right format accepted by this class.", "writeableBitmap");
         //}

            double width = _writeableBitmap.BackBufferStride / WriteableBitmapExtensions.SizeOfArgb;
            _length = (int)(width * _pixelHeight);

            lock (UpdateCountByBmp)
            {
         // Ensure the bitmap is in the dictionary of mapped Instances
         if (!UpdateCountByBmp.ContainsKey(writeableBitmap))
         {
            // Set UpdateCount to 1 for this bitmap 
            UpdateCountByBmp.Add(writeableBitmap, 1);

            // Lock the bitmap
            writeableBitmap.Lock();
         }
         else
         {
            // For previously contextualized bitmaps increment the update count
            IncrementRefCount(writeableBitmap);
         }
            }

            _backBuffer = (int*)writeableBitmap.BackBuffer;

        }


      /// <summary>
      /// The pixels as ARGB integer values, where each channel is 8 bit.
      /// </summary>
      public unsafe int* Pixels
      {
         [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
         get { return _backBuffer; }
      }

      /// <summary>
      /// The pixel format
      /// </summary>
      public PixelFormat Format
      {
          [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
          get { return _writeableBitmap.Format; }
      }


      /// <summary>
      /// The number of pixels.
      /// </summary>
      public int Length
      {
         [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
         get
         {
            return _length;
         }
      }

      /// <summary>
      /// Performs a Copy operation from source to destination BitmapContext
      /// </summary>
      /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
      [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
      public static unsafe void BlockCopy(BitmapContext src, int srcOffset, BitmapContext dest, int destOffset, int count)
      {
            NativeMethods.CopyUnmanagedMemory((byte*)src.Pixels, srcOffset, (byte*)dest.Pixels, destOffset, count);
      }

      /// <summary>
      /// Performs a Copy operation from source Array to destination BitmapContext
      /// </summary>
      /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
      [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
      public static unsafe void BlockCopy(int[] src, int srcOffset, BitmapContext dest, int destOffset, int count)
      {
         fixed (int* srcPtr = src)
         {
            NativeMethods.CopyUnmanagedMemory((byte*)srcPtr, srcOffset, (byte*)dest.Pixels, destOffset, count);
         }
      }

      /// <summary>
      /// Performs a Copy operation from source Array to destination BitmapContext
      /// </summary>
      /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
      [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
      public static unsafe void BlockCopy(byte[] src, int srcOffset, BitmapContext dest, int destOffset, int count)
      {
         fixed (byte* srcPtr = src)
         {
            NativeMethods.CopyUnmanagedMemory(srcPtr, srcOffset, (byte*)dest.Pixels, destOffset, count);
         }
      }

      /// <summary>
      /// Performs a Copy operation from source BitmapContext to destination Array
      /// </summary>
      /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
      [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
      public static unsafe void BlockCopy(BitmapContext src, int srcOffset, byte[] dest, int destOffset, int count)
      {
         fixed (byte* destPtr = dest)
         {
            NativeMethods.CopyUnmanagedMemory((byte*)src.Pixels, srcOffset, destPtr, destOffset, count);
         }
      }

      /// <summary>
      /// Performs a Copy operation from source BitmapContext to destination Array
      /// </summary>
      /// <remarks>Equivalent to calling Buffer.BlockCopy in Silverlight, or native memcpy in WPF</remarks>
      [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
      public static unsafe void BlockCopy(BitmapContext src, int srcOffset, int[] dest, int destOffset, int count)
      {
         fixed (int* destPtr = dest)
         {
            NativeMethods.CopyUnmanagedMemory((byte*)src.Pixels, srcOffset, (byte*)destPtr, destOffset, count);
         }
      }

      /// <summary>
      /// Clears the BitmapContext, filling the underlying bitmap with zeros
      /// </summary>
      [System.Runtime.TargetedPatchingOptOut("Candidate for inlining across NGen boundaries for performance reasons")]
      public void Clear()
      {
         NativeMethods.SetUnmanagedMemory(_writeableBitmap.BackBuffer, 0, _writeableBitmap.BackBufferStride * _writeableBitmap.PixelHeight);
      }

      /// <summary>
      /// Disposes the BitmapContext, unlocking it and invalidating if WPF
      /// </summary>
      public void Dispose()
      {
         // Decrement the update count. If it hits zero
         if (DecrementRefCount(_writeableBitmap) == 0)
         {
            // Remove this bitmap from the update map 
            UpdateCountByBmp.Remove(_writeableBitmap);

            // Invalidate the bitmap if ReadWrite _mode
            if (_mode == ReadWriteMode.ReadWrite)
            {
               _writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, _writeableBitmap.PixelWidth, _writeableBitmap.PixelHeight));
            }

            // Unlock the bitmap
            _writeableBitmap.Unlock();
         }
      }


        private static void IncrementRefCount(WriteableBitmap target)
        {
            UpdateCountByBmp[target]++;
        }

        private static int DecrementRefCount(WriteableBitmap target)
        {
            int current;
            if (!UpdateCountByBmp.TryGetValue(target, out current))
            {
                return -1;
            }
            current--;
            UpdateCountByBmp[target] = current;
            return current;
        }
    }
}