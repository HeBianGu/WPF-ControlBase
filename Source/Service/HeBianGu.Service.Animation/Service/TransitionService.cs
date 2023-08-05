// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace HeBianGu.Service.Animation
{
    public static class TransitionService
    {
        public static bool GetIsClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseProperty);
        }

        public static void SetIsClose(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseProperty, value);
        }

        /// <summary> 应用窗体关闭和显示 </summary>
        public static readonly DependencyProperty IsCloseProperty =
            DependencyProperty.RegisterAttached("IsClose", typeof(bool), typeof(TransitionService), new PropertyMetadata(false, OnIsCloseChanged));

        public static void OnIsCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = d as UIElement;

            TransitionCollection actions = TransitionService.GetVisibleActions(element);

            if (actions == null) return;

            bool v = (bool)e.NewValue;

            if (v)
            {
                //  Do ：显示动画
                foreach (ITransition item in actions)
                {
                    item.BeginVisible(element);
                }
            }
            else
            {
                //  Do ：隐藏动画
                foreach (ITransition item in actions)
                {
                    item.BeginHidden(element, () =>
                    {
                        if (element is Window window)
                        {
                            window.Close();
                        }

                        return;
                    });
                }
            }
        }

        public static bool GetIsVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }

        /// <summary> 应用控件显示和隐藏 </summary>
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(TransitionService), new PropertyMetadata(true, OnIsVisibleChanged));

        public static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;


            TransitionCollection actions = TransitionService.GetVisibleActions(element);

            if (actions == null) return;

            bool v = (bool)e.NewValue;

            if (v)
            {
                //  Do ：显示动画
                foreach (ITransition item in actions)
                {
                    item.BeginVisible(element);
                }
            }
            else
            {
                //  Do ：隐藏动画
                foreach (ITransition item in actions)
                {
                    item.BeginHidden(element);
                }
            }
        }


        public static bool GetIsBeginVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBeginVisibleProperty);
        }

        public static void SetIsBeginVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBeginVisibleProperty, value);
        }

        /// <summary> 解决没有loaded时触发动画导致元素不显示的问题 </summary>
        public static readonly DependencyProperty IsBeginVisibleProperty =
            DependencyProperty.RegisterAttached("IsBeginVisible", typeof(bool), typeof(TransitionService), new PropertyMetadata(true, OnIsBeginVisibleChanged));

        public static void OnIsBeginVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;

            TransitionCollection actions = TransitionService.GetVisibleActions(element);

            if (actions == null) return;

            bool v = (bool)e.NewValue;

            //  Do ：解决没有loaded时触发动画导致元素不显示的问题
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                if (v)
                {
                    //  Do ：显示动画
                    foreach (ITransition item in actions)
                    {
                        item.BeginVisible(element);
                    }
                }
                else
                {
                    //  Do ：隐藏动画
                    foreach (ITransition item in actions)
                    {
                        item.BeginHidden(element);
                    }
                }

            }));
        }


        public static TransitionCollection GetVisibleActions(DependencyObject obj)
        {
            return (TransitionCollection)obj.GetValue(VisibleActionsProperty);
        }

        public static void SetVisibleActions(DependencyObject obj, TransitionCollection value)
        {
            obj.SetValue(VisibleActionsProperty, value);
        }
        /// <summary> 显示隐藏过度效果 </summary>
        public static readonly DependencyProperty VisibleActionsProperty =
            DependencyProperty.RegisterAttached("VisibleActions", typeof(TransitionCollection), typeof(TransitionService), new PropertyMetadata(new TransitionCollection() { new ScaleGeomotryTransition() { VisibleDuration = 1500, HideDuration = 500, PointOriginType = PointOriginType.MouseInnerOrCenter } }));



        public static List<ITransition> GetAll()
        {
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(l => typeof(ITransition).IsAssignableFrom(l) && l.IsClass && !l.IsAbstract);

            return types.Select(l => Activator.CreateInstance(l)).Cast<ITransition>().ToList();
        }

        public static List<TransitionCollection> GetSource()
        {
            List<TransitionCollection> result = new List<TransitionCollection>();

            List<ITransition> ts = GetAll();

            foreach (ITransition t in ts)
            {
                TransitionCollection transitions = new TransitionCollection();

                transitions.Add(t);

                result.Add(transitions);
            }

            return result;
        }
    }
}
