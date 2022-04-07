using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfControlDemo.View
{
    /// <summary>
    /// PathDemo.xaml 的交互逻辑
    /// </summary>
    public partial class PathDemo : Page
    {
        public PathDemo()
        {
            InitializeComponent();
        }


        #region - 按曲线运动 -

        private void Test_Double()
        {
            Canvas.SetTop(border1, -border1.ActualHeight / 2);
            Canvas.SetLeft(border1, -border1.ActualWidth / 2);

            border1.RenderTransformOrigin = new Point(0.5, 0.5);

            TranslateTransform translate = new TranslateTransform();
            RotateTransform rotate = new RotateTransform();
            TransformGroup group = new TransformGroup();
            group.Children.Add(rotate);//先旋转
            group.Children.Add(translate);//再平移
            border1.RenderTransform = group;

            NameScope.SetNameScope(this, new NameScope());
            RegisterName("translate", translate);
            RegisterName("rotate", rotate);

            DoubleAnimationUsingPath animationX = new DoubleAnimationUsingPath
            {
                PathGeometry = path1.Data.GetFlattenedPathGeometry(),
                Source = PathAnimationSource.X,
                Duration = new Duration(TimeSpan.FromSeconds(2))
            };

            DoubleAnimationUsingPath animationY = new DoubleAnimationUsingPath
            {
                PathGeometry = path1.Data.GetFlattenedPathGeometry(),
                Source = PathAnimationSource.Y,
                Duration = animationX.Duration
            };

            DoubleAnimationUsingPath animationAngle = new DoubleAnimationUsingPath
            {
                PathGeometry = path1.Data.GetFlattenedPathGeometry(),
                Source = PathAnimationSource.Angle,
                Duration = animationX.Duration
            };

            Storyboard story = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            story.Children.Add(animationX);
            story.Children.Add(animationY);
            story.Children.Add(animationAngle);
            Storyboard.SetTargetName(animationX, "translate");
            Storyboard.SetTargetName(animationY, "translate");
            Storyboard.SetTargetName(animationAngle, "rotate");
            Storyboard.SetTargetProperty(animationX, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTargetProperty(animationY, new PropertyPath(TranslateTransform.YProperty));
            Storyboard.SetTargetProperty(animationAngle, new PropertyPath(RotateTransform.AngleProperty));

            story.Begin(this);
        }

        private void Test_Matrix()
        {
            Canvas.SetLeft(border1, -border1.ActualWidth / 2);
            Canvas.SetTop(border1, -border1.ActualHeight / 2);
            border1.RenderTransformOrigin = new Point(0.5, 0.5);

            MatrixTransform matrix = new MatrixTransform();
            border1.RenderTransform = matrix;

            NameScope.SetNameScope(this, new NameScope());
            RegisterName("matrix", matrix);

            MatrixAnimationUsingPath matrixAnimation = new MatrixAnimationUsingPath
            {
                PathGeometry = path1.Data.GetFlattenedPathGeometry(),
                Duration = new Duration(TimeSpan.FromSeconds(2)),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            matrixAnimation.IsOffsetCumulative = !matrixAnimation.AutoReverse;
            matrixAnimation.DoesRotateWithTangent = true;//旋转


            Storyboard story = new Storyboard();
            story.Children.Add(matrixAnimation);
            Storyboard.SetTargetName(matrixAnimation, "matrix");
            Storyboard.SetTargetProperty(matrixAnimation, new PropertyPath(MatrixTransform.MatrixProperty));
            story.Begin(this);
        }

        private void Test_Double2()
        {
            Canvas.SetTop(border1, -border1.ActualHeight / 2);
            Canvas.SetLeft(border1, -border1.ActualWidth / 2);
            border1.RenderTransformOrigin = new Point(0.5, 0.5);

            TranslateTransform translate = new TranslateTransform();
            border1.RenderTransform = translate;

            NameScope.SetNameScope(this, new NameScope());
            RegisterName("translate", translate);

            DoubleAnimationUsingPath animationX = new DoubleAnimationUsingPath
            {
                PathGeometry = path1.Data.GetFlattenedPathGeometry(),
                Source = PathAnimationSource.X,
                Duration = new Duration(TimeSpan.FromSeconds(2))
            };

            DoubleAnimationUsingPath animationY = new DoubleAnimationUsingPath
            {
                PathGeometry = path1.Data.GetFlattenedPathGeometry(),
                Source = PathAnimationSource.Y,
                Duration = animationX.Duration
            };


            Storyboard story = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = true
            };
            story.Children.Add(animationX);
            story.Children.Add(animationY);
            Storyboard.SetTargetName(animationX, "translate");
            Storyboard.SetTargetName(animationY, "translate");
            Storyboard.SetTargetProperty(animationX, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTargetProperty(animationY, new PropertyPath(TranslateTransform.YProperty));

            story.Begin(this);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Test_Double();
            //this.Test_Matrix();
        }
        #endregion

    }
}
