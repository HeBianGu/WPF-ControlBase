// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.General.WpfControlLib
{

    /// <summary> WindowBase动画扩展 </summary>
    public static class WindowBaseStoryBoardExtention
    {
        ///// <summary> 从下到上移动关闭窗口 </summary>
        //public static void CloseOfDownToUp(this WindowBase w)
        //{
        //    DoubleAnimation dbAscending = new DoubleAnimation(0, -360, new Duration(TimeSpan.FromSeconds(1)));

        //    Storyboard storyboard = new Storyboard();
        //    //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
        //    storyboard.Children.Add(dbAscending);
        //    Storyboard.SetTarget(dbAscending, w);
        //    Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
        //    storyboard.Completed += delegate { w.Close(); };
        //    storyboard.Begin();
        //}

        ///// <summary> 从下到上移动关闭窗口 </summary>
        //public static void CloseOfUoToDown(this WindowBase w)
        //{
        //    DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(0.5)));

        //    Storyboard storyboard = new Storyboard();
        //    //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
        //    storyboard.Children.Add(dbAscending);
        //    Storyboard.SetTarget(dbAscending, w);
        //    Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
        //    storyboard.Completed += delegate { w.Close(); };
        //    storyboard.Begin();
        //} 

        ///// <summary> 从下到上渐隐藏窗体 </summary>
        //public static void CloseDownToUpOps(this WindowBase w)
        //{
        //    w.OpacityMask = w.TryFindResource("S.WindowOpMack.ClosedBrush") as LinearGradientBrush;

        //    ColorAnimation color1 = new ColorAnimation((Color)ColorConverter.ConvertFromString("#00000000"), new Duration(new TimeSpan(0)));

        //    DoubleAnimation double1 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1)));
        //    DoubleAnimation double2 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.75)));
        //    double2.BeginTime = TimeSpan.FromSeconds(0.25);

        //    Storyboard storyboard = new Storyboard();
        //    Storyboard.SetTarget(double1, w);
        //    Storyboard.SetTargetProperty(double1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[1].Offset"));
        //    storyboard.Children.Add(double1);
        //    Storyboard.SetTarget(double2, w);
        //    Storyboard.SetTargetProperty(double2, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Offset"));
        //    storyboard.Children.Add(double2);
        //    Storyboard.SetTarget(color1, w);
        //    Storyboard.SetTargetProperty(color1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Color"));
        //    storyboard.Children.Add(color1);

        //    storyboard.Completed += delegate { w.Close(); };
        //    storyboard.Begin();
        //}


        ///// <summary> 从下到上渐隐藏窗体 </summary>
        //public static void CloseDownToUpOpsOpen(this WindowBase w)
        //{
        //    w.OpacityMask = w.TryFindResource("S.WindowOpMack.ClosedBrush") as LinearGradientBrush;

        //    ColorAnimation color1 = new ColorAnimation((Color)ColorConverter.ConvertFromString("#00000000"), new Duration(new TimeSpan(0)));

        //    DoubleAnimation double1 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1)));
        //    DoubleAnimation double2 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.75)));
        //    double2.BeginTime = TimeSpan.FromSeconds(0.25);

        //    Storyboard storyboard = new Storyboard();
        //    Storyboard.SetTarget(double1, w);
        //    Storyboard.SetTargetProperty(double1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[1].Offset"));
        //    storyboard.Children.Add(double1);
        //    Storyboard.SetTarget(double2, w);
        //    Storyboard.SetTargetProperty(double2, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Offset"));
        //    storyboard.Children.Add(double2);
        //    Storyboard.SetTarget(color1, w);
        //    Storyboard.SetTargetProperty(color1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Color"));
        //    storyboard.Children.Add(color1);

        //    storyboard.Completed += delegate { w.Close(); };
        //    storyboard.Begin();
        //}


        ///// <summary> 缩小到右下角隐藏 </summary>
        //public static void ScaleReduceWithAction(this WindowBase w, Point point, double second, double power, Action complate)
        //{
        //    w.RenderTransformOrigin = point;

        //    EasingFunctionBase easeFunction = new PowerEase()
        //    {
        //        EasingMode = EasingMode.EaseInOut,
        //        Power = power
        //    };

        //    {
        //        DoubleAnimation dbAscending = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(second)));
        //        dbAscending.EasingFunction = easeFunction;

        //        Storyboard storyboard = new Storyboard();
        //        //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
        //        storyboard.Children.Add(dbAscending);
        //        Storyboard.SetTarget(dbAscending, w);
        //        Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
        //        storyboard.Begin();
        //    }

        //    {
        //        DoubleAnimation dbAscending1 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(second)));
        //        dbAscending1.EasingFunction = easeFunction;

        //        Storyboard storyboard1 = new Storyboard();
        //        //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
        //        storyboard1.Children.Add(dbAscending1);
        //        Storyboard.SetTarget(dbAscending1, w);
        //        Storyboard.SetTargetProperty(dbAscending1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
        //        storyboard1.Completed += delegate { complate.Invoke(); };
        //        storyboard1.Begin();
        //    }

        //}

        //public static void HideOfScaleReduce(this WindowBase w)
        //{
        //    ScaleReduceWithAction(w, new Point(1, 1), 0.5, 5,() => w.Hide());
        //}

        //public static void CloseOfScaleReduceCenter(this WindowBase w)
        //{
        //    ScaleReduceWithAction(w, new Point(1, 1), 0.5, 5, () => w.Close());
        //}


        ///// <summary> 缩小到右下角显示</summary>
        //public static void ScaleEnlarge(this WindowBase w, Point point, double second, double power)
        //{
        //    w.RenderTransformOrigin = point;

        //    EasingFunctionBase easeFunction = new PowerEase()
        //    {
        //        EasingMode = EasingMode.EaseInOut,
        //        Power = power
        //    };

        //    {
        //        DoubleAnimation dbAscending = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(second)));
        //        dbAscending.EasingFunction = easeFunction;


        //        Storyboard storyboard = new Storyboard();
        //        //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
        //        storyboard.Children.Add(dbAscending);
        //        Storyboard.SetTarget(dbAscending, w);
        //        Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
        //        storyboard.Begin();
        //    }

        //    {
        //        DoubleAnimation dbAscending1 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(second)));
        //        dbAscending1.EasingFunction = easeFunction; 

        //        Storyboard storyboard1 = new Storyboard();
        //        //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
        //        storyboard1.Children.Add(dbAscending1);
        //        Storyboard.SetTarget(dbAscending1, w);
        //        Storyboard.SetTargetProperty(dbAscending1, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));

        //        storyboard1.Begin();
        //    }

        //}

        //public static void ShowOfScaleEnlarge(this WindowBase w)
        //{
        //    ((Window)w).Show();

        //    ScaleEnlarge(w, new Point(1, 1), 0.5, 5);
        //}

        //public static void ShowOfScaleEnlargeWithCenter(this WindowBase w, Point point, double second, double power)
        //{
        //    w.Show();

        //    ScaleEnlarge(w, new Point(0.5, 0.5), 1.5, 5);
        //}

    }


}
