using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Control.SharpDx
{
    public abstract class SharpDxImage : System.Windows.Controls.Image
    {
        private SharpDX.Direct3D11.Device _device;
        private Texture2D _texture2D;
        private SharpDxImageSource _dx11ImageSource;
        private RenderTarget _renderTarget;
        private SharpDX.Direct2D1.Factory _factory;
        public RenderTarget RenderTarget => _renderTarget;
        public SharpDxImage()
        {
            this.Loaded += (l, k) =>
            {
                _device = new SharpDX.Direct3D11.Device(DriverType.Hardware, DeviceCreationFlags.BgraSupport);
                _dx11ImageSource = new SharpDxImageSource();
                //d3DSurface.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;
                this.CreateAndBindTargets();
                base.Source = this._dx11ImageSource;
            };
            this.Unloaded += (l, k) =>
            {
                this.DisposeD3D();
            };
            base.Stretch = System.Windows.Media.Stretch.Fill;
        }


        public int RenderWait
        {
            get { return this._dx11ImageSource.RenderWait; }
            set { this._dx11ImageSource.RenderWait = value; }
        }


        public SharpDX.Direct2D1.AlphaMode AlphaMode
        {
            get { return (SharpDX.Direct2D1.AlphaMode)GetValue(AlphaModeProperty); }
            set { SetValue(AlphaModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlphaModeProperty =
            DependencyProperty.Register("AlphaMode", typeof(SharpDX.Direct2D1.AlphaMode), typeof(SharpDxImage), new FrameworkPropertyMetadata(SharpDX.Direct2D1.AlphaMode.Premultiplied, (d, e) =>
            {
                SharpDxImage control = d as SharpDxImage;

                if (control == null) return;

                if (e.OldValue is SharpDX.Direct2D1.AlphaMode o)
                {

                }

                if (e.NewValue is SharpDX.Direct2D1.AlphaMode n)
                {

                }

            }));


        public Format RenderTargetFormat
        {
            get { return (Format)GetValue(RenderTargetFormatProperty); }
            set { SetValue(RenderTargetFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RenderTargetFormatProperty =
            DependencyProperty.Register("RenderTargetFormat", typeof(Format), typeof(SharpDxImage), new FrameworkPropertyMetadata(Format.Unknown, (d, e) =>
            {
                SharpDxImage control = d as SharpDxImage;

                if (control == null) return;

                if (e.OldValue is Format o)
                {

                }

                if (e.NewValue is Format n)
                {

                }

            }));

        public Format TextureFormat
        {
            get { return (Format)GetValue(TextureFormatProperty); }
            set { SetValue(TextureFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextureFormatProperty =
            DependencyProperty.Register("TextureFormat", typeof(Format), typeof(SharpDxImage), new FrameworkPropertyMetadata(Format.B8G8R8A8_UNorm, (d, e) =>
            {
                SharpDxImage control = d as SharpDxImage;

                if (control == null) return;

                if (e.OldValue is Format o)
                {

                }

                if (e.NewValue is Format n)
                {

                }

            }));



        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            this.CreateAndBindTargets();
            base.OnRenderSizeChanged(sizeInfo);
        }

        private void DisposeD3D()
        {
            //d3DSurface.IsFrontBufferAvailableChanged -= OnIsFrontBufferAvailableChanged;
            base.Source = null;
            Disposer.SafeDispose(ref _renderTarget);
            Disposer.SafeDispose(ref _factory);
            Disposer.SafeDispose(ref _dx11ImageSource);
            Disposer.SafeDispose(ref _texture2D);
            Disposer.SafeDispose(ref _device);
        }


        private void CreateAndBindTargets()
        {
            if (this._dx11ImageSource == null)
                return;
            this._dx11ImageSource.SetRenderTarget(null);

            Disposer.SafeDispose(ref _renderTarget);
            Disposer.SafeDispose(ref _factory);
            Disposer.SafeDispose(ref _texture2D);

            var width = Math.Max((int)this.ActualWidth, 100);
            var height = Math.Max((int)this.ActualHeight, 100);

            var renderDesc = new Texture2DDescription
            {
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                Format = this.TextureFormat,
                Width = width,
                Height = height,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                OptionFlags = ResourceOptionFlags.Shared,
                CpuAccessFlags = CpuAccessFlags.None,
                ArraySize = 1
            };

            _texture2D = new Texture2D(_device, renderDesc);
            var surface = _texture2D.QueryInterface<Surface>();
            _factory = new SharpDX.Direct2D1.Factory();
            var rtp = new RenderTargetProperties(new PixelFormat(this.RenderTargetFormat, this.AlphaMode));
            _renderTarget = new RenderTarget(_factory, surface, rtp);
            //resCache.RenderTarget = d2DRenderTarget;
            _dx11ImageSource.SetRenderTarget(_texture2D);
            _device.ImmediateContext.Rasterizer.SetViewport(0, 0, width, height, 0.0f, 1.0f);
        }

        public void CallRender(Action<RenderTarget> action)
        {
            if (_device == null)
                return;
            _renderTarget.BeginDraw();
            action?.Invoke(_renderTarget);
            _renderTarget.EndDraw();
            //_device.ImmediateContext.Flush();
            //_dx11ImageSource.InvalidateD3DImage();
        }

        protected void InvalidateRender()
        {
            _device?.ImmediateContext.Flush();
            _dx11ImageSource?.InvalidateD3DImage();
        }

        public RenderTargetContext RenderOpen()
        {
            if (_device == null)
                return null;
            _renderTarget.BeginDraw();
            return new RenderTargetContext(_renderTarget, _device, _dx11ImageSource);
        }
    }

    public class RenderTargetContext : IDisposable
    {
        private readonly RenderTarget _renderTarget;
        public RenderTarget RenderTarget => _renderTarget;

        private readonly SharpDX.Direct3D11.Device _device;
        private readonly SharpDxImageSource _dx11ImageSource;

        internal RenderTargetContext(RenderTarget renderTarget, SharpDX.Direct3D11.Device device, SharpDxImageSource dx11ImageSource)
        {
            _renderTarget = renderTarget;
            _device = device;
            _dx11ImageSource = dx11ImageSource;
        }
        public void Dispose()
        {
            _renderTarget.EndDraw();
            _device.ImmediateContext.Flush();
            _dx11ImageSource.InvalidateD3DImage();
        }
    }
}
