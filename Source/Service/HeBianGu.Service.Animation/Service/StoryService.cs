// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace HeBianGu.Service.Animation
{
    public static class StoryService
    {
        public static StoryCollection GetStorys(DependencyObject obj)
        {
            return (StoryCollection)obj.GetValue(StorysProperty);
        }

        public static void SetStorys(DependencyObject obj, StoryCollection value)
        {
            obj.SetValue(StorysProperty, value);
        }

        /// <summary> 显示隐藏过度效果 </summary>
        public static readonly DependencyProperty StorysProperty =
            DependencyProperty.RegisterAttached("Storys", typeof(StoryCollection), typeof(StoryService), new PropertyMetadata(null, OnStorysChanged));

        private static void OnStorysChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;

            if (element == null) return;

            bool isstart = StoryService.GetIsStart(element);

            if (isstart)
            {
                StoryService.SetIsStart(element, false);
                StoryService.SetIsStart(element, true);
            }
        }


        #region - 应用动画效果的开始、停止 -

        public static bool GetIsStart(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsStartProperty);
        }

        public static void SetIsStart(DependencyObject obj, bool value)
        {
            obj.SetValue(IsStartProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsStartProperty =
            DependencyProperty.RegisterAttached("IsStart", typeof(bool), typeof(StoryService), new PropertyMetadata(false, OnIsStartChanged));

        public static void OnIsStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;

            StoryCollection actions = StoryService.GetStorys(element);

            if (actions == null) return;

            bool v = (bool)e.NewValue;

            if (v)
            {
                //  Do ：显示动画
                foreach (IStory item in actions)
                {
                    item.Start(element);
                }
            }
            else
            {
                //  Do ：隐藏动画
                foreach (IStory item in actions)
                {
                    item.Stop();
                }
            }
        }

        #endregion


        public static List<IStory> GetAll()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(l => typeof(ITransition).IsAssignableFrom(l) && l.IsClass && !l.IsAbstract);

            return types.Select(l => Activator.CreateInstance(l)).Cast<IStory>().ToList();
        }

        public static List<StoryCollection> GetSource()
        {
            List<StoryCollection> result = new List<StoryCollection>();

            List<IStory> ts = GetAll();

            foreach (IStory t in ts)
            {
                StoryCollection transitions = new StoryCollection();

                transitions.Add(t);

                result.Add(transitions);
            }

            return result;
        }
    }
}
