// Copyright ? 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Base.WpfBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Timers;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// 可以注册 单击 双击 ..N击的触发器
    /// </summary>
    public class MouseTrigger : EventTriggerBase<UIElement>
    {
        public static readonly DependencyProperty FiredOnProperty = DependencyProperty.Register("FiredOn", typeof(KeyTriggerFiredOn), typeof(MouseTrigger));
        /// <summary>
        /// Determines whether or not to listen to the KeyDown or KeyUp event.
        /// </summary>
        public KeyTriggerFiredOn FiredOn
        {
            get { return (KeyTriggerFiredOn)this.GetValue(MouseTrigger.FiredOnProperty); }
            set { this.SetValue(MouseTrigger.FiredOnProperty, value); }
        }

        protected override string GetEventName()
        {
            return "Loaded";
        }


        public MouseButton MouseButton
        {
            get { return (MouseButton)GetValue(MouseButtonProperty); }
            set { SetValue(MouseButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseButtonProperty =
            DependencyProperty.Register("MouseButton", typeof(MouseButton), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(MouseButton), (d, e) =>
            {
                MouseTrigger control = d as MouseTrigger;

                if (control == null) return;

                if (e.OldValue is MouseButton o)
                {

                }

                if (e.NewValue is MouseButton n)
                {

                }

            }));


        public int ClickCount
        {
            get { return (int)GetValue(ClickCountProperty); }
            set { SetValue(ClickCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickCountProperty =
            DependencyProperty.Register("ClickCount", typeof(int), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(int), (d, e) =>
            {
                MouseTrigger control = d as MouseTrigger;

                if (control == null) return;

                if (e.OldValue is int o)
                {

                }

                if (e.NewValue is int n)
                {

                }

            }));


        public bool Handled
        {
            get { return (bool)GetValue(HandledProperty); }
            set { SetValue(HandledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HandledProperty =
            DependencyProperty.Register("Handled", typeof(bool), typeof(MouseTrigger), new FrameworkPropertyMetadata(default(bool), (d, e) =>
            {
                MouseTrigger control = d as MouseTrigger;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        protected override void OnEvent(EventArgs eventArgs)
        {
            if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
            {
                this.Source.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(this.OnMousePress), true);
            }
            else
            {
                this.Source.AddHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(this.OnMousePress), true);
            }
        }

        List<MouseButtonEventArgs> _args = new List<MouseButtonEventArgs>();
        private void OnMousePress(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ClickCount);
            System.Diagnostics.Debug.WriteLine(e.ChangedButton);

            if (e.ClickCount != this.ClickCount)
                return;
            if (e.ChangedButton != this.MouseButton)
                return;
            if (e.Handled)
                return;
            this.InvokeActions(e);
            e.Handled = this.Handled;
        }

        protected override void OnDetaching()
        {
            if (this.Source != null)
            {
                if (this.FiredOn == KeyTriggerFiredOn.KeyDown)
                {
                    this.Source.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(this.OnMousePress));
                }
                else
                {
                    this.Source.RemoveHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(this.OnMousePress));
                }
            }

            base.OnDetaching();
        }
    }

    /// <summary>
    /// 按住会没间隔一段时间执行
    /// </summary>
    public class MousePressTrigger : EventTriggerBase<UIElement>
    {
        System.Timers.Timer _timer = new System.Timers.Timer();


        public MousePressTrigger()
        {
            _timer.Interval = this.Interval;
            _timer.Elapsed += (l, k) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.ElapsedInvokeActions();
                });
            };
        }

        protected virtual void ElapsedInvokeActions()
        {
            this.InvokeActions(null);
        }

        protected void Stop()
        {
            this._timer.Stop();
        }

        public bool UseInvokeOnDown
        {
            get { return (bool)GetValue(UseInvokeOnDownProperty); }
            set { SetValue(UseInvokeOnDownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseInvokeOnDownProperty =
            DependencyProperty.Register("UseInvokeOnDown", typeof(bool), typeof(MousePressTrigger), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                MousePressTrigger control = d as MousePressTrigger;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }

            }));


        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(double), typeof(MousePressTrigger), new FrameworkPropertyMetadata(1000.0, (d, e) =>
            {
                MousePressTrigger control = d as MousePressTrigger;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));


        protected override string GetEventName()
        {
            return "Loaded";
        }

        public MouseButton MouseButton
        {
            get { return (MouseButton)GetValue(MouseButtonProperty); }
            set { SetValue(MouseButtonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseButtonProperty =
            DependencyProperty.Register("MouseButton", typeof(MouseButton), typeof(MousePressTrigger), new FrameworkPropertyMetadata(default(MouseButton), (d, e) =>
            {
                MousePressTrigger control = d as MousePressTrigger;

                if (control == null) return;

                if (e.OldValue is MouseButton o)
                {

                }

                if (e.NewValue is MouseButton n)
                {

                }

            }));

        protected override void OnEvent(EventArgs eventArgs)
        {
            this.Source.AddHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(this.OnMouseDown), true);
            this.Source.AddHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(this.OnMouseUp), true);
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _timer.Stop();
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != this.MouseButton)
                return;
            if (e.ButtonState == MouseButtonState.Released)
                return;
            _timer.Start();

            if (this.UseInvokeOnDown)
                this.InvokeActions(e);
        }

        protected override void OnDetaching()
        {
            if (this.Source != null)
            {
                this.Source.RemoveHandler(UIElement.MouseDownEvent, new MouseButtonEventHandler(this.OnMouseDown));
                this.Source.RemoveHandler(UIElement.MouseUpEvent, new MouseButtonEventHandler(this.OnMouseUp));
            }

            base.OnDetaching();
        }
    }

    /// <summary>
    /// 按住一定时间间隔才会执行
    /// </summary>
    public class MousePressClickTrigger : MousePressTrigger
    {
        public MousePressClickTrigger()
        {
            this.UseInvokeOnDown = false;
        }
        protected override void ElapsedInvokeActions()
        {
            this.InvokeActions(null);
            this.Stop();
        }
    }
}
