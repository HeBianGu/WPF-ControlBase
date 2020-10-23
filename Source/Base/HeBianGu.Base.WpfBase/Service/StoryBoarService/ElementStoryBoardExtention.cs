using HeBianGu.Base.WpfBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace System.Windows
{
    public static class ElementStoryBoardExtention
    {
        /// <summary> 执行动画 </summary>
        public static void BegionDoubleStoryBoard(this UIElement element, double start, double end, double duration, string propertyName)
        {
            var engine = DoubleStoryboardEngine.Create(start, end, duration, propertyName);

            engine.Start(element);

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
            if (element.RenderTransform == Transform.Identity || element.RenderTransform == null)
            {
                var dg = Application.Current.FindResource("S.TransformGroup.Default") as TransformGroup;

                if (dg == null) return false;

                element.RenderTransform = dg.Clone();
            }

            return true;
        }

        public static void BeginAnimationXY(this UIElement element, double toX, double toY, double duration = 1000)
        {
            var tran = element.GetTransform<TranslateTransform>();

            if (tran == null) return;

            DoubleAnimation xd = new DoubleAnimation(toX, TimeSpan.FromMilliseconds(duration));
            DoubleAnimation yd = new DoubleAnimation(toY, TimeSpan.FromMilliseconds(duration));
            tran.BeginAnimation(TranslateTransform.XProperty, xd);
            tran.BeginAnimation(TranslateTransform.YProperty, yd);
        }

        public static void BeginAnimationScale(this UIElement element, double scaleX, double scaleY, double duration = 1000)
        {
            var tran = element.GetTransform<ScaleTransform>();
            if (tran == null) return;

            DoubleAnimation xd = new DoubleAnimation(scaleX, TimeSpan.FromMilliseconds(duration));
            DoubleAnimation yd = new DoubleAnimation(scaleY, TimeSpan.FromMilliseconds(duration));
            tran.BeginAnimation(ScaleTransform.ScaleXProperty, xd);
            tran.BeginAnimation(ScaleTransform.ScaleYProperty, yd);
        }

        public static void BeginAnimationAngle(this UIElement element, double toAngle, double duration = 1000)
        {
            var tran = element.GetTransform<RotateTransform>();

            if (tran == null) return;

            DoubleAnimation xd = new DoubleAnimation(toAngle, TimeSpan.FromMilliseconds(duration));
            tran.BeginAnimation(RotateTransform.AngleProperty, xd);
        }

        public static void BeginAnimationSkew(this UIElement element, double toAngleX, double toAngleY, double duration = 1000)
        {
            var tran = element.GetTransform<SkewTransform>();
            if (tran == null) return;

            DoubleAnimation d1 = new DoubleAnimation(toAngleX, TimeSpan.FromMilliseconds(duration));
            DoubleAnimation d2 = new DoubleAnimation(toAngleY, TimeSpan.FromMilliseconds(duration));

            tran.BeginAnimation(SkewTransform.AngleXProperty, d1);
            tran.BeginAnimation(SkewTransform.AngleYProperty, d2);
        }
    }
}
