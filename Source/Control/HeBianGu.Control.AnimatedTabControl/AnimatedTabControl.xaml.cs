// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace HeBianGu.Control.AnimatedTabControl
{
    /// <summary>
    /// AnimatedTabControl.xaml 的交互逻辑
    /// </summary>
    public partial class AnimatedTabControl : TabControl
    {
        public static ComponentResourceKey DefaultKey => new ComponentResourceKey(typeof(AnimatedTabControl), "S.AnimatedTabControl.Default");

        static AnimatedTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedTabControl), new FrameworkPropertyMetadata(typeof(AnimatedTabControl)));
        }

        public static readonly RoutedEvent SelectionChangingEvent = EventManager.RegisterRoutedEvent(
            "SelectionChanging", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(AnimatedTabControl));

        private DispatcherTimer timer;

        //public AnimatedTabControl()
        //{
        //    DefaultStyleKey = typeof(AnimatedTabControl);
        //}

        public event RoutedEventHandler SelectionChanging
        {
            add { AddHandler(SelectionChangingEvent, value); }
            remove { RemoveHandler(SelectionChangingEvent, value); }
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(
                (Action)delegate
                {
                    this.RaiseSelectionChangingEvent();

                    this.StopTimer();

                    this.timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 500) };

                    EventHandler handler = null;
                    handler = (sender, args) =>
                    {
                        this.StopTimer();
                        base.OnSelectionChanged(e);
                    };
                    this.timer.Tick += handler;
                    this.timer.Start();
                });
        }

        // This method raises the Tap event
        private void RaiseSelectionChangingEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(SelectionChangingEvent);
            RaiseEvent(args);
        }

        private void StopTimer()
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer = null;
            }
        }
    }
}
