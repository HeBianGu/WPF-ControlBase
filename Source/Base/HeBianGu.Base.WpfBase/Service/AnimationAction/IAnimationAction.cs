using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.Base.WpfBase
{
    public enum PointOriginType
    {
        Default = 0, Custom, Center, MousePosition, RandomInner
    }

    public interface IAnimationAction
    {
        /// <summary> 用于切换页面 隐藏 </summary>
        void BeginPrevious(UIElement element, Action complate = null);

        /// <summary> 用于切换页面 显示 </summary>

        void BeginCurrent(UIElement element, Action complate = null);

        /// <summary> 用于加载页面 隐藏 </summary>
        void BeginHidden(UIElement element, Action complate = null);

        /// <summary> 用于加载页面 显示 </summary>
        void BeginVisible(UIElement element, Action complate = null);
    }

    public class ActionCollection : ObservableCollection<IAnimationAction>
    {

    }

    public abstract class AnimationActionBase : IAnimationAction
    {
        public AnimationActionBase()
        {
            this.RenderTransformOrigin = new Point(0.5, 0.5);
        }
        public double Start { get; set; }

        public double End { get; set; }

        public Double Duration
        {
            set
            {
                this.HideDuration = value;
                this.VisibleDuration = value;
            }
        }

        public Double HideDuration { get; set; } = 500.0;

        public Double VisibleDuration { get; set; } = 500.0;

        public Point RenderTransformOrigin { get; set; }

        public Double BeginTime { get; set; } = 0.0;

        public virtual void BeginPrevious(UIElement element, Action complate = null)
        {
            Panel.SetZIndex(element, 1);

            element.RenderTransformOrigin = RenderTransformOrigin;
        }

        public virtual void BeginCurrent(UIElement element, Action complate = null)
        {
            element.RenderTransformOrigin = RenderTransformOrigin;

            Panel.SetZIndex(element, 0);

            if (this.BeginTime == 0.0)
            {
                element.Visibility = Visibility.Visible;
            }
            else
            {
                Storyboard storyboard = new Storyboard();

                ObjectAnimationUsingKeyFrames frames = new ObjectAnimationUsingKeyFrames();

                DiscreteObjectKeyFrame keyFrame1 = new DiscreteObjectKeyFrame(Visibility.Hidden, KeyTime.FromTimeSpan(TimeSpan.Zero));
                DiscreteObjectKeyFrame keyFrame2 = new DiscreteObjectKeyFrame(Visibility.Visible, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(this.BeginTime)));

                frames.KeyFrames.Add(keyFrame1);
                frames.KeyFrames.Add(keyFrame2);

                Storyboard.SetTarget(frames, element);
                Storyboard.SetTargetProperty(frames, new PropertyPath(FrameworkElement.VisibilityProperty));
                storyboard.Children.Add(frames);

                storyboard.Begin();
            }
        }

        public virtual void BeginHidden(UIElement element, Action complate = null)
        {
            this.BeginPrevious(element, complate);
        }

        public virtual void BeginVisible(UIElement element, Action complate = null)
        {
            this.BeginCurrent(element, complate);
        }
    }

    public class OpacityAction : AnimationActionBase
    {
        public double StartOpacity { get; set; }

        public double EndOpacity { get; set; }

        public OpacityAction()
        {
            this.StartOpacity = 0;

            this.EndOpacity = 1;
        }
        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            //element.RenderTransformOrigin = this.RenderTransformOrigin;

            element.BeginAnimationOpacity(this.EndOpacity, this.StartOpacity, this.HideDuration, l =>
            {
                element.Visibility = Visibility.Hidden;
                complate?.Invoke();
            });

        }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            //element.RenderTransformOrigin = this.RenderTransformOrigin;

            element.Visibility = Visibility.Visible;

            element.BeginAnimationOpacity(this.StartOpacity, this.EndOpacity, this.VisibleDuration, l =>
            {
                complate?.Invoke();
            });
        }
    }

    public class NoneAction : AnimationActionBase
    {
        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            element.Visibility = Visibility.Collapsed;
        }
    }

    public class AngleAction : AnimationActionBase
    {
        public AngleAction()
        {
            this.RenderTransformOrigin = new Point(0, 0);

            this.VisibleDuration = 1;
        }
        public double StartAngle { get; set; } = 90;

        public double EndAngle { get; set; } = 90;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationAngle(this.StartAngle, 0, this.VisibleDuration, l =>
            {
                element.Visibility = Visibility.Visible;
                complate?.Invoke();
            });
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationAngle(0, this.EndAngle, this.HideDuration,
                l =>
                {
                    l.Visibility = Visibility.Hidden;
                    complate?.Invoke();
                });
        }

        //public override void BeginVisible(UIElement element, Action complate = null)
        //{
        //    if (!element.CheckDefaultTransformGroup()) return;

        //    base.BeginPrevious(element);

        //    element.BeginAnimationAngle(this.StartAngle, this.EndAngle, this.VisibleDuration,
        //        l =>
        //        {
        //            l.Visibility = Visibility.Visible;
        //            complate?.Invoke();
        //        });
        //}

    }

    public class ScaleAction : OpacityAction
    {
        public double StartX { get; set; } = 0.2;

        public double EndX { get; set; } = 0.5;

        public double StartY { get; set; } = 0.2;

        public double EndY { get; set; } = 0.5;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.RenderTransformOrigin = this.RenderTransformOrigin;

            element.BeginAnimationScaleX(this.StartX, 1, this.VisibleDuration, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));

            element.BeginAnimationScaleY(this.StartY, 1, this.VisibleDuration, l => complate?.Invoke(), l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.RenderTransformOrigin = this.RenderTransformOrigin;

            element.BeginAnimationScaleX(1, this.EndX, this.HideDuration, l => element.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);

            element.BeginAnimationScaleY(1, this.EndY, this.HideDuration, l =>
            {
                element.Visibility = Visibility.Hidden;
                complate?.Invoke();

            }, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class TranslateAction : OpacityAction
    {
        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationX(this.StartPoint.X, 0, this.VisibleDuration, l => complate?.Invoke(), l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
            element.BeginAnimationY(this.StartPoint.Y, 0, this.VisibleDuration, l => complate?.Invoke(), l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));

        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationX(0, this.EndPoint.X, this.HideDuration, l =>
            {
                element.Visibility = Visibility.Hidden;
                complate?.Invoke();
            }, l => l.FillBehavior = FillBehavior.Stop);
            element.BeginAnimationY(0, this.EndPoint.Y, this.HideDuration, l => element.Visibility = Visibility.Hidden, l => l.FillBehavior = FillBehavior.Stop);

        }
    }

    public class HorizontalAction : AnimationActionBase
    {
        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationX(-(element as FrameworkElement).ActualWidth, 0, this.VisibleDuration, l => complate?.Invoke());
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationX(0, (element as FrameworkElement).ActualWidth, this.HideDuration, l =>
            {
                //element.Visibility = Visibility.Hidden;
                element.Visibility = Visibility.Collapsed;
                complate?.Invoke();
            }, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class VerticalAction : AnimationActionBase
    {
        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            element.BeginAnimationY(-(element as FrameworkElement).ActualHeight, 0, this.VisibleDuration, l => complate?.Invoke());
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            element.BeginAnimationY(0, (element as FrameworkElement).ActualHeight, this.HideDuration, l =>
            {
                element.Visibility = Visibility.Hidden;
                complate?.Invoke();
            }, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class SkewAction : OpacityAction
    {
        public double StartAngleX { get; set; } = 0;

        public double EndAngleX { get; set; } = 360;

        public double StartAngleY { get; set; } = 0;

        public double EndAngleY { get; set; } = 360;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginCurrent(element);

            if (this.StartAngleX != this.EndAngleX)
                element.BeginAnimationSkewX(this.StartAngleX, this.EndAngleX, this.VisibleDuration * 1.5, l => complate?.Invoke(), l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));

            if (this.StartAngleY != this.EndAngleY)
                element.BeginAnimationSkewY(this.StartAngleY, this.EndAngleY, this.VisibleDuration * 1.5, null, l => l.BeginTime = TimeSpan.FromMilliseconds(this.BeginTime));
        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            if (!element.CheckDefaultTransformGroup()) return;

            base.BeginPrevious(element);

            if (this.StartAngleX != this.EndAngleX)
                element.BeginAnimationSkewX(this.EndAngleX, this.StartAngleX, this.HideDuration, l => complate?.Invoke(), l => l.FillBehavior = FillBehavior.Stop);

            if (this.StartAngleY != this.EndAngleY)
                element.BeginAnimationSkewY(this.EndAngleY, this.StartAngleY, this.HideDuration, null, l => l.FillBehavior = FillBehavior.Stop);
        }
    }

    public class LinearOpacityMaskAction : AnimationActionBase
    {
        public LinearOpacityMaskAction()
        {
            RectangleGeometry geometry = new RectangleGeometry();

            geometry.Rect = new Rect() { X = 0.01, Y = 0.01, Width = 0.9, Height = 0.9 };

            this.Geometry = geometry;

            this.Viewport = new Rect() { X = 0.1, Y = 0.2, Height = 0.1, Width = 0.1 };

            this.StartPoint = new Point(0, 0);

            this.EndPoint = new Point(1, 1);
        }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            element.OpacityMask = null;
        }

        public Rect Viewport { get; set; }

        public Geometry Geometry { get; set; }

        public Point StartPoint { get; set; }

        public Point EndPoint { get; set; }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            //DrawingBrush brush = new DrawingBrush();

            //brush.TileMode = TileMode.Tile;

            //brush.Viewport = Viewport;

            //brush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;

            //GeometryDrawing drawing = new GeometryDrawing();

            //LinearGradientBrush linear = new LinearGradientBrush();

            //linear.StartPoint = this.StartPoint;

            //linear.EndPoint = this.EndPoint;

            //linear.GradientStops.Add(new GradientStop() { Offset = 0, Color = Colors.Black });
            //linear.GradientStops.Add(new GradientStop() { Offset = 1, Color = Colors.Transparent });

            //drawing.Geometry = this.Geometry;

            //drawing.Brush = linear;

            //brush.Drawing = drawing;

            //element.OpacityMask = brush;

            //element.BegionDoubleStoryBoard(1, 0, this.HideDuration / 1000.0,
            //    "OpacityMask.(DrawingBrush.Drawing).(GeometryDrawing.Brush).(LinearGradientBrush.GradientStops)[1].Offset", l =>
            //    {
            //        element.Visibility = Visibility.Hidden;
            //        complate?.Invoke();
            //    });

            Action action = () =>
              {
                  element.Visibility = Visibility.Hidden;
                  complate?.Invoke();
              };

            this.Begin(element, 1, 0, "OpacityMask.(DrawingBrush.Drawing).(GeometryDrawing.Brush).(LinearGradientBrush.GradientStops)[1].Offset", action);

        }

        public override void BeginVisible(UIElement element, Action complate = null)
        {
            base.BeginVisible(element);

            this.Begin(element, 0, 1, "OpacityMask.(DrawingBrush.Drawing).(GeometryDrawing.Brush).(LinearGradientBrush.GradientStops)[0].Offset", complate);
        }

        void Begin(UIElement element, double from, double to, string propertyPath, Action complate = null)
        {
            DrawingBrush brush = new DrawingBrush();

            brush.TileMode = TileMode.Tile;

            brush.Viewport = Viewport;

            brush.ViewboxUnits = BrushMappingMode.RelativeToBoundingBox;

            GeometryDrawing drawing = new GeometryDrawing();

            LinearGradientBrush linear = new LinearGradientBrush();

            linear.StartPoint = this.StartPoint;

            linear.EndPoint = this.EndPoint;

            linear.GradientStops.Add(new GradientStop() { Offset = 0, Color = Colors.Black });
            linear.GradientStops.Add(new GradientStop() { Offset = 1, Color = Colors.Transparent });

            drawing.Geometry = this.Geometry;

            drawing.Brush = linear;

            brush.Drawing = drawing;

            element.OpacityMask = brush;

            element.BegionDoubleStoryBoard(from, to, this.HideDuration / 1000.0, propertyPath, l =>
                {
                    element.OpacityMask = null;
                    complate?.Invoke();
                });
        }
    }

    public class ImageOpacityMaskAction : AnimationActionBase
    {
        public ImageSource ImageSource { get; set; }

        public double StartTime { get; set; }

        public PointOriginType PointOriginType { get; set; }

        public Rect Viewport { get; set; } = new Rect(0.2, 0.2, 0.2, 0.2);

        public BrushMappingMode ViewportUnits { get; set; } = BrushMappingMode.RelativeToBoundingBox;

        public TileMode TileMode { get; set; } = TileMode.Tile;

        public bool IsSingle { get; set; } = true;

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            Panel.SetZIndex(element, 1);

            var toSlide = element as FrameworkElement;

            var origin = this.RenderTransformOrigin;

            if (this.PointOriginType == PointOriginType.MousePosition)
            {
                //  Do ：按鼠标位置计算
                var postion = Mouse.GetPosition(toSlide);
                double x = postion.X / toSlide.ActualWidth;
                double y = postion.Y / toSlide.ActualHeight;

                origin = new Point(x, y);
            }
            else if (this.PointOriginType == PointOriginType.RandomInner)
            {
                //  Do ：随机计算
                Random random = new Random();
                origin = new Point(random.NextDouble(), random.NextDouble());
            }
            else if (this.PointOriginType == PointOriginType.Center)
            {
                //  Do ：中心点计算
                origin = new Point(0.5, 0.5);
            }

            var horizontalProportion = toSlide.ActualWidth * origin.X;

            var verticalProportion = toSlide.ActualHeight * origin.Y;

            ImageBrush image = new ImageBrush(this.ImageSource);

            if (!IsSingle)
            {
                image.Viewport = this.Viewport;

                image.ViewportUnits = this.ViewportUnits;

                image.TileMode = this.TileMode;
            }


            ScaleTransform scaleTransform = new ScaleTransform(0, 0);

            scaleTransform.CenterX = horizontalProportion;

            scaleTransform.CenterY = verticalProportion;

            image.Transform = scaleTransform;

            toSlide.SetCurrentValue(UIElement.OpacityMaskProperty, image);

            //var scaleAnimation = new DoubleAnimationUsingKeyFrames();

            //scaleAnimation.Completed += (sender, args) =>
            //{
            //    element.SetCurrentValue(UIElement.OpacityMaskProperty, null);
            //};
            //scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromMilliseconds(this.StartTime)));
            //scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(10, TimeSpan.FromMilliseconds(this.VisibleDuration)));

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = 10;
            animation.BeginTime = TimeSpan.FromMilliseconds(this.StartTime);
            animation.Duration = TimeSpan.FromMilliseconds(this.VisibleDuration);

            animation.Completed += (sender, args) =>
            {
                element.SetCurrentValue(UIElement.OpacityMaskProperty, null);
                complate?.Invoke();
            };

            animation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

        }

        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            Panel.SetZIndex(element, 0);

            element.OpacityMask = null;
        }

        public override void BeginHidden(UIElement element, Action complate = null)
        {
            var toSlide = element as FrameworkElement;

            var origin = this.RenderTransformOrigin;

            if (this.PointOriginType == PointOriginType.MousePosition)
            {
                //  Do ：按鼠标位置计算
                var postion = Mouse.GetPosition(toSlide);
                double x = postion.X / toSlide.ActualWidth;
                double y = postion.Y / toSlide.ActualHeight;

                origin = new Point(x, y);
            }
            else if (this.PointOriginType == PointOriginType.RandomInner)
            {
                //  Do ：随机计算
                Random random = new Random();
                origin = new Point(random.NextDouble(), random.NextDouble());
            }
            else if (this.PointOriginType == PointOriginType.Center)
            {
                //  Do ：中心点计算
                origin = new Point(0.5, 0.5);
            }

            var horizontalProportion = toSlide.ActualWidth * origin.X;

            var verticalProportion = toSlide.ActualHeight * origin.Y;

            ImageBrush image = new ImageBrush(this.ImageSource);

            if (!IsSingle)
            {
                image.Viewport = this.Viewport;

                image.ViewportUnits = this.ViewportUnits;

                image.TileMode = this.TileMode;
            }

            ScaleTransform scaleTransform = new ScaleTransform(0, 0);

            scaleTransform.CenterX = horizontalProportion;

            scaleTransform.CenterY = verticalProportion;

            image.Transform = scaleTransform;

            toSlide.SetCurrentValue(UIElement.OpacityMaskProperty, image);

            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 10;
            animation.To = 0;
            animation.BeginTime = TimeSpan.FromMilliseconds(this.StartTime);
            animation.Duration = TimeSpan.FromMilliseconds(this.VisibleDuration);

            animation.Completed += (sender, args) =>
            {
                element.SetCurrentValue(UIElement.OpacityMaskProperty, null);
                element.Visibility = Visibility.Collapsed;
                complate?.Invoke();
            };

            animation.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseIn };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
    }

    public class ScaleGeomotryAction : AnimationActionBase
    {
        public ScaleGeomotryAction()
        {
            this.Geometry = new EllipseGeometry();
        }

        public double StartTime { get; set; }

        public PointOriginType PointOriginType { get; set; }

        public Geometry Geometry { get; set; }

        public override void BeginCurrent(UIElement element, Action complate = null)
        {
            base.BeginCurrent(element);

            //Panel.SetZIndex(element, 1);

            //var toSlide = element as FrameworkElement;

            //var origin = this.RenderTransformOrigin;

            //if (this.PointOriginType == PointOriginType.MousePosition)
            //{
            //    //  Do ：按鼠标位置计算
            //    var postion = Mouse.GetPosition(toSlide);
            //    double x = postion.X / toSlide.ActualWidth;
            //    double y = postion.Y / toSlide.ActualHeight;

            //    origin = new Point(x, y);
            //}
            //else if (this.PointOriginType == PointOriginType.RandomInner)
            //{
            //    //  Do ：随机计算
            //    Random random = new Random();
            //    origin = new Point(random.NextDouble(), random.NextDouble());
            //}
            //else if (this.PointOriginType == PointOriginType.Center)
            //{
            //    //  Do ：中心点计算
            //    origin = new Point(0.5, 0.5);
            //}

            //var horizontalProportion = toSlide.ActualWidth * Math.Max(1.0 - origin.X, 1.0 * origin.X);

            //var verticalProportion = toSlide.ActualHeight * Math.Max(1.0 - origin.Y, 1.0 * origin.Y);

            //var radius = Math.Sqrt(Math.Pow(horizontalProportion, 2) + Math.Pow(verticalProportion, 2));

            //var transformGroup = new TransformGroup();

            //var scaleTransform = new ScaleTransform(0, 0);
            //transformGroup.Children.Add(scaleTransform);

            //var translateTransform = new TranslateTransform(toSlide.ActualWidth * origin.X, toSlide.ActualHeight * origin.Y);
            //transformGroup.Children.Add(translateTransform);

            //if (this.Geometry is EllipseGeometry ellipse)
            //{
            //    ellipse.RadiusX = radius;
            //    ellipse.RadiusY = radius;

            //}
            //else if (this.Geometry is RectangleGeometry rectangle)
            //{
            //    rectangle.Rect = new Rect(-horizontalProportion, -verticalProportion, 2 * horizontalProportion, 2 * verticalProportion);
            //}

            //this.Geometry.Transform = transformGroup;

            //toSlide.SetCurrentValue(UIElement.ClipProperty, this.Geometry);

            //var scaleAnimation = new DoubleAnimationUsingKeyFrames();

            //scaleAnimation.Completed += (sender, args) =>
            //{

            //    System.Diagnostics.Debug.WriteLine(this.Geometry.Bounds.Width);

            //    toSlide.SetCurrentValue(UIElement.ClipProperty, null);
            //};
            //scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.FromMilliseconds(this.StartTime)));
            //scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, TimeSpan.FromMilliseconds(this.VisibleDuration)));

            //scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            //scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);


            this.Begin(element, 0, 1);
        }


        public override void BeginPrevious(UIElement element, Action complate = null)
        {
            base.BeginPrevious(element);

            Panel.SetZIndex(element, 0);

            element.BeginAnimationOpacity(1, 0.9, this.HideDuration, l => element.Visibility = Visibility.Hidden);
        }

        public override void BeginHidden(UIElement element, Action complate = null)
        {
            this.Begin(element, 1, 0, () => element.Visibility = Visibility.Collapsed);
        }


        void Begin(UIElement element, double from, double to, Action complate = null)
        {
            var toSlide = element as FrameworkElement;

            var origin = this.RenderTransformOrigin;

            if (this.PointOriginType == PointOriginType.MousePosition)
            {
                //  Do ：按鼠标位置计算
                var postion = Mouse.GetPosition(toSlide);
                double x = postion.X / toSlide.ActualWidth;
                double y = postion.Y / toSlide.ActualHeight;

                origin = new Point(x, y);
            }
            else if (this.PointOriginType == PointOriginType.RandomInner)
            {
                //  Do ：随机计算
                Random random = new Random();
                origin = new Point(random.NextDouble(), random.NextDouble());
            }
            else if (this.PointOriginType == PointOriginType.Center)
            {
                //  Do ：中心点计算
                origin = new Point(0.5, 0.5);
            }

            var horizontalProportion = toSlide.ActualWidth * Math.Max(1.0 - origin.X, 1.0 * origin.X);

            var verticalProportion = toSlide.ActualHeight * Math.Max(1.0 - origin.Y, 1.0 * origin.Y);

            var radius = Math.Sqrt(Math.Pow(horizontalProportion, 2) + Math.Pow(verticalProportion, 2));

            var transformGroup = new TransformGroup();

            var scaleTransform = new ScaleTransform(0, 0);
            transformGroup.Children.Add(scaleTransform);

            var translateTransform = new TranslateTransform(toSlide.ActualWidth * origin.X, toSlide.ActualHeight * origin.Y);
            transformGroup.Children.Add(translateTransform);

            if (this.Geometry is EllipseGeometry ellipse)
            {
                ellipse.RadiusX = radius;
                ellipse.RadiusY = radius;

            }
            else if (this.Geometry is RectangleGeometry rectangle)
            {
                rectangle.Rect = new Rect(-horizontalProportion, -verticalProportion, 2 * horizontalProportion, 2 * verticalProportion);
            }

            this.Geometry.Transform = transformGroup;

            toSlide.SetCurrentValue(UIElement.ClipProperty, this.Geometry);

            var scaleAnimation = new DoubleAnimationUsingKeyFrames();

            scaleAnimation.Completed += (sender, args) =>
            {
                System.Diagnostics.Debug.WriteLine(this.Geometry.Bounds.Width);

                toSlide.SetCurrentValue(UIElement.ClipProperty, null);

                complate?.Invoke();
            };
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(from, TimeSpan.FromMilliseconds(this.StartTime)));
            scaleAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(to, TimeSpan.FromMilliseconds(this.VisibleDuration)));

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
        }
    }
}
