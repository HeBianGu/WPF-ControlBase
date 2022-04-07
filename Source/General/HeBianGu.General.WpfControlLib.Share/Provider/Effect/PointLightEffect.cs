// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace HeBianGu.General.WpfControlLib
{
    /// <summary>
    ///     An effect that colorizes a texture and adds two light points.
    /// </summary>
    public class PointLightEffect : ShaderEffect
    {
        #region Fields

        private static readonly PixelShader _pixelShader = new PixelShader();

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes the <see cref="PointLightEffect" /> class.
        /// </summary>
        static PointLightEffect()
        {
            //_pixelShader.UriSource = UriHelper.MakePackUri("Provider/Effect/PointLight.ps");

            _pixelShader.UriSource = UriHelper.MakePackUri(@"C:\Solution\Github\WPF-ControlBase-master (1) (1)\WPF-ControlBase-master\WPF-ControlBase-master\Source\General\HeBianGu.General.WpfControlLib\Provider\Effect/PointLight.ps");

        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="PointLightEffect" /> class.
        /// </summary>
        public PointLightEffect()
        {
            PixelShader = _pixelShader;

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(FillColorProperty);
            UpdateShaderValue(Light1PositionProperty);
            UpdateShaderValue(Light1ColorProperty);
            UpdateShaderValue(Light2PositionProperty);
            UpdateShaderValue(Light2ColorProperty);
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        ///     Gets or sets the input texture.
        /// </summary>
        /// <value>The input texture.</value>
        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Input" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty("Input", typeof(PointLightEffect), 0);

        /// <summary>
        ///     Gets or sets the fill color.
        /// </summary>
        /// <value>The fill color.</value>
        public Color FillColor
        {
            get { return (Color)GetValue(FillColorProperty); }
            set { SetValue(FillColorProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="FillColor" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register("FillColor", typeof(Color), typeof(PointLightEffect),
                new UIPropertyMetadata(PixelShaderConstantCallback(0)));

        /// <summary>
        ///     Gets or sets the first light's position.
        /// </summary>
        /// <value>The first light's position.</value>
        public Point Light1Position
        {
            get { return (Point)GetValue(Light1PositionProperty); }
            set { SetValue(Light1PositionProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Light1Position" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty Light1PositionProperty =
            DependencyProperty.Register("Light1Position", typeof(Point), typeof(PointLightEffect),
                new UIPropertyMetadata(PixelShaderConstantCallback(1)));

        /// <summary>
        ///     Gets or sets the color of the first light.
        /// </summary>
        /// <value>The color of the first light.</value>
        public Color Light1Color
        {
            get { return (Color)GetValue(Light1ColorProperty); }
            set { SetValue(Light1ColorProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Light1Color" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty Light1ColorProperty =
            DependencyProperty.Register("Light1Color", typeof(Color), typeof(PointLightEffect),
                new UIPropertyMetadata(PixelShaderConstantCallback(2)));

        /// <summary>
        ///     Gets or sets the second light's position.
        /// </summary>
        /// <value>The second light's position.</value>
        public Point Light2Position
        {
            get { return (Point)GetValue(Light2PositionProperty); }
            set { SetValue(Light2PositionProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Light2Position" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty Light2PositionProperty =
            DependencyProperty.Register("Light2Position", typeof(Point), typeof(PointLightEffect),
                new UIPropertyMetadata(PixelShaderConstantCallback(3)));

        /// <summary>
        ///     Gets or sets the color of the second light.
        /// </summary>
        /// <value>The color of the second light.</value>
        public Color Light2Color
        {
            get { return (Color)GetValue(Light2ColorProperty); }
            set { SetValue(Light2ColorProperty, value); }
        }

        /// <summary>
        ///     Identifies the <see cref="Light2Color" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty Light2ColorProperty =
            DependencyProperty.Register("Light2Color", typeof(Color), typeof(PointLightEffect),
                new UIPropertyMetadata(PixelShaderConstantCallback(4)));

        #endregion
    }
}