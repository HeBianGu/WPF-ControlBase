// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Runtime.InteropServices;

namespace HeBianGu.Control.Gif
{
    [StructLayout(LayoutKind.Sequential)]
    public class GifPropertyItemInternal : IDisposable
    {
        public int id;
        public int len;
        public short type;
        public IntPtr value = IntPtr.Zero;

        internal GifPropertyItemInternal()
        {
        }

        public byte[] Value
        {
            get
            {
                if (len == 0)
                    return null;

                byte[] bytes = new byte[len];

                Marshal.Copy(value,
                    bytes,
                    0,
                    len);
                return bytes;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        ~GifPropertyItemInternal()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (value != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(value);
                value = IntPtr.Zero;
            }

            if (disposing)
                GC.SuppressFinalize(this);
        }

        internal static GifPropertyItemInternal ConvertFromPropertyItem(GifPropertyItem propItem)
        {
            GifPropertyItemInternal propItemInternal = new GifPropertyItemInternal
            {
                id = propItem.Id,
                len = 0,
                type = propItem.Type
            };

            byte[] propItemValue = propItem.Value;
            if (propItemValue != null)
            {
                int length = propItemValue.Length;
                propItemInternal.len = length;
                propItemInternal.value = Marshal.AllocHGlobal(length);
                Marshal.Copy(propItemValue, 0, propItemInternal.value, length);
            }

            return propItemInternal;
        }

        internal static GifPropertyItem[] ConvertFromMemory(IntPtr propdata, int count)
        {
            GifPropertyItem[] props = new GifPropertyItem[count];

            for (int i = 0; i < count; i++)
            {
                GifPropertyItemInternal propcopy = null;
                try
                {
                    propcopy = (GifPropertyItemInternal)ExternDllHelper.PtrToStructure(propdata,
                        typeof(GifPropertyItemInternal));

                    props[i] = new GifPropertyItem
                    {
                        Id = propcopy.id,
                        Len = propcopy.len,
                        Type = propcopy.type,
                        Value = propcopy.Value
                    };

                    // this calls Marshal.Copy and creates a copy of the original memory into a byte array.

                    propcopy.value = IntPtr.Zero; // we dont actually own this memory so dont free it.
                }
                finally
                {
                    propcopy?.Dispose();
                }

                propdata = (IntPtr)((long)propdata + Marshal.SizeOf(typeof(GifPropertyItemInternal)));
            }

            return props;
        }
    }
}