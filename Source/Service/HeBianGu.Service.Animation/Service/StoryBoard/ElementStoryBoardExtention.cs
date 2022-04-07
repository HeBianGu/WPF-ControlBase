// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using HeBianGu.Base.WpfBase;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace System.Windows
{
    public static class ElementStoryBoardExtention
    {
        /// <summary> 执行动画 </summary>
        public static StoryboardEngineBase BegionDoubleStoryBoard(this UIElement element, double start, double end, double duration, string propertyName, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            DoubleStoryboardEngine engine = DoubleStoryboardEngine.Create(start, end, duration, propertyName);

            return engine.Start(element, Completed, init);

        }

        /// <summary> 获取 RenderTransform 中 Transform 元素 </summary>
        public static T GetTransform<T>(this UIElement element) where T : Transform
        {
            TransformGroup group = (TransformGroup)element.RenderTransform;

            T translate = group.Children.FirstOrDefault(l => l is T) as T;

            return translate;
        }

        public static bool CheckDefaultTransformGroup(this UIElement element)
        {
            if (element.RenderTransform == Transform.Identity || element.RenderTransform == null || element.RenderTransform.IsFrozen)
            {
                TransformGroup dg = Application.Current.TryFindResource(SystemKeys.TransformGroup) as TransformGroup;

                if (dg == null)
                {
                    TransformGroup group = new TransformGroup();
                    group.Children.Add(new ScaleTransform());
                    group.Children.Add(new SkewTransform());
                    group.Children.Add(new RotateTransform());
                    group.Children.Add(new TranslateTransform());

                    element.RenderTransform = group;
                }
                else
                {

                    element.RenderTransform = dg.Clone();
                }

            }

            return true;
        }

        public static void BeginAnimationXY(this UIElement element, double toX, double toY, double duration = 1000)
        {
            TranslateTransform tran = element.GetTransform<TranslateTransform>();

            if (tran == null) return;

            DoubleAnimation xd = new DoubleAnimation(toX, TimeSpan.FromMilliseconds(duration));
            DoubleAnimation yd = new DoubleAnimation(toY, TimeSpan.FromMilliseconds(duration));
            tran.BeginAnimation(TranslateTransform.XProperty, xd);
            tran.BeginAnimation(TranslateTransform.YProperty, yd);
        }

        public static void BeginAnimationScale(this UIElement element, double scaleX, double scaleY, double duration = 1000)
        {
            ScaleTransform tran = element.GetTransform<ScaleTransform>();
            if (tran == null) return;

            DoubleAnimation xd = new DoubleAnimation(scaleX, TimeSpan.FromMilliseconds(duration));
            DoubleAnimation yd = new DoubleAnimation(scaleY, TimeSpan.FromMilliseconds(duration));
            tran.BeginAnimation(ScaleTransform.ScaleXProperty, xd);
            tran.BeginAnimation(ScaleTransform.ScaleYProperty, yd);
        }

        public static void BeginAnimationAngle(this UIElement element, double toAngle, double duration = 1000)
        {
            RotateTransform tran = element.GetTransform<RotateTransform>();

            if (tran == null) return;

            DoubleAnimation xd = new DoubleAnimation(toAngle, TimeSpan.FromMilliseconds(duration));
            tran.BeginAnimation(RotateTransform.AngleProperty, xd);
        }

        public static void BeginAnimationOpacity(this UIElement element, double to, double duration = 1000)
        {
            DoubleAnimation xd = new DoubleAnimation(to, TimeSpan.FromMilliseconds(duration));
            element.BeginAnimation(UIElement.OpacityProperty, xd);
        }

        public static void BeginAnimationSkew(this UIElement element, double toAngleX, double toAngleY, double duration = 1000)
        {
            SkewTransform tran = element.GetTransform<SkewTransform>();
            if (tran == null) return;

            DoubleAnimation d1 = new DoubleAnimation(toAngleX, TimeSpan.FromMilliseconds(duration));
            DoubleAnimation d2 = new DoubleAnimation(toAngleY, TimeSpan.FromMilliseconds(duration));

            tran.BeginAnimation(SkewTransform.AngleXProperty, d1);
            tran.BeginAnimation(SkewTransform.AngleYProperty, d2);
        }


        public static void BeginAnimationPath(this UIElement element, PathGeometry geometry, double duration = 1000, bool useAngle = true, bool reverse = false)
        {
            if (useAngle)
            {
                RotateTransform tran = element.GetTransform<RotateTransform>();
                if (tran == null) return;
                DoubleAnimationUsingPath path = new DoubleAnimationUsingPath() { PathGeometry = geometry };
                path.Source = PathAnimationSource.Angle;
                path.Duration = new Duration(TimeSpan.FromMilliseconds(duration));

                if (reverse)
                {
                    path.BeginTime = TimeSpan.FromMilliseconds(-duration);
                    path.AutoReverse = true;
                }
                tran.BeginAnimation(RotateTransform.AngleProperty, path);
            }

            {
                TranslateTransform tran = element.GetTransform<TranslateTransform>();
                if (tran == null) return;
                DoubleAnimationUsingPath path = new DoubleAnimationUsingPath() { PathGeometry = geometry };
                path.Source = PathAnimationSource.X;
                path.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
                if (reverse)
                {
                    path.BeginTime = TimeSpan.FromMilliseconds(-duration);
                    path.AutoReverse = true;
                }
                tran.BeginAnimation(TranslateTransform.XProperty, path);
            }

            {
                TranslateTransform tran = element.GetTransform<TranslateTransform>();
                if (tran == null) return;
                DoubleAnimationUsingPath path = new DoubleAnimationUsingPath() { PathGeometry = geometry };
                path.Duration = new Duration(TimeSpan.FromMilliseconds(duration));
                path.Source = PathAnimationSource.Y;
                if (reverse)
                {
                    path.BeginTime = TimeSpan.FromMilliseconds(-duration);
                    path.AutoReverse = true;
                }
                tran.BeginAnimation(TranslateTransform.YProperty, path);
            }
        }

        public static StoryboardEngineBase BeginAnimationOpacity(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, UIElement.OpacityProperty.Name, Completed, init);
        }

        public static StoryboardEngineBase BeginAnimationX(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            TranslateTransform tran = element.GetTransform<TranslateTransform>();

            if (tran == null) return null;

            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)", Completed, init);
        }

        public static StoryboardEngineBase BeginAnimationY(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            TranslateTransform tran = element.GetTransform<TranslateTransform>();

            if (tran == null) return null;

            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)", Completed, init);
        }

        public static StoryboardEngineBase BeginAnimationScaleY(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            ScaleTransform tran = element.GetTransform<ScaleTransform>();

            if (tran == null) return null;

            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)", Completed, init);
        }

        public static StoryboardEngineBase BeginAnimationScaleX(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            ScaleTransform tran = element.GetTransform<ScaleTransform>();

            if (tran == null) return null;

            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)", Completed, init);
        }

        public static StoryboardEngineBase BeginAnimationSkewX(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            SkewTransform tran = element.GetTransform<SkewTransform>();

            if (tran == null) return null;

            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(SkewTransform.AngleX)", Completed, init);
        }

        public static void BeginAnimationSkewY(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            SkewTransform tran = element.GetTransform<SkewTransform>();

            if (tran == null) return;

            element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[1].(SkewTransform.AngleY)", Completed, init);
        }

        public static StoryboardEngineBase BeginAnimationAngle(this UIElement element, double from, double to, double duration = 1000, Action<UIElement> Completed = null, Action<StoryboardEngineBase> init = null)
        {
            RotateTransform tran = element.GetTransform<RotateTransform>();

            if (tran == null) return null;

            return element.BegionDoubleStoryBoard(from, to, duration / 1000.0, "(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)", Completed, init);
        }

        public static void Wait(this UIElement element, double duration = 1000, Action<UIElement> Completed = null)
        {
            element.BegionDoubleStoryBoard(1, 1, duration / 1000.0, "Opacity", Completed);
        }
    }
}
