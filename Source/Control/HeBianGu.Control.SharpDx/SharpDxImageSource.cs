using SharpDX.Direct3D9;
using System;
using System.Threading;
using System.Windows.Interop;

namespace HeBianGu.Control.SharpDx
{
    class SharpDxImageSource : D3DImage, IDisposable
    {
        private static int ActiveClients;
        private static Direct3DEx D3DContext;
        private static DeviceEx D3DDevice;
        private Texture _texture;
        public int RenderWait { get; set; } = 2; // default: 2ms
        public SharpDxImageSource()
        {
            if (SharpDxImageSource.ActiveClients == 0)
            {
                var presentParams = GetPresentParameters();
                var createFlags = CreateFlags.HardwareVertexProcessing | CreateFlags.Multithreaded | CreateFlags.FpuPreserve;
                SharpDxImageSource.D3DContext = new Direct3DEx();
                SharpDxImageSource.D3DDevice = new DeviceEx(D3DContext, 0, DeviceType.Hardware, IntPtr.Zero, createFlags, presentParams);
            }
            SharpDxImageSource.ActiveClients++;
        }

        public void Dispose()
        {
            SetRenderTarget(null);
            Disposer.SafeDispose(ref _texture);
            SharpDxImageSource.ActiveClients--;

            if (SharpDxImageSource.ActiveClients == 0)
            {
                Disposer.SafeDispose(ref _texture);
                Disposer.SafeDispose(ref SharpDxImageSource.D3DDevice);
                Disposer.SafeDispose(ref SharpDxImageSource.D3DContext);
            }
        }

        public void InvalidateD3DImage()
        {
            if (_texture != null)
            {
                base.Lock();
                if (RenderWait != 0)
                {
                    Thread.Sleep(RenderWait);
                }
                base.AddDirtyRect(new System.Windows.Int32Rect(0, 0, base.PixelWidth, base.PixelHeight));
                base.Unlock();
            }
        }

        public void SetRenderTarget(SharpDX.Direct3D11.Texture2D target)
        {
            if (_texture != null)
            {
                _texture = null;
                base.Lock();
                base.SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
                base.Unlock();
            }

            if (target == null)
                return;

            var format = SharpDxImageSource.TranslateFormat(target);
            var handle = GetSharedHandle(target);

            if (!IsShareable(target))
                throw new ArgumentException("Texture must be created with ResouceOptionFlags.Shared");

            if (format == Format.Unknown)
                throw new ArgumentException("Texture format is not compatible with OpenSharedResouce");

            if (handle == IntPtr.Zero)
                throw new ArgumentException("Invalid handle");

            _texture = new Texture(SharpDxImageSource.D3DDevice, target.Description.Width, target.Description.Height, 1, Usage.RenderTarget, format, Pool.Default, ref handle);

            using (var surface = _texture.GetSurfaceLevel(0))
            {
                base.Lock();
                base.SetBackBuffer(D3DResourceType.IDirect3DSurface9, surface.NativePointer);
                base.Unlock();
            }
        }

        private static void ResetD3D()
        {
            if (SharpDxImageSource.ActiveClients == 0)
                return;

            var presentParams = GetPresentParameters();
            SharpDxImageSource.D3DDevice.ResetEx(ref presentParams);
        }

        private static PresentParameters GetPresentParameters()
        {
            var presentParams = new PresentParameters();
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            presentParams.DeviceWindowHandle = NativeMethods.GetDesktopWindow();
            presentParams.PresentationInterval = PresentInterval.Default;

            return presentParams;
        }

        private IntPtr GetSharedHandle(SharpDX.Direct3D11.Texture2D texture)
        {
            using (var resource = texture.QueryInterface<SharpDX.DXGI.Resource>())
            {
                return resource.SharedHandle;
            }
        }

        private static Format TranslateFormat(SharpDX.Direct3D11.Texture2D texture)
        {
            switch (texture.Description.Format)
            {
                case SharpDX.DXGI.Format.R10G10B10A2_UNorm: return SharpDX.Direct3D9.Format.A2B10G10R10;
                case SharpDX.DXGI.Format.R16G16B16A16_Float: return SharpDX.Direct3D9.Format.A16B16G16R16F;
                case SharpDX.DXGI.Format.B8G8R8A8_UNorm: return SharpDX.Direct3D9.Format.A8R8G8B8;
                default: return SharpDX.Direct3D9.Format.Unknown;
            }
        }

        private static bool IsShareable(SharpDX.Direct3D11.Texture2D texture)
        {
            return (texture.Description.OptionFlags & SharpDX.Direct3D11.ResourceOptionFlags.Shared) != 0;
        }
    }
}
