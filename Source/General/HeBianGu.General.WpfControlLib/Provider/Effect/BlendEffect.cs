// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    ///     An effect that blend two textures.
    /// </summary>
    public class BlendEffect : ShaderEffect
    {
        #region Fields

        private static readonly PixelShader _pixelShader = new PixelShader();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes the <see cref="BlendEffect" /> class.
        /// </summary>
        static BlendEffect()
        {
            //_pixelShader.UriSource = UriHelper.MakePackUri("Provider/Effect/Blend.ps");

            _pixelShader.UriSource = new System.Uri(@"C:\Solution\Github\WPF-ControlBase-master (1) (1)\WPF-ControlBase-master\WPF-ControlBase-master\Source\General\HeBianGu.General.WpfControlLib\Provider\Effect\Blend.ps", System.UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="BlendEffect" /> class.
        /// </summary>
        public BlendEffect()
        {
            PixelShader = _pixelShader;

            UpdateShaderValue(Input1Property);
            UpdateShaderValue(Input2Property);
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        ///     Gets or sets the first input.
        /// </summary>
        /// <value>The first input.</value>
        public Brush Input1
        {
            get { return (Brush)GetValue(Input1Property); }
            set { SetValue(Input1Property, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Input1" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty Input1Property =
            RegisterPixelShaderSamplerProperty("Input1", typeof(BlendEffect), 0);

        /// <summary>
        ///     Gets or sets the second input.
        /// </summary>
        /// <value>The second input.</value>
        public Brush Input2
        {
            get { return (Brush)GetValue(Input2Property); }
            set { SetValue(Input2Property, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Input2" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty Input2Property =
            RegisterPixelShaderSamplerProperty("Input2", typeof(BlendEffect), 1);

        #endregion
    }
}