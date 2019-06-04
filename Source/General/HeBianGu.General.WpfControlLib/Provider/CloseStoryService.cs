#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 四川*******有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[河边骨]   时间：2017/12/11 15:36:15 
 * 文件名：Class1 
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace HeBianGu.General.WpfControlLib
{
   partial class CloseStoryService
    {
        /// <summary> 从下到上移动关闭窗口 </summary>
        public void DownToUpClose(Window w)
        {
            DoubleAnimation dbAscending = new DoubleAnimation(0, -360, new Duration(TimeSpan.FromSeconds(1)));

            Storyboard storyboard = new Storyboard();
            //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Children.Add(dbAscending);
            Storyboard.SetTarget(dbAscending, w);
            Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            storyboard.Completed += delegate { w.Close(); };
            storyboard.Begin();
        }

        /// <summary> 从下到上移动关闭窗口 </summary>
        public void UoToDownClose(Window w)
        {
            DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(0.5)));

            Storyboard storyboard = new Storyboard();
            //dbAscending.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Children.Add(dbAscending);
            Storyboard.SetTarget(dbAscending, w);
            Storyboard.SetTargetProperty(dbAscending, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            storyboard.Completed += delegate { w.Close(); };
            storyboard.Begin();
        }


        /// <summary> 从下到上渐隐藏窗体 </summary>
        public void DownToUpOpsClose(Window w)
        {
            w.OpacityMask = w.FindResource("ClosedBrush") as LinearGradientBrush;

            ColorAnimation color1 = new ColorAnimation((Color)ColorConverter.ConvertFromString("#00000000"), new Duration(new TimeSpan(0)));

            DoubleAnimation double1 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1)));
            DoubleAnimation double2 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.75)));
            double2.BeginTime = TimeSpan.FromSeconds(0.25);

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(double1, w);
            Storyboard.SetTargetProperty(double1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[1].Offset"));
            storyboard.Children.Add(double1);
            Storyboard.SetTarget(double2, w);
            Storyboard.SetTargetProperty(double2, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Offset"));
            storyboard.Children.Add(double2);
            Storyboard.SetTarget(color1, w);
            Storyboard.SetTargetProperty(color1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Color"));
            storyboard.Children.Add(color1);

            storyboard.Completed += delegate { w.Close(); };
            storyboard.Begin();
        }


        /// <summary> 从下到上渐隐藏窗体 </summary>
        public void DownToUpOpsOpen(Window w)
        {
            w.OpacityMask = w.FindResource("ClosedBrush") as LinearGradientBrush;

            ColorAnimation color1 = new ColorAnimation((Color)ColorConverter.ConvertFromString("#00000000"), new Duration(new TimeSpan(0)));

            DoubleAnimation double1 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(1)));
            DoubleAnimation double2 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.75)));
            double2.BeginTime = TimeSpan.FromSeconds(0.25);

            Storyboard storyboard = new Storyboard();
            Storyboard.SetTarget(double1, w);
            Storyboard.SetTargetProperty(double1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[1].Offset"));
            storyboard.Children.Add(double1);
            Storyboard.SetTarget(double2, w);
            Storyboard.SetTargetProperty(double2, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Offset"));
            storyboard.Children.Add(double2);
            Storyboard.SetTarget(color1, w);
            Storyboard.SetTargetProperty(color1, new PropertyPath("OpacityMask.(GradientBrush.GradientStops)[2].Color"));
            storyboard.Children.Add(color1);

            storyboard.Completed += delegate { w.Close(); };
            storyboard.Begin();
        }


    }


    /// <summary> 此类的说明 </summary>
    partial class CloseStoryService
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static CloseStoryService t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static CloseStoryService Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new CloseStoryService();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private CloseStoryService()
        {

        }
        #endregion - 单例模式 End -

    }

}
