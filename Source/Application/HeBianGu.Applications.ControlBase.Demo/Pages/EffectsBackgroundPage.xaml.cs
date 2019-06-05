using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfControlDemo.View
{
    /// <summary>
    /// EffectsBackgroundPage.xaml 的交互逻辑
    /// </summary>
    public partial class EffectsBackgroundPage : Page
    {
        #region Fields

        private static readonly ReadOnlyCollection<EffectColors> effectColors = new List<EffectColors>
        {
            new EffectColors(Colors.Thistle, Colors.Plum, Colors.HotPink),
            new EffectColors(Colors.Plum, Colors.Orange, Colors.Fuchsia),
            new EffectColors(Colors.LightSteelBlue, Colors.Snow, Colors.LightSlateGray),
            new EffectColors(Colors.DimGray, Colors.SandyBrown, Colors.HotPink),
            new EffectColors(Colors.PaleGoldenrod, Colors.Goldenrod, Colors.HotPink),
            new EffectColors(Colors.Silver, Colors.Khaki, Colors.HotPink),
            new EffectColors(Colors.LightBlue, Colors.Khaki, Colors.PaleGreen),
            new EffectColors(Colors.LightGray, Colors.Aquamarine, Colors.LightSteelBlue),
            new EffectColors(Colors.HotPink, Colors.Gold, Colors.LightBlue),
            new EffectColors(Colors.PaleGreen, Colors.Gold, Colors.Aquamarine),
            new EffectColors(Colors.CadetBlue, Colors.LightSkyBlue, Colors.LemonChiffon),
            new EffectColors(Colors.Brown, Colors.Khaki, Colors.Violet),
            new EffectColors(Colors.Silver, Colors.Khaki, Colors.HotPink),
            new EffectColors(Colors.PaleVioletRed, Colors.Khaki, Colors.HotPink),
            new EffectColors(Colors.PaleTurquoise, Colors.PaleGoldenrod, Colors.Plum),
            new EffectColors(Colors.Aquamarine, Colors.Gold, Colors.CornflowerBlue),
            new EffectColors(Colors.CadetBlue, Colors.Khaki, Colors.LemonChiffon),
            new EffectColors(Colors.Thistle, Colors.Khaki, Colors.HotPink),
        }.AsReadOnly();

        private static readonly ReadOnlyCollection<ReadOnlyCollection<Point>> points =
            new List<ReadOnlyCollection<Point>>
            {
                new List<Point>
                {
                    new Point(0.0, 0.0),
                    new Point(0.0, 0.5),
                    new Point(0.5, 0.0),
                    new Point(1.0, 0.0),
                }.AsReadOnly(),
                new List<Point>
                {
                    new Point(0.9, 0.0),
                    new Point(0.9, 0.7),
                    new Point(0.5, 1.0),
                }.AsReadOnly(),
                new List<Point>
                {
                    new Point(0.5, 0.0),
                    new Point(0.9, 0.0),
                    new Point(0.8, 0.3),
                    new Point(0.9, 0.8),
                }.AsReadOnly(),
                new List<Point>
                {
                    new Point(0.2, 0.9),
                    new Point(0.4, 0.8),
                    new Point(0.6, 0.9),
                }.AsReadOnly(),
                new List<Point>
                {
                    new Point(0.9, 0.2),
                    new Point(0.1, 0.0),
                }.AsReadOnly(),
                new List<Point>
                {
                    new Point(0.0, 0.5),
                    new Point(0.2, 0.8),
                    new Point(0.5, 0.9),
                }.AsReadOnly(),
            }.AsReadOnly();

        private readonly Random random = new Random();
        private readonly IList<string> imagePaths;

        private DispatcherTimer imageTimer;
        private DispatcherTimer colorTimer;
        private Storyboard effectStoryboard;

        #endregion

        #region Constructor

        public EffectsBackgroundPage()
        {
            InitializeComponent();

            imagePaths = ImageHelper.GetPaths();

            Loaded += OnLoaded;
        }

        #endregion

        #region Methods

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;

            if (imagePaths.Count > 0)
            {
                ChangeImage();

                imageTimer = new DispatcherTimer();
                imageTimer.Interval = TimeSpan.FromSeconds(30);
                imageTimer.Tick += delegate { ChangeImage(); };
                imageTimer.Start();

                ChangeColors();

                colorTimer = new DispatcherTimer();
                colorTimer.Interval = TimeSpan.FromSeconds(10);
                colorTimer.Tick += delegate { ChangeColors(); };
                colorTimer.Start();
            }
        }

        private void ChangeColors()
        {
            // randomly select light colors
            EffectColors colors = effectColors[random.Next(0, effectColors.Count)];

            Storyboard lightStoryboard = new Storyboard();
            lightStoryboard.AddAnimation(new ColorAnimation(colors.Fill, Duration.Automatic), "lightEffect",
                PointLightEffect.FillColorProperty);
            lightStoryboard.AddAnimation(new ColorAnimation(colors.Light1, Duration.Automatic), "lightEffect",
                PointLightEffect.Light1ColorProperty);
            lightStoryboard.AddAnimation(new ColorAnimation(colors.Light2, Duration.Automatic), "lightEffect",
                PointLightEffect.Light2ColorProperty);
            lightStoryboard.Begin(this);
        }

        private void ChangeImage()
        {
            // randomly select image
            image.Source = new BitmapImage(new Uri(imagePaths[random.Next(0, imagePaths.Count)]));

            PrepareStoryboard();
            AddSceneAnimations();
            AddLightAnimations();
            effectStoryboard.Begin(this, true);
        }

        private void PrepareStoryboard()
        {
            if (effectStoryboard == null)
            {
                effectStoryboard = new Storyboard();
            }
            else
            {
                effectStoryboard.Remove(this);
                effectStoryboard.Children.Clear();
            }
        }

        private void AddLightAnimations()
        {
            // randomly select light path
            int pointIndex = random.Next(0, points.Count / 2) * 2;

            effectStoryboard.AddAnimation(CreatePointAnimation(pointIndex), "lightEffect",
                PointLightEffect.Light1PositionProperty);
            effectStoryboard.AddAnimation(CreatePointAnimation(pointIndex + 1), "lightEffect",
                PointLightEffect.Light2PositionProperty);
        }

        private void AddSceneAnimations()
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(30));

            image.ClearValue(MarginProperty);

            switch (random.Next(0, 3))
            {
                case 0:
                    // slide
                    int amount = 500;
                    image.Margin = new Thickness(0, 0, -amount, 0);
                    effectStoryboard.AddAnimation(
                        new DoubleAnimation(-amount, 0, new Duration(TimeSpan.FromSeconds(amount / 2))), "imageTranslate",
                        TranslateTransform.XProperty);
                    break;
                case 1:
                    // scale in
                    effectStoryboard.AddAnimation(new DoubleAnimation(1.5, 1, duration), "imageScale",
                        ScaleTransform.ScaleXProperty);
                    effectStoryboard.AddAnimation(new DoubleAnimation(1.5, 1, duration), "imageScale",
                        ScaleTransform.ScaleYProperty);
                    break;
                case 2:
                    // scale out
                    effectStoryboard.AddAnimation(new DoubleAnimation(1, 1.5, duration), "imageScale",
                        ScaleTransform.ScaleXProperty);
                    effectStoryboard.AddAnimation(new DoubleAnimation(1, 1.5, duration), "imageScale",
                        ScaleTransform.ScaleYProperty);
                    break;
                default:
                    break;
            }
        }

        private AnimationTimeline CreatePointAnimation(int index)
        {
            PointAnimationUsingKeyFrames anim = new PointAnimationUsingKeyFrames();
            anim.Duration = new Duration(TimeSpan.FromSeconds(30));

            foreach (Point point in points[index])
            {
                anim.KeyFrames.Add(new LinearPointKeyFrame(point));
            }

            return anim;
        }

        #endregion

        #region Inner Types

        private class EffectColors
        {
            public EffectColors()
            {
            }

            public EffectColors(Color fill, Color light1, Color light2)
            {
                Fill = fill;
                Light1 = light1;
                Light2 = light2;
            }

            public Color Fill { get; set; }
            public Color Light1 { get; set; }
            public Color Light2 { get; set; }
        }

        #endregion
    }
}
